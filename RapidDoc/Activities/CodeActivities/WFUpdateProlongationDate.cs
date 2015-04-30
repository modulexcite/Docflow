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


namespace RapidDoc.Activities.CodeActivities
{

    public sealed class WFUpdateProlongationDate : CodeActivity
    {       
        [RequiredArgument]
        public InArgument<Guid> RefDocId { get; set; }

        [RequiredArgument]
        public InArgument<DateTime> ProlongationDate { get; set; }

        [RequiredArgument]
        public InArgument<string> inputCurrentUser { get; set; }

        [Inject]
        public IWorkflowService _service { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            Guid refDocId = context.GetValue(this.RefDocId);
            DateTime prolongationDate = context.GetValue(this.ProlongationDate);
            string currentUserId = context.GetValue(this.inputCurrentUser);

            _service = DependencyResolver.Current.GetService<IWorkflowService>();
            _service.UpdateProlongationDate(refDocId, prolongationDate, currentUserId);

        }
    }
}
