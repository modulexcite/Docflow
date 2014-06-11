using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.Services;
using RapidDoc.Models.ViewModels;

namespace RapidDoc.Controllers
{
    public class ReportController : BasicController
    {
        private readonly IWorkflowTrackerService _WorkflowTrackerService;

        public ReportController(IWorkflowTrackerService workflowTrackerService)
        {
            _WorkflowTrackerService = workflowTrackerService;
        }

        //
        // GET: /Report/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GenerateReport(ReportParametersBasicView model)
        {
            return View();
        }
	}
}