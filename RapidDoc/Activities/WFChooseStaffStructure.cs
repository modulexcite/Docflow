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
        [RequiredArgument]
        public InArgument<Expression<Func<EmplTable, bool>>> inputPredicate { get; set; }

        [RequiredArgument]
        public InArgument<string> inputCurrentUser { get; set; }

        [RequiredArgument]
        public OutArgument<string> outputBookmark { get; set; }

        [RequiredArgument]
        public InArgument<DocumentState> inputStep { get; set; }

        [RequiredArgument]
        public InArgument<Guid> inputDocumentId { get; set; }

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
