using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Repository;
using RapidDoc.Models.ViewModels;

namespace RapidDoc.Models.Services
{
    public interface ICustomCheckDocument
    {
        List<string> CheckCustomDocument(Type type, dynamic actionModel);
        List<string> CheckCustomDocumentHY(Type type, dynamic actionModel);
        List<string> CheckCustomPostDocument(Type type, dynamic actionModel, DocumentTable documentTable, bool isSign);
        dynamic PreUpdateViewModel(Type type, dynamic actionModel);
        void UpdateDocumentData(DocumentTable document, IDictionary<string, object> documentData);
    }

    public class CustomCheckDocument : ICustomCheckDocument
    {
        private IUnitOfWork _uow;
        private readonly IServiceIncidentService _ServiceIncidentService;
        private readonly ITripSettingsService _TripSettingsService;
        private readonly IWorkflowTrackerService _WorkflowTrackerService;

        public CustomCheckDocument(IUnitOfWork uow, IWorkflowTrackerService workflowTrackerService, IServiceIncidentService serviceIncidentService, ITripSettingsService tripSettingsService)
        {
            _uow = uow;
            _WorkflowTrackerService = workflowTrackerService;

            //Custom
            _ServiceIncidentService = serviceIncidentService;
            _TripSettingsService = tripSettingsService;
        }

        public List<string> CheckCustomDocument(Type type, dynamic actionModel)
        {
            List<string> errorList = new List<string>();

            if (type == (new USR_REQ_IT_CTS_SetPersonalButton_View()).GetType())
            {
                if (actionModel.ServiceTypeButtonNo01 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo01 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo01 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo01 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo01) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo01)))
                {
                    errorList.Add(String.Format("Кнопка 1. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo01));
                }
                if (actionModel.ServiceTypeButtonNo02 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo02 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo02 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo02 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo02) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo02)))
                {
                    errorList.Add(String.Format("Кнопка 2. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo02));
                }
                if (actionModel.ServiceTypeButtonNo03 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo03 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo03 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo03 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo03) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo03)))
                {
                    errorList.Add(String.Format("Кнопка 3. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo03));
                }
                if (actionModel.ServiceTypeButtonNo04 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo04 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo04 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo04 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo04) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo04)))
                {
                    errorList.Add(String.Format("Кнопка 4. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo04));
                }
                if (actionModel.ServiceTypeButtonNo05 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo05 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo05 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo05 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo05) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo05)))
                {
                    errorList.Add(String.Format("Кнопка 5. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo05));
                }
                if (actionModel.ServiceTypeButtonNo06 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo06 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo06 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo06 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo06) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo06)))
                {
                    errorList.Add(String.Format("Кнопка 6. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo06));
                }
                if (actionModel.ServiceTypeButtonNo07 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo07 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo07 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo07 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo07) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo07)))
                {
                    errorList.Add(String.Format("Кнопка 7. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo07));
                }
                if (actionModel.ServiceTypeButtonNo08 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo08 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo08 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo08 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo08) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo08)))
                {
                    errorList.Add(String.Format("Кнопка 8. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo08));
                }
                if (actionModel.ServiceTypeButtonNo09 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo09 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo09 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo09 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo09) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo09)))
                {
                    errorList.Add(String.Format("Кнопка 9. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo09));
                }
                if (actionModel.ServiceTypeButtonNo10 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo10 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo10 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo10 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo10) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo10)))
                {
                    errorList.Add(String.Format("Кнопка 10. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo10));
                }
                if (actionModel.ServiceTypeButtonNo11 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo11 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo11 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo11 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo11) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo11)))
                {
                    errorList.Add(String.Format("Кнопка 11. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo11));
                }
                if (actionModel.ServiceTypeButtonNo12 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo12 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo12 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo12 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo12) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo12)))
                {
                    errorList.Add(String.Format("Кнопка 12. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo12));
                }
                if (actionModel.ServiceTypeButtonNo13 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo13 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo13 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo13 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo13) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo13)))
                {
                    errorList.Add(String.Format("Кнопка 13. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo13));
                }
                if (actionModel.ServiceTypeButtonNo14 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo14 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo14 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo14 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo14) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo14)))
                {
                    errorList.Add(String.Format("Кнопка 14. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo14));
                }
                if (actionModel.ServiceTypeButtonNo15 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo15 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo15 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo15 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo15) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo15)))
                {
                    errorList.Add(String.Format("Кнопка 15. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo15));
                }
                if (actionModel.ServiceTypeButtonNo16 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo16 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo16 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo16 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo16) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo16)))
                {
                    errorList.Add(String.Format("Кнопка 16. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo16));
                }
            }

            if (type == (new USR_REQ_IT_CTP_IncidentIT_View()).GetType())
            {
                if (actionModel.ServiceName == null && actionModel.Id != null)
                {
                    errorList.Add("Не указан сервис ИТ");
                }

                if (actionModel.ServiceName != null && actionModel.Id != null)
                {
                    ServiceIncidentTable incidentTable = _ServiceIncidentService.GetAll().FirstOrDefault(x => x.ServiceName == actionModel.ServiceName && x.ServiceIncidientLevel == ((ServiceIncidientLevel)actionModel.ServiceIncidientLevel) && x.ServiceIncidientPriority == ((ServiceIncidientPriority)actionModel.ServiceIncidientPriority) && x.ServiceIncidientLocation == ((ServiceIncidientLocation)actionModel.ServiceIncidientLocation));

                    if (incidentTable == null)
                    {
                        errorList.Add("Не правильно указан сервис ИТ");
                    }
                }
            }

            if (type == (new USR_REQ_OKS_RequestForVisa_View()).GetType())
            {
                if (actionModel.FromDate > actionModel.ToDate)
                {
                    errorList.Add("Неверно указан диапазон дат");
                }
            }

            if (type == (new USR_REQ_UB_RequestForImportTMCZIF_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    errorList.Add("Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForImportORZZIF_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    errorList.Add("Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForImportTMCNoneZIF_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    errorList.Add("Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForImportTMCUZL_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    errorList.Add("Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForExportAsset_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    errorList.Add("Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForExportZIFOre_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    errorList.Add("Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForExportOSPVHZIF_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    errorList.Add("Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForExportItems_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    errorList.Add("Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForHU_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    errorList.Add("Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForMovementItems_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    errorList.Add("Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForMovementAssets_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    errorList.Add("Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForExportItemFromORZ_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    errorList.Add("Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForExportItemFromZIF_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    errorList.Add("Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UBP_RequestForExportWastes_View()).GetType())
            {
                if (actionModel.Number1 == true && (actionModel.Number32 == String.Empty || actionModel.Number63 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 1");
                }

                if (actionModel.Number2 == true && (actionModel.Number33 == String.Empty || actionModel.Number64 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 2");
                }

                if (actionModel.Number3 == true && (actionModel.Number34 == String.Empty || actionModel.Number65 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 3");
                }

                if (actionModel.Number4 == true && (actionModel.Number35 == String.Empty || actionModel.Number66 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 4");
                }

                if (actionModel.Number5 == true && (actionModel.Number36 == String.Empty || actionModel.Number67 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 5");
                }

                if (actionModel.Number6 == true && (actionModel.Number37 == String.Empty || actionModel.Number68 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 6");
                }

                if (actionModel.Number7 == true && (actionModel.Number38 == String.Empty || actionModel.Number69 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 7");
                }

                if (actionModel.Number8 == true && (actionModel.Number39 == String.Empty || actionModel.Number70 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 8");
                }

                if (actionModel.Number9 == true && (actionModel.Number40 == String.Empty || actionModel.Number71 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 9");
                }

                if (actionModel.Number10 == true && (actionModel.Number41 == String.Empty || actionModel.Number72 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 10");
                }

                if (actionModel.Number11 == true && (actionModel.Number42 == String.Empty || actionModel.Number73 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 11");
                }

                if (actionModel.Number12 == true && (actionModel.Number43 == String.Empty || actionModel.Number74 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 12");
                }

                if (actionModel.Number13 == true && (actionModel.Number44 == String.Empty || actionModel.Number75 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 13");
                }

                if (actionModel.Number14 == true && (actionModel.Number45 == String.Empty || actionModel.Number76 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 14");
                }

                if (actionModel.Number15 == true && (actionModel.Number46 == String.Empty || actionModel.Number77 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 15");
                }

                if (actionModel.Number16 == true && (actionModel.Number47 == String.Empty || actionModel.Number78 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 16");
                }

                if (actionModel.Number17 == true && (actionModel.Number48 == String.Empty || actionModel.Number79 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 17");
                }

                if (actionModel.Number18 == true && (actionModel.Number49 == String.Empty || actionModel.Number80 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 18");
                }

                if (actionModel.Number19 == true && (actionModel.Number50 == String.Empty || actionModel.Number81 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 19");
                }

                if (actionModel.Number20 == true && (actionModel.Number51 == String.Empty || actionModel.Number82 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 20");
                }

                if (actionModel.Number21 == true && (actionModel.Number52 == String.Empty || actionModel.Number83 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 21");
                }

                if (actionModel.Number22 == true && (actionModel.Number53 == String.Empty || actionModel.Number84 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 22");
                }

                if (actionModel.Number23 == true && (actionModel.Number54 == String.Empty || actionModel.Number85 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 23");
                }

                if (actionModel.Number24 == true && (actionModel.Number55 == String.Empty || actionModel.Number86 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 24");
                }

                if (actionModel.Number25 == true && (actionModel.Number56 == String.Empty || actionModel.Number87 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 25");
                }

                if (actionModel.Number26 == true && (actionModel.Number57 == String.Empty || actionModel.Number88 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 26");
                }

                if (actionModel.Number27 == true && (actionModel.Number58 == String.Empty || actionModel.Number89 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 27");
                }

                if (actionModel.Number28 == true && (actionModel.Number59 == String.Empty || actionModel.Number90 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 28");
                }

                if (actionModel.Number29 == true && (actionModel.Number60 == String.Empty || actionModel.Number91 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 29");
                }

                if (actionModel.Number30 == true && (actionModel.Number61 == String.Empty || actionModel.Number92 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 30");
                }

                if (actionModel.Number31 == true && (actionModel.Number62 == String.Empty || actionModel.Number93 == String.Empty))
                {
                    errorList.Add("Укажите количество и вес в строке 31");
                }
            }
            if (type == (new USR_REQ_UZL_RequestForPeopleAcceptanceItems_View()).GetType())
            {
                if ((actionModel.UserChooseManual1 != null && actionModel.UserChooseManual2 != null && actionModel.UserChooseManual3 != null && actionModel.UserChooseManual4 != null) && ((actionModel.UserChooseManual4 == "") && (actionModel.UserChooseManual1 == "") && (actionModel.UserChooseManual2 == "") && (actionModel.UserChooseManual3 == "")))
                {
                    errorList.Add("Неоюходимо заполнить хотя бы одного руководителя");
                }
                if (((actionModel.UserChooseManual1 != String.Empty && actionModel.Department1 == String.Empty) || (actionModel.Department1 != String.Empty && actionModel.UserChooseManual1 == String.Empty)) && actionModel.Department1 != null)
                {
                    errorList.Add("ФИО Руководителя 1 и его подразделение должно быть заполнено");
                }

                if (((actionModel.UserChooseManual2 != String.Empty && actionModel.Department2 == String.Empty) || (actionModel.Department2 != String.Empty && actionModel.UserChooseManual2 == String.Empty)) && actionModel.Department2 != null)
                {
                    errorList.Add("ФИО Руководителя 2 и его подразделение должно быть заполнено");
                }

                if (((actionModel.UserChooseManual3 != String.Empty && actionModel.Department3 == String.Empty) || (actionModel.Department3 != String.Empty && actionModel.UserChooseManual3 == String.Empty)) && actionModel.Department3 != null)
                {
                    errorList.Add("ФИО Руководителя 3 и его подразделение должно быть заполнено");
                }

                if (((actionModel.UserChooseManual4 != String.Empty && actionModel.Department4 == String.Empty) || (actionModel.Department4 != String.Empty && actionModel.UserChooseManual4 == String.Empty)) && actionModel.Department4 != null)
                {
                    errorList.Add("ФИО Руководителя 4 и его подразделение должно быть заполнено");
                }

                if ((actionModel.Contact1 == String.Empty || actionModel.UserChooseManual5 == ",") && actionModel.Contact1 != null)
                {
                    errorList.Add("ФИО специалиста 1 и его контакты должны быть заполнены");
                }

                if ((actionModel.Contact2 == String.Empty || actionModel.UserChooseManual6 == ",") && actionModel.Contact2 != null)
                {
                    errorList.Add("ФИО специалиста 2 и его контакты должны быть заполнены");
                }

                if ((actionModel.Contact3 == String.Empty || actionModel.UserChooseManual7 == ",") && actionModel.Contact3 != null)
                {
                    errorList.Add("ФИО специалиста 3 и его контакты должны быть заполнены");
                }

                if ((actionModel.Contact4 == String.Empty || actionModel.UserChooseManual8 == ",") && actionModel.Contact4 != null)
                {
                    errorList.Add("ФИО специалиста 4 и его контакты должны быть заполнены");
                }
            }

            if (type == (new USR_REQ_UBUO_RequestCreateSettlView_View()).GetType())
            {
                if (actionModel.NameSettlView1 != String.Empty && (actionModel.WaySettl1 == String.Empty || actionModel.ReflectedBU1 == String.Empty || actionModel.ReflectedDepartment1 == String.Empty || actionModel.AverageEarnings1 == String.Empty || actionModel.TypeCost1 == String.Empty))
                {
                    errorList.Add("В строке 1 не заполнены все необходимые поля");
                }

                if (actionModel.NameSettlView2 != String.Empty && (actionModel.WaySettl2 == String.Empty || actionModel.ReflectedBU2 == String.Empty || actionModel.ReflectedDepartment2 == String.Empty || actionModel.AverageEarnings2 == String.Empty || actionModel.TypeCost2 == String.Empty))
                {
                    errorList.Add("В строке 2 не заполнены все необходимые поля");
                }

                if (actionModel.NameSettlView3 != String.Empty && (actionModel.WaySettl3 == String.Empty || actionModel.ReflectedBU3 == String.Empty || actionModel.ReflectedDepartment3 == String.Empty || actionModel.AverageEarnings3 == String.Empty || actionModel.TypeCost3 == String.Empty))
                {
                    errorList.Add("В строке 3 не заполнены все необходимые поля");
                }

                if (actionModel.NameSettlView4 != String.Empty && (actionModel.WaySettl4 == String.Empty || actionModel.ReflectedBU4 == String.Empty || actionModel.ReflectedDepartment4 == String.Empty || actionModel.AverageEarnings4 == String.Empty || actionModel.TypeCost4 == String.Empty))
                {
                    errorList.Add("В строке 4 не заполнены все необходимые поля");
                }

                if (actionModel.NameSettlView5 != String.Empty && (actionModel.WaySettl5 == String.Empty || actionModel.ReflectedBU5 == String.Empty || actionModel.ReflectedDepartment5 == String.Empty || actionModel.AverageEarnings5 == String.Empty || actionModel.TypeCost5 == String.Empty))
                {
                    errorList.Add("В строке 5 не заполнены все необходимые поля");
                }

                if (actionModel.NameSettlView6 != String.Empty && (actionModel.WaySettl6 == String.Empty || actionModel.ReflectedBU6 == String.Empty || actionModel.ReflectedDepartment6 == String.Empty || actionModel.AverageEarnings6 == String.Empty || actionModel.TypeCost6 == String.Empty))
                {
                    errorList.Add("В строке 6 не заполнены все необходимые поля");
                }

                if (actionModel.NameSettlView7 != String.Empty && (actionModel.WaySettl7 == String.Empty || actionModel.ReflectedBU7 == String.Empty || actionModel.ReflectedDepartment7 == String.Empty || actionModel.AverageEarnings7 == String.Empty || actionModel.TypeCost7 == String.Empty))
                {
                    errorList.Add("В строке 7 не заполнены все необходимые поля");
                }

                if (actionModel.NameSettlView8 != String.Empty && (actionModel.WaySettl8 == String.Empty || actionModel.ReflectedBU8 == String.Empty || actionModel.ReflectedDepartment8 == String.Empty || actionModel.AverageEarnings8 == String.Empty || actionModel.TypeCost8 == String.Empty))
                {
                    errorList.Add("В строке 8 не заполнены все необходимые поля");
                }

                if (actionModel.NameSettlView9 != String.Empty && (actionModel.WaySettl9 == String.Empty || actionModel.ReflectedBU9 == String.Empty || actionModel.ReflectedDepartment9 == String.Empty || actionModel.AverageEarnings9 == String.Empty || actionModel.TypeCost9 == String.Empty))
                {
                    errorList.Add("В строке 9 не заполнены все необходимые поля");
                }
            }

            if (type == (new USR_REQ_URP_RequestForHRChGraphTime_View()).GetType())
            {
                if (actionModel.Name1 != String.Empty && (actionModel.Position1 == String.Empty || actionModel.Department1 == String.Empty || actionModel.Reason1 == String.Empty || actionModel.Agreement1 == String.Empty || actionModel.StartDate1 == null || actionModel.EndDate1 == null))
                {
                    errorList.Add("В строке 1 не заполнены все необходимые поля");
                }

                if (actionModel.Name2 != String.Empty && (actionModel.Position2 == String.Empty || actionModel.Department2 == String.Empty || actionModel.Reason2 == String.Empty || actionModel.Agreement2 == String.Empty || actionModel.StartDate2 == null || actionModel.EdnDate2 == null))
                {
                    errorList.Add("В строке 2 не заполнены все необходимые поля");
                }

                if (actionModel.Name3 != String.Empty && (actionModel.Position3 == String.Empty || actionModel.Department3 == String.Empty || actionModel.Reason3 == String.Empty || actionModel.Agreement3 == String.Empty || actionModel.StartDate3 == null || actionModel.EdnDate3 == null))
                {
                    errorList.Add("В строке 3 не заполнены все необходимые поля");
                }

                if (actionModel.Name4 != String.Empty && (actionModel.Position4 == String.Empty || actionModel.Department4 == String.Empty || actionModel.Reason4 == String.Empty || actionModel.Agreement4 == String.Empty || actionModel.StartDate4 == null || actionModel.EdnDate4 == null))
                {
                    errorList.Add("В строке 4 не заполнены все необходимые поля");
                }

                if (actionModel.Name5 != String.Empty && (actionModel.Position5 == String.Empty || actionModel.Department5 == String.Empty || actionModel.Reason5 == String.Empty || actionModel.Agreement5 == String.Empty || actionModel.StartDate5 == null || actionModel.EndDate5 == null))
                {
                    errorList.Add("В строке 5 не заполнены все необходимые поля");
                }

                if (actionModel.Name6 != String.Empty && (actionModel.Position6 == String.Empty || actionModel.Department6 == String.Empty || actionModel.Reason6 == String.Empty || actionModel.Agreement6 == String.Empty || actionModel.StartDate6 == null || actionModel.EndDate6 == null))
                {
                    errorList.Add("В строке 6 не заполнены все необходимые поля");
                }

                if (actionModel.Name7 != String.Empty && (actionModel.Position7 == String.Empty || actionModel.Department7 == String.Empty || actionModel.Reason7 == String.Empty || actionModel.Agreement7 == String.Empty || actionModel.StartDate7 == null || actionModel.EndDate7 == null))
                {
                    errorList.Add("В строке 7 не заполнены все необходимые поля");
                }

                if (actionModel.Name8 != String.Empty && (actionModel.Position8 == String.Empty || actionModel.Department8 == String.Empty || actionModel.Reason8 == String.Empty || actionModel.Agreement8 == String.Empty || actionModel.StartDate8 == null || actionModel.EndDate8 == null))
                {
                    errorList.Add("В строке 8 не заполнены все необходимые поля");
                }

                if (actionModel.Name9 != String.Empty && (actionModel.Position9 == String.Empty || actionModel.Department9 == String.Empty || actionModel.Reason9 == String.Empty || actionModel.Agreement9 == String.Empty || actionModel.StartDate9 == null || actionModel.EndDate9 == null))
                {
                    errorList.Add("В строке 9 не заполнены все необходимые поля");
                }

                if (actionModel.Name10 != String.Empty && (actionModel.Position10 == String.Empty || actionModel.Department10 == String.Empty || actionModel.Reason10 == String.Empty || actionModel.Agreement10 == String.Empty || actionModel.StartDate10 == null || actionModel.EndDate10 == null))
                {
                    errorList.Add("В строке 10 не заполнены все необходимые поля");
                }
            }

            if (type == (new USR_REQ_URP_RequestForHRChGraphConst_View()).GetType())
            {
                if (actionModel.Name1 != String.Empty && (actionModel.Position1 == String.Empty || actionModel.Department1 == String.Empty || actionModel.Reason1 == String.Empty || actionModel.Agreement1 == String.Empty || actionModel.StartDate1 == null))
                {
                    errorList.Add("В строке 1 не заполнены все необходимые поля");
                }

                if (actionModel.Name2 != String.Empty && (actionModel.Position2 == String.Empty || actionModel.Department2 == String.Empty || actionModel.Reason2 == String.Empty || actionModel.Agreement2 == String.Empty || actionModel.StartDate2 == null))
                {
                    errorList.Add("В строке 2 не заполнены все необходимые поля");
                }

                if (actionModel.Name3 != String.Empty && (actionModel.Position3 == String.Empty || actionModel.Department3 == String.Empty || actionModel.Reason3 == String.Empty || actionModel.Agreement3 == String.Empty || actionModel.StartDate3 == null))
                {
                    errorList.Add("В строке 3 не заполнены все необходимые поля");
                }

                if (actionModel.Name4 != String.Empty && (actionModel.Position4 == String.Empty || actionModel.Department4 == String.Empty || actionModel.Reason4 == String.Empty || actionModel.Agreement4 == String.Empty || actionModel.StartDate4 == null))
                {
                    errorList.Add("В строке 4 не заполнены все необходимые поля");
                }

                if (actionModel.Name5 != String.Empty && (actionModel.Position5 == String.Empty || actionModel.Department5 == String.Empty || actionModel.Reason5 == String.Empty || actionModel.Agreement5 == String.Empty || actionModel.StartDate5 == null))
                {
                    errorList.Add("В строке 5 не заполнены все необходимые поля");
                }

                if (actionModel.Name6 != String.Empty && (actionModel.Position6 == String.Empty || actionModel.Department6 == String.Empty || actionModel.Reason6 == String.Empty || actionModel.Agreement6 == String.Empty || actionModel.StartDate6 == null))
                {
                    errorList.Add("В строке 6 не заполнены все необходимые поля");
                }

                if (actionModel.Name7 != String.Empty && (actionModel.Position7 == String.Empty || actionModel.Department7 == String.Empty || actionModel.Reason7 == String.Empty || actionModel.Agreement7 == String.Empty || actionModel.StartDate7 == null))
                {
                    errorList.Add("В строке 7 не заполнены все необходимые поля");
                }

                if (actionModel.Name8 != String.Empty && (actionModel.Position8 == String.Empty || actionModel.Department8 == String.Empty || actionModel.Reason8 == String.Empty || actionModel.Agreement8 == String.Empty || actionModel.StartDate8 == null))
                {
                    errorList.Add("В строке 8 не заполнены все необходимые поля");
                }

                if (actionModel.Name9 != String.Empty && (actionModel.Position9 == String.Empty || actionModel.Department9 == String.Empty || actionModel.Reason9 == String.Empty || actionModel.Agreement9 == String.Empty || actionModel.StartDate9 == null))
                {
                    errorList.Add("В строке 9 не заполнены все необходимые поля");
                }

                if (actionModel.Name10 != String.Empty && (actionModel.Position10 == String.Empty || actionModel.Department10 == String.Empty || actionModel.Reason10 == String.Empty || actionModel.Agreement10 == String.Empty || actionModel.StartDate10 == null))
                {
                    errorList.Add("В строке 10 не заполнены все необходимые поля");
                }
            }

            if (type == (new USR_REQ_URP_RequestForPaymentFirstDay_View()).GetType())
            {
                if (actionModel.Name1 != String.Empty && (actionModel.Position1 == String.Empty || actionModel.Term1 == 0 || actionModel.Amount1 == String.Empty || actionModel.StartDate1 == null))
                {
                    errorList.Add("В строке 1 не заполнены все необходимые поля");
                }

                if (actionModel.Name2 != String.Empty && (actionModel.Position2 == String.Empty || actionModel.Term2 == 0 || actionModel.Amount2 == String.Empty || actionModel.StartDate2 == null))
                {
                    errorList.Add("В строке 2 не заполнены все необходимые поля");
                }

                if (actionModel.Name3 != String.Empty && (actionModel.Position3 == String.Empty || actionModel.Term3 == 0 || actionModel.Amount3 == String.Empty || actionModel.StartDate3 == null))
                {
                    errorList.Add("В строке 3 не заполнены все необходимые поля");
                }

                if (actionModel.Name4 != String.Empty && (actionModel.Position3 == String.Empty || actionModel.Amount4 == String.Empty || actionModel.Term4 == 0 || actionModel.StartDate4 == null))
                {
                    errorList.Add("В строке 4 не заполнены все необходимые поля");
                }
            }

            return errorList;
        }

        public List<string> CheckCustomDocumentHY(Type type, dynamic actionModel)
        {
            List<string> errorList = new List<string>();

            if (type == (new USR_REQ_HY_EmergencyPurposeTRU_View()).GetType())
            {
                if (actionModel.ItemName1 != String.Empty && (actionModel.Unit1 == String.Empty || actionModel.Qty1 == String.Empty || actionModel.Location1 == String.Empty || actionModel.Reason1 == String.Empty))
                {
                    errorList.Add("В строке 1 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName2 != String.Empty && (actionModel.Unit2 == String.Empty || actionModel.Qty2 == String.Empty || actionModel.Location2 == String.Empty || actionModel.Reason2 == String.Empty))
                {
                    errorList.Add("В строке 2 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName3 != String.Empty && (actionModel.Unit3 == String.Empty || actionModel.Qty3 == String.Empty || actionModel.Location3 == String.Empty || actionModel.Reason3 == String.Empty))
                {
                    errorList.Add("В строке 3 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName4 != String.Empty && (actionModel.Unit4 == String.Empty || actionModel.Qty4 == String.Empty || actionModel.Location4 == String.Empty || actionModel.Reason4 == String.Empty))
                {
                    errorList.Add("В строке 4 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName5 != String.Empty && (actionModel.Unit5 == String.Empty || actionModel.Qty5 == String.Empty || actionModel.Location5 == String.Empty || actionModel.Reason5 == String.Empty))
                {
                    errorList.Add("В строке 5 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName6 != String.Empty && (actionModel.Unit6 == String.Empty || actionModel.Qty6 == String.Empty || actionModel.Location6 == String.Empty || actionModel.Reason6 == String.Empty))
                {
                    errorList.Add("В строке 6 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName7 != String.Empty && (actionModel.Unit7 == String.Empty || actionModel.Qty7 == String.Empty || actionModel.Location7 == String.Empty || actionModel.Reason7 == String.Empty))
                {
                    errorList.Add("В строке 7 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName8 != String.Empty && (actionModel.Unit8 == String.Empty || actionModel.Qty8 == String.Empty || actionModel.Location8 == String.Empty || actionModel.Reason8 == String.Empty))
                {
                    errorList.Add("В строке 8 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName9 != String.Empty && (actionModel.Unit9 == String.Empty || actionModel.Qty9 == String.Empty || actionModel.Location9 == String.Empty || actionModel.Reason9 == String.Empty))
                {
                    errorList.Add("В строке 9 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName10 != String.Empty && (actionModel.Unit10 == String.Empty || actionModel.Qty10 == String.Empty || actionModel.Location10 == String.Empty || actionModel.Reason10 == String.Empty))
                {
                    errorList.Add("В строке 10 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName11 != String.Empty && (actionModel.Unit11 == String.Empty || actionModel.Qty11 == String.Empty || actionModel.Location11 == String.Empty || actionModel.Reason11 == String.Empty))
                {
                    errorList.Add("В строке 11 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName12 != String.Empty && (actionModel.Unit12 == String.Empty || actionModel.Qty12 == String.Empty || actionModel.Location12 == String.Empty || actionModel.Reason12 == String.Empty))
                {
                    errorList.Add("В строке 12 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName13 != String.Empty && (actionModel.Unit13 == String.Empty || actionModel.Qty13 == String.Empty || actionModel.Location13 == String.Empty || actionModel.Reason13 == String.Empty))
                {
                    errorList.Add("В строке 13 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName14 != String.Empty && (actionModel.Unit14 == String.Empty || actionModel.Qty14 == String.Empty || actionModel.Location14 == String.Empty || actionModel.Reason14 == String.Empty))
                {
                    errorList.Add("В строке 14 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName15 != String.Empty && (actionModel.Unit15 == String.Empty || actionModel.Qty15 == String.Empty || actionModel.Location15 == String.Empty || actionModel.Reason15 == String.Empty))
                {
                    errorList.Add("В строке 15 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName16 != String.Empty && (actionModel.Unit16 == String.Empty || actionModel.Qty16 == String.Empty || actionModel.Location16 == String.Empty || actionModel.Reason16 == String.Empty))
                {
                    errorList.Add("В строке 16 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName17 != String.Empty && (actionModel.Unit17 == String.Empty || actionModel.Qty17 == String.Empty || actionModel.Location17 == String.Empty || actionModel.Reason17 == String.Empty))
                {
                    errorList.Add("В строке 17 не заполнены все необходимые поля");
                }
            }

            if (type == (new USR_REQ_HY_RequestTRU_View()).GetType())
            {
                if (actionModel.ItemName1 != String.Empty && (actionModel.Unit1 == String.Empty || actionModel.Qty1 == String.Empty || actionModel.Location1 == String.Empty || actionModel.Reason1 == String.Empty))
                {
                    errorList.Add("В строке 1 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName2 != String.Empty && (actionModel.Unit2 == String.Empty || actionModel.Qty2 == String.Empty || actionModel.Location2 == String.Empty || actionModel.Reason2 == String.Empty))
                {
                    errorList.Add("В строке 2 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName3 != String.Empty && (actionModel.Unit3 == String.Empty || actionModel.Qty3 == String.Empty || actionModel.Location3 == String.Empty || actionModel.Reason3 == String.Empty))
                {
                    errorList.Add("В строке 3 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName4 != String.Empty && (actionModel.Unit4 == String.Empty || actionModel.Qty4 == String.Empty || actionModel.Location4 == String.Empty || actionModel.Reason4 == String.Empty))
                {
                    errorList.Add("В строке 4 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName5 != String.Empty && (actionModel.Unit5 == String.Empty || actionModel.Qty5 == String.Empty || actionModel.Location5 == String.Empty || actionModel.Reason5 == String.Empty))
                {
                    errorList.Add("В строке 5 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName6 != String.Empty && (actionModel.Unit6 == String.Empty || actionModel.Qty6 == String.Empty || actionModel.Location6 == String.Empty || actionModel.Reason6 == String.Empty))
                {
                    errorList.Add("В строке 6 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName7 != String.Empty && (actionModel.Unit7 == String.Empty || actionModel.Qty7 == String.Empty || actionModel.Location7 == String.Empty || actionModel.Reason7 == String.Empty))
                {
                    errorList.Add("В строке 7 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName8 != String.Empty && (actionModel.Unit8 == String.Empty || actionModel.Qty8 == String.Empty || actionModel.Location8 == String.Empty || actionModel.Reason8 == String.Empty))
                {
                    errorList.Add("В строке 8 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName9 != String.Empty && (actionModel.Unit9 == String.Empty || actionModel.Qty9 == String.Empty || actionModel.Location9 == String.Empty || actionModel.Reason9 == String.Empty))
                {
                    errorList.Add("В строке 9 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName10 != String.Empty && (actionModel.Unit10 == String.Empty || actionModel.Qty10 == String.Empty || actionModel.Location10 == String.Empty || actionModel.Reason10 == String.Empty))
                {
                    errorList.Add("В строке 10 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName11 != String.Empty && (actionModel.Unit11 == String.Empty || actionModel.Qty11 == String.Empty || actionModel.Location11 == String.Empty || actionModel.Reason11 == String.Empty))
                {
                    errorList.Add("В строке 11 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName12 != String.Empty && (actionModel.Unit12 == String.Empty || actionModel.Qty12 == String.Empty || actionModel.Location12 == String.Empty || actionModel.Reason12 == String.Empty))
                {
                    errorList.Add("В строке 12 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName13 != String.Empty && (actionModel.Unit13 == String.Empty || actionModel.Qty13 == String.Empty || actionModel.Location13 == String.Empty || actionModel.Reason13 == String.Empty))
                {
                    errorList.Add("В строке 13 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName14 != String.Empty && (actionModel.Unit14 == String.Empty || actionModel.Qty14 == String.Empty || actionModel.Location14 == String.Empty || actionModel.Reason14 == String.Empty))
                {
                    errorList.Add("В строке 14 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName15 != String.Empty && (actionModel.Unit15 == String.Empty || actionModel.Qty15 == String.Empty || actionModel.Location15 == String.Empty || actionModel.Reason15 == String.Empty))
                {
                    errorList.Add("В строке 15 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName16 != String.Empty && (actionModel.Unit16 == String.Empty || actionModel.Qty16 == String.Empty || actionModel.Location16 == String.Empty || actionModel.Reason16 == String.Empty))
                {
                    errorList.Add("В строке 16 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName17 != String.Empty && (actionModel.Unit17 == String.Empty || actionModel.Qty17 == String.Empty || actionModel.Location17 == String.Empty || actionModel.Reason17 == String.Empty))
                {
                    errorList.Add("В строке 17 не заполнены все необходимые поля");
                }
            }

            if (type == (new USR_REQ_HY_EmergencyRequestTRU_View()).GetType())
            {
                if (actionModel.ItemName1 != String.Empty && (actionModel.Unit1 == String.Empty || actionModel.Qty1 == String.Empty || actionModel.Location1 == String.Empty || actionModel.Reason1 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 1 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName2 != String.Empty && (actionModel.Unit2 == String.Empty || actionModel.Qty2 == String.Empty || actionModel.Location2 == String.Empty || actionModel.Reason2 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 2 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName3 != String.Empty && (actionModel.Unit3 == String.Empty || actionModel.Qty3 == String.Empty || actionModel.Location3 == String.Empty || actionModel.Reason3 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 3 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName4 != String.Empty && (actionModel.Unit4 == String.Empty || actionModel.Qty4 == String.Empty || actionModel.Location4 == String.Empty || actionModel.Reason4 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 4 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName5 != String.Empty && (actionModel.Unit5 == String.Empty || actionModel.Qty5 == String.Empty || actionModel.Location5 == String.Empty || actionModel.Reason5 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 5 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName6 != String.Empty && (actionModel.Unit6 == String.Empty || actionModel.Qty6 == String.Empty || actionModel.Location6 == String.Empty || actionModel.Reason6 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 6 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName7 != String.Empty && (actionModel.Unit7 == String.Empty || actionModel.Qty7 == String.Empty || actionModel.Location7 == String.Empty || actionModel.Reason7 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 7 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName8 != String.Empty && (actionModel.Unit8 == String.Empty || actionModel.Qty8 == String.Empty || actionModel.Location8 == String.Empty || actionModel.Reason8 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 8 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName9 != String.Empty && (actionModel.Unit9 == String.Empty || actionModel.Qty9 == String.Empty || actionModel.Location9 == String.Empty || actionModel.Reason9 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 9 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName10 != String.Empty && (actionModel.Unit10 == String.Empty || actionModel.Qty10 == String.Empty || actionModel.Location10 == String.Empty || actionModel.Reason10 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 10 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName11 != String.Empty && (actionModel.Unit11 == String.Empty || actionModel.Qty11 == String.Empty || actionModel.Location11 == String.Empty || actionModel.Reason11 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 11 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName12 != String.Empty && (actionModel.Unit12 == String.Empty || actionModel.Qty12 == String.Empty || actionModel.Location12 == String.Empty || actionModel.Reason12 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 12 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName13 != String.Empty && (actionModel.Unit13 == String.Empty || actionModel.Qty13 == String.Empty || actionModel.Location13 == String.Empty || actionModel.Reason13 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 13 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName14 != String.Empty && (actionModel.Unit14 == String.Empty || actionModel.Qty14 == String.Empty || actionModel.Location14 == String.Empty || actionModel.Reason14 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 14 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName15 != String.Empty && (actionModel.Unit15 == String.Empty || actionModel.Qty15 == String.Empty || actionModel.Location15 == String.Empty || actionModel.Reason15 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 15 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName16 != String.Empty && (actionModel.Unit16 == String.Empty || actionModel.Qty16 == String.Empty || actionModel.Location16 == String.Empty || actionModel.Reason16 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 16 не заполнены все необходимые поля");
                }

                if (actionModel.ItemName17 != String.Empty && (actionModel.Unit17 == String.Empty || actionModel.Qty17 == String.Empty || actionModel.Location17 == String.Empty || actionModel.Reason17 == String.Empty || (actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                {
                    errorList.Add("В строке 17 не заполнены все необходимые поля");
                }
            }

            return errorList;
        }

        public List<string> CheckCustomPostDocument(Type type, dynamic actionModel, DocumentTable documentTable, bool isSign)
        {
            List<string> errorList = new List<string>();

            if (type == (new USR_REQ_HY_BookingRoom_View()).GetType())
            {
                if (documentTable.ActivityName == "Начальник ХУ" && isSign == true)
                {
                    if (actionModel.UserChooseManual1 == String.Empty)
                    {
                        errorList.Add("Необходимо указать Исполнителя");
                    }
                }
            }

            if (type == (new USR_REQ_HY_FindApartment_View()).GetType())
            {
                if (documentTable.ActivityName == "Начальник ХУ" && isSign == true)
                {
                    if (actionModel.UserChooseManual1 == String.Empty)
                    {
                        errorList.Add("Необходимо указать Исполнителя");
                    }
                }
            }

            if (type == (new USR_REQ_HY_RequestRepair_View()).GetType())
            {
                if (documentTable.ActivityName == "Начальник ХУ" && isSign == true)
                {
                    if (actionModel.UserChooseManual1 == String.Empty)
                    {
                        errorList.Add("Необходимо указать Исполнителя");
                    }
                }
            }

            if (type == (new USR_REQ_HY_EmergencyPurposeTRU_View()).GetType())
            {
                if ((documentTable.ActivityName == "СХО" || documentTable.ActivityName == "Начальник СХО") && isSign == true)
                {
                    if (actionModel.ItemName1 != String.Empty && ((actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                    {
                        errorList.Add("В строке 1 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName2 != String.Empty && ((actionModel.Price2 == String.Empty && actionModel.Amount2 == String.Empty) || actionModel.AccountBZ2 == String.Empty))
                    {
                        errorList.Add("В строке 2 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName3 != String.Empty && ((actionModel.Price3 == String.Empty && actionModel.Amount3 == String.Empty) || actionModel.AccountBZ3 == String.Empty))
                    {
                        errorList.Add("В строке 3 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName4 != String.Empty && ((actionModel.Price4 == String.Empty && actionModel.Amount4 == String.Empty) || actionModel.AccountBZ4 == String.Empty))
                    {
                        errorList.Add("В строке 4 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName5 != String.Empty && ((actionModel.Price5 == String.Empty && actionModel.Amount5 == String.Empty) || actionModel.AccountBZ5 == String.Empty))
                    {
                        errorList.Add("В строке 5 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName6 != String.Empty && ((actionModel.Price6 == String.Empty && actionModel.Amount6 == String.Empty) || actionModel.AccountBZ6 == String.Empty))
                    {
                        errorList.Add("В строке 6 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName7 != String.Empty && ((actionModel.Price7 == String.Empty && actionModel.Amount7 == String.Empty) || actionModel.AccountBZ7 == String.Empty))
                    {
                        errorList.Add("В строке 7 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName8 != String.Empty && ((actionModel.Price8 == String.Empty && actionModel.Amount8 == String.Empty) || actionModel.AccountBZ8 == String.Empty))
                    {
                        errorList.Add("В строке 8 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName9 != String.Empty && ((actionModel.Price9 == String.Empty && actionModel.Amount9 == String.Empty) || actionModel.AccountBZ9 == String.Empty))
                    {
                        errorList.Add("В строке 9 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName10 != String.Empty && ((actionModel.Price10 == String.Empty && actionModel.Amount10 == String.Empty) || actionModel.AccountBZ10 == String.Empty))
                    {
                        errorList.Add("В строке 10 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName11 != String.Empty && ((actionModel.Price11 == String.Empty && actionModel.Amount11 == String.Empty) || actionModel.AccountBZ11 == String.Empty))
                    {
                        errorList.Add("В строке 11 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName12 != String.Empty && ((actionModel.Price12 == String.Empty && actionModel.Amount12 == String.Empty) || actionModel.AccountBZ12 == String.Empty))
                    {
                        errorList.Add("В строке 12 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName13 != String.Empty && ((actionModel.Price13 == String.Empty && actionModel.Amount13 == String.Empty) || actionModel.AccountBZ13 == String.Empty))
                    {
                        errorList.Add("В строке 13 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName14 != String.Empty && ((actionModel.Price14 == String.Empty && actionModel.Amount14 == String.Empty) || actionModel.AccountBZ14 == String.Empty))
                    {
                        errorList.Add("В строке 14 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName15 != String.Empty && ((actionModel.Price15 == String.Empty && actionModel.Amount15 == String.Empty) || actionModel.AccountBZ15 == String.Empty))
                    {
                        errorList.Add("В строке 15 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName16 != String.Empty && ((actionModel.Price16 == String.Empty && actionModel.Amount16 == String.Empty) || actionModel.AccountBZ16 == String.Empty))
                    {
                        errorList.Add("В строке 16 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName17 != String.Empty && ((actionModel.Price17 == String.Empty && actionModel.Amount17 == String.Empty) || actionModel.AccountBZ17 == String.Empty))
                    {
                        errorList.Add("В строке 17 не заполнены все необходимые поля");
                    }
                }

                if (documentTable.ActivityName == "Начальник СХО" && isSign == true)
                {
                    if (String.IsNullOrEmpty(actionModel.UserChooseManual2))
                    {
                        errorList.Add("Нужно заполнить поле Исполнитель");
                    }
                }
            }

            if (type == (new USR_REQ_HY_RequestTRU_View()).GetType())
            {
                if ((documentTable.ActivityName == "СХО" || documentTable.ActivityName == "Начальник СХО") && isSign == true)
                {
                    if (actionModel.ItemName1 != String.Empty && ((actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                    {
                        errorList.Add("В строке 1 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName2 != String.Empty && ((actionModel.Price2 == String.Empty && actionModel.Amount2 == String.Empty) || actionModel.AccountBZ2 == String.Empty))
                    {
                        errorList.Add("В строке 2 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName3 != String.Empty && ((actionModel.Price3 == String.Empty && actionModel.Amount3 == String.Empty) || actionModel.AccountBZ3 == String.Empty))
                    {
                        errorList.Add("В строке 3 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName4 != String.Empty && ((actionModel.Price4 == String.Empty && actionModel.Amount4 == String.Empty) || actionModel.AccountBZ4 == String.Empty))
                    {
                        errorList.Add("В строке 4 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName5 != String.Empty && ((actionModel.Price5 == String.Empty && actionModel.Amount5 == String.Empty) || actionModel.AccountBZ5 == String.Empty))
                    {
                        errorList.Add("В строке 5 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName6 != String.Empty && ((actionModel.Price6 == String.Empty && actionModel.Amount6 == String.Empty) || actionModel.AccountBZ6 == String.Empty))
                    {
                        errorList.Add("В строке 6 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName7 != String.Empty && ((actionModel.Price7 == String.Empty && actionModel.Amount7 == String.Empty) || actionModel.AccountBZ7 == String.Empty))
                    {
                        errorList.Add("В строке 7 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName8 != String.Empty && ((actionModel.Price8 == String.Empty && actionModel.Amount8 == String.Empty) || actionModel.AccountBZ8 == String.Empty))
                    {
                        errorList.Add("В строке 8 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName9 != String.Empty && ((actionModel.Price9 == String.Empty && actionModel.Amount9 == String.Empty) || actionModel.AccountBZ9 == String.Empty))
                    {
                        errorList.Add("В строке 9 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName10 != String.Empty && ((actionModel.Price10 == String.Empty && actionModel.Amount10 == String.Empty) || actionModel.AccountBZ10 == String.Empty))
                    {
                        errorList.Add("В строке 10 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName11 != String.Empty && ((actionModel.Price11 == String.Empty && actionModel.Amount11 == String.Empty) || actionModel.AccountBZ11 == String.Empty))
                    {
                        errorList.Add("В строке 11 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName12 != String.Empty && ((actionModel.Price12 == String.Empty && actionModel.Amount12 == String.Empty) || actionModel.AccountBZ12 == String.Empty))
                    {
                        errorList.Add("В строке 12 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName13 != String.Empty && ((actionModel.Price13 == String.Empty && actionModel.Amount13 == String.Empty) || actionModel.AccountBZ13 == String.Empty))
                    {
                        errorList.Add("В строке 13 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName14 != String.Empty && ((actionModel.Price14 == String.Empty && actionModel.Amount14 == String.Empty) || actionModel.AccountBZ14 == String.Empty))
                    {
                        errorList.Add("В строке 14 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName15 != String.Empty && ((actionModel.Price15 == String.Empty && actionModel.Amount15 == String.Empty) || actionModel.AccountBZ15 == String.Empty))
                    {
                        errorList.Add("В строке 15 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName16 != String.Empty && ((actionModel.Price16 == String.Empty && actionModel.Amount16 == String.Empty) || actionModel.AccountBZ16 == String.Empty))
                    {
                        errorList.Add("В строке 16 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName17 != String.Empty && ((actionModel.Price17 == String.Empty && actionModel.Amount17 == String.Empty) || actionModel.AccountBZ17 == String.Empty))
                    {
                        errorList.Add("В строке 17 не заполнены все необходимые поля");
                    }
                }

                if (documentTable.ActivityName == "Начальник СХО" && isSign == true)
                {
                    if (String.IsNullOrEmpty(actionModel.UserChooseManual2))
                    {
                        errorList.Add("Нужно заполнить поле Исполнитель");
                    }
                }
            }

            if (type == (new USR_REQ_HY_EmergencyRequestTRU_View()).GetType())
            {
                if ((documentTable.ActivityName == "Начальник СХО") && isSign == true)
                {
                    if (actionModel.ItemName1 != String.Empty && ((actionModel.Price1 == String.Empty && actionModel.Amount1 == String.Empty) || actionModel.AccountBZ1 == String.Empty))
                    {
                        errorList.Add("В строке 1 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName2 != String.Empty && ((actionModel.Price2 == String.Empty && actionModel.Amount2 == String.Empty) || actionModel.AccountBZ2 == String.Empty))
                    {
                        errorList.Add("В строке 2 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName3 != String.Empty && ((actionModel.Price3 == String.Empty && actionModel.Amount3 == String.Empty) || actionModel.AccountBZ3 == String.Empty))
                    {
                        errorList.Add("В строке 3 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName4 != String.Empty && ((actionModel.Price4 == String.Empty && actionModel.Amount4 == String.Empty) || actionModel.AccountBZ4 == String.Empty))
                    {
                        errorList.Add("В строке 4 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName5 != String.Empty && ((actionModel.Price5 == String.Empty && actionModel.Amount5 == String.Empty) || actionModel.AccountBZ5 == String.Empty))
                    {
                        errorList.Add("В строке 5 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName6 != String.Empty && ((actionModel.Price6 == String.Empty && actionModel.Amount6 == String.Empty) || actionModel.AccountBZ6 == String.Empty))
                    {
                        errorList.Add("В строке 6 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName7 != String.Empty && ((actionModel.Price7 == String.Empty && actionModel.Amount7 == String.Empty) || actionModel.AccountBZ7 == String.Empty))
                    {
                        errorList.Add("В строке 7 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName8 != String.Empty && ((actionModel.Price8 == String.Empty && actionModel.Amount8 == String.Empty) || actionModel.AccountBZ8 == String.Empty))
                    {
                        errorList.Add("В строке 8 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName9 != String.Empty && ((actionModel.Price9 == String.Empty && actionModel.Amount9 == String.Empty) || actionModel.AccountBZ9 == String.Empty))
                    {
                        errorList.Add("В строке 9 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName10 != String.Empty && ((actionModel.Price10 == String.Empty && actionModel.Amount10 == String.Empty) || actionModel.AccountBZ10 == String.Empty))
                    {
                        errorList.Add("В строке 10 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName11 != String.Empty && ((actionModel.Price11 == String.Empty && actionModel.Amount11 == String.Empty) || actionModel.AccountBZ11 == String.Empty))
                    {
                        errorList.Add("В строке 11 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName12 != String.Empty && ((actionModel.Price12 == String.Empty && actionModel.Amount12 == String.Empty) || actionModel.AccountBZ12 == String.Empty))
                    {
                        errorList.Add("В строке 12 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName13 != String.Empty && ((actionModel.Price13 == String.Empty && actionModel.Amount13 == String.Empty) || actionModel.AccountBZ13 == String.Empty))
                    {
                        errorList.Add("В строке 13 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName14 != String.Empty && ((actionModel.Price14 == String.Empty && actionModel.Amount14 == String.Empty) || actionModel.AccountBZ14 == String.Empty))
                    {
                        errorList.Add("В строке 14 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName15 != String.Empty && ((actionModel.Price15 == String.Empty && actionModel.Amount15 == String.Empty) || actionModel.AccountBZ15 == String.Empty))
                    {
                        errorList.Add("В строке 15 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName16 != String.Empty && ((actionModel.Price16 == String.Empty && actionModel.Amount16 == String.Empty) || actionModel.AccountBZ16 == String.Empty))
                    {
                        errorList.Add("В строке 16 не заполнены все необходимые поля");
                    }
                    if (actionModel.ItemName17 != String.Empty && ((actionModel.Price17 == String.Empty && actionModel.Amount17 == String.Empty) || actionModel.AccountBZ17 == String.Empty))
                    {
                        errorList.Add("В строке 17 не заполнены все необходимые поля");
                    }

                    if (String.IsNullOrEmpty(actionModel.UserChooseManual1))
                    {
                        errorList.Add("Нужно заполнить поле Исполнитель");
                    }
                }
            }

            return errorList;
        }

        public dynamic PreUpdateViewModel(Type type, dynamic actionModel)
        {
            if (type == (new USR_REQ_UBUO_RequestCalcDriveTrip_View()).GetType() || type == (new USR_REQ_TRIP_RegistrationBusinessTripKZ_View()).GetType() || type == (new USR_REQ_TRIP_RegistrationBusinessTripPP_View()).GetType())
            {
                if (actionModel.FIO1 != null)
                {
                    EmplTripType emplTripType1 = (EmplTripType)actionModel.EmplTripType1;
                    TripDirection tripDirection1 = (TripDirection)actionModel.TripDirection1;
                    TripSettingsTable tripSettingsTable = _TripSettingsService.FirstOrDefault(x => x.EmplTripType == emplTripType1 && x.TripDirection == tripDirection1);
                    actionModel.DayRate1 = tripSettingsTable.DayRate;
                    actionModel.ResidenceRate1 = tripSettingsTable.ResidenceRate;
                }

                if (actionModel.FIO2 != null)
                {
                    EmplTripType emplTripType2 = (EmplTripType)actionModel.EmplTripType2;
                    TripDirection tripDirection2 = (TripDirection)actionModel.TripDirection2;
                    TripSettingsTable tripSettingsTable = _TripSettingsService.FirstOrDefault(x => x.EmplTripType == emplTripType2 && x.TripDirection == tripDirection2);
                    actionModel.DayRate2 = tripSettingsTable.DayRate;
                    actionModel.ResidenceRate2 = tripSettingsTable.ResidenceRate;
                }

                if (actionModel.FIO3 != null)
                {
                    EmplTripType emplTripType3 = (EmplTripType)actionModel.EmplTripType3;
                    TripDirection tripDirection3 = (TripDirection)actionModel.TripDirection3;
                    TripSettingsTable tripSettingsTable = _TripSettingsService.FirstOrDefault(x => x.EmplTripType == emplTripType3 && x.TripDirection == tripDirection3);
                    actionModel.DayRate3 = tripSettingsTable.DayRate;
                    actionModel.ResidenceRate3 = tripSettingsTable.ResidenceRate;
                }

                if (actionModel.FIO4 != null)
                {
                    EmplTripType emplTripType4 = (EmplTripType)actionModel.EmplTripType4;
                    TripDirection tripDirection4 = (TripDirection)actionModel.TripDirection4;
                    TripSettingsTable tripSettingsTable = _TripSettingsService.FirstOrDefault(x => x.EmplTripType == emplTripType4 && x.TripDirection == tripDirection4);
                    actionModel.DayRate4 = tripSettingsTable.DayRate;
                    actionModel.ResidenceRate4 = tripSettingsTable.ResidenceRate;
                }
            }

            return actionModel;
        }

        public void UpdateDocumentData(DocumentTable document, IDictionary<string, object> documentData)
        {
            if (document.ProcessTable.TableName == "USR_REQ_IT_CTP_IncidentIT")
            {
                if (document.ActivityName == "Исполнитель")
                {
                    ServiceIncidientPriority priority = ((ServiceIncidientPriority)documentData["ServiceIncidientPriority"]);
                    ServiceIncidientLevel level = ((ServiceIncidientLevel)documentData["ServiceIncidientLevel"]);
                    ServiceIncidientLocation location = ((ServiceIncidientLocation)documentData["ServiceIncidientLocation"]);

                    var serviceIncident = _ServiceIncidentService.GetAll().ToList().FirstOrDefault(x => x.ServiceName == ((string)documentData["ServiceName"]) && x.ServiceIncidientPriority == priority && x.ServiceIncidientLevel == level && x.ServiceIncidientLocation == location);
                    if (serviceIncident != null)
                    {
                        var items = _WorkflowTrackerService.GetPartial(x => x.DocumentTableId == document.Id && x.ActivityName == document.ActivityName).ToList();

                        foreach (var item in items)
                        {
                            item.SLAOffset = serviceIncident.SLAIncident;
                            _WorkflowTrackerService.SaveDomain(item);
                        }
                    }
                }
            }
        }
    }
}