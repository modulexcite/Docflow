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
using RapidDoc.Models.ViewModels;

namespace RapidDoc.Activities
{

    public sealed class WFChooseSpecificUserFromService : CodeActivity
    {
        [RequiredArgument]
        public InArgument<Dictionary<string, Object>> inputServiceName { get; set; }

        [RequiredArgument]
        public InArgument<Guid> inputDocumentId { get; set; }

        [RequiredArgument]
        public InArgument<string> inputCurrentUser { get; set; }

        [RequiredArgument]
        public InArgument<DocumentState> inputStep { get; set; }

        [RequiredArgument]
        public OutArgument<string> outputBookmark { get; set; }

        public InArgument<bool> useManual { get; set; }

        public InArgument<int> slaOffset { get; set; }

        public InArgument<bool> executionStep { get; set; }

        [RequiredArgument]
        public OutArgument<bool> outputSkipStep { get; set; }
        [RequiredArgument]
        public OutArgument<DocumentState> outputStep { get; set; }

        public InArgument<bool> noneSkip { get; set; }

        [Inject]
        public IWorkflowService _service { get; set; }

        [Inject]
        public IServiceIncidentService _serviceServiceIncident { get; set; }
     
        protected override void Execute(CodeActivityContext context)
        {
            Dictionary<string, Object> currentService = context.GetValue(this.inputServiceName);
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
            
            if (executionStep == true || noneSkipStep == true || userFunctionResult.Skip == false)
            {
                _service.CreateTrackerRecord(documentStep, documentId, this.DisplayName, userFunctionResult.Users, currentUserId, this.Id, useManual, slaOffset, executionStep);
                outputSkipStep.Set(context, false);
            }
            else
                outputSkipStep.Set(context, true);

            outputBookmark.Set(context, this.DisplayName);
            outputStep.Set(context, documentStep);
        }
    }
}
