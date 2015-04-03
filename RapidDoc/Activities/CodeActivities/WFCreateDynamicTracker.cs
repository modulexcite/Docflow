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

        [Inject]
        public IWorkflowService _service { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            List<string> users = context.GetValue(this.inputUsers);
            Guid documentId = context.GetValue(this.inputDocumentId);

            _service = DependencyResolver.Current.GetService<IWorkflowService>();

            _service.CreateDynamicTracker(users, documentId);

        }
    }
}
