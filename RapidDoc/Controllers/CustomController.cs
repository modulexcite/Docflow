using Newtonsoft.Json;
using RapidDoc.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.DomainModels;

namespace RapidDoc.Controllers
{
    public class CustomController : BasicController
    {
        private readonly IEmplService _EmplService;
        private readonly ISystemService _SystemService;
        private readonly IDocumentService _DocumentService;
        private readonly IServiceIncidentService _ServiceIncidentService;

        public CustomController(IEmplService emplService, ISystemService systemService, IDocumentService documentService, IServiceIncidentService serviceIncidentService)
        {
            _EmplService = emplService;
            _SystemService = systemService;
            _DocumentService = documentService;
            _ServiceIncidentService = serviceIncidentService;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult JsonEmpl()
        {
            var jsondata = _EmplService.GetJsonEmpl();
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }

        //Custom
        public ActionResult GetIncidentAdminData(RapidDoc.Models.ViewModels.USR_REQ_IT_CTP_IncidentIT_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id, document.ProcessTableId))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if(current != null)
                {
                    if(current.Any(x => x.ActivityName == "Администратор"))
                    {
                        ViewBag.ServiceIncidentList = _ServiceIncidentService.GetDropListServiceIncident(String.Empty);
                        return PartialView("USR_REQ_IT_CTP_IncidentIT_Edit_Administrator", model);
                    }
                }
            }

            if (document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution
                || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Closed || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Cancelled)
            {
                if (model.ServiceName != null)
                {
                    return PartialView("USR_REQ_IT_CTP_IncidentIT_View_Administrator", model);
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetReissueComputerData(RapidDoc.Models.ViewModels.USR_REQ_IT_CTP_ReissueComputer_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id, document.ProcessTableId))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Проверка данных"))
                    {
                        return PartialView("USR_REQ_IT_CTP_ReissueComputer_Edit_TableCheck", model);
                    }
                }
            }

            if (document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution
                || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Closed || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Cancelled)
            {
                return PartialView("USR_REQ_IT_CTP_ReissueComputer_View_TableView", model);
            }

            return PartialView("_Empty");
        }
	}
}