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
using RapidDoc.Models.Infrastructure;

namespace RapidDoc.Controllers
{
    public class CustomController : BasicController
    {
        private readonly IEmplService _EmplService;
        private readonly ISystemService _SystemService;
        private readonly IDocumentService _DocumentService;
        private readonly IServiceIncidentService _ServiceIncidentService;
        private readonly ITripSettingsService _TripSettingsService;

        public CustomController(IUnitOfWork uow, IEmplService emplService, ISystemService systemService, IDocumentService documentService, IServiceIncidentService serviceIncidentService, ICompanyService companyService, IAccountService accountService, ITripSettingsService tripSettingsService)
            : base(uow, companyService, accountService)
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

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
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

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "КД"))
                    {
                        return PartialView("USR_REQ_KD_RequestForCompetitonProc_Edit_Manual", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualRequest1(RapidDoc.Models.ViewModels.USR_REQ_KD_RequestForCompetitonProcUZL_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "КД"))
                    {
                        return PartialView("USR_REQ_KD_RequestForCompetitonProcUZL_Edit_Manual", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualRequest2(RapidDoc.Models.ViewModels.USR_REQ_KD_RequestForCompetitonProcServices_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "КД"))
                    {
                        return PartialView("USR_REQ_KD_RequestForCompetitonProcServices_Edit_Manual", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualRequest3(RapidDoc.Models.ViewModels.USR_REQ_KD_RequestForCompetitonProcServicesBGP_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "КД"))
                    {
                        return PartialView("USR_REQ_KD_RequestForCompetitonProcServicesBGP_Edit_Manual", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetReissueComputerData(RapidDoc.Models.ViewModels.USR_REQ_IT_CTP_ReissueComputer_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
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

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Руководитель 1"))
                    {
                        return PartialView("USR_REQ_UZL_RequestForPeopleAcceptanceItems_Edit_Manual1", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualAcceptanceItems2(RapidDoc.Models.ViewModels.USR_REQ_UZL_RequestForPeopleAcceptanceItems_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Руководитель 2"))
                    {
                        return PartialView("USR_REQ_UZL_RequestForPeopleAcceptanceItems_Edit_Manual2", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualAcceptanceItems3(RapidDoc.Models.ViewModels.USR_REQ_UZL_RequestForPeopleAcceptanceItems_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Руководитель 3"))
                    {
                        return PartialView("USR_REQ_UZL_RequestForPeopleAcceptanceItems_Edit_Manual3", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualAcceptanceItems4(RapidDoc.Models.ViewModels.USR_REQ_UZL_RequestForPeopleAcceptanceItems_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Руководитель 4"))
                    {
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

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Начальник ССД"))
                    {
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

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Начальник ССД"))
                    {
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

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "УЗЛ"))
                    {
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

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
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

        public ActionResult GetManualURPInstruction(RapidDoc.Models.ViewModels.USR_REQ_UKR_RequestForExpertiseInstruction_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "УКР"))
                    {
                        return PartialView("USR_REQ_UKR_RequestForExpertiseInstruction_Edit_Manual", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualURPDepartment(RapidDoc.Models.ViewModels.USR_REQ_UKR_RequestForExpertiseDepartment_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "УКР"))
                    {
                        return PartialView("USR_REQ_UKR_RequestForExpertiseDepartment_Edit_Manual", model);
                    }
                }
            }

            return PartialView("_Empty");
        }
        //Запрос на рекруткарты-->
        public ActionResult GetManualHRCardITR11(RapidDoc.Models.ViewModels.USR_REQ_URP_RequestForHRCardITR1_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Непосредственный руководитель"))
                    {
                        return PartialView("USR_REQ_URP_RequestForHRCardITR1_Edit_Manual1", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualHRCardITR12(RapidDoc.Models.ViewModels.USR_REQ_URP_RequestForHRCardITR1_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "ИД"))
                    {
                        return PartialView("USR_REQ_URP_RequestForHRCardITR1_Edit_Manual2", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualHRCardITR13(RapidDoc.Models.ViewModels.USR_REQ_URP_RequestForHRCardITR1_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Генеральный директор/и.о. ГД"))
                    {
                        return PartialView("USR_REQ_URP_RequestForHRCardITR1_Edit_Manual3", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualHRCardITR21(RapidDoc.Models.ViewModels.USR_REQ_URP_RequestForHRCardITR2_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Непосредственный руководитель"))
                    {
                        return PartialView("USR_REQ_URP_RequestForHRCardITR2_Edit_Manual1", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualHRCardITR22(RapidDoc.Models.ViewModels.USR_REQ_URP_RequestForHRCardITR2_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Начальник управление"))
                    {
                        return PartialView("USR_REQ_URP_RequestForHRCardITR2_Edit_Manual2", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualRequestForITWeekend(RapidDoc.Models.ViewModels.USR_REQ_IT_CAP_RequestForITWeekend_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Начальники служб УИТ"))
                    {
                        return PartialView("USR_REQ_IT_CAP_RequestForITWeekend_Edit_Manual", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualHRCardITR23(RapidDoc.Models.ViewModels.USR_REQ_URP_RequestForHRCardITR2_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "ИД"))
                    {
                        return PartialView("USR_REQ_URP_RequestForHRCardITR2_Edit_Manual3", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualHRCardITRZIF1(RapidDoc.Models.ViewModels.USR_REQ_URP_RequestForHRCardITRZIF_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Непосредственный руководитель"))
                    {
                        return PartialView("USR_REQ_URP_RequestForHRCardITRZIF_Edit_Manual1", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualHRCardITRZIF2(RapidDoc.Models.ViewModels.USR_REQ_URP_RequestForHRCardITRZIF_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Директор фабрики"))
                    {
                        return PartialView("USR_REQ_URP_RequestForHRCardITRZIF_Edit_Manual2", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualHRCardWork1(RapidDoc.Models.ViewModels.USR_REQ_URP_RequestForHRCardWork_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Непосредственный руководитель"))
                    {
                        return PartialView("USR_REQ_URP_RequestForHRCardWork_Edit_Manual1", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualHRCardWork2(RapidDoc.Models.ViewModels.USR_REQ_URP_RequestForHRCardWork_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Начальник управления"))
                    {
                        return PartialView("USR_REQ_URP_RequestForHRCardWork_Edit_Manual2", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualHRCardWork3(RapidDoc.Models.ViewModels.USR_REQ_URP_RequestForHRCardWork_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Исполнительный директор"))
                    {
                        return PartialView("USR_REQ_URP_RequestForHRCardWork_Edit_Manual3", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualHRCardWorkZIF1(RapidDoc.Models.ViewModels.USR_REQ_URP_RequestForHRCardWorkZIF_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Непосредственный руководитель"))
                    {
                        return PartialView("USR_REQ_URP_RequestForHRCardWorkZIF_Edit_Manual1", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetManualHRCardWorkZIF2(RapidDoc.Models.ViewModels.USR_REQ_URP_RequestForHRCardWorkZIF_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Исполнительный директор"))
                    {
                        return PartialView("USR_REQ_URP_RequestForHRCardWorkZIF_Edit_Manual2", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetRequestForProvisionGraphVac(RapidDoc.Models.ViewModels.USR_REQ_URP_RequestForProvisionGraphVac_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Исполнитель"))
                    {
                        return PartialView("USR_REQ_URP_RequestForProvisionGraphVac_Edit_Exec", model);
                    }
                }
            }

            return PartialView("_Empty");
        }
        //<--Запрос на рекруткарты

        //УТ-->
        public ActionResult GetRequestAuxiliaryTransportOCTMCInvest(RapidDoc.Models.ViewModels.USR_REQ_YT_AuxiliaryTransportOCTMCInvest_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_AuxiliaryTransportOCTMCInvest_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_AuxiliaryTransportOCTMCInvest_View_Show", model);
        }

        public ActionResult GetRequestAuxiliaryTransportOCTMCOper(RapidDoc.Models.ViewModels.USR_REQ_YT_AuxiliaryTransportOCTMCOper_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_AuxiliaryTransportOCTMCOper_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_AuxiliaryTransportOCTMCOper_View_Show", model);
        }

        public ActionResult GetRequestAuxiliaryTransportDayOff(RapidDoc.Models.ViewModels.USR_REQ_YT_AuxiliaryTransportDayOff_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_AuxiliaryTransportDayOff_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_AuxiliaryTransportDayOff_View_Show", model);
        }

        public ActionResult GetRequestAuxiliaryTransportWorkDays(RapidDoc.Models.ViewModels.USR_REQ_YT_AuxiliaryTransportWorkDays_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_AuxiliaryTransportWorkDays_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_AuxiliaryTransportWorkDays_View_Show", model);
        }

        public ActionResult GetRequestAuxiliaryTransportOutABK(RapidDoc.Models.ViewModels.USR_REQ_YT_AuxiliaryTransportOutABK_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_AuxiliaryTransportOutABK_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_AuxiliaryTransportOutABK_View_Show", model);
        }

        public ActionResult GetRequestStandbyTransport(RapidDoc.Models.ViewModels.USR_REQ_YT_StandbyTransport_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_StandbyTransport_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_StandbyTransport_View_Show", model);
        }

        public ActionResult GetRequestStandbyTransportUIT(RapidDoc.Models.ViewModels.USR_REQ_YT_StandbyTransportUIT_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_StandbyTransportUIT_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_StandbyTransportUIT_View_Show", model);
        }

        public ActionResult GetRequestLightTransportTripManage(RapidDoc.Models.ViewModels.USR_REQ_YT_LightTransportTripManage_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_LightTransportTripManage_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_LightTransportTripManage_View_Show", model);
        }

        public ActionResult GetRequestLightTransportTripATK(RapidDoc.Models.ViewModels.USR_REQ_YT_LightTransportTripATK_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_LightTransportTripATK_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_LightTransportTripATK_View_Show", model);
        }

        public ActionResult GetRequestLightTransportOCTMCInvest(RapidDoc.Models.ViewModels.USR_REQ_YT_LightTransportOCTMCInvest_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_LightTransportOCTMCInvest_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_LightTransportOCTMCInvest_View_Show", model);
        }

        public ActionResult GetRequestLightTransportOCTMCOper(RapidDoc.Models.ViewModels.USR_REQ_YT_LightTransportOCTMCOper_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_LightTransportOCTMCOper_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_LightTransportOCTMCOper_View_Show", model);
        }

        public ActionResult GetRequestLightTransportOutOrganizationInvest(RapidDoc.Models.ViewModels.USR_REQ_YT_LightTransportOutOrganizationInvest_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_LightTransportOutOrganizationInvest_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_LightTransportOutOrganizationInvest_View_Show", model);
        }

        public ActionResult GetRequestLightTransportOutOrganizationOper(RapidDoc.Models.ViewModels.USR_REQ_YT_LightTransportOutOrganizationOper_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_LightTransportOutOrganizationOper_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_LightTransportOutOrganizationOper_View_Show", model);
        }

        public ActionResult GetRequestLightTransportTripDayOff(RapidDoc.Models.ViewModels.USR_REQ_YT_LightTransportTripDayOff_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_LightTransportTripDayOff_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_LightTransportTripDayOff_View_Show", model);
        }

        public ActionResult GetRequestPassangerTransportTrip(RapidDoc.Models.ViewModels.USR_REQ_YT_PassangerTransportTrip_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_PassangerTransportTrip_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_PassangerTransportTrip_View_Show", model);
        }

        public ActionResult GetRequestPassangerTransportTripManage(RapidDoc.Models.ViewModels.USR_REQ_YT_PassangerTransportTripManage_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_PassangerTransportTripManage_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_PassangerTransportTripManage_View_Show", model);
        }

        public ActionResult GetRequestPassangerTransportTripATK(RapidDoc.Models.ViewModels.USR_REQ_YT_PassangerTransportTripATK_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_PassangerTransportTripATK_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_PassangerTransportTripATK_View_Show", model);
        }

        public ActionResult GetRequestPassangerTransportOutOrganizationInvest(RapidDoc.Models.ViewModels.USR_REQ_YT_PassangerTransportOutOrganizationInvest_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_PassangerTransportOutOrganizationInvest_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_PassangerTransportOutOrganizationInvest_View_Show", model);
        }

        public ActionResult GetRequestPassangerTransportOutOrganizationOper(RapidDoc.Models.ViewModels.USR_REQ_YT_PassangerTransportOutOrganizationOper_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_PassangerTransportOutOrganizationOper_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_PassangerTransportOutOrganizationOper_View_Show", model);
        }

        public ActionResult GetRequestPassangerTransportDayOff(RapidDoc.Models.ViewModels.USR_REQ_YT_PassangerTransportDayOff_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_PassangerTransportDayOff_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_PassangerTransportDayOff_View_Show", model);
        }

        public ActionResult GetRequestPassangerTransportDayOffZIF(RapidDoc.Models.ViewModels.USR_REQ_YT_PassangerTransportDayOffZIF_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_PassangerTransportDayOffZIF_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_PassangerTransportDayOffZIF_View_Show", model);
        }

        public ActionResult GetRequestPassangerTransportCorporate(RapidDoc.Models.ViewModels.USR_REQ_YT_PassangerTransportCorporate_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Диспетчер УТ" || x.ActivityName == "ЦВТ" || x.ActivityName == "ЦЛТ" || x.ActivityName == "ЦПТ"))
                    {
                        return PartialView("USR_REQ_YT_PassangerTransportCorporate_View_Edit", model);
                    }
                }
            }

            return PartialView("USR_REQ_YT_PassangerTransportCorporate_View_Show", model);
        }
        //<--УТ

        public ActionResult GetRequestBookingRoom(RapidDoc.Models.ViewModels.USR_REQ_HY_BookingRoom_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Начальник ХУ"))
                    {
                        return PartialView("USR_REQ_HY_BookingRoom_Edit_HY", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetRequestFindApartment(RapidDoc.Models.ViewModels.USR_REQ_HY_FindApartment_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Начальник ХУ"))
                    {
                        return PartialView("USR_REQ_HY_FindApartment_Edit_HY", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetRequestRequestRepair(RapidDoc.Models.ViewModels.USR_REQ_HY_RequestRepair_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Начальник ХУ"))
                    {
                        return PartialView("USR_REQ_HY_RequestRepair_Edit_HY", model);
                    }
                }
            }

            return PartialView("_Empty");
        }

        public ActionResult GetRequestEmergencyPurposeTRU(RapidDoc.Models.ViewModels.USR_REQ_HY_EmergencyPurposeTRU_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "СХО" || x.ActivityName == "Начальник СХО"))
                    {
                        return PartialView("USR_REQ_HY_EmergencyPurposeTRU_Edit_CXO", model);
                    }
                }
            }

            return PartialView("USR_REQ_HY_EmergencyPurposeTRU_View_Full", model);
        }

        public ActionResult GetRequestTRU(RapidDoc.Models.ViewModels.USR_REQ_HY_RequestTRU_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "СХО" || x.ActivityName == "Начальник СХО"))
                    {
                        return PartialView("USR_REQ_HY_RequestTRU_Edit_CXO", model);
                    }
                }
            }

            return PartialView("USR_REQ_HY_RequestTRU_View_Full", model);
        }

        public ActionResult GetEmergencyRequestTRU(RapidDoc.Models.ViewModels.USR_REQ_HY_EmergencyRequestTRU_View model)
        {
            DocumentTable document = _DocumentService.Find(model.DocumentTableId);

            if ((document.DocumentState == RapidDoc.Models.Repository.DocumentState.Agreement || document.DocumentState == RapidDoc.Models.Repository.DocumentState.Execution) && _DocumentService.isSignDocument(document.Id))
            {
                var current = _DocumentService.GetCurrentSignStep(document.Id);
                if (current != null)
                {
                    if (current.Any(x => x.ActivityName == "Начальник СХО"))
                    {
                        return PartialView("USR_REQ_HY_EmergencyRequestTRU_Edit_CXO", model);
                    }
                }
            }

            return PartialView("USR_REQ_HY_EmergencyRequestTRU_View_Full", model);
        }

        [HttpPost]
        public ActionResult UpdateCalcBTripPPTRIP(byte EmplTripType, byte TripDirection, int Day, int DayLive, int TicketSum)
        {
            EmplTripType emplTripType = (EmplTripType)EmplTripType;
            TripDirection tripDirection = (TripDirection)TripDirection;

            TripSettingsTable tripSettingsTable = _TripSettingsService.FirstOrDefault(x => x.EmplTripType == emplTripType && x.TripDirection == tripDirection);
            if (tripSettingsTable != null)
            {
                var model = new USR_REQ_TRIP_RequestCalcDriveBTripCalsPP_View(emplTripType, tripDirection, Day, DayLive, TicketSum, tripSettingsTable.DayRate, tripSettingsTable.ResidenceRate);
                return PartialView(@"~/Views/Custom/USR_REQ_TRIP_RegistrationBusinessTripPP_Calc.cshtml", model);
            }

            return PartialView("_Empty");
        }

        [HttpPost]
        public ActionResult UpdateCalcBTripKZTRIP(byte EmplTripType, byte TripDirection, int Day, int DayLive, int TicketSum)
        {
            EmplTripType emplTripType = (EmplTripType)EmplTripType;
            TripDirection tripDirection = (TripDirection)TripDirection;

            TripSettingsTable tripSettingsTable = _TripSettingsService.FirstOrDefault(x => x.EmplTripType == emplTripType && x.TripDirection == tripDirection);
            if (tripSettingsTable != null)
            {
                var model = new USR_REQ_TRIP_RequestCalcDriveBTripCalsKZ_View(emplTripType, tripDirection, Day, DayLive, TicketSum, tripSettingsTable.DayRate, tripSettingsTable.ResidenceRate);
                return PartialView(@"~/Views/Custom/USR_REQ_TRIP_RegistrationBusinessTripKZ_Calc.cshtml", model);
            }

            return PartialView("_Empty");
        }
	}
}