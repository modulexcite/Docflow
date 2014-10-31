using Newtonsoft.Json;
using RapidDoc.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Repository;
using RapidDoc.Models.ViewModels;

namespace RapidDoc.Controllers
{
    public class CustomController : BasicController
    {
        private readonly IEmplService _EmplService;
        private readonly ISystemService _SystemService;
        private readonly IDocumentService _DocumentService;
        private readonly IServiceIncidentService _ServiceIncidentService;
        private readonly ITripSettingsService _TripSettingsService;

        public CustomController(IEmplService emplService, ISystemService systemService, IDocumentService documentService, IServiceIncidentService serviceIncidentService, ICompanyService companyService, IAccountService accountService, ITripSettingsService tripSettingsService)
            : base(companyService, accountService)
        {
            _EmplService = emplService;
            _SystemService = systemService;
            _DocumentService = documentService;
            _ServiceIncidentService = serviceIncidentService;
            _TripSettingsService = tripSettingsService;
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

        public ActionResult GetManualRequest(RapidDoc.Models.ViewModels.USR_REQ_KD_RequestForCompetitonProc_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id, document.ProcessTableId))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Начальник ОКЗ КД"))
                    {
                        ViewBag.ServiceIncidentList = _ServiceIncidentService.GetDropListServiceIncident(String.Empty);
                        return PartialView("USR_REQ_KD_RequestForCompetitonProc_Edit_Manual", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualRequest1(RapidDoc.Models.ViewModels.USR_REQ_KD_RequestForCompetitonProcUZL_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id, document.ProcessTableId))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Начальник ОКЗ КД"))
                    {
                        ViewBag.ServiceIncidentList = _ServiceIncidentService.GetDropListServiceIncident(String.Empty);
                        return PartialView("USR_REQ_KD_RequestForCompetitonProcUZL_Edit_Manual", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualRequest2(RapidDoc.Models.ViewModels.USR_REQ_KD_RequestForCompetitonProcServices_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id, document.ProcessTableId))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Начальник ОКЗ КД"))
                    {
                        ViewBag.ServiceIncidentList = _ServiceIncidentService.GetDropListServiceIncident(String.Empty);
                        return PartialView("USR_REQ_KD_RequestForCompetitonProcServices_Edit_Manual", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualRequest3(RapidDoc.Models.ViewModels.USR_REQ_KD_RequestForCompetitonProcServicesBGP_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id, document.ProcessTableId))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Начальник ОКЗ КД"))
                    {
                        ViewBag.ServiceIncidentList = _ServiceIncidentService.GetDropListServiceIncident(String.Empty);
                        return PartialView("USR_REQ_KD_RequestForCompetitonProcServicesBGP_Edit_Manual", model);
                    }
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
        
        //Запрос на выделение сотрудника для приемки ТМЦ-->
        public ActionResult GetManualAcceptanceItems1(RapidDoc.Models.ViewModels.USR_REQ_UZL_RequestForPeopleAcceptanceItems_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id, document.ProcessTableId))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Руководитель 1"))
                    {
                        ViewBag.ServiceIncidentList = _ServiceIncidentService.GetDropListServiceIncident(String.Empty);
                        return PartialView("USR_REQ_UZL_RequestForPeopleAcceptanceItems_Edit_Manual1", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualAcceptanceItems2(RapidDoc.Models.ViewModels.USR_REQ_UZL_RequestForPeopleAcceptanceItems_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id, document.ProcessTableId))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Руководитель 2"))
                    {
                        ViewBag.ServiceIncidentList = _ServiceIncidentService.GetDropListServiceIncident(String.Empty);
                        return PartialView("USR_REQ_UZL_RequestForPeopleAcceptanceItems_Edit_Manual2", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualAcceptanceItems3(RapidDoc.Models.ViewModels.USR_REQ_UZL_RequestForPeopleAcceptanceItems_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id, document.ProcessTableId))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Руководитель 3"))
                    {
                        ViewBag.ServiceIncidentList = _ServiceIncidentService.GetDropListServiceIncident(String.Empty);
                        return PartialView("USR_REQ_UZL_RequestForPeopleAcceptanceItems_Edit_Manual3", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualAcceptanceItems4(RapidDoc.Models.ViewModels.USR_REQ_UZL_RequestForPeopleAcceptanceItems_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id, document.ProcessTableId))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Руководитель 4"))
                    {
                        ViewBag.ServiceIncidentList = _ServiceIncidentService.GetDropListServiceIncident(String.Empty);
                        return PartialView("USR_REQ_UZL_RequestForPeopleAcceptanceItems_Edit_Manual4", model);
                    }
                }
            }

            return PartialView("_Empty");
        }
        //<--Запрос на выделение сотрудника для приемки ТМЦ

        //Запрос на передачу договоров в ССД (договора с нерезидентами)-->

        public ActionResult GetManualContractNoneresident(RapidDoc.Models.ViewModels.USR_REQ_UZL_RequestForContractNoneresident_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id, document.ProcessTableId))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Начальник ССД"))
                    {
                        ViewBag.ServiceIncidentList = _ServiceIncidentService.GetDropListServiceIncident(String.Empty);
                        return PartialView("USR_REQ_UZL_RequestForContractNoneresident_Edit_Manual", model);
                    }
                }
            }

            return PartialView("_Empty");
        }
        //<--Запрос на передачу договоров в ССД (договора с нерезидентами)

        //Запрос на передачу договоров в ССД (договора с нерезидентами)-->

        public ActionResult GetManualContractResident(RapidDoc.Models.ViewModels.USR_REQ_UZL_RequestForContractResident_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id, document.ProcessTableId))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Начальник ССД"))
                    {
                        ViewBag.ServiceIncidentList = _ServiceIncidentService.GetDropListServiceIncident(String.Empty);
                        return PartialView("USR_REQ_UZL_RequestForContractResident_Edit_Manual", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        //<--Запрос на передачу договоров в ССД (договора с нерезидентами)

        //Запрос на предоставление КП-->

        public ActionResult GetManualRepresentationKD(RapidDoc.Models.ViewModels.USR_REQ_UZL_RequestForrepresentationKD_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id, document.ProcessTableId))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "УЗЛ"))
                    {
                        ViewBag.ServiceIncidentList = _ServiceIncidentService.GetDropListServiceIncident(String.Empty);
                        return PartialView("USR_REQ_UZL_RequestForrepresentationKD_Edit_Manual", model);
                    }
                }
            }

            return PartialView("_Empty");
        }
        //<--Запрос на предоставление КП

        public ActionResult GetRequestCreateSettlViewData(RapidDoc.Models.ViewModels.USR_REQ_UBUO_RequestCreateSettlView_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id, document.ProcessTableId))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Начальник СТЗП"))
                    {
                        return PartialView("USR_REQ_UBUO_RequestCreateSettlView_Edit_StatAcxcounting", model);
                    }
                    if (current.Any(x => x.ActivityName == "Начальник СНУ"))
                    {
                        return PartialView("USR_REQ_UBUO_RequestCreateSettlView_Edit_BeginAcxcounting", model);
                    }
                }
            }

            return PartialView("USR_REQ_UBUO_RequestCreateSettlView_View_Full", model);
        }

        [HttpPost]
        public ActionResult UpdateCalcTripUBUO(byte EmplTripType, byte TripDirection, int Day, int DayLive, int TicketSum)
        {
            EmplTripType emplTripType = (EmplTripType)EmplTripType;
            TripDirection tripDirection = (TripDirection)TripDirection;

            TripSettingsTable tripSettingsTable = _TripSettingsService.FirstOrDefault(x => x.EmplTripType == emplTripType && x.TripDirection == tripDirection);
            if (tripSettingsTable != null)
            {
                var model = new USR_REQ_UBUO_RequestCalcDriveTripCals_View(emplTripType, tripDirection, Day, DayLive, TicketSum, tripSettingsTable.DayRate, tripSettingsTable.ResidenceRate);
                return PartialView(@"~/Views/Custom/USR_REQ_UBUO_RequestCalcDriveTrip_Calc.cshtml", model);
            }

            return PartialView("_Empty");
        }
	}
}