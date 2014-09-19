using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Linq.Expressions;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Repository;
using Ninject;
using RapidDoc.Models.Services;
using System.Web.Mvc;

namespace RapidDoc.Activities
{

    public sealed class WFChooseStaffStructure : CodeActivity
    {
        public InArgument<Expression<Func<EmplTable, bool>>> inputPredicate { get; set; }
        public InArgument<string> inputCurrentUser { get; set; }
        public OutArgument<string> outputBookmark { get; set; }
        public InArgument<DocumentState> inputStep { get; set; }
        public InArgument<Guid> inputDocumentId { get; set; }
        public InArgument<bool> useManual { get; set; }
        public InArgument<int> slaOffset { get; set; }
        public InArgument<bool> executionStep { get; set; }
        public OutArgument<bool> outputSkipStep { get; set; }
        public OutArgument<DocumentState> outputStep { get; set; }
        public InArgument<bool> noneSkip { get; set; }

        [Inject]
        public IWorkflowService _service { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Guid documentId = context.GetValue(this.inputDocumentId);
            DocumentState documentStep = context.GetValue(this.inputStep);
            string currentUserId = context.GetValue(this.inputCurrentUser);
            bool useManual = context.GetValue(this.useManual);
            int slaOffset = context.GetValue(this.slaOffset);
            Expression<Func<EmplTable, bool>> predicate = context.GetValue(this.inputPredicate);
            bool executionStep = context.GetValue(this.executionStep);
            bool noneSkipStep = context.GetValue(this.noneSkip);

            _service = DependencyResolver.Current.GetService<IWorkflowService>();
            WFUserFunctionResult userFunctionResult = _service.WFStaffStructure(documentId, predicate);
            
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
