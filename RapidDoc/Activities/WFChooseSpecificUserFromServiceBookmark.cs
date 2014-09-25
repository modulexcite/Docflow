using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using RapidDoc.Models.Services;
using RapidDoc.Models.DomainModels;
using RapidDoc.App_Start;
using System.Activities.Tracking;
using Ninject;
using System.Web.Mvc;
using RapidDoc.Models.Repository;

namespace RapidDoc.Activities
{
    public class WFChooseSpecificUserFromServiceBookmark : NativeActivity
    {
        public WFChooseSpecificUserFromServiceBookmark() : base() { }

        public InArgument<Guid> inputDocumentId { get; set; }
        public InArgument<string> inputCurrentUser { get; set; }
        public InArgument<DocumentState> inputStep { get; set; }
        public InArgument<bool> useManual { get; set; }
        public InArgument<int> slaOffset { get; set; }
        public InArgument<bool> executionStep { get; set; }
        public InArgument<bool> noneSkip { get; set; }
        public InArgument<Dictionary<string, Object>> inputDocumentData { get; set; }

        public OutArgument<Dictionary<String, Object>> outputDocumentData { get; set; }
        public OutArgument<string> outputCurrentUser { get; set; }
        public OutArgument<DocumentState> outputStep { get; set; }

        [Inject]
        public IWorkflowService _service { get; set; }

        [Inject]
        public IServiceIncidentService _serviceServiceIncident { get; set; }

        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }

        NativeActivityContext CreateTrackingRecord(DocumentState stepParametr, NativeActivityContext context)
        {
            var customRecord = new CustomTrackingRecord("RespondActivityRecord");
            customRecord.Data.Add("outputStep", context.GetValue<DocumentState>(inputStep));
            context.Track(customRecord);
            return context;
        }

        protected override void Execute(NativeActivityContext context)
        {
            Dictionary<string, Object> currentService = context.GetValue(this.inputDocumentData);
            Guid documentId = context.GetValue(this.inputDocumentId);
            DocumentState documentStep = context.GetValue(this.inputStep);
            string currentUserId = context.GetValue(this.inputCurrentUser);
            bool useManual = context.GetValue(this.useManual);
            int slaOffset = context.GetValue(this.slaOffset);
            bool executionStep = context.GetValue(this.executionStep);
            String serviceName = (string)currentService["ServiceName"];
            ServiceIncidientPriority priority = (ServiceIncidientPriority)currentService["ServiceIncidientPriority"];
            ServiceIncidientLevel level = (ServiceIncidientLevel)currentService["ServiceIncidientLevel"];
            ServiceIncidientLocation location = (ServiceIncidientLocation)currentService["ServiceIncidientLocation"];
            bool noneSkipStep = context.GetValue(this.noneSkip);

            _service = DependencyResolver.Current.GetService<IWorkflowService>();
            _serviceServiceIncident = DependencyResolver.Current.GetService<IServiceIncidentService>();

            string roleName = _service.WFChooseSpecificUserFromService(serviceName, priority, level, location);
            WFUserFunctionResult userFunctionResult = _service.WFRoleUser(documentId, roleName);

            if ((userFunctionResult.Skip == false) || (executionStep == true || noneSkipStep == true))
            {
                _service.CreateTrackerRecord(documentStep, documentId, this.DisplayName, userFunctionResult.Users, currentUserId, this.Id, useManual, slaOffset, executionStep);
                context = CreateTrackingRecord(context.GetValue<DocumentState>(inputStep), context);
                context.CreateBookmark(this.DisplayName,
                    new BookmarkCallback(this.resumeBookmark));
            }
        }

        void resumeBookmark(NativeActivityContext context, Bookmark bookmark, object obj)
        {
            context = CreateTrackingRecord(context.GetValue<DocumentState>(inputStep), context);
            IDictionary<string, object> inputArguments = (IDictionary<string, object>)obj;
            context.SetValue(outputStep, (DocumentState)inputArguments["inputStep"]);
            context.SetValue(outputCurrentUser, (string)inputArguments["inputCurrentUser"]);
            context.SetValue(outputDocumentData, (Dictionary<String, Object>)inputArguments["documentData"]);
        }
    }
}