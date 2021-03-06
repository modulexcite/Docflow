﻿using System;
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

namespace RapidDoc.Activities.CodeActivities
{

    public sealed class WFSetUsersForTaskProlongation : CodeActivity
    {      
        [RequiredArgument]
        public InArgument<List<WFTrackerUsersTable>> inputUserNames { get; set; }

        [RequiredArgument]
        public InArgument<Guid> inputDocumentId { get; set; }

        [RequiredArgument]
        public InArgument<string> inputCurrentUser { get; set; }

        [RequiredArgument]
        public InArgument<DocumentState> inputStep { get; set; }

        [RequiredArgument]
        public InArgument<int> inputActivityId { get; set; }

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

        protected override void Execute(CodeActivityContext context)
        {
            List<WFTrackerUsersTable> userNames = context.GetValue(this.inputUserNames);
            Guid documentId = context.GetValue(this.inputDocumentId);
            DocumentState documentStep = context.GetValue(this.inputStep);
            string currentUserId = context.GetValue(this.inputCurrentUser);
            bool useManual = context.GetValue(this.useManual);
            int slaOffset = context.GetValue(this.slaOffset);
            bool executionStep = context.GetValue(this.executionStep);
            bool noneSkipStep = context.GetValue(this.noneSkip);
            int inputActivityId = context.GetValue(this.inputActivityId);

            _service = DependencyResolver.Current.GetService<IWorkflowService>();

            _service.CreateTrackerRecord(documentStep, documentId, "Исполнитель", userNames, currentUserId, inputActivityId.ToString(), useManual, slaOffset, executionStep);
                
            outputSkipStep.Set(context, false);
            outputBookmark.Set(context, "Исполнитель");
            outputStep.Set(context, documentStep);          
        }
    }
}
