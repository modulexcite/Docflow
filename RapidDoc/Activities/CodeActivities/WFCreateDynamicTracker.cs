using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Ninject;
using RapidDoc.Models.Services;
using System.Web.Mvc;

namespace RapidDoc.Activities.CodeActivities
{

    public sealed class WFCreateDynamicTracker : CodeActivity
    {      
        [RequiredArgument]
        public InArgument<List<string>> inputUsers { get; set; }

        [RequiredArgument]
        public InArgument<Guid> inputDocumentId { get; set; }

        [RequiredArgument]
        public InArgument<string> inputCurrentUser { get; set; }

        [RequiredArgument]
        public InArgument<bool> inputParallel { get; set; }

        [Inject]
        public IWorkflowService _service { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            List<string> users = context.GetValue(this.inputUsers);
            Guid documentId = context.GetValue(this.inputDocumentId);
            string currentUserId = context.GetValue(this.inputCurrentUser);
            bool isParallel = context.GetValue(this.inputParallel);

            _service = DependencyResolver.Current.GetService<IWorkflowService>();

            _service.CreateDynamicTracker(users, documentId, currentUserId, isParallel);
        }
    }
}
