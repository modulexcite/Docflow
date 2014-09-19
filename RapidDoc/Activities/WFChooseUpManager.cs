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
using System.Web;
using RapidDoc;
using RapidDoc.Models.Infrastructure;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using RapidDoc.Models.Repository;

namespace RapidDoc.Activities
{
    
    public sealed class WFChooseUpManager : CodeActivity
    {       
        public InArgument<Guid> inputDocumentId { get; set; }
        public InArgument<int> inputLevel { get; set; }
        public InArgument<string> inputCurrentUser { get; set; }      
        public InArgument<DocumentState> inputStep { get; set; }
        public OutArgument<string> outputBookmark { get; set; }
        public InArgument<bool> useManual { get; set; }
        public InArgument<int> slaOffset { get; set; }
        public InArgument<string> profileName { get; set; }
        public InArgument<bool> executionStep { get; set; }
        public OutArgument<bool> outputSkipStep { get; set; }
        public OutArgument<DocumentState> outputStep { get; set; }
        public InArgument<bool> noneSkip { get; set; }
        
        [Inject]
        public IWorkflowService _service { get; set; }
      
        protected override void Execute(CodeActivityContext context)
        {
            int level = context.GetValue(this.inputLevel);
            Guid documentId = context.GetValue(this.inputDocumentId);
            DocumentState documentStep = context.GetValue(this.inputStep);
            string currentUserId = context.GetValue(this.inputCurrentUser); 
            bool useManual = context.GetValue(this.useManual);
            int slaOffset = context.GetValue(this.slaOffset);
            string profileName = context.GetValue(this.profileName);
            bool executionStep = context.GetValue(this.executionStep);
            bool noneSkipStep = context.GetValue(this.noneSkip);

            _service = DependencyResolver.Current.GetService<IWorkflowService>();
            WFUserFunctionResult userFunctionResult = _service.WFMatchingUpManager(documentId, currentUserId, level, profileName);

            if (userFunctionResult.Skip == false)
                _service.CreateTrackerRecord(documentStep, documentId, this.DisplayName, userFunctionResult.Users, currentUserId, this.Id, useManual, slaOffset, executionStep);
            else if (executionStep == true || noneSkipStep == true)
                _service.CreateTrackerRecord(documentStep, documentId, this.DisplayName, userFunctionResult.Users, currentUserId, this.Id, useManual, slaOffset, executionStep);

            outputBookmark.Set(context, this.DisplayName.Replace("<step>", ""));
            outputSkipStep.Set(context, executionStep ? false : userFunctionResult.Skip);
            outputStep.Set(context, documentStep);
        }
    }
}
