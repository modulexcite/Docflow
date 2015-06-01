using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RapidDoc.Models.Repository;

namespace RapidDoc.Models.ViewModels
{
    #region Заявки ИТ Связь
    public class USR_REQ_IT_CTS_DeliveryOfPinCode_View : BasicDocumentRequestView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        [Display(Name = "Выход на международную связь")]
        public bool International { get; set; }
    }

    public class USR_REQ_IT_CTS_DeliveryOfWS_View : BasicDocumentView
    {
        [Display(Name = "Тип радиостанции")]
        public AllWSType WSType { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Место установки")]
        public string RequestText { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Users", ResourceType = typeof(CustomRes.Custom))]
        public string Users { get; set; }
    }

    public class USR_REQ_IT_CTS_DeliveryOfComponentsWS_View : BasicDocumentView
    {
        [Display(Name = "Тип комплектующих")]
        public WSComponents WSComponents { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Место установки")]
        public string RequestText { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Users", ResourceType = typeof(CustomRes.Custom))]
        public string Users { get; set; }
    }

    public class USR_REQ_IT_CTS_DisassemblingOfWS_View : BasicDocumentView
    {
        [Display(Name = "Тип радиостанции")]
        public WSType WSType { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Место демонтажа")]
        public string RequestText { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Users", ResourceType = typeof(CustomRes.Custom))]
        public string Users { get; set; }
    }

    public class USR_REQ_IT_CTS_ReplacementPhone_View : BasicDocumentView
    {
        [Display(Name = "Заменить тип телефона")]
        public PhoneType FromPhoneType { get; set; }

        [Display(Name = "На тип")]
        public PhoneType ToPhoneType { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Место установки")]
        public string RequestText { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Users", ResourceType = typeof(CustomRes.Custom))]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CTS_ReplacementWorkPlace_View : BasicDocumentView
    {
        [Display(Name = "Что необходимо сделать?")]
        public WorkPlaceMovement WorkPlaceMovementType { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "С рабочего места")]
        public string FromPlace { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "На рабочее место")]
        public string ToPlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Users", ResourceType = typeof(CustomRes.Custom))]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
    }

    public class USR_REQ_IT_CTS_ReregistrationUserInPhoneSystem_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Users", ResourceType = typeof(CustomRes.Custom))]
        public string Users { get; set; }
    }

    public class USR_REQ_IT_CTS_DeleteRezervationNumber_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Что необходимо сделать?")]
        public ActionsPhone ActionsPhone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Период времени")]
        public string PeriodTime { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CTS_SetUpPhone_View : BasicDocumentView
    {
        [Display(Name = "Тип телефона")]
        public PhoneType PhoneType { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Users", ResourceType = typeof(CustomRes.Custom))]
        public string Users { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Место установки")]
        public string RequestText { get; set; }

        [Display(Name = "Выход на городскую связь")]
        public bool AccessPublic { get; set; }
    }

    public class USR_REQ_IT_CTS_ProblemWithPhone_View : BasicDocumentView
    {
        [Display(Name = "Тип проблемы")]
        public ProblemTypeCTS ProblemType { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Описание проблемы")]
        public string Problem { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Users", ResourceType = typeof(CustomRes.Custom))]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
    }

    public class USR_REQ_IT_CTS_SetUpDVO_View : BasicDocumentView
    {
        [Display(Name = "Тип переадресации")]
        public ForwardType ForwardType { get; set; }

        [Display(Name = "Номер переадресации")]
        public string ForwardPhone { get; set; }

        [Display(Name = "Количество звонков ")]
        public int ForwardNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Users", ResourceType = typeof(CustomRes.Custom))]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
    }

    public class USR_REQ_IT_CTS_DeliveryOfService_View : BasicDocumentView
    {
        [Display(Name = "Объект обслуживания")]
        public CTS_ServiceList ServiceList { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Описание")]
        public string RequestText { get; set; }
    }

    public class USR_REQ_IT_CTS_SetPersonalButton_View : BasicDocumentView
    {
        public ServiceType ServiceTypeButtonNo01 { get; set; }
        public ServiceType ServiceTypeButtonNo02 { get; set; }
        public ServiceType ServiceTypeButtonNo03 { get; set; }
        public ServiceType ServiceTypeButtonNo04 { get; set; }
        public ServiceType ServiceTypeButtonNo05 { get; set; }
        public ServiceType ServiceTypeButtonNo06 { get; set; }
        public ServiceType ServiceTypeButtonNo07 { get; set; }
        public ServiceType ServiceTypeButtonNo08 { get; set; }
        public ServiceType ServiceTypeButtonNo09 { get; set; }
        public ServiceType ServiceTypeButtonNo10 { get; set; }
        public ServiceType ServiceTypeButtonNo11 { get; set; }
        public ServiceType ServiceTypeButtonNo12 { get; set; }
        public ServiceType ServiceTypeButtonNo13 { get; set; }
        public ServiceType ServiceTypeButtonNo14 { get; set; }
        public ServiceType ServiceTypeButtonNo15 { get; set; }
        public ServiceType ServiceTypeButtonNo16 { get; set; }

        public string TextButtonNo01 { get; set; }
        public string TextButtonNo02 { get; set; }
        public string TextButtonNo03 { get; set; }
        public string TextButtonNo04 { get; set; }
        public string TextButtonNo05 { get; set; }
        public string TextButtonNo06 { get; set; }
        public string TextButtonNo07 { get; set; }
        public string TextButtonNo08 { get; set; }
        public string TextButtonNo09 { get; set; }
        public string TextButtonNo10 { get; set; }
        public string TextButtonNo11 { get; set; }
        public string TextButtonNo12 { get; set; }
        public string TextButtonNo13 { get; set; }
        public string TextButtonNo14 { get; set; }
        public string TextButtonNo15 { get; set; }
        public string TextButtonNo16 { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string RequestText { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Users", ResourceType = typeof(CustomRes.Custom))]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
    }
    #endregion

    #region Заявки ИТ ERP
    public class USR_REQ_IT_ERP_RequestPermission1C8Salary_View : BasicDocumentRequestView
    {

    }

    public class USR_REQ_IT_ERP_RequestPermission1C77_View : BasicDocumentView
    {
        public bool Altynavto_old { get; set; }
        public bool ATK2010 { get; set; }
        public bool Goldservice2002_2003 { get; set; }
        public bool Goldservice2004_2005 { get; set; }
        public bool vg_2005_db { get; set; }
        public bool vg_2006 { get; set; }
        public bool vg_2008 { get; set; }
        public bool vgok_2005 { get; set; }
        public bool vgok_2009 { get; set; }
        public bool vgok_zp_2005 { get; set; }
        public bool Zarp2005 { get; set; }
        public bool CP { get; set; }
        public bool CP_2005 { get; set; }
        public bool CP_2004 { get; set; }
        public bool CP_2003 { get; set; }
        public bool NarTab_2005 { get; set; }
        public bool NarTab_2006 { get; set; }
        public bool NarTab_2007 { get; set; }
        public bool NarTab_2009 { get; set; }
        public bool NarTab_2010 { get; set; }
        public bool NarTab_2011 { get; set; }
        public bool NarTab_AMY { get; set; }
        public bool Base { get; set; }
        public bool Base_2003 { get; set; }
        public bool Base_2005 { get; set; }

        public AccessRight1C77 AccessRight01 { get; set; }
        public AccessRight1C77 AccessRight02 { get; set; }
        public AccessRight1C77 AccessRight03 { get; set; }
        public AccessRight1C77 AccessRight04 { get; set; }
        public AccessRight1C77 AccessRight05 { get; set; }
        public AccessRight1C77 AccessRight06 { get; set; }
        public AccessRight1C77 AccessRight07 { get; set; }
        public AccessRight1C77 AccessRight08 { get; set; }
        public AccessRight1C77 AccessRight09 { get; set; }
        public AccessRight1C77 AccessRight10 { get; set; }
        public AccessRight1C77 AccessRight11 { get; set; }
        public AccessRight1C77 AccessRight12 { get; set; }
        public AccessRight1C77 AccessRight13 { get; set; }
        public AccessRight1C77 AccessRight14 { get; set; }
        public AccessRight1C77 AccessRight15 { get; set; }
        public AccessRight1C77 AccessRight16 { get; set; }
        public AccessRight1C77 AccessRight17 { get; set; }
        public AccessRight1C77 AccessRight18 { get; set; }
        public AccessRight1C77 AccessRight19 { get; set; }
        public AccessRight1C77 AccessRight20 { get; set; }
        public AccessRight1C77 AccessRight21 { get; set; }
        public AccessRight1C77 AccessRight22 { get; set; }
        public AccessRight1C77 AccessRight23 { get; set; }
        public AccessRight1C77 AccessRight24 { get; set; }
        public AccessRight1C77 AccessRight25 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Users", ResourceType = typeof(CustomRes.Custom))]
        public string Users { get; set; }
    }

    public class USR_REQ_IT_ERP_RequestPermissionAccountingDAX_View : BasicDocumentRequestView
    {

    }

    public class USR_REQ_IT_ERP_RequestPermissionTreasuryDAX_View : BasicDocumentRequestView
    {

    }

    public class USR_REQ_IT_ERP_ModificationDAX_View : BasicDocumentView
    {
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string RequestText { get; set; }

        [Display(Name = "Казначейство")]
        public bool Treasury { get; set; }

        [Display(Name = "Заявочная компания")]
        public bool Purchases { get; set; }

        [Display(Name = "Финансы")]
        public bool Finance { get; set; }
    }

    public class USR_REQ_IT_ERP_ChangeAnalyticalModel_View : BasicDocumentView
    {
        [Display(Name = "План счетов")]
        public string AccountNum { get; set; }

        [Display(Name = "Вид затрат")]
        public string TypeCosts { get; set; }

        [Display(Name = "Вид деятельности")]
        public string TypeActivity { get; set; }

        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Другое")]
        public string Other { get; set; }
    }
    #endregion

    #region Заявки ИТ СТП
    public class USR_REQ_IT_CTP_EquipmentInstallation_View : BasicDocumentView
    {
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование оборудования")]
        public string NameEquipment { get; set; }

        [Display(Name = "Количество")]
        public int NumberEquipment { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Местоположение")]
        public string Location { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }

    public class USR_REQ_IT_CTP_InstallNewComputer_View : BasicDocumentView
    {
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Местоположение")]
        public string Location { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пользователи")]
        public string Users { get; set; }
    }

    public class USR_REQ_IT_CTP_InstallSoftware_View : BasicDocumentView
    {
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Программное обеспечение")]
        public string Software { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пользователи")]
        public string Users { get; set; }
    }

    public class USR_REQ_IT_CTP_RecoverySimCard_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пользователи")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер телефона")]
        public string Pnone { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CTP_IssueSimCard_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пользователи")]
        public string Users { get; set; }

        [Display(Name = "Лимит")]
        public int Limit { get; set; }
    }

    public class USR_REQ_IT_CTP_IssueMaterial_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пользователи")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Инвентарные номера оборудования")]
        public string ItemId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование материала")]
        public string ItemName { get; set; }

        [Display(Name = "Количество")]
        public int Qty { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CTP_IssueStorage_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пользователи")]
        public string Users { get; set; }

        [Display(Name = "Тип носителя")]
        public StorageType StorageType { get; set; }

        [Display(Name = "Объем")]
        public StorageVolume StorageVolume { get; set; }

        [Display(Name = "Количество")]
        public int Qty { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CTP_ReplaceCartridge_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Модель принтера")]
        public string ModelPrinter { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Инвентарный номер принтера")]
        public string RassetId { get; set; }

        [Display(Name = "Количество")]
        public int Qty { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Местоположение")]
        public string Location { get; set; }
    }

    public class USR_REQ_IT_CTP_ReplaceComputer_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пользователи")]
        public string Users { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CTP_RequestEquipment_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пользователи")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Имя компьютера")]
        public string CopmputerName { get; set; }

        public string SystemUnitModel { get; set; }
        public string SystemUnitSerial { get; set; }

        public string NotebookModel { get; set; }
        public string NotebookSerial { get; set; }

        public string MonitorModel { get; set; }
        public string MonitorSerial { get; set; }

        public string PrinterModel { get; set; }
        public string PrinterSerial { get; set; }

        public string OtherEquipmentModel { get; set; }
        public string OtherEquipmentSerial { get; set; }
    }

    public class USR_REQ_IT_CTP_ReissueComputer_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "С пользователя")]
        public string FromUsers { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "На пользователя")]
        public string ToUsers { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Имя компьютера")]
        public string CopmputerName { get; set; }

        public string SystemUnitModel { get; set; }
        public string SystemUnitSerial { get; set; }
        public string SystemUnitItemId { get; set; }

        public string NotebookModel { get; set; }
        public string NotebookSerial { get; set; }
        public string NotebookItemId { get; set; }

        public string MonitorModel { get; set; }
        public string MonitorSerial { get; set; }
        public string MonitorItemId { get; set; }

        public string PrinterModel { get; set; }
        public string PrinterSerial { get; set; }
        public string PrinterItemId { get; set; }

        public string OtherEquipmentModel { get; set; }
        public string OtherEquipmentSerial { get; set; }
        public string OtherEquipmentItemId { get; set; }
    }

    public class USR_REQ_IT_CTP_IncidentIT_View : BasicDocumentRequestView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактный номер телефона")]
        public string Phone { get; set; } 

        [Display(Name = "Сервис")]
        public string ServiceName { get; set; }

        [Display(Name = "Приоритет")]
        public ServiceIncidientPriority ServiceIncidientPriority { get; set; }

        [Display(Name = "Уровень поддержки")]
        public ServiceIncidientLevel ServiceIncidientLevel { get; set; }

        [Display(Name = "Местоположение")]
        public ServiceIncidientLocation ServiceIncidientLocation { get; set; }
    }

    public class USR_REQ_IT_CTP_RequestTRU_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Представитель ФЭУ")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цех, участок")]
        public string Department { get; set; }

        public string ItemName1 { get; set; }
        public string ItemName2 { get; set; }
        public string ItemName3 { get; set; }
        public string ItemName4 { get; set; }
        public string ItemName5 { get; set; }
        public string ItemName6 { get; set; }
        public string ItemName7 { get; set; }
        public string ItemName8 { get; set; }
        public string ItemName9 { get; set; }
        public string ItemName10 { get; set; }
        public string ItemName11 { get; set; }
        public string ItemName12 { get; set; }
        public string ItemName13 { get; set; }
        public string ItemName14 { get; set; }
        public string ItemName15 { get; set; }
        public string ItemName16 { get; set; }
        public string ItemName17 { get; set; }

        public string Unit1 { get; set; }
        public string Unit2 { get; set; }
        public string Unit3 { get; set; }
        public string Unit4 { get; set; }
        public string Unit5 { get; set; }
        public string Unit6 { get; set; }
        public string Unit7 { get; set; }
        public string Unit8 { get; set; }
        public string Unit9 { get; set; }
        public string Unit10 { get; set; }
        public string Unit11 { get; set; }
        public string Unit12 { get; set; }
        public string Unit13 { get; set; }
        public string Unit14 { get; set; }
        public string Unit15 { get; set; }
        public string Unit16 { get; set; }
        public string Unit17 { get; set; }

        public string Qty1 { get; set; }
        public string Qty2 { get; set; }
        public string Qty3 { get; set; }
        public string Qty4 { get; set; }
        public string Qty5 { get; set; }
        public string Qty6 { get; set; }
        public string Qty7 { get; set; }
        public string Qty8 { get; set; }
        public string Qty9 { get; set; }
        public string Qty10 { get; set; }
        public string Qty11 { get; set; }
        public string Qty12 { get; set; }
        public string Qty13 { get; set; }
        public string Qty14 { get; set; }
        public string Qty15 { get; set; }
        public string Qty16 { get; set; }
        public string Qty17 { get; set; }

        public string Price1 { get; set; }
        public string Price2 { get; set; }
        public string Price3 { get; set; }
        public string Price4 { get; set; }
        public string Price5 { get; set; }
        public string Price6 { get; set; }
        public string Price7 { get; set; }
        public string Price8 { get; set; }
        public string Price9 { get; set; }
        public string Price10 { get; set; }
        public string Price11 { get; set; }
        public string Price12 { get; set; }
        public string Price13 { get; set; }
        public string Price14 { get; set; }
        public string Price15 { get; set; }
        public string Price16 { get; set; }
        public string Price17 { get; set; }

        public string Amount1 { get; set; }
        public string Amount2 { get; set; }
        public string Amount3 { get; set; }
        public string Amount4 { get; set; }
        public string Amount5 { get; set; }
        public string Amount6 { get; set; }
        public string Amount7 { get; set; }
        public string Amount8 { get; set; }
        public string Amount9 { get; set; }
        public string Amount10 { get; set; }
        public string Amount11 { get; set; }
        public string Amount12 { get; set; }
        public string Amount13 { get; set; }
        public string Amount14 { get; set; }
        public string Amount15 { get; set; }
        public string Amount16 { get; set; }
        public string Amount17 { get; set; }

        public string Location1 { get; set; }
        public string Location2 { get; set; }
        public string Location3 { get; set; }
        public string Location4 { get; set; }
        public string Location5 { get; set; }
        public string Location6 { get; set; }
        public string Location7 { get; set; }
        public string Location8 { get; set; }
        public string Location9 { get; set; }
        public string Location10 { get; set; }
        public string Location11 { get; set; }
        public string Location12 { get; set; }
        public string Location13 { get; set; }
        public string Location14 { get; set; }
        public string Location15 { get; set; }
        public string Location16 { get; set; }
        public string Location17 { get; set; }

        public Months Month1 { get; set; }
        public Months Month2 { get; set; }
        public Months Month3 { get; set; }
        public Months Month4 { get; set; }
        public Months Month5 { get; set; }
        public Months Month6 { get; set; }
        public Months Month7 { get; set; }
        public Months Month8 { get; set; }
        public Months Month9 { get; set; }
        public Months Month10 { get; set; }
        public Months Month11 { get; set; }
        public Months Month12 { get; set; }
        public Months Month13 { get; set; }
        public Months Month14 { get; set; }
        public Months Month15 { get; set; }
        public Months Month16 { get; set; }
        public Months Month17 { get; set; }

        public string Reason1 { get; set; }
        public string Reason2 { get; set; }
        public string Reason3 { get; set; }
        public string Reason4 { get; set; }
        public string Reason5 { get; set; }
        public string Reason6 { get; set; }
        public string Reason7 { get; set; }
        public string Reason8 { get; set; }
        public string Reason9 { get; set; }
        public string Reason10 { get; set; }
        public string Reason11 { get; set; }
        public string Reason12 { get; set; }
        public string Reason13 { get; set; }
        public string Reason14 { get; set; }
        public string Reason15 { get; set; }
        public string Reason16 { get; set; }
        public string Reason17 { get; set; }

        public string AccountBZ1 { get; set; }
        public string AccountBZ2 { get; set; }
        public string AccountBZ3 { get; set; }
        public string AccountBZ4 { get; set; }
        public string AccountBZ5 { get; set; }
        public string AccountBZ6 { get; set; }
        public string AccountBZ7 { get; set; }
        public string AccountBZ8 { get; set; }
        public string AccountBZ9 { get; set; }
        public string AccountBZ10 { get; set; }
        public string AccountBZ11 { get; set; }
        public string AccountBZ12 { get; set; }
        public string AccountBZ13 { get; set; }
        public string AccountBZ14 { get; set; }
        public string AccountBZ15 { get; set; }
        public string AccountBZ16 { get; set; }
        public string AccountBZ17 { get; set; }

        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Description3 { get; set; }
        public string Description4 { get; set; }
        public string Description5 { get; set; }
        public string Description6 { get; set; }
        public string Description7 { get; set; }
        public string Description8 { get; set; }
        public string Description9 { get; set; }
        public string Description10 { get; set; }
        public string Description11 { get; set; }
        public string Description12 { get; set; }
        public string Description13 { get; set; }
        public string Description14 { get; set; }
        public string Description15 { get; set; }
        public string Description16 { get; set; }
        public string Description17 { get; set; }
    }
    #endregion

    #region Заявки ИТ САиИБ
    public class USR_REQ_IT_CAP_RemoveSignLotus_View : BasicDocumentView
    {
        [Display(Name = "Что необходимо удалить")]
        public DeleteSignLotus DeleteSignLotus { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Автор документа")] 
        public string AuthorDocument { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование документа")]       
        public string DocumentName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "База")]
        public string DocumentBase { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата/время документа")]
        public DateTime? DocumentDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО согласовавшего")]  
        public string SignName { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование")]     
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_CreateSubscription_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [Display(Name = "Наименование группы рассылки")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string GroupName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Состав группы рассылки")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string GroupUsers { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_CreateNetworkFolder_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [Display(Name = "Наименование сетевой папки")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string NetworkFolder { get; set; }

        [Display(Name = "Полный путь к сетевой папке")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Path { get; set; }

        [Display(Name = "Права доступа ")]
        public AccessRightBasic AccessRight { get; set; }
    }

    public class USR_REQ_IT_CAP_DelegationExchServ_View : BasicDocumentView
    {
        [Display(Name = "С пользователя")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string FromUsers { get; set; }

        [Display(Name = "На пользователя")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string ToUsers { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Причина")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }

        [Display(Name = "Действует с")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "Действует по")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public DateTime? ToDate { get; set; }
    }

    public class USR_REQ_IT_CAP_AddUserSubscription_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [Display(Name = "RTDUET_GMO")]
        public bool RTDUET_GMO { get; set; }

        [Display(Name = "RTDUET_II_ST_DOIZM")]
        public bool RTDUET_II_ST_DOIZM { get; set; }

        [Display(Name = "RTDUET_KKD")]
        public bool RTDUET_KKD { get; set; }

        [Display(Name = "RTDUET_KSMD")]
        public bool RTDUET_KSMD { get; set; }

        [Display(Name = "RTDUET_OSPViHH")]
        public bool RTDUET_OSPViHH { get; set; }

        [Display(Name = "RTDUET_OTDiI")]
        public bool RTDUET_OTDiI { get; set; }

        [Display(Name = "RTDUET_RAZDFLOT")]
        public bool RTDUET_RAZDFLOT { get; set; }

        [Display(Name = "Guide ZIF")]
        public bool GuideZIF { get; set; }

        [Display(Name = "HR_Kokshetau")]
        public bool HR_Kokshetau { get; set; }

        [Display(Name = "Meeting to TB ZIF")]
        public bool MeetingTB_ZIF { get; set; }

        [Display(Name = "Dispetcher Group")]
        public bool DispetcherGroup { get; set; }

        [Display(Name = "Internal Audit Group")]
        public bool InternalAuditGroup { get; set; }

        [Display(Name = "Reservist 2014")]
        public bool Reservist2014 { get; set; }

        [Display(Name = "RT_Duet_Comments")]
        public bool RT_Duet_Comments { get; set; }

        [Display(Name = "dailydigest")]
        public bool dailydigest { get; set; }

        [Display(Name = "raigorodok")]
        public bool raigorodok { get; set; }

        [Display(Name = "PI System Clients")]
        public bool PISystemClients { get; set; }

        [Display(Name = "Manager")]
        public bool Manager { get; set; }

        [Display(Name = "Ispolnitelnye Direktora ATK")]
        public bool IspolnitelnyeDirektoraATK { get; set; }

        [Display(Name = "Otan")]
        public bool Otan { get; set; }

        [Display(Name = "Nachalniki Upravleniy ATK")]
        public bool NachalnikiUpravleniyATK { get; set; }

        [Display(Name = "Nachalniki Sluzhb ATK")]
        public bool NachalnikiSluzhbATK { get; set; }

        [Display(Name = "Svodka OTK")]
        public bool SvodkaOTK { get; set; }

        [Display(Name = "Operational Notification Kazzinc")]
        public bool OperationalNotificationKazzinc { get; set; }

        [Display(Name = "Dispatchers Daily Reports")]
        public bool DispatchersDailyReports { get; set; }

        [Display(Name = "ChiefAccountantATK")]
        public bool ChiefAccountantATK { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }

    public class USR_REQ_IT_CAP_ChangePassAD_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Причина")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }

        [Display(Name = "Контактный телефон")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string ContactPhone { get; set; }
    }

    public class USR_REQ_IT_CAP_ChangePassLotus_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Причина")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_UnlockUserAD_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Причина")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }

        [Display(Name = "Контактный телефон")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string ContactPhone { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessRightParagraf_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [Display(Name = "IP-адрес компьютера")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string IP { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessRightLotus_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [Display(Name = "БРД/Заявки")]
        public bool Request { get; set; }

        [Display(Name = "Обращение граждан")]
        public bool TreatmentPeople { get; set; }

        [Display(Name = "Материалы к совещаниям Техкомитета")]
        public bool MaterialMeetingsTehkomitet { get; set; }

        [Display(Name = "Согласование")]
        public bool Agreement { get; set; }

        [Display(Name = "Приказы")]
        public bool Orders { get; set; }

        [Display(Name = "Контроль")]
        public bool Control { get; set; }

        [Display(Name = "Командировки")]
        public bool Trips { get; set; }

        [Display(Name = "Корреспонденция")]
        public bool Correspondence { get; set; }

        [Display(Name = "Материалы к Совещаниям")]
        public bool MaterialsMeeting { get; set; }

        [Display(Name = "Материалы к Совещаниям Директората")]
        public bool MaterialsMeetingDirectorate { get; set; }

        [Display(Name = "Протокольные решения")]
        public bool ProtocolSolutions { get; set; }

        [Display(Name = "Протокольные решения Директората")]
        public bool ProtocolSolutionsDirectorate { get; set; }

        [Display(Name = "Протокольные решения по управлениям")]
        public bool ProtocolManagementSolution { get; set; }

        [Display(Name = "Протокольные решения Балансовой комиссии")]
        public bool ProtocolSolutionsCommission { get; set; }

        [Display(Name = "Согласование договоров СВД")]
        public bool AgreementsSVD { get; set; }

        [Display(Name = "Реестр договоров СВД")]
        public bool RegisterContractsSVD { get; set; }

        public AccessRightBasic AccessRight01 { get; set; }
        public AccessRightBasic AccessRight02 { get; set; }
        public AccessRightBasic AccessRight03 { get; set; }
        public AccessRightBasic AccessRight04 { get; set; }
        public AccessRightBasic AccessRight05 { get; set; }
        public AccessRightBasic AccessRight06 { get; set; }
        public AccessRightBasic AccessRight07 { get; set; }
        public AccessRightBasic AccessRight08 { get; set; }
        public AccessRightBasic AccessRight09 { get; set; }
        public AccessRightBasic AccessRight10 { get; set; }
        public AccessRightBasic AccessRight11 { get; set; }
        public AccessRightBasic AccessRight12 { get; set; }
        public AccessRightBasic AccessRight13 { get; set; }
        public AccessRightBasic AccessRight14 { get; set; }
        public AccessRightBasic AccessRight15 { get; set; }
        public AccessRightBasic AccessRight16 { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessSendLotus_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [Display(Name = "Все пользователи АТК")]
        public bool AllATKEmployees { get; set; }

        [Display(Name = "Пользователи Общества, исключая ИД.")]
        public bool ATKEmployees { get; set; }

        [Display(Name = "Вице-Президент и Президент")]
        public bool TopManagement { get; set; }

        [Display(Name = "Группа ИД")]
        public bool ATKDirectionGroup { get; set; }

        [Display(Name = "Вице-президенты")]
        public bool VicePresident { get; set; }

        [Display(Name = "Президент")]
        public bool President { get; set; }

        [Display(Name = "Группа начальники управления")]
        public bool ATKManagementChiefs { get; set; }

        [Display(Name = "Группа Начальников управлений и Начальников служб")]
        public bool ATKManagementGroup { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessRightFTP_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [Display(Name = "Название FTP")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string FTP { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessRightNetworkFolder_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [Display(Name = "Путь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Path { get; set; }

        [Display(Name = "Уровень доступа")]
        public AccessRightBasic AccessRight { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessRightInternet_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessRightInternetZIF_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessRightInternetBGP_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_DelegationDocflow_View : BasicDocumentView
    {
        [Display(Name = "С пользователя")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string FromUsers { get; set; }

        [Display(Name = "На пользователя")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string ToUsers { get; set; }

        [Display(Name = "С даты")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "До даты")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public DateTime? ToDate { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Наименование документа")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string DocumentName { get; set; }
    }

    public class USR_REQ_IT_CAP_CreateUserAD_View : BasicDocumentView
    {
        [Display(Name = "Active Directory")]
        public bool ActiveDirectory { get; set; }

        [Display(Name = "Exchange (Outlook)")]
        public bool Exchange { get; set; }

        [Display(Name = "ФИО")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата рождения")]
        public DateTime? BirthDay { get; set; }

        [Display(Name = "Должность")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Title { get; set; }

        [Display(Name = "Непосредственный руководитель")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [Display(Name = "Блок")]
        public BlocksATK BlocksATK { get; set; }

        [Display(Name = "Подразделение")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Department { get; set; }

        [Display(Name = "Действует до")]
        public DateTime? ToDate { get; set; }

        [Display(Name = "Сотовый номер")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Contact { get; set; }

        [Display(Name = "В штате, номер приказа")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Description { get; set; }
    }

    public class USR_REQ_IT_CAP_CreateUserADFreelance_View : BasicDocumentView
    {
        [Display(Name = "Active Directory")]
        public bool ActiveDirectory { get; set; }

        [Display(Name = "ФИО")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Name { get; set; }

        [Display(Name = "Дата рождения")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public DateTime? BirthDay { get; set; }

        [Display(Name = "Должность")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Title { get; set; }

        [Display(Name = "Непосредственный руководитель")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [Display(Name = "Блок")]
        public BlocksATK BlocksATK { get; set; }

        [Display(Name = "Подразделение")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Department { get; set; }

        [Display(Name = "Действует до")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public DateTime? ToDate { get; set; }

        [Display(Name = "Сотовый и внутренний номер")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Contact { get; set; }
    }

    public class USR_REQ_IT_CAP_RecoveryData_View : BasicDocumentView
    {
        [Display(Name = "Путь к файлу")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string PathFile { get; set; }

        [Display(Name = "Имя файла")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string FileName { get; set; }

        [Display(Name = "Дата последнего сохранения")]
        public DateTime? LastDate { get; set; }

        [Display(Name = "Контактный телефон")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Contact { get; set; }
    }

    public class USR_REQ_IT_CAP_ArchiveMail_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Description { get; set; }
    }

    public class USR_REQ_IT_CAP_CreateUserLync_View : BasicDocumentView
    {
        [Display(Name = "Имя пользователя")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [Display(Name = "Внутренний телефон")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string InternalPhone { get; set; }
    }

    public class USR_REQ_IT_CAP_CreateUserExchange_View : BasicDocumentView
    {
        [Display(Name = "Имя пользователя")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Name { get; set; }

        [Display(Name = "Электронная почта")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Email { get; set; }

        [Display(Name = "Название организации")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string CompanyName { get; set; }

        [Display(Name = "Должность")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Title { get; set; }

        [Display(Name = "Контактный телефон")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Contact { get; set; }
    }

    public class USR_REQ_IT_CAP_CreateUserAutograf_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [Display(Name = "Контактный телефон")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Contact { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_NoLinkInternet_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание проблемы")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Problem { get; set; }

        [Display(Name = "Адрес интернет сайта (опционально)")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Site { get; set; }
    }

    public class USR_REQ_IT_CAP_CapacityMail_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_HardSoftwareMaintenance_View : BasicDocumentView
    {
        [Display(Name = "Объект обслуживания")]
        public HardSoftwareMaintenance HardSoftwareMaintenance { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Description { get; set; }
    }

    public class USR_REQ_IT_CAP_ChangeRoute_View : BasicDocumentView
    {
        [DataType(DataType.MultilineText)]
        [Display(Name = "Наименование запроса/маршрута согласования")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Name { get; set; }

        [Display(Name = "Старый маршрут")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string OldRoute { get; set; }

        [Display(Name = "Новый маршрут")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string NewRoute { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }

        [Display(Name = "Контактный телефон")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Contact { get; set; }
    }

    public class USR_REQ_IT_CAP_CreateUserLotus_View : BasicDocumentView
    {
        [Display(Name = "ФИО")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Name { get; set; }

        [Display(Name = "Подразделение")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Department { get; set; }

        [Display(Name = "Должность")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }

    public class USR_REQ_IT_CAP_AddOrChangeTemplate_View : BasicDocumentView
    {
        [Display(Name = "Действие")]
        public AddOrChange ActionType { get; set; }

        [Display(Name = "Подразделение\\Путь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Path { get; set; }

        [Display(Name = "Наименование запроса")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Name { get; set; }

        [Display(Name = "Контактный телефон")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Contact { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }

    public class USR_REQ_IT_CAP_ChangeOrder_View : BasicDocumentView
    {
        [Display(Name = "№ Приказа")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string OrderNum { get; set; }

        [Display(Name = "Дата регистрации")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public DateTime? OrderDate { get; set; }

        [Display(Name = "Тема")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Subject { get; set; }

        [Display(Name = "Необходимые изменения")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Description { get; set; }

        [Display(Name = "Причина")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }

        [Display(Name = "Контактный телефон")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Contact { get; set; }
    }

    public class USR_REQ_IT_CAP_ChangeOrderWage_View : BasicDocumentView 
    {
        [Display(Name = "№ Приказа")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string OrderNum { get; set; }

        [Display(Name = "Дата регистрации")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public DateTime? OrderDate { get; set; }

        [Display(Name = "Тема")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Subject { get; set; }

        [Display(Name = "Необходимые изменения")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Description { get; set; }

        [Display(Name = "Причина")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }

        [Display(Name = "Контактный телефон")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Contact { get; set; }
    }

    public class USR_REQ_IT_CAP_RequestForITWeekend_View : BasicDocumentView
    {
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование")]
        public string Reason { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Управление/служба(отдел)")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дни выхода")]
        public string Period { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник (ФИО)")]
        public string UserChooseManual1 { get; set; }
    }
    #endregion

    #region Зиф
    public class USR_REQ_ZIF_RequestForFuel_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }
    }

    public class USR_REQ_ZIF_RequestForSIZ_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }
    }

    public class USR_REQ_ZIF_RequestForItems_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }
    }

    public class USR_REQ_ZIF_RequestForItemsMech_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }
    }

    public class USR_REQ_ZIF_RequestForItemsEnerg_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }
    }

    public class USR_REQ_ZIF_RequestForItemsASYTP_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }
    }

    public class USR_REQ_ZIF_RequestForCarpenterWork_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактное лицо")]
        public string Contact { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование необходимости работ, описание")]
        public string Reason { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наличие у заказчика необходимого материала для ремонта/изготовления")]
        public string Available { get; set; }
    }

    public class USR_REQ_ZIF_RequestForCreatingItemsJDE_View : BasicDocumentView
    {

    }
    #endregion

    #region ОКС

    public class USR_REQ_OKS_RequestForTranslate_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контакты (имя, телефон, электронная почта)")]
        public string Contact { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель перевода")]
        public string Purpose { get; set; }

        [Display(Name = "Направление перевода")]
        public TranslateDirection Direction { get; set; }

        [Display(Name = "Количество страниц в исходном документе")]
        public int CountPage { get; set; }

        [Display(Name = "Приоритет")]
        public ServiceIncidientPriority Priority { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование приоритетности")]
        public string ExplanationPriority { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Инициатор")]
        public string Users { get; set; }
    }

    public class USR_REQ_OKS_RequestForTranslateKAZ_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контакты (имя, телефон, электронная почта)")]
        public string Contact { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель перевода")]
        public string Purpose { get; set; }

        [Display(Name = "Направление перевода")]
        public TranslateDirectionKAZ Direction { get; set; }

        [Display(Name = "Количество страниц в исходном документе")]
        public int CountPage { get; set; }

        [Display(Name = "Приоритет")]
        public ServiceIncidientPriority Priority { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование приоритетности")]
        public string ExplanationPriority { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Инициатор")]
        public string Users { get; set; }
    }

    public class USR_REQ_OKS_RequestForPrintBlank_View : BasicDocumentView
    {
        [Display(Name = "Номер и дата приказа")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string DateAndNumber { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Причина запроса")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Purpose { get; set; }
    }

    public class USR_REQ_OKS_RequestForArchive_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Данные по запрашиваемому документу")]
        public string DataDoument { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Период")]
        public string Period { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина запроса")]
        public string Reason { get; set; }
    }

    public class USR_REQ_OKS_RequestForVisa_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пользователь")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Страна следования")]
        public string Country { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "С даты")]
        public DateTime? FromDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "До даты")]
        public DateTime? ToDate { get; set; }

    }
    public class USR_REQ_OKS_RequestForTicket_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Фамилия, Имя (на русском)")]
        public string NameRus { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Фамилия, Имя (на английском)")]
        public string NameEng { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата рождения")]
        public DateTime? DateBirth { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "№уд.личности/паспорта")]
        public string PassportNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Срок действия документа")]
        public string Validity { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Маршрут")]
        public string Route { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public string Date { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Время вылета")]
        public string TimeDeparture { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Класс")]
        public string Category { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель поездки")]
        public string Reason { get; set; }
        [Display(Name = "Оплата")]
        public PayType PayType { get; set; }
    }
    public class USR_REQ_OKS_RequestForTicketPermission_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Фамилия, Имя (на русском)")]
        public string NameRus { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Фамилия, Имя (на английском)")]
        public string NameEng { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата рождения")]
        public DateTime? DateBirth { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "№уд.личности/паспорта")]
        public string PassportNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Срок действия документа")]
        public string Validity { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Маршрут")]
        public string Route { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public string Date { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Время вылета")]
        public string TimeDeparture { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Класс")]
        public string Category { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }
        [Display(Name = "Оплата")]
        public PayType PayType { get; set; }
    }
    #endregion

    #region РОГР

    public class USR_REQ_ROGR_RequestForMiningVehicle_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Users", ResourceType = typeof(CustomRes.Custom))]
        public string Users { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата/время")]
        public DateTime? Date { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование")]
        public string Reason { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Объем выполняемых работ")]
        public string Volume { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Ответственное лицо выдающее наряд/задание")]
        public string Person { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Вид горной техники")]
        public string VehicleType { get; set; }
    }

    #endregion

    #region СГЭ ЗИФ

    public class USR_REQ_SGMZIF_RequestForRepair_View : BasicDocumentView
    {
        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактное лицо")]
        public string Contact { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактный телефон")]
        public string Telephone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование оборудования, позиция по схеме")]
        public string Equipment { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Описание неисправности")]
        public string DescriptionFail { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Тип двигателя")]
        public string TypeEngine { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номинальная мощность (кВт)")]
        public string PowerEngine { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номинальная частота вращения (об/мин)")]
        public string Speed { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номинальный ток")]
        public string Current { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номинальное напряжение (В)")]
        public string Voltage { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Схема соединения")]
        public string SchemeConnection { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Степерь защиты (IP)")]
        public string RateDefense { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Монтажное исполнение: IM (крепление с помощью фланца и т.д.)")]
        public string InstallPerformance { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Заводской (серийный) номер")]
        public string SerialNumber { get; set; }
    }

    #endregion
        
    #region ЮУ

    public class USR_REQ_JU_RequestForAssurance_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО Заказчика")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение-инициатор")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Полное наименование документа для нотариального заверения")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата заверения документа не менее 2-х дней с момента получения заявки ЮУ")]
        public DateTime? DateAssurance { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель заверения документа")]
        public string AimAssurance { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Куда и кому предоставляется документ, с указанием адреса и наименования юр. Лица либо физ. Лица")]
        public string WhereAndWhom { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Количество экземпляров")]
        public int CountExamples { get; set; }
    }

    public class USR_REQ_JU_RequestForProxyDoc_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Внутрений телефон Заказчика")]
        public string Telephone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение-инициатор")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Полное наименование юридического или физического лица, перед которым предоставляются интересы Товарищиства")]
        public string JuName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Полное и доступное описание полномочий лица, которому предоставляется право предоствления интересов Товарищества")]
        public string PhyName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата оформления доверености")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Срок действия доверености")]
        public string Term { get; set; }
    }

    public class USR_REQ_JU_RequestForArchiveContract_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО Заказчика")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение-инициатор")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер договора")]
        public string NumberContract { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата договора")]
        public DateTime? DateContract { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование контрагента")]
        public string NameClient { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель предоставления договора")]
        public string Purpose { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Вид документа для предоставления")]
        public TypeJUDocument TypeDocument { get; set; }
    }

    public class USR_REQ_JU_RequestForApproveDoc_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО Заказчика")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение-инициатор")]
        public string Department { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Полное наименование документа для нотариального заверения")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата заверения документа не менее 2-х дней с момента получения заявки ЮУ")]
        public DateTime? DateApprove { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель заверения документа")]
        public string Purpose { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Куда и кому предоставляется документ, с указанием адреса и наименования юр. Лица либо физ. Лица")]
        public string Destination { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Количество экземпляров")]
        public int CountExamples { get; set; }
    }

    public class USR_REQ_JU_RequestForNPA_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО Заказчика")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение-инициатор")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "№ Нормативно-правового акта (далее НПА)")]
        public string NormalAct { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата НПА")]
        public DateTime? NPADate { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Полное наименование НПА")]
        public string FullName { get; set; }
    }

    public class USR_REQ_JU_RequestForExplanationNormalAct_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО Заказчика")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение-инициатор")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "№")]
        public string NumberDoc { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Полное наименование")]
        public string FullName { get; set; }

        [Display(Name = "Собственное мнение заявителя по разъяснению НПА")]
        public string OwnMind { get; set; }
    }

    public class USR_REQ_JU_RequestForExpertise_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }
    }
    #endregion

    #region ФЭУ

    public class USR_REQ_FEU_RequestForFinExpertise_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }
    }

    public class USR_REQ_FEU_RequestForCorrectCalendar_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование поставщика")]
        public string Provider { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение-инициатор")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Основание платежа № и дата договора")]
        public string Foundation { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "№ и дата счет-фактуры")]
        public string NumberDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Назначение платежа")]
        public string Appointment { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сумма платежа")]
        public string Amount { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Валюта платежа")]
        public string Currency { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Статья бюджета")]
        public string Article { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина корректировки (полное обоснование)")]
        public string Reason { get; set; }
    }

    #endregion

    #region УЭ

    public class USR_REQ_UE_RequestForOutputElectricalEqu_View : BasicDocumentView
    {
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Перечень выводимого в ремонт оборудования")]
        public string Equipment { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Какие потребители будут затронуты при переключениях")]
        public string Consumers { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Перечень отключаемых потребителей")]
        public string ConsumersOff { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Объем работ")]
        public string Value { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата и время начала работ")]
        public DateTime? BeginDate { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата и время окончания работ")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Время аварийной готовности")]
        public string AccidentTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отвественный за выполнение работ")]
        public string Responsible { get; set; }
    }

    public class USR_REQ_UE_RequestForDismantling_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата/время проведения буровзравных работ")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Описание и объем работ к выполнению")]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наличие техники")]
        public string AvailableEqu { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактный телефон")]
        public string Telephone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Лицо, ответственное за подготовку рабочего места")]
        public string Person { get; set; }
    }

    public class USR_REQ_UE_RequestForForecastWater_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование трубопровода")]
        public PipeName Pipe { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Объем")]
        public string Value { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "С даты")]
        public DateTime? BeginDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "По дату")]
        public DateTime? EndDate { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование необходимости")]
        public string Explanation { get; set; }

    }

    public class USR_REQ_UE_RequestForElectricRepairs_View : BasicDocumentView
    {
        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата возникновения неисправности")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактное лицо")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактный телефон")]
        public string Telephone { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование оборудования")]
        public string NameEqu { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Описание неисправности")]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наличие у заказчика необходимого материала для ремонта")]
        public string Availability { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Лицо, ответственные за подготовку рабочего места")]
        public string Person { get; set; }
    }

    #endregion

    #region УСХ

    public class USR_REQ_USH_RequestForReclassification_View : BasicDocumentView
    {
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование")]
        public string Explanation { get; set; }
    }

    #endregion

    #region УПБ

    public class USR_REQ_UBP_RequestForExportWastes_View : BasicDocumentView
    {
        public bool Number1 { get; set; }
        public bool Number2 { get; set; }
        public bool Number3 { get; set; }
        public bool Number4 { get; set; }
        public bool Number5 { get; set; }
        public bool Number6 { get; set; }
        public bool Number7 { get; set; }
        public bool Number8 { get; set; }
        public bool Number9 { get; set; }
        public bool Number10 { get; set; }
        public bool Number11 { get; set; }
        public bool Number12 { get; set; }
        public bool Number13 { get; set; }
        public bool Number14 { get; set; }
        public bool Number15 { get; set; }
        public bool Number16 { get; set; }
        public bool Number17 { get; set; }
        public bool Number18 { get; set; }
        public bool Number19 { get; set; }
        public bool Number20 { get; set; }
        public bool Number21 { get; set; }
        public bool Number22 { get; set; }
        public bool Number23 { get; set; }
        public bool Number24 { get; set; }
        public bool Number25 { get; set; }
        public bool Number26 { get; set; }
        public bool Number27 { get; set; }
        public bool Number28 { get; set; }
        public bool Number29 { get; set; }
        public bool Number30 { get; set; }
        public bool Number31 { get; set; }
        public bool Number94 { get; set; }
        public bool Number97 { get; set; }
        public bool Number100 { get; set; }

        public string Number32 { get; set; }
        public string Number33 { get; set; }
        public string Number34 { get; set; }
        public string Number35 { get; set; }
        public string Number36 { get; set; }
        public string Number37 { get; set; }
        public string Number38 { get; set; }
        public string Number39 { get; set; }
        public string Number40 { get; set; }
        public string Number41 { get; set; }
        public string Number42 { get; set; }
        public string Number43 { get; set; }
        public string Number44 { get; set; }
        public string Number45 { get; set; }
        public string Number46 { get; set; }
        public string Number47 { get; set; }
        public string Number48 { get; set; }
        public string Number49 { get; set; }
        public string Number50 { get; set; }
        public string Number51 { get; set; }
        public string Number52 { get; set; }
        public string Number53 { get; set; }
        public string Number54 { get; set; }
        public string Number55 { get; set; }
        public string Number56 { get; set; }
        public string Number57 { get; set; }
        public string Number58 { get; set; }
        public string Number59 { get; set; }
        public string Number60 { get; set; }
        public string Number61 { get; set; }
        public string Number62 { get; set; }
        public string Number95 { get; set; }
        public string Number98 { get; set; }
        public string Number101 { get; set; }

        public string Number63 { get; set; }
        public string Number64 { get; set; }
        public string Number65 { get; set; }
        public string Number66 { get; set; }
        public string Number67 { get; set; }
        public string Number68 { get; set; }
        public string Number69 { get; set; }
        public string Number70 { get; set; }
        public string Number71 { get; set; }
        public string Number72 { get; set; }
        public string Number73 { get; set; }
        public string Number74 { get; set; }
        public string Number75 { get; set; }
        public string Number76 { get; set; }
        public string Number77 { get; set; }
        public string Number78 { get; set; }
        public string Number79 { get; set; }
        public string Number80 { get; set; }
        public string Number81 { get; set; }
        public string Number82 { get; set; }
        public string Number83 { get; set; }
        public string Number84 { get; set; }
        public string Number85 { get; set; }
        public string Number86 { get; set; }
        public string Number87 { get; set; }
        public string Number88 { get; set; }
        public string Number89 { get; set; }
        public string Number90 { get; set; }
        public string Number91 { get; set; }
        public string Number92 { get; set; }
        public string Number93 { get; set; }
        public string Number96 { get; set; }
        public string Number99 { get; set; }
        public string Number102 { get; set; }
    }

    public class USR_REQ_UBP_RequestForGetConclusion_View : BasicDocumentView
    {
        [Display(Name = "Подразделение")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Department { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование")]
        public string Explanation { get; set; }
    }

    public class USR_REQ_UBP_RequestForInstructionBIOT_View : BasicDocumentView
    {
        [Display(Name = "Подразделение")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Тех. Руководитель")]
        public string UserChooseManual1 { get; set; }
    }
    #endregion

    #region КД

    public class USR_REQ_KD_RequestForCompetitonProc_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование закупа")]
        public string NamePurchase { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Статья бюджета")]
        public string BudgetArticle { get; set; }

        [Display(Name = "Проект")]
        public string Project { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Общая сумма, выделенная для закупки (в тенге, без учета НДС)")]
        public string Amount { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Условия оплаты")]
        public string Circumstance { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Место поставки товара")]
        public string Destination { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Исполнитель")]
        public string UserChooseManual1 { get; set; }
    }

    public class USR_REQ_KD_RequestForCompetitonProcUZL_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование закупа")]
        public string NamePurchase { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Статья бюджета")]
        public string BudgetArticle { get; set; }

        [Display(Name = "Проект")]
        public string Project { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Общая сумма, выделенная для закупки (в тенге, без учета НДС)")]
        public string Amount { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Условия оплаты")]
        public string Circumstance { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Место поставки товара")]
        public string Destination { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Исполнитель")]
        public string UserChooseManual1 { get; set; }
    }

    public class USR_REQ_KD_RequestForCompetitonProcServices_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование закупа")]
        public string NamePurchase { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Статья бюджета")]
        public string BudgetArticle { get; set; }

        [Display(Name = "Проект")]
        public string Project { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Общая сумма, выделенная для закупки (в тенге, без учета НДС)")]
        public string Amount { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Условия оплаты")]
        public string Circumstance { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Место оказания/выполнения услуг/работ")]
        public string Destination { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Исполнитель")]
        public string UserChooseManual1 { get; set; }
    }

    public class USR_REQ_KD_RequestForCompetitonProcServicesBGP_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование закупа")]
        public string NamePurchase { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Статья бюджета")]
        public string BudgetArticle { get; set; }

        [Display(Name = "Проект")]
        public string Project { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Общая сумма, выделенная для закупки (в тенге, без учета НДС)")]
        public string Amount { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Условия оплаты")]
        public string Circumstance { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Место оказания/выполнения услуг/работ")]
        public string Destination { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Исполнитель")]
        public string UserChooseManual1 { get; set; }
    }
    #endregion

    #region СК

    public class USR_REQ_SK_RequestForRegContactNonresident_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Инициатор Ф.И.О.")]
        public string Users { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата создания заявки")]
        public DateTime? OrderDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение инициатор")]
        public string Department { get; set; }

        [Display(Name = "Вид контрагента")]
        public ContragentType ContragentType { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование клиента\\поставщика")]
        public string ContragentName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер и дата основного контракта")]
        public string DataMainContract { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер и дата приложения\\доп.соглащения")]
        public string DataAdditionalContract { get; set; }

    }

    #endregion

    #region УБ

    public class USR_REQ_UB_RequestForImportTMCZIF_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование ТМЦ")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Количество")]
        public string Count { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт отправки")]
        public string DeparturePlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string DestinationPlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Марка автомобиля, гос.номер")]
        public string CarBrand { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО водителя, сопровождающих лиц")]
        public string NamesPeople { get; set; }
    
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Aim { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Display(Name = "Пост №1 (Блокпост АТК)")]
        public bool Post1 { get; set; }
        [Display(Name = "Пост №2 (Проходная ЗИФ)")]
        public bool Post2 { get; set; }
        [Display(Name = "Пост №3 (КПП Ж/Д ЗИФ)")]
        public bool Post3 { get; set; }
        [Display(Name = "Пост №4 (Пристройка КИ ЗИФ)")]
        public bool Post4 { get; set; }
        [Display(Name = "Пост №6 (КПП Транспортный АТК)")]
        public bool Post5 { get; set; }
        [Display(Name = "Пост №11(КПП Транспортный ЗИФ)")]
        public bool Post6 { get; set; }
        [Display(Name = "Пост №28(Реагентное отделение)")]
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForImportORZZIF_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование ТМЦ")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Количество")]
        public string Count { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт отправки")]
        public string DeparturePlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string DestinationPlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Марка автомобиля, гос.номер")]
        public string CarBrand { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО водителя, сопровождающих лиц")]
        public string NamesPeople { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Aim { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Display(Name = "Пост №1 (Блокпост АТК)")]
        public bool Post1 { get; set; }
        [Display(Name = "Пост №2 (Проходная ЗИФ)")]
        public bool Post2 { get; set; }
        [Display(Name = "Пост №3 (КПП Ж/Д ЗИФ)")]
        public bool Post3 { get; set; }
        [Display(Name = "Пост №4 (Пристройка КИ ЗИФ)")]
        public bool Post4 { get; set; }
        [Display(Name = "Пост №6 (КПП Транспортный АТК)")]
        public bool Post5 { get; set; }
        [Display(Name = "Пост №11(КПП Транспортный ЗИФ)")]
        public bool Post6 { get; set; }
        [Display(Name = "Пост №28(Реагентное отделение)")]
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForImportTMCNoneZIF_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование ТМЦ")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Количество")]
        public string Count { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт отправки")]
        public string DeparturePlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string DestinationPlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Марка автомобиля, гос.номер")]
        public string CarBrand { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО водителя, сопровождающих лиц")]
        public string NamesPeople { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Aim { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Display(Name = "Пост №1 (Блокпост АТК)")]
        public bool Post1 { get; set; }
        [Display(Name = "Пост №2 (Проходная ЗИФ)")]
        public bool Post2 { get; set; }
        [Display(Name = "Пост №3 (КПП Ж/Д ЗИФ)")]
        public bool Post3 { get; set; }
        [Display(Name = "Пост №4 (Пристройка КИ ЗИФ)")]
        public bool Post4 { get; set; }
        [Display(Name = "Пост №6 (КПП Транспортный АТК)")]
        public bool Post5 { get; set; }
        [Display(Name = "Пост №11(КПП Транспортный ЗИФ)")]
        public bool Post6 { get; set; }
        [Display(Name = "Пост №28(Реагентное отделение)")]
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForImportTMCUZL_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование ТМЦ")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Количество")]
        public string Count { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт отправки")]
        public string DeparturePlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string DestinationPlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Марка автомобиля, гос.номер")]
        public string CarBrand { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО водителя, сопровождающих лиц")]
        public string NamesPeople { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Aim { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Display(Name = "Пост №1 (Блокпост АТК)")]
        public bool Post1 { get; set; }
        [Display(Name = "Пост №2 (Проходная ЗИФ)")]
        public bool Post2 { get; set; }
        [Display(Name = "Пост №3 (КПП Ж/Д ЗИФ)")]
        public bool Post3 { get; set; }
        [Display(Name = "Пост №4 (Пристройка КИ ЗИФ)")]
        public bool Post4 { get; set; }
        [Display(Name = "Пост №6 (КПП Транспортный АТК)")]
        public bool Post5 { get; set; }
        [Display(Name = "Пост №11(КПП Транспортный ЗИФ)")]
        public bool Post6 { get; set; }
        [Display(Name = "Пост №28(Реагентное отделение)")]
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForInOutNotebook_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование организации")]
        public string Organization { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Модель ноутбука")]
        public string Model { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО владельца")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Объект доступа")]
        public ObjectAccess ObjectAccess { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Aim { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "С даты")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "До даты")]
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }
    }

    public class USR_REQ_UB_RequestForCarAccess_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО(полностью)")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "С даты")]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "До даты")]
        public DateTime? ToDate { get; set; }
    }

    public class USR_REQ_UB_RequestForExportAsset_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование ОС")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Инвентарный номер ОС")]
        public string Number { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Количество")]
        public string Count { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "МОЛ")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт отправки")]
        public string DeparturePlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string DestinationPlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Марка автомобиля, гос.номер")]
        public string CarBrand { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО водителя, сопровождающих лиц")]
        public string NamesPeople { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Aim { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Ожидаемая дата возврата собственных ОС")]
        public string DateReturnOC { get; set; }

        [Display(Name = "Пост №1 (Блокпост АТК)")]
        public bool Post1 { get; set; }
        [Display(Name = "Пост №2 (Проходная ЗИФ)")]
        public bool Post2 { get; set; }
        [Display(Name = "Пост №3 (КПП Ж/Д ЗИФ)")]
        public bool Post3 { get; set; }
        [Display(Name = "Пост №4 (Пристройка КИ ЗИФ)")]
        public bool Post4 { get; set; }
        [Display(Name = "Пост №6 (КПП Транспортный АТК)")]
        public bool Post5 { get; set; }
        [Display(Name = "Пост №11(КПП Транспортный ЗИФ)")]
        public bool Post6 { get; set; }
        [Display(Name = "Пост №28(Реагентное отделение)")]
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForExportZIFOre_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Количество")]
        public string Count { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string DestinationPlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Марка автомобиля, гос.номер")]
        public string CarBrand { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО водителя, сопровождающих лиц")]
        public string NamesPeople { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Aim { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Display(Name = "Пост №1 (Блокпост АТК)")]
        public bool Post1 { get; set; }
        [Display(Name = "Пост №2 (Проходная ЗИФ)")]
        public bool Post2 { get; set; }
        [Display(Name = "Пост №3 (КПП Ж/Д ЗИФ)")]
        public bool Post3 { get; set; }
        [Display(Name = "Пост №4 (Пристройка КИ ЗИФ)")]
        public bool Post4 { get; set; }
        [Display(Name = "Пост №6 (КПП Транспортный АТК)")]
        public bool Post5 { get; set; }
        [Display(Name = "Пост №11(КПП Транспортный ЗИФ)")]
        public bool Post6 { get; set; }
        [Display(Name = "Пост №28(Реагентное отделение)")]
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForExportOSPVHZIF_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование ТМЦ")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Количество")]
        public string Count { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт отправки")]
        public string DeparturePlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string DestinationPlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Марка автомобиля, гос.номер")]
        public string CarBrand { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО водителя, сопровождающих лиц")]
        public string NamesPeople { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Aim { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Display(Name = "Пост №1 (Блокпост АТК)")]
        public bool Post1 { get; set; }
        [Display(Name = "Пост №2 (Проходная ЗИФ)")]
        public bool Post2 { get; set; }
        [Display(Name = "Пост №3 (КПП Ж/Д ЗИФ)")]
        public bool Post3 { get; set; }
        [Display(Name = "Пост №4 (Пристройка КИ ЗИФ)")]
        public bool Post4 { get; set; }
        [Display(Name = "Пост №6 (КПП Транспортный АТК)")]
        public bool Post5 { get; set; }
        [Display(Name = "Пост №11(КПП Транспортный ЗИФ)")]
        public bool Post6 { get; set; }
        [Display(Name = "Пост №28(Реагентное отделение)")]
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForExportItems_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование ТМЦ")]
        public string ItemName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номенклатурный номер ТМЦ")]
        public string ItemNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт отправки")]
        public string DeparturePlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Склад")]
        public Warehouse Warehouse { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Количество")]
        public string Count { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Руководитель МОЛ-а указанного склада")]
        public string NamesMOL { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string DestinationPlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Марка автомобиля, гос.номер")]
        public string CarBrand { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО водителя, сопровождающих лиц")]
        public string NamesPeople { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Aim { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Ожидаемая дата возврата собственных ТМЦ")]
        public string DateReturnOC { get; set; }

        [Display(Name = "Пост №1 (Блокпост АТК)")]
        public bool Post1 { get; set; }
        [Display(Name = "Пост №2 (Проходная ЗИФ)")]
        public bool Post2 { get; set; }
        [Display(Name = "Пост №3 (КПП Ж/Д ЗИФ)")]
        public bool Post3 { get; set; }
        [Display(Name = "Пост №4 (Пристройка КИ ЗИФ)")]
        public bool Post4 { get; set; }
        [Display(Name = "Пост №6 (КПП Транспортный АТК)")]
        public bool Post5 { get; set; }
        [Display(Name = "Пост №11(КПП Транспортный ЗИФ)")]
        public bool Post6 { get; set; }
        [Display(Name = "Пост №28(Реагентное отделение)")]
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForHU_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование ТМЦ")]
        public string ItemName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт отправки")]
        public string DeparturePlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Склад")]
        public Warehouse Warehouse { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Количество")]
        public string Count { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Руководитель МОЛ-а указанного склада")]
        public string NamesMOL { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string DestinationPlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Марка автомобиля, гос.номер")]
        public string CarBrand { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО водителя, сопровождающих лиц")]
        public string NamesPeople { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Aim { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Ожидаемая дата возврата собственных ТМЦ")]
        public string DateReturnOC { get; set; }

        [Display(Name = "Пост №1 (Блокпост АТК)")]
        public bool Post1 { get; set; }
        [Display(Name = "Пост №2 (Проходная ЗИФ)")]
        public bool Post2 { get; set; }
        [Display(Name = "Пост №3 (КПП Ж/Д ЗИФ)")]
        public bool Post3 { get; set; }
        [Display(Name = "Пост №4 (Пристройка КИ ЗИФ)")]
        public bool Post4 { get; set; }
        [Display(Name = "Пост №6 (КПП Транспортный АТК)")]
        public bool Post5 { get; set; }
        [Display(Name = "Пост №11(КПП Транспортный ЗИФ)")]
        public bool Post6 { get; set; }
        [Display(Name = "Пост №28(Реагентное отделение)")]
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForMovementItems_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование ТМЦ")]
        public string ItemName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номенклатурный номер ТМЦ")]
        public string ItemNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт отправки")]
        public string DeparturePlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Склад")]
        public Warehouse Warehouse { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Количество")]
        public string Count { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Руководитель МОЛ-а указанного склада")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string DestinationPlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Марка автомобиля, гос.номер")]
        public string CarBrand { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО водителя, сопровождающих лиц")]
        public string NamesPeople { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Aim { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Ожидаемая дата возврата собственных ТМЦ")]
        public string DateReturnOC { get; set; }

        [Display(Name = "Пост №1 (Блокпост АТК)")]
        public bool Post1 { get; set; }
        [Display(Name = "Пост №2 (Проходная ЗИФ)")]
        public bool Post2 { get; set; }
        [Display(Name = "Пост №3 (КПП Ж/Д ЗИФ)")]
        public bool Post3 { get; set; }
        [Display(Name = "Пост №4 (Пристройка КИ ЗИФ)")]
        public bool Post4 { get; set; }
        [Display(Name = "Пост №6 (КПП Транспортный АТК)")]
        public bool Post5 { get; set; }
        [Display(Name = "Пост №11(КПП Транспортный ЗИФ)")]
        public bool Post6 { get; set; }
        [Display(Name = "Пост №28(Реагентное отделение)")]
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForMovementAssets_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование ОС")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Инвентарный номер ОС")]
        public string Number { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Количество")]
        public string Count { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "МОЛ")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт отправки")]
        public string DeparturePlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string DestinationPlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Марка автомобиля, гос.номер")]
        public string CarBrand { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО водителя, сопровождающих лиц")]
        public string NamesPeople { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Aim { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Ожидаемая дата возврата собственных ОС")]
        public string DateReturnOC { get; set; }

        [Display(Name = "Пост №1 (Блокпост АТК)")]
        public bool Post1 { get; set; }
        [Display(Name = "Пост №2 (Проходная ЗИФ)")]
        public bool Post2 { get; set; }
        [Display(Name = "Пост №3 (КПП Ж/Д ЗИФ)")]
        public bool Post3 { get; set; }
        [Display(Name = "Пост №4 (Пристройка КИ ЗИФ)")]
        public bool Post4 { get; set; }
        [Display(Name = "Пост №6 (КПП Транспортный АТК)")]
        public bool Post5 { get; set; }
        [Display(Name = "Пост №11(КПП Транспортный ЗИФ)")]
        public bool Post6 { get; set; }
        [Display(Name = "Пост №28(Реагентное отделение)")]
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForTemporaryORZ_View : BasicDocumentView
    {
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Aim { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО(полностью)")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Организация")]
        public string Company { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "С даты")]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "До даты")]
        public DateTime? ToDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Время нахождения в ОРЗ")]
        public string Time { get; set; }
    }

    public class USR_REQ_UB_RequestForTemporaryAccess_View : BasicDocumentView
    {
        [Display(Name = "Объект доступа")]
        public ObjectAccess ObjectAccess { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Aim { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО(полностью)")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Организация")]
        public string Company { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "С даты")]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "До даты")]
        public DateTime? ToDate { get; set; } 
    }

    public class USR_REQ_UB_RequestForExportItemFromZIF_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование ТМЦ")]
        public string ItemName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Количество")]
        public string ItemNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт отправки")]
        public string DeparturePlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string DestinationPlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Марка автомобиля, гос.номер")]
        public string CarBrand { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО водителя, сопровождающих лиц")]
        public string NamesPeople { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Aim { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Display(Name = "Пост №1 (Блокпост АТК)")]
        public bool Post1 { get; set; }
        [Display(Name = "Пост №2 (Проходная ЗИФ)")]
        public bool Post2 { get; set; }
        [Display(Name = "Пост №3 (КПП Ж/Д ЗИФ)")]
        public bool Post3 { get; set; }
        [Display(Name = "Пост №4 (Пристройка КИ ЗИФ)")]
        public bool Post4 { get; set; }
        [Display(Name = "Пост №6 (КПП Транспортный АТК)")]
        public bool Post5 { get; set; }
        [Display(Name = "Пост №11(КПП Транспортный ЗИФ)")]
        public bool Post6 { get; set; }
        [Display(Name = "Пост №28(Реагентное отделение)")]
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForExportItemFromORZ_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование ТМЦ")]
        public string ItemName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номенклатурный номер ТМЦ")]
        public string ItemNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт отправки")]
        public string DeparturePlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string DestinationPlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Марка автомобиля, гос.номер")]
        public string CarBrand { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО водителя, сопровождающих лиц")]
        public string NamesPeople { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Aim { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Display(Name = "Пост №1 (Блокпост АТК)")]
        public bool Post1 { get; set; }
        [Display(Name = "Пост №2 (Проходная ЗИФ)")]
        public bool Post2 { get; set; }
        [Display(Name = "Пост №3 (КПП Ж/Д ЗИФ)")]
        public bool Post3 { get; set; }
        [Display(Name = "Пост №4 (Пристройка КИ ЗИФ)")]
        public bool Post4 { get; set; }
        [Display(Name = "Пост №6 (КПП Транспортный АТК)")]
        public bool Post5 { get; set; }
        [Display(Name = "Пост №11(КПП Транспортный ЗИФ)")]
        public bool Post6 { get; set; }
        [Display(Name = "Пост №28(Реагентное отделение)")]
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForPhotoRealization_View : BasicDocumentView
    {
        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Начальная дата проведения")]
        public DateTime? FromPhotoDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Конечная дата проведения")]
        public DateTime? ToPhotoDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Тип")]
        public PhotoType PhotoType { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Участок/цех проведения")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Место проведения")]
        public string Place { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Объект фото/видеосъемки")]
        public string Target { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Purpose { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО производителя фото/видеосъемки, должность, участок")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Оборудование для проведения фото/видеосъемки")]
        public string Equipment { get; set; }
    }
    #endregion

    #region УБУиО

    public class USR_REQ_UBUO_RequestForInspectionPropertyItems_View : BasicDocumentView
    {

    }

    public class USR_REQ_UBUO_RequestForInspectionPropertyAssets_View : BasicDocumentView
    {

    }

    public class USR_REQ_UBUO_RequestForClearenceLetter_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пользователь")]
        public string User { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Табельный номер")]
        public string Number { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение-инициатор")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Полное наименование юридического или физического лица, перед которым представляються интересы Товарищества")]
        public string FullName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "№ и дата Договора")]
        public string Contract { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование ТМЦ/ОС")]
        public string NameItem { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Количество")]
        public string Count { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата оформления доверености")]
        public DateTime? LetterDate { get; set; }

        [Display(Name = "Сумма доверенности с учетом НДС/без НДС")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Amount { get; set; }
    }

    public class USR_REQ_UBUO_RequestForReferenceTax_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Заказчик")]
        public string Customer { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Для предъявления")]
        public string ForNotice { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "В целях")]
        public string Aim { get; set; }
    }

    public class USR_REQ_UBUO_RequestCreateSettlView_View : BasicDocumentView
    {
        public string NameSettlView1 { get; set; }
        public string NameSettlView2 { get; set; }
        public string NameSettlView3 { get; set; }
        public string NameSettlView4 { get; set; }
        public string NameSettlView5 { get; set; }
        public string NameSettlView6 { get; set; }
        public string NameSettlView7 { get; set; }
        public string NameSettlView8 { get; set; }
        public string NameSettlView9 { get; set; }

        public SettlView SettlView1 { get; set; }
        public SettlView SettlView2 { get; set; }
        public SettlView SettlView3 { get; set; }
        public SettlView SettlView4 { get; set; }
        public SettlView SettlView5 { get; set; }
        public SettlView SettlView6 { get; set; }
        public SettlView SettlView7 { get; set; }
        public SettlView SettlView8 { get; set; }
        public SettlView SettlView9 { get; set; }

        public SettlType SettlType1 { get; set; }
        public SettlType SettlType2 { get; set; }
        public SettlType SettlType3 { get; set; }
        public SettlType SettlType4 { get; set; }
        public SettlType SettlType5 { get; set; }
        public SettlType SettlType6 { get; set; }
        public SettlType SettlType7 { get; set; }
        public SettlType SettlType8 { get; set; }
        public SettlType SettlType9 { get; set; }

        public string WaySettl1 { get; set; }
        public string WaySettl2 { get; set; }
        public string WaySettl3 { get; set; }
        public string WaySettl4 { get; set; }
        public string WaySettl5 { get; set; }
        public string WaySettl6 { get; set; }
        public string WaySettl7 { get; set; }
        public string WaySettl8 { get; set; }
        public string WaySettl9 { get; set; }

        public bool TimesheetManagement1 { get; set; }
        public bool TimesheetManagement2 { get; set; }
        public bool TimesheetManagement3 { get; set; }
        public bool TimesheetManagement4 { get; set; }
        public bool TimesheetManagement5 { get; set; }
        public bool TimesheetManagement6 { get; set; }
        public bool TimesheetManagement7 { get; set; }
        public bool TimesheetManagement8 { get; set; }
        public bool TimesheetManagement9 { get; set; }

        public string ReflectedBU1 { get; set; }
        public string ReflectedBU2 { get; set; }
        public string ReflectedBU3 { get; set; }
        public string ReflectedBU4 { get; set; }
        public string ReflectedBU5 { get; set; }
        public string ReflectedBU6 { get; set; }
        public string ReflectedBU7 { get; set; }
        public string ReflectedBU8 { get; set; }
        public string ReflectedBU9 { get; set; }

        public string ReflectedDepartment1 { get; set; }
        public string ReflectedDepartment2 { get; set; }
        public string ReflectedDepartment3 { get; set; }
        public string ReflectedDepartment4 { get; set; }
        public string ReflectedDepartment5 { get; set; }
        public string ReflectedDepartment6 { get; set; }
        public string ReflectedDepartment7 { get; set; }
        public string ReflectedDepartment8 { get; set; }
        public string ReflectedDepartment9 { get; set; }

        public string AverageEarnings1 { get; set; }
        public string AverageEarnings2 { get; set; }
        public string AverageEarnings3 { get; set; }
        public string AverageEarnings4 { get; set; }
        public string AverageEarnings5 { get; set; }
        public string AverageEarnings6 { get; set; }
        public string AverageEarnings7 { get; set; }
        public string AverageEarnings8 { get; set; }
        public string AverageEarnings9 { get; set; }

        public string TypeCost1 { get; set; }
        public string TypeCost2 { get; set; }
        public string TypeCost3 { get; set; }
        public string TypeCost4 { get; set; }
        public string TypeCost5 { get; set; }
        public string TypeCost6 { get; set; }
        public string TypeCost7 { get; set; }
        public string TypeCost8 { get; set; }
        public string TypeCost9 { get; set; }

        public bool IPN1 { get; set; }
        public bool IPN2 { get; set; }
        public bool IPN3 { get; set; }
        public bool IPN4 { get; set; }
        public bool IPN5 { get; set; }
        public bool IPN6 { get; set; }
        public bool IPN7 { get; set; }
        public bool IPN8 { get; set; }
        public bool IPN9 { get; set; }

        public bool OPV1 { get; set; }
        public bool OPV2 { get; set; }
        public bool OPV3 { get; set; }
        public bool OPV4 { get; set; }
        public bool OPV5 { get; set; }
        public bool OPV6 { get; set; }
        public bool OPV7 { get; set; }
        public bool OPV8 { get; set; }
        public bool OPV9 { get; set; }

        public bool CO1 { get; set; }
        public bool CO2 { get; set; }
        public bool CO3 { get; set; }
        public bool CO4 { get; set; }
        public bool CO5 { get; set; }
        public bool CO6 { get; set; }
        public bool CO7 { get; set; }
        public bool CO8 { get; set; }
        public bool CO9 { get; set; }

        public bool CN1 { get; set; }
        public bool CN2 { get; set; }
        public bool CN3 { get; set; }
        public bool CN4 { get; set; }
        public bool CN5 { get; set; }
        public bool CN6 { get; set; }
        public bool CN7 { get; set; }
        public bool CN8 { get; set; }
        public bool CN9 { get; set; }

        public bool FEP1 { get; set; }
        public bool FEP2 { get; set; }
        public bool FEP3 { get; set; }
        public bool FEP4 { get; set; }
        public bool FEP5 { get; set; }
        public bool FEP6 { get; set; }
        public bool FEP7 { get; set; }
        public bool FEP8 { get; set; }
        public bool FEP9 { get; set; }

        public СompositionFEP СompositionFEP1 { get; set; }
        public СompositionFEP СompositionFEP2 { get; set; }
        public СompositionFEP СompositionFEP3 { get; set; }
        public СompositionFEP СompositionFEP4 { get; set; }
        public СompositionFEP СompositionFEP5 { get; set; }
        public СompositionFEP СompositionFEP6 { get; set; }
        public СompositionFEP СompositionFEP7 { get; set; }
        public СompositionFEP СompositionFEP8 { get; set; }
        public СompositionFEP СompositionFEP9 { get; set; }

        public ViewCostWorkforce ViewCostWorkforce1 { get; set; }
        public ViewCostWorkforce ViewCostWorkforce2 { get; set; }
        public ViewCostWorkforce ViewCostWorkforce3 { get; set; }
        public ViewCostWorkforce ViewCostWorkforce4 { get; set; }
        public ViewCostWorkforce ViewCostWorkforce5 { get; set; }
        public ViewCostWorkforce ViewCostWorkforce6 { get; set; }
        public ViewCostWorkforce ViewCostWorkforce7 { get; set; }
        public ViewCostWorkforce ViewCostWorkforce8 { get; set; }
        public ViewCostWorkforce ViewCostWorkforce9 { get; set; }

        public TypeCompensation TypeCompensation1 { get; set; }
        public TypeCompensation TypeCompensation2 { get; set; }
        public TypeCompensation TypeCompensation3 { get; set; }
        public TypeCompensation TypeCompensation4 { get; set; }
        public TypeCompensation TypeCompensation5 { get; set; }
        public TypeCompensation TypeCompensation6 { get; set; }
        public TypeCompensation TypeCompensation7 { get; set; }
        public TypeCompensation TypeCompensation8 { get; set; }
        public TypeCompensation TypeCompensation9 { get; set; }
    }

    public class USR_REQ_UBUO_RequestCalcDriveTrip_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер приказа")]
        public string OrderNum { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата приказа")]
        public DateTime? OrderDate { get; set; }

        [Display(Name = "ФИО")]
        public string FIO1 { get; set; }
        [Display(Name = "ФИО")]
        public string FIO2 { get; set; }
        [Display(Name = "ФИО")]
        public string FIO3 { get; set; }
        [Display(Name = "ФИО")]
        public string FIO4 { get; set; }

        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType1 { get; set; }
        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType2 { get; set; }
        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType3 { get; set; }
        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType4 { get; set; }

        [Display(Name = "Направление")]
        public TripDirection TripDirection1 { get; set; }
        [Display(Name = "Направление")]
        public TripDirection TripDirection2 { get; set; }
        [Display(Name = "Направление")]
        public TripDirection TripDirection3 { get; set; }
        [Display(Name = "Направление")]
        public TripDirection TripDirection4 { get; set; }

        [Display(Name = "Количество суток")]
        public int Day1 { get; set; }
        [Display(Name = "Количество суток")]
        public int Day2 { get; set; }
        [Display(Name = "Количество суток")]
        public int Day3 { get; set; }
        [Display(Name = "Количество суток")]
        public int Day4 { get; set; }

        [Display(Name = "Количество суток проживания")]
        public int DayLive1 { get; set; }
        [Display(Name = "Количество суток проживания")]
        public int DayLive2 { get; set; }
        [Display(Name = "Количество суток проживания")]
        public int DayLive3 { get; set; }
        [Display(Name = "Количество суток проживания")]
        public int DayLive4 { get; set; }

        [Display(Name = "Проезд")]
        public TripPassage TripPassage1 { get; set; }
        [Display(Name = "Проезд")]
        public TripPassage TripPassage2 { get; set; }
        [Display(Name = "Проезд")]
        public TripPassage TripPassage3 { get; set; }
        [Display(Name = "Проезд")]
        public TripPassage TripPassage4 { get; set; }

        [Display(Name = "Сумма проезда")]
        public int TicketSum1 { get; set; }
        [Display(Name = "Сумма проезда")]
        public int TicketSum2 { get; set; }
        [Display(Name = "Сумма проезда")]
        public int TicketSum3 { get; set; }
        [Display(Name = "Сумма проезда")]
        public int TicketSum4 { get; set; }

        public int DayRate1 { get; set; }
        public int DayRate2 { get; set; }
        public int DayRate3 { get; set; }
        public int DayRate4 { get; set; }

        public int ResidenceRate1 { get; set; }
        public int ResidenceRate2 { get; set; }
        public int ResidenceRate3 { get; set; }
        public int ResidenceRate4 { get; set; }
    }

    public class USR_REQ_UBUO_RequestCalcDriveTripCals_View
    {
        public USR_REQ_UBUO_RequestCalcDriveTripCals_View(EmplTripType emplTripType, TripDirection tripDirection, int day, int dayLive, int ticketSum, int dayRate, int residenceRate)
        {
            EmplTripType = emplTripType;
            TripDirection = tripDirection;
            Day = day;
            DayLive = dayLive;
            TicketSum = ticketSum;
            DayRate = dayRate;
            ResidenceRate = residenceRate;
        }

        public EmplTripType EmplTripType { get; set; }
        public TripDirection TripDirection { get; set; }
        public int Day { get; set; }
        public int DayLive { get; set; }
        public int TicketSum { get; set; }
        public int DayRate { get; set; }
        public int ResidenceRate { get; set; }
    }
    #endregion

    #region УЗЛ

    public class USR_REQ_UZL_RequestForAccident_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование")]
        public string Reason { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина отсутствия в ОБ\\БКВ")]
        public string ReasonAbsent { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Источник финансирования")]
        public string FinancialSource { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "C даты")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "По дату")]
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сумма заявки")]
        public string Amount { get; set; }
    }

    public class USR_REQ_UZL_RequestForUnscheduledIB_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование")]
        public string Reason { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина отсутствия в ОБ\\БКВ")]
        public string ReasonAbsent { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Источник финансирования")]
        public string FinancialSource { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "C даты")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "По дату")]
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сумма заявки")]
        public string Amount { get; set; }
    }

    public class USR_REQ_UZL_RequestForUnscheduledOB_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование")]
        public string Reason { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина отсутствия в ОБ\\БКВ")]
        public string ReasonAbsent { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Источник финансирования")]
        public string FinancialSource { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "C даты")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "По дату")]
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сумма заявки")]
        public string Amount { get; set; }
    }

    public class USR_REQ_UZL_RequestForIlliquid_View : BasicDocumentView
    {

        [Display(Name = "Подразделение инициатора")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Department { get; set; }

        [Display(Name = "Наименование ТМЦ")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string ItemName { get; set; }

        [Display(Name = "Количество")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string ItemCount { get; set; }

        [Display(Name = "Мероприятие")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Event { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public DateTime? Date { get; set; }
    
    }

    public class USR_REQ_UZL_RequestForPeopleAcceptanceItems_View : BasicDocumentView
    {

        [Display(Name = "Инициатор")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Iniciator { get; set; }

        [Display(Name = "Подразделение")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Department { get; set; }

        [Display(Name = "Контактные телефоны")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string ContactPhone { get; set; }

        [Display(Name = "Тема")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Theme { get; set; }

        [Display(Name = "ФИО Руководителя 1")]
        public string UserChooseManual1 { get; set; }

        [Display(Name = "ФИО Руководителя 2")]
        public string UserChooseManual2 { get; set; }

        [Display(Name = "ФИО Руководителя 3")]
        public string UserChooseManual3 { get; set; }

        [Display(Name = "ФИО Руководителя 4")]
        public string UserChooseManual4 { get; set; }

        [Display(Name = "Подразделение")]
        public string Department1 { get; set; }

        [Display(Name = "Подразделение")]
        public string Department2 { get; set; }

        [Display(Name = "Подразделение")]
        public string Department3 { get; set; }

        [Display(Name = "Подразделение")]
        public string Department4 { get; set; }

        [Display(Name = "ФИО специалиста 1")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string UserChooseManual5 { get; set; }

        [Display(Name = "ФИО специалиста 2")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string UserChooseManual6 { get; set; }

        [Display(Name = "ФИО специалиста 3")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string UserChooseManual7 { get; set; }

        [Display(Name = "ФИО специалиста 4")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string UserChooseManual8 { get; set; }

        [Display(Name = "Контакты специалиста 1")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Contact1 { get; set; }

        [Display(Name = "Контакты специалиста 2")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Contact2 { get; set; }

        [Display(Name = "Контакты специалиста 3")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Contact3 { get; set; }

        [Display(Name = "Контакты специалиста 4")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Contact4 { get; set; }
    
    }

    public class USR_REQ_UZL_RequestForContractNoneresident_View : BasicDocumentView
    {
        [Display(Name = "Исполнитель")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string UserChooseManual1 { get; set; }
    }

    public class USR_REQ_UZL_RequestForContractResident_View : BasicDocumentView
    {
        [Display(Name = "Исполнитель")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string UserChooseManual1 { get; set; }
    }

    public class USR_REQ_UZL_RequestForrepresentationKD_View : BasicDocumentView
    {
        [Display(Name = "Исполнитель")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string UserChooseManual1 { get; set; }
    }

    public class USR_REQ_UZL_RequestForUT_View : BasicDocumentView
    {
        [DataType(DataType.MultilineText)]
        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Источник финансирования")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string FinancialSource { get; set; }

    }

    public class USR_REQ_UZL_RequestForExtraIBBGP_View : BasicDocumentView
    {
        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Explanation { get; set; }

        [Display(Name = "Причина отсутствия в ОБ\\БКВ")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Источник финансирования")]
        public string FinancialSource { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "C даты")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "По дату")]
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сумма заявки")]
        public string Amount { get; set; }


    }

    public class USR_REQ_UZL_RequestForExtraIBZIF_View : BasicDocumentView
    {
        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Explanation { get; set; }

        [Display(Name = "Причина отсутствия в ОБ\\БКВ")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Источник финансирования")]
        public string FinancialSource { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "C даты")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "По дату")]
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сумма заявки")]
        public string Amount { get; set; }
    }

    public class USR_REQ_UZL_RequestForExtraOBBGP_View : BasicDocumentView
    {
        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Explanation { get; set; }

        [Display(Name = "Причина отсутствия в ОБ\\БКВ")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Источник финансирования")]
        public string FinancialSource { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "C даты")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "По дату")]
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сумма заявки")]
        public string Amount { get; set; }
    }

    public class USR_REQ_UZL_RequestForExtraOBZIF_View : BasicDocumentView
    {
        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Explanation { get; set; }

        [Display(Name = "Причина отсутствия в ОБ\\БКВ")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Источник финансирования")]
        public string FinancialSource { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "C даты")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "По дату")]
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сумма заявки")]
        public string Amount { get; set; }
    }

    public class USR_REQ_UZL_RequestForCrashedStone_View : BasicDocumentView
	{
        [Display(Name = "Подразделение")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Department { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Предмет заявки/характеристика")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Explanation { get; set; }

        [Display(Name = "Объем")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Value { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата необходимости")]
        public DateTime? Date { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Цель")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Purpose { get; set; }

	}
    #endregion

    #region УКР

    public class USR_REQ_UKR_RequestForExpertiseDKU_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД пользователя")]
        public string UserExecutive { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }        
    }

    public class USR_REQ_UKR_RequestForExpertiseInstruction_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД пользователя")]
        public string UserExecutive { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Исполнитель")]
        public string UserChooseManual1 { get; set; }
    }

    public class USR_REQ_UKR_RequestForExpertiseDepartment_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД пользователя")]
        public string UserExecutive { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Исполнитель")]
        public string UserChooseManual1 { get; set; }
    }

    #endregion

    #region УРП

    public class USR_REQ_URP_RequestForKindergarten_View : BasicDocumentView
    {
        [Display(Name = "Группа преподавания")]
        public TeachGroup TeachGroup { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО ребенка")]
        public string ChildName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата рождения")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Степень родства")]
        public RelateRate RelateRate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО отца")]
        public string FatherName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Место работы, должность отца")]
        public string FatherWorkPlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО матери")]
        public string MotherName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Место работы, должность матери")]
        public string MotherWorkPlace { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Фактический адрес проживания")]
        public string ActualAddress { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактные данные")]
        public string Contact { get; set; }
    }

    public class USR_REQ_URP_RequestForResponsibilities_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование")]
        public string Reason { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО")]
        public string UsersOff { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Управление/служба(отдел)")]
        public string DepartmentOff { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "График работы")]
        public string ScheduleOff { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО")]
        public string UsersOn { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Управление/служба(отдел)")]
        public string DepartmentOn { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "График работы")]
        public string ScheduleOn { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? ToDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Доплата")]
        public string Amount { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }

    }

    public class USR_REQ_URP_RequestForResponsibilitiesSOTB_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование")]
        public string Reason { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО")]
        public string UsersOff { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Управление/служба(отдел)")]
        public string DepartmentOff { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "График работы")]
        public string ScheduleOff { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО")]
        public string UsersOn { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Управление/служба(отдел)")]
        public string DepartmentOn { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "График работы")]
        public string ScheduleOn { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? ToDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Доплата")]
        public string Amount { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }

    }

    public class USR_REQ_URP_RequestForPrepayMaster_View : BasicDocumentView
    {
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование")]
        public string Reason { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО сотрудника")]
        public string FIO { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "График работы")]
        public string Schedule { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public DateTime? DocDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "% доплаты по ШР")]
        public string Percent { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

    }

    public class USR_REQ_URP_RequestForAccrualExceptZIFBGP_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }


        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Период")]
        public string Period { get; set; }
    }

    public class USR_REQ_URP_RequestForAccrualBGP_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }


        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Период")]
        public string Period { get; set; }
    }

    public class USR_REQ_URP_RequestForAccrualZIF_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }


        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Период")]
        public string Period { get; set; }
    }

    public class USR_REQ_URP_RequestForAccrualSM_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }


        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Период")]
        public string Period { get; set; }
    }

    public class USR_REQ_URP_RequestForAccrualPTU_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }


        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Период")]
        public string Period { get; set; }
    }

    public class USR_REQ_URP_RequestForTeaching_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО и должность сотрудника")]
        public string ToUsers { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]

        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Тематика обучения")]
        public string Theme { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата проведения обучения")]
        public DateTime? DocDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Место проведения обучения")]
        public string Place { get; set; }

        [Display(Name = "Компания-организатор")]
        public string Company { get; set; }
    }

    public class USR_REQ_URP_RequestForWithdrawVocationITR1_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отзываемый сотрудник")]
        public string Employee { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отозвать с")]
        public string Subject { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForWithdrawVocationITR2_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отзываемый сотрудник")]
        public string Employee { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отозвать с")]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForWVAuxiliaryBlock_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отзываемый сотрудник")]
        public string Employee { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отозвать с")]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForWVMiningBlock_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отзываемый сотрудник")]
        public string Employee { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отозвать с")]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForWVFinBlock_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отзываемый сотрудник")]
        public string Employee { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отозвать с")]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForWVStraight_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отзываемый сотрудник")]
        public string Employee { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отозвать с")]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForForeignVisa_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Family name, first name (Имя, Фамилия)")]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Date and place of birth (Дата и место рождения)")]
        public string DateAndPlace { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Citizenship (Гражданство)")]
        public string Citizenship { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Nationality (Национальность)")]
        public string Nationality { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Passport No., date of issue and expiry (Номер паспорта, дата выдачи и окончания действия)")]
        public string Passport { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Place of employment, position (company's address) Место работы, должность (адрес организации)")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Residential address (Адрес проживания)")]
        public string Address { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Place of visa issue (Место получие визы)")]
        public string PlaceVisa { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Period of stay (Период пребывания)")]
        public string Period { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Multiplicity of visa (Многократность визы)")]
        public string Multiplicity { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Purpose of visit (Цель визита)")]
        public string Purpose { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Route of travel and the places of visit in the Republic of Kazakhstan(Маршрут путешествия, места, которые планируете посетить в Казахстане)")]
        public string Route { get; set; }
    }

    public class USR_REQ_URP_RequestForTransferVocationITR1_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник, которому переносят отпуск")]
        public string Employee { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Запланированная дата отпуска по графику")]
        public string PlanDate { get; set; }
       
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Перенести на")]
        public string TransferDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForTransferVocationITR2_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник, которому переносят отпуск")]
        public string Employee { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Запланированная дата отпуска по графику")]
        public string PlanDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Перенести на")]
        public string TransferDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForTransfVacWorkerMining_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник, которому переносят отпуск")]
        public string Employee { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Запланированная дата отпуска по графику")]
        public DateTime? PlanDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Перенести на")]
        public string TransferDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForTransfVacWorkerStraight_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник, которому переносят отпуск")]
        public string Employee { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Запланированная дата отпуска по графику")]
        public DateTime? PlanDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Перенести на")]
        public string TransferDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForTransfVacWorkerFin_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник, которому переносят отпуск")]
        public string Employee { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Запланированная дата отпуска по графику")]
        public DateTime? PlanDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Перенести на")]
        public string TransferDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForTransfVacWorkerAuxiliary_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник, которому переносят отпуск")]
        public string Employee { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Запланированная дата отпуска по графику")]
        public DateTime? PlanDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Перенести на")]
        public string TransferDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForProvisionGraphVac_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }

        [Display(Name = "Исполнители")]
        public string UserChooseManual2 { get; set; }
    }

    public class USR_REQ_URP_RequestForProvisionGraphExit_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Период")]
        public string Period { get; set; }
    }

    public class USR_REQ_URP_RequestForWeekend_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование")]
        public string Reason { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Управление/служба(отдел)")]
        public string Department { get; set; }        

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник(ФИО)")]
        public string Employee { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дни выхода")]
        public string Period { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }
    }

    public class USR_REQ_URP_RequestForReduction_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Период")]
        public string Period { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }

    }

    public class USR_REQ_URP_RequestForReductionCandidate_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Период")]
        public string Period { get; set; }
    }

    public class USR_REQ_URP_RequestForReductionTB_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение-нарушитель")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Руководитель подразделения - нарушителя")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Снижаемая часть")]
        public string Bulk { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Период")]
        public string Period { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование")]
        public string Reason { get; set; }
    }

    public class USR_REQ_URP_RequestForReductionThird_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение-нарушитель")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Руководитель подразделения - нарушителя")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока подразделения нарушителя")]
        public string UserChooseManual2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока инициатора")]
        public string UserChooseManual3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Период")]
        public string Period { get; set; }
    }

    public class USR_REQ_URP_RequestForSelectionPersonal_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Вакантная должность")]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Кол-во вакансий")]
        public int Count { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина появления вакансий")]
        public string Reason { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Желательная дата закрытия вакансий")]
        public DateTime? ClosedDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Возможность привлечения иногородних")]
        public string Possibility { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Руководитель подразделения")]
        public string Executive { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактное лицо")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Внутренний телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "1.1 Возраст")]
        public string Age { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "1.2 Образование")]
        public string Education { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "1.3 Опыт работы (в какой области, стаж, должность)")]
        public string Experience { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "1.4 Обязательные требования знаний и навыков к вакантной должности")]
        public string Requirement { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "1.5 Наличие желательных знаний и навыков")]
        public string Skills { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "1.6 Личностные качества")]
        public string Character { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "1.7 Дополнительные требования")]
        public string Additional { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "2.1 Функциональные обязанности:")]
        public string FuncDuties { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "2.2 Дополнительные обязанности:")]
        public string AddDuties { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "2.3 Непосредственный руководитель:")]
        public string DirectExecutive { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "2.4 В случае, если вакансия предполагает управление людьми, указать количество подчиненных")]
        public string Management { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "2.5 Ориентировочная заработная плата")]
        public string Salary { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "2.6 Дополнительные выплаты (бонусы, премии)")]
        public string AddSalary { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "2.7 График работы")]
        public string Schedule { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "2.8 Командировки: куда и как часто")]
        public string BusinessTrip { get; set; }
    }

    public class USR_REQ_URP_RequestForHRCardITR1_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "На должность")]
        public string OnPosition { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО кандидата")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Непосредственный руководитель")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наставник")]
        public string Tutor { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Исполнительный директор")]
        public string UserChooseManual2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report1 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report2 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report3 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report4 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview4 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion4 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason4 { get; set; }

        [Display(Name = "Условия")]
        public string Circumstances { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Рекомендации/Требования")]
        public string RecommendationOZTB { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Код установленной ЗП")]
        public string SalaryCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Испытательный срок")]
        public string Period { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Режим (график) работы")]
        public string Schedule { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Решение кандидата")]
        public string Solution { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Статус местожительства")]
        public StatusResidence Status { get; set; }

        [Display(Name = "Зачислить во внешний кадровый резерв")]
        public bool External { get; set; }
    }

    public class USR_REQ_URP_RequestForHRCardITR2_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "На должность")]
        public string OnPosition { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО кандидата")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Непосредственный руководитель")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наставник")]
        public string Tutor { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Начальник управления")]
        public string UserChooseManual2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Исполнительный директор")]
        public string UserChooseManual3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report1 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report2 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report3 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report4 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview4 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion4 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason4 { get; set; }

        [Display(Name = "Условия")]
        public string Circumstances { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Рекомендации/Требования")]
        public string RecommendationOZTB { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Код установленной ЗП")]
        public string SalaryCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Испытательный срок")]
        public string Period { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Режим (график) работы")]
        public string Schedule { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Решение кандидата")]
        public string Solution { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Статус местожительства")]
        public StatusResidence Status { get; set; }

        [Display(Name = "Зачислить во внешний кадровый резерв")]
        public bool External { get; set; }

    }

    public class USR_REQ_URP_RequestForHRCardITRZIF_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "На должность")]
        public string OnPosition { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО кандидата")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Непосредственный руководитель")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наставник")]
        public string Tutor { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Директор ЗИФ")]
        public string UserChooseManual2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report1 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report2 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report3 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason3 { get; set; }

        [Display(Name = "Условия")]
        public string Circumstances { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Рекомендации/Требования")]
        public string RecommendationOZTB { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Код установленной ЗП")]
        public string SalaryCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Испытательный срок")]
        public string Period { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Режим (график) работы")]
        public string Schedule { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Решение кандидата")]
        public string Solution { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Статус местожительства")]
        public StatusResidence Status { get; set; }

        [Display(Name = "Зачислить во внешний кадровый резерв")]
        public bool External { get; set; }
    
    }

    public class USR_REQ_URP_RequestForHRCardWork_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "На должность")]
        public string OnPosition { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО кандидата")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Непосредственный руководитель")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наставник")]
        public string Tutor { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Начальник управления")]
        public string UserChooseManual2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Исполнительный директор")]
        public string UserChooseManual3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report1 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report2 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report3 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report4 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview4 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion4 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason4 { get; set; }

        [Display(Name = "Условия")]
        public string Circumstances { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Рекомендации/Требования")]
        public string RecommendationOZTB { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Код установленной ЗП")]
        public string SalaryCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Испытательный срок")]
        public string Period { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Режим (график) работы")]
        public string Schedule { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Решение кандидата")]
        public string Solution { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Статус местожительства")]
        public StatusResidence Status{ get; set; }

        [Display(Name = "Зачислить во внешний кадровый резерв")]
        public bool External { get; set; }
    }

    public class USR_REQ_URP_RequestForHRCardWorkZIF_View : BasicDocumentView
    {

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "На должность")]
        public string OnPosition { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО кандидата")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Непосредственный руководитель")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наставник")]
        public string Tutor { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Исполнительный директор")]
        public string UserChooseManual2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report1 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report2 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Отчет")]
        public string Report3 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата собеседования или телефоного интервью")]
        public DateTime? DateInterview3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Мнение")]
        public HROpinion Opinion3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причины")]
        public string Reason3 { get; set; }

        [Display(Name = "Условия")]
        public string Circumstances { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Рекомендации/Требования")]
        public string RecommendationOZTB { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Код установленной ЗП")]
        public string SalaryCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Испытательный срок")]
        public string Period { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Режим (график) работы")]
        public string Schedule { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Решение кандидата")]
        public string Solution { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Статус местожительства")]
        public StatusResidence Status { get; set; }

        [Display(Name = "Зачислить во внешний кадровый резерв")]
        public bool External { get; set; }  
    
    }

    public class USR_REQ_URP_RequestForHRChGraphTime_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }

        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Name5 { get; set; }
        public string Name6 { get; set; }
        public string Name7 { get; set; }
        public string Name8 { get; set; }
        public string Name9 { get; set; }
        public string Name10 { get; set; }

        public string Position1 { get; set; }
        public string Position2 { get; set; }
        public string Position3 { get; set; }
        public string Position4 { get; set; }
        public string Position5 { get; set; }
        public string Position6 { get; set; }
        public string Position7 { get; set; }
        public string Position8 { get; set; }
        public string Position9 { get; set; }
        public string Position10 { get; set; }

        public string Department1 { get; set; }
        public string Department2 { get; set; }
        public string Department3 { get; set; }
        public string Department4 { get; set; }
        public string Department5 { get; set; }
        public string Department6 { get; set; }
        public string Department7 { get; set; }
        public string Department8 { get; set; }
        public string Department9 { get; set; }
        public string Department10 { get; set; }

        public HRGraphics Graphics1 { get; set; }
        public HRGraphics Graphics2 { get; set; }
        public HRGraphics Graphics3 { get; set; }
        public HRGraphics Graphics4 { get; set; }
        public HRGraphics Graphics5 { get; set; }
        public HRGraphics Graphics6 { get; set; }
        public HRGraphics Graphics7 { get; set; }
        public HRGraphics Graphics8 { get; set; }
        public HRGraphics Graphics9 { get; set; }
        public HRGraphics Graphics10 { get; set; }

        public HRDuration FirstDuration1 { get; set; }
        public HRDuration FirstDuration2 { get; set; }
        public HRDuration FirstDuration3 { get; set; }
        public HRDuration FirstDuration4 { get; set; }
        public HRDuration FirstDuration5 { get; set; }
        public HRDuration FirstDuration6 { get; set; }
        public HRDuration FirstDuration7 { get; set; }
        public HRDuration FirstDuration8 { get; set; }
        public HRDuration FirstDuration9 { get; set; }
        public HRDuration FirstDuration10 { get; set; }

        public HRGraphics GraphicsCorrect1 { get; set; }
        public HRGraphics GraphicsCorrect2 { get; set; }
        public HRGraphics GraphicsCorrect3 { get; set; }
        public HRGraphics GraphicsCorrect4 { get; set; }
        public HRGraphics GraphicsCorrect5 { get; set; }
        public HRGraphics GraphicsCorrect6 { get; set; }
        public HRGraphics GraphicsCorrect7 { get; set; }
        public HRGraphics GraphicsCorrect8 { get; set; }
        public HRGraphics GraphicsCorrect9 { get; set; }
        public HRGraphics GraphicsCorrect10 { get; set; }

        public HRDuration SecondDuration1 { get; set; }
        public HRDuration SecondDuration2 { get; set; }
        public HRDuration SecondDuration3 { get; set; }
        public HRDuration SecondDuration4 { get; set; }
        public HRDuration SecondDuration5 { get; set; }
        public HRDuration SecondDuration6 { get; set; }
        public HRDuration SecondDuration7 { get; set; }
        public HRDuration SecondDuration8 { get; set; }
        public HRDuration SecondDuration9 { get; set; }
        public HRDuration SecondDuration10 { get; set; }


        public string Reason1 { get; set; }
        public string Reason2 { get; set; }
        public string Reason3 { get; set; }
        public string Reason4 { get; set; }
        public string Reason5 { get; set; }
        public string Reason6 { get; set; }
        public string Reason7 { get; set; }
        public string Reason8 { get; set; }
        public string Reason9 { get; set; }
        public string Reason10 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate1 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate2 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate3 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate4 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate5 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate6 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate7 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate8 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate9 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate10 { get; set; }

        public string EndDate1 { get; set; }
        public string EndDate2 { get; set; }
        public string EndDate3 { get; set; }
        public string EndDate4 { get; set; }
        public string EndDate5 { get; set; }
        public string EndDate6 { get; set; }
        public string EndDate7 { get; set; }
        public string EndDate8 { get; set; }
        public string EndDate9 { get; set; }
        public string EndDate10 { get; set; }
    }

    public class USR_REQ_URP_RequestForHRChGraphConst_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ИД Блока")]
        public string UserChooseManual1 { get; set; }

        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Name5 { get; set; }
        public string Name6 { get; set; }
        public string Name7 { get; set; }
        public string Name8 { get; set; }
        public string Name9 { get; set; }
        public string Name10 { get; set; }

        public string Position1 { get; set; }
        public string Position2 { get; set; }
        public string Position3 { get; set; }
        public string Position4 { get; set; }
        public string Position5 { get; set; }
        public string Position6 { get; set; }
        public string Position7 { get; set; }
        public string Position8 { get; set; }
        public string Position9 { get; set; }
        public string Position10 { get; set; }

        public string Department1 { get; set; }
        public string Department2 { get; set; }
        public string Department3 { get; set; }
        public string Department4 { get; set; }
        public string Department5 { get; set; }
        public string Department6 { get; set; }
        public string Department7 { get; set; }
        public string Department8 { get; set; }
        public string Department9 { get; set; }
        public string Department10 { get; set; }

        public HRGraphics Graphics1 { get; set; }
        public HRGraphics Graphics2 { get; set; }
        public HRGraphics Graphics3 { get; set; }
        public HRGraphics Graphics4 { get; set; }
        public HRGraphics Graphics5 { get; set; }
        public HRGraphics Graphics6 { get; set; }
        public HRGraphics Graphics7 { get; set; }
        public HRGraphics Graphics8 { get; set; }
        public HRGraphics Graphics9 { get; set; }
        public HRGraphics Graphics10 { get; set; }

        public HRDuration FirstDuration1 { get; set; }
        public HRDuration FirstDuration2 { get; set; }
        public HRDuration FirstDuration3 { get; set; }
        public HRDuration FirstDuration4 { get; set; }
        public HRDuration FirstDuration5 { get; set; }
        public HRDuration FirstDuration6 { get; set; }
        public HRDuration FirstDuration7 { get; set; }
        public HRDuration FirstDuration8 { get; set; }
        public HRDuration FirstDuration9 { get; set; }
        public HRDuration FirstDuration10 { get; set; }

        public HRGraphics GraphicsCorrect1 { get; set; }
        public HRGraphics GraphicsCorrect2 { get; set; }
        public HRGraphics GraphicsCorrect3 { get; set; }
        public HRGraphics GraphicsCorrect4 { get; set; }
        public HRGraphics GraphicsCorrect5 { get; set; }
        public HRGraphics GraphicsCorrect6 { get; set; }
        public HRGraphics GraphicsCorrect7 { get; set; }
        public HRGraphics GraphicsCorrect8 { get; set; }
        public HRGraphics GraphicsCorrect9 { get; set; }
        public HRGraphics GraphicsCorrect10 { get; set; }

        public HRDuration SecondDuration1 { get; set; }
        public HRDuration SecondDuration2 { get; set; }
        public HRDuration SecondDuration3 { get; set; }
        public HRDuration SecondDuration4 { get; set; }
        public HRDuration SecondDuration5 { get; set; }
        public HRDuration SecondDuration6 { get; set; }
        public HRDuration SecondDuration7 { get; set; }
        public HRDuration SecondDuration8 { get; set; }
        public HRDuration SecondDuration9 { get; set; }
        public HRDuration SecondDuration10 { get; set; }


        public string Reason1 { get; set; }
        public string Reason2 { get; set; }
        public string Reason3 { get; set; }
        public string Reason4 { get; set; }
        public string Reason5 { get; set; }
        public string Reason6 { get; set; }
        public string Reason7 { get; set; }
        public string Reason8 { get; set; }
        public string Reason9 { get; set; }
        public string Reason10 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate1 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate2 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate3 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate4 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate5 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate6 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate7 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate8 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate9 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate10 { get; set; }
    }

    public class USR_REQ_URP_RequestForPaymentFirstDay_View : BasicDocumentView
    {
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }

        public HRDepartmentFirstDay Department1 { get; set; }
        public HRDepartmentFirstDay Department2 { get; set; }
        public HRDepartmentFirstDay Department3 { get; set; }
        public HRDepartmentFirstDay Department4 { get; set; }

        public string Position1 { get; set; }
        public string Position2 { get; set; }
        public string Position3 { get; set; }
        public string Position4 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate1 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate2 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate3 { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate4 { get; set; }

        public HRDestinationFirstDay Destination1 { get; set; }
        public HRDestinationFirstDay Destination2 { get; set; }
        public HRDestinationFirstDay Destination3 { get; set; }
        public HRDestinationFirstDay Destination4 { get; set; }

        public int Term1 { get; set; }
        public int Term2 { get; set; }
        public int Term3 { get; set; }
        public int Term4 { get; set; }

        public string Amount1 { get; set; }
        public string Amount2 { get; set; }
        public string Amount3 { get; set; }
        public string Amount4 { get; set; }
    }
    #endregion

    #region УТ
    public class USR_REQ_YT_AuxiliaryTransportOCTMCInvest_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Вспомогательная техника")]
        public string Transport { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Инвентарный номер и наименование ОС либо ТМЦ")]
        public string ItemId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Откуда забрать")]
        public string PlaceDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Время выезда")]
        public TimeSpan TimeDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер проекта в рамках которого производится доставка ОС\\ТМЦ")]
        public string NumberProject { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер объекта, в рамках которого производится доставка ОС\\ТМЦ")]
        public string NumberObject { get; set; }

        [Display(Name = "Срок командирования")]
        public string Terms { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_AuxiliaryTransportOCTMCOper_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Вспомогательная техника")]
        public string Transport { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Инвентарный номер и наименование ОС либо ТМЦ")]
        public string ItemId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Откуда забрать")]
        public string PlaceDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Время выезда")]
        public TimeSpan TimeDeparture { get; set; }

        [Display(Name = "Срок командирования")]
        public string Terms { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_AuxiliaryTransportDayOff_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Вспомогательная техника")]
        public string Transport { get; set; }

        [Display(Name = "В связи с")]
        public PurposeAuxiliaryTransportTrip PurposeAuxiliaryTransportTrip { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Выполнение работ")]
        public string ExecutionWork { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Ответственное лицо из числа ИТР за погрузочно-разгрузочные работы")]
        public string EmplName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт предоставления транспорта")]
        public string PlaceDeparture { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Время с")]
        public TimeSpan StartWorkTime { get; set; }

        [Display(Name = "Время по")]
        public TimeSpan EndWorkTime { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_AuxiliaryTransportWorkDays_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Вспомогательная техника")]
        public string Transport { get; set; }

        [Display(Name = "В связи с")]
        public PurposeAuxiliaryTransportTrip PurposeAuxiliaryTransportTrip { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Выполнение работ")]
        public string ExecutionWork { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Ответственное лицо из числа ИТР за погрузочно-разгрузочные работы")]
        public string EmplName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт предоставления транспорта")]
        public string PlaceDeparture { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Время с")]
        public TimeSpan StartWorkTime { get; set; }

        [Display(Name = "Время по")]
        public TimeSpan EndWorkTime { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_AuxiliaryTransportOutABK_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Вспомогательная техника")]
        public string Transport { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "В связи с")]
        public string Purpose { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Выполнение работ")]
        public string ExecutionWork { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Ответственное лицо из числа ИТР за погрузочно-разгрузочные работы")]
        public string EmplName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Время с")]
        public TimeSpan StartWorkTime { get; set; }

        [Display(Name = "Время по")]
        public TimeSpan EndWorkTime { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_StandbyTransport_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Заказчик (ФИО, подразделение)")]
        public string Client { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Время")]
        public TimeSpan StartWorkTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Маршрут")]
        public string Route { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель поездки")]
        public string Purpose { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_StandbyTransportUIT_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Заказчик (ФИО, подразделение)")]
        public string Client { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Время")]
        public TimeSpan StartWorkTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Маршрут")]
        public string Route { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель поездки")]
        public string Purpose { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_LightTransportTripManage_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Display(Name = "Цель")]
        public PurposeTrip PurposeTrip { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Сотрудник")]
        public EmplTrip EmplTrip { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО сотрудника, которого необходимо встетить\\отвезти")]
        public string EmplName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактные телефоны сотрудника, которого необходимо встретить\\отвезти")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Откуда забрать")]
        public string PlaceDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Время (во сколько нужно быть в пункте назначения)")]
        public TimeSpan TimeDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Данные рейса (номер, время)")]
        public string Flight { get; set; }

        [Display(Name = "Срок командирования")]
        public string Terms { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_LightTransportTripATK_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Display(Name = "Требуется")]
        public PurposeTrip PurposeTrip { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник")]
        public string Users { get; set; }

        [Display(Name = "Откуда")]
        public EmplTrip EmplTrip { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО сотрудника, которого необходимо встетить\\отвезти")]
        public string EmplName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактные телефоны сотрудника, которого необходимо встретить\\отвезти")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Откуда забрать")]
        public string PlaceDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Время (во сколько нужно быть в пункте назначения)")]
        public TimeSpan TimeDeparture { get; set; }

        [Display(Name = "Срок командирования")]
        public string Terms { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_LightTransportOCTMCInvest_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Инвентарный номер и наименование ОС либо ТМЦ")]
        public string ItemId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Откуда забрать")]
        public string PlaceDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Время выезда")]
        public TimeSpan TimeDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер проекта, в рамках которого производится доставка ОС\\ТМЦ")]
        public string NumberProject { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер объекта, в рамках которого производится доставка ОС\\ТМЦ")]
        public string NumberObject { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_LightTransportOCTMCOper_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Инвентарный номер и наименование ОС либо ТМЦ")]
        public string ItemId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Откуда забрать")]
        public string PlaceDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Время выезда")]
        public TimeSpan TimeDeparture { get; set; }

        [Display(Name = "Срок командирования")]
        public string Terms { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_LightTransportOutOrganizationInvest_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Display(Name = "Цель")]
        public PurposeTrip PurposeTrip { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Сотрудник")]
        public EmplTrip EmplTrip { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО сотрудника, которого необходимо встетить\\отвезти")]
        public string EmplName { get; set; }

        [Display(Name = "Наименование предприятия, в котором работают сотрудники")]
        public string Organization { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель приезда вышеуказанных сотрудников")]
        public string Purpose { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактные телефоны сотрудника, которого необходимо встретить\\отвезти")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Откуда забрать")]
        public string PlaceDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Данные рейса (номер, время)")]
        public string Flight { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер проекта, в рамках которого приезжают вышеуказанные сотрудники")]
        public string NumberProject { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер объекта, в рамках которого приезжают вышеуказанные сотрудники")]
        public string NumberObject { get; set; }

        [Display(Name = "Выставление счета за предоставление транспорта")]
        public bool Account { get; set; }

        [Display(Name = "Срок командирования")]
        public string Terms { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_LightTransportOutOrganizationOper_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Display(Name = "Цель")]
        public PurposeTrip PurposeTrip { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Сотрудник")]
        public EmplTrip EmplTrip { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО сотрудника, которого необходимо встретить\\отвезти")]
        public string EmplName { get; set; }

        [Display(Name = "Наименование предприятия, в котором работают сотрудники")]
        public string Organization { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель приезда вышеуказанных сотрудников")]
        public string Purpose { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактные телефоны сотрудника, которого необходимо встретить\\отвезти")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Откуда забрать")]
        public string PlaceDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Данные рейса (номер, время)")]
        public string Flight { get; set; }

        [Display(Name = "Выставление счета за предоставление транспорта")]
        public bool Account { get; set; }

        [Display(Name = "Срок командирования")]
        public string Terms { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_LightTransportTripDayOff_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Время с")]
        public TimeSpan StartWorkTime { get; set; }

        [Display(Name = "Время по")]
        public TimeSpan EndWorkTime { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_PassangerTransportTrip_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Заказчик")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактный телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель, количество пассажиров")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Время с")]
        public TimeSpan StartWorkTime { get; set; }

        [Display(Name = "Время по")]
        public TimeSpan EndWorkTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Маршрут")]
        public string Route { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_PassangerTransportTripManage_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Время с")]
        public TimeSpan StartWorkTime { get; set; }

        [Display(Name = "Время по")]
        public TimeSpan EndWorkTime { get; set; }

        [Display(Name = "Срок командирования")]
        public string Terms { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_PassangerTransportTripATK_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО сотрудника, которого необходимо встретить\\сопровождать")]
        public string EmplName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактные телефоны сотрудника, которого необходимо встретить\\сопровождать")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Откуда забрать")]
        public string PlaceDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Время (во сколько нужно быть в пункте назначения)")]
        public TimeSpan TimeArrive { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Данные рейса (номер, время)")]
        public string Flight { get; set; }

        [Display(Name = "Срок командирования")]
        public string Terms { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_PassangerTransportOutOrganizationInvest_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Display(Name = "Цель")]
        public PurposeTrip PurposeTrip { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Сотрудник")]
        public EmplTrip EmplTrip { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО сотрудника, которого необходимо встретить\\отвезти")]
        public string EmplName { get; set; }

        [Display(Name = "Наименование предприятия, в котором работают сотрудники")]
        public string Organization { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель приезда вышеуказанных сотрудников")]
        public string Purpose { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактные телефоны сотрудника, которого необходимо встретить\\отвезти")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Откуда забрать")]
        public string PlaceDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Данные рейса (номер, время)")]
        public string Flight { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер проекта, в рамках которого приезжают вышеуказанные сотрудники")]
        public string NumberProject { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер объекта, в рамках которого приезжают вышеуказанные сотрудники")]
        public string NumberObject { get; set; }

        [Display(Name = "Выставление счета за предоставление транспорта")]
        public bool Account { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_PassangerTransportOutOrganizationOper_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Display(Name = "Цель")]
        public PurposeTrip PurposeTrip { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Сотрудник")]
        public EmplTrip EmplTrip { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО сотрудника, которого необходимо встретить\\отвезти")]
        public string EmplName { get; set; }

        [Display(Name = "Наименование предприятия, в котором работают сотрудники")]
        public string Organization { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель приезда вышеуказанных сотрудников")]
        public string Purpose { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактные телефоны сотрудника, которого необходимо встретить\\отвезти")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Откуда забрать")]
        public string PlaceDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Данные рейса (номер, время)")]
        public string Flight { get; set; }

        [Display(Name = "Выставление счета за предоставление транспорта")]
        public bool Account { get; set; }

        [Display(Name = "Срок командирования")]
        public string Terms { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_PassangerTransportDayOff_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Время с")]
        public TimeSpan StartWorkTime { get; set; }

        [Display(Name = "Время по")]
        public TimeSpan EndWorkTime { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_PassangerTransportDayOffZIF_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Время с")]
        public TimeSpan StartWorkTime { get; set; }

        [Display(Name = "Время по")]
        public TimeSpan EndWorkTime { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_PassangerTransportCorporate_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Заказчик")]
        public string Client { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО ответственного лица за организацию поездки")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактный телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель, количество пассажиров")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public DateTime? DateCorporate { get; set; }

        [Display(Name = "Время с")]
        public TimeSpan StartWorkTime { get; set; }

        [Display(Name = "Время по")]
        public TimeSpan EndWorkTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Маршрут")]
        public string Route { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_RequestForEmergAuxiliaryTransportZIF_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Вспомогательная техника")]
        public string Transport { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Выполнение работ")]
        public string ExecutionWork { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Ответственное лицо из числа ИТР за работы (ФИО, контакты)")]
        public string EmplName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт предоставления транспорта")]
        public string PlaceDeparture { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Время с")]
        public TimeSpan StartWorkTime { get; set; }

        [Display(Name = "Время по")]
        public TimeSpan EndWorkTime { get; set; }

        [Display(Name = "Модель и номер авто")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_RequestForAuxiliaryTransportDayOffZIF_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Вспомогательная техника")]
        public string Transport { get; set; }

        [Display(Name = "В связи с")]
        public PurposeAuxiliaryTransportTrip PurposeAuxiliaryTransportTrip { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Выполнение работ")]
        public string ExecutionWork { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Ответственное лицо из числа ИТР за погрузочно-разгрузочные работы")]
        public string EmplName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт предоставления транспорта")]
        public string PlaceDeparture { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Время с")]
        public TimeSpan StartWorkTime { get; set; }

        [Display(Name = "Время по")]
        public TimeSpan EndWorkTime { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_RequestForAuxiliaryTransportWorkDaysZIF_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Вспомогательная техника")]
        public string Transport { get; set; }

        [Display(Name = "В связи с")]
        public PurposeAuxiliaryTransportTrip PurposeAuxiliaryTransportTrip { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Выполнение работ")]
        public string ExecutionWork { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Ответственное лицо из числа ИТР за погрузочно-разгрузочные работы")]
        public string EmplName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт предоставления транспорта")]
        public string PlaceDeparture { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Время с")]
        public TimeSpan StartWorkTime { get; set; }

        [Display(Name = "Время по")]
        public TimeSpan EndWorkTime { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }    
    }

    public class USR_REQ_YT_RequestForStandbyTransportZIF_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Заказчик (ФИО, подразделение)")]
        public string Client { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Время")]
        public TimeSpan StartWorkTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Маршрут")]
        public string Route { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель поездки")]
        public string Purpose { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_RequestForLightTransportTripManageZIF_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Display(Name = "Цель")]
        public PurposeTrip PurposeTrip { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Сотрудник")]
        public EmplTrip EmplTrip { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО сотрудника, которого необходимо встетить\\отвезти")]
        public string EmplName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактные телефоны сотрудника, которого необходимо встретить\\отвезти")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Откуда забрать")]
        public string PlaceDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Время (во сколько нужно быть в пункте назначения)")]
        public TimeSpan TimeDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Данные рейса (номер, время)")]
        public string Flight { get; set; }

        [Display(Name = "Срок командирования")]
        public string Terms { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_RequestForLightTransportTripDayOffZIF_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Время с")]
        public TimeSpan StartWorkTime { get; set; }

        [Display(Name = "Время по")]
        public TimeSpan EndWorkTime { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_RequestForPassangerTransportZIF_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Заказчик")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактный телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель, количество пассажиров")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Время с")]
        public TimeSpan StartWorkTime { get; set; }

        [Display(Name = "Время по")]
        public TimeSpan EndWorkTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Маршрут")]
        public string Route { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_RequestForPassangerTransportTripZIF_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пункт назначения")]
        public string Place { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата по")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО сотрудника, которого необходимо встретить\\сопровождать")]
        public string EmplName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактные телефоны сотрудника, которого необходимо встретить\\сопровождать")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Откуда забрать")]
        public string PlaceDeparture { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Время (во сколько нужно быть в пункте назначения)")]
        public TimeSpan TimeArrive { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Данные рейса (номер, время)")]
        public string Flight { get; set; }

        [Display(Name = "Срок командирования")]
        public string Terms { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_RequestForStandbyTransportUZL_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Заказчик (ФИО, подразделение)")]
        public string Client { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Время")]
        public TimeSpan StartWorkTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Маршрут")]
        public string Route { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель поездки")]
        public string Purpose { get; set; }

        [Display(Name = "Марка, номер автотранспорта")]
        public string NumberCar { get; set; }

        [Display(Name = "ФИО водителя автотранспорта")]
        public string NameDriverCar { get; set; }      
    }
    #endregion

    #region ХУ
    public class USR_REQ_HY_EmergencyPurposeTRU_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Представитель ФЭУ")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Исполнитель")]
        public string UserChooseManual2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цех, участок")]
        public string Department { get; set; }

        public string ItemName1 { get; set; }
        public string ItemName2 { get; set; }
        public string ItemName3 { get; set; }
        public string ItemName4 { get; set; }
        public string ItemName5 { get; set; }
        public string ItemName6 { get; set; }
        public string ItemName7 { get; set; }
        public string ItemName8 { get; set; }
        public string ItemName9 { get; set; }
        public string ItemName10 { get; set; }
        public string ItemName11 { get; set; }
        public string ItemName12 { get; set; }
        public string ItemName13 { get; set; }
        public string ItemName14 { get; set; }
        public string ItemName15 { get; set; }
        public string ItemName16 { get; set; }
        public string ItemName17 { get; set; }

        public string Unit1 { get; set; }
        public string Unit2 { get; set; }
        public string Unit3 { get; set; }
        public string Unit4 { get; set; }
        public string Unit5 { get; set; }
        public string Unit6 { get; set; }
        public string Unit7 { get; set; }
        public string Unit8 { get; set; }
        public string Unit9 { get; set; }
        public string Unit10 { get; set; }
        public string Unit11 { get; set; }
        public string Unit12 { get; set; }
        public string Unit13 { get; set; }
        public string Unit14 { get; set; }
        public string Unit15 { get; set; }
        public string Unit16 { get; set; }
        public string Unit17 { get; set; }

        public string Qty1 { get; set; }
        public string Qty2 { get; set; }
        public string Qty3 { get; set; }
        public string Qty4 { get; set; }
        public string Qty5 { get; set; }
        public string Qty6 { get; set; }
        public string Qty7 { get; set; }
        public string Qty8 { get; set; }
        public string Qty9 { get; set; }
        public string Qty10 { get; set; }
        public string Qty11 { get; set; }
        public string Qty12 { get; set; }
        public string Qty13 { get; set; }
        public string Qty14 { get; set; }
        public string Qty15 { get; set; }
        public string Qty16 { get; set; }
        public string Qty17 { get; set; }

        public string Price1 { get; set; }
        public string Price2 { get; set; }
        public string Price3 { get; set; }
        public string Price4 { get; set; }
        public string Price5 { get; set; }
        public string Price6 { get; set; }
        public string Price7 { get; set; }
        public string Price8 { get; set; }
        public string Price9 { get; set; }
        public string Price10 { get; set; }
        public string Price11 { get; set; }
        public string Price12 { get; set; }
        public string Price13 { get; set; }
        public string Price14 { get; set; }
        public string Price15 { get; set; }
        public string Price16 { get; set; }
        public string Price17 { get; set; }

        public string Amount1 { get; set; }
        public string Amount2 { get; set; }
        public string Amount3 { get; set; }
        public string Amount4 { get; set; }
        public string Amount5 { get; set; }
        public string Amount6 { get; set; }
        public string Amount7 { get; set; }
        public string Amount8 { get; set; }
        public string Amount9 { get; set; }
        public string Amount10 { get; set; }
        public string Amount11 { get; set; }
        public string Amount12 { get; set; }
        public string Amount13 { get; set; }
        public string Amount14 { get; set; }
        public string Amount15 { get; set; }
        public string Amount16 { get; set; }
        public string Amount17 { get; set; }

        public string Location1 { get; set; }
        public string Location2 { get; set; }
        public string Location3 { get; set; }
        public string Location4 { get; set; }
        public string Location5 { get; set; }
        public string Location6 { get; set; }
        public string Location7 { get; set; }
        public string Location8 { get; set; }
        public string Location9 { get; set; }
        public string Location10 { get; set; }
        public string Location11 { get; set; }
        public string Location12 { get; set; }
        public string Location13 { get; set; }
        public string Location14 { get; set; }
        public string Location15 { get; set; }
        public string Location16 { get; set; }
        public string Location17 { get; set; }

        public Months Month1 { get; set; }
        public Months Month2 { get; set; }
        public Months Month3 { get; set; }
        public Months Month4 { get; set; }
        public Months Month5 { get; set; }
        public Months Month6 { get; set; }
        public Months Month7 { get; set; }
        public Months Month8 { get; set; }
        public Months Month9 { get; set; }
        public Months Month10 { get; set; }
        public Months Month11 { get; set; }
        public Months Month12 { get; set; }
        public Months Month13 { get; set; }
        public Months Month14 { get; set; }
        public Months Month15 { get; set; }
        public Months Month16 { get; set; }
        public Months Month17 { get; set; }

        public string Reason1 { get; set; }
        public string Reason2 { get; set; }
        public string Reason3 { get; set; }
        public string Reason4 { get; set; }
        public string Reason5 { get; set; }
        public string Reason6 { get; set; }
        public string Reason7 { get; set; }
        public string Reason8 { get; set; }
        public string Reason9 { get; set; }
        public string Reason10 { get; set; }
        public string Reason11 { get; set; }
        public string Reason12 { get; set; }
        public string Reason13 { get; set; }
        public string Reason14 { get; set; }
        public string Reason15 { get; set; }
        public string Reason16 { get; set; }
        public string Reason17 { get; set; }

        public string AccountBZ1 { get; set; }
        public string AccountBZ2 { get; set; }
        public string AccountBZ3 { get; set; }
        public string AccountBZ4 { get; set; }
        public string AccountBZ5 { get; set; }
        public string AccountBZ6 { get; set; }
        public string AccountBZ7 { get; set; }
        public string AccountBZ8 { get; set; }
        public string AccountBZ9 { get; set; }
        public string AccountBZ10 { get; set; }
        public string AccountBZ11 { get; set; }
        public string AccountBZ12 { get; set; }
        public string AccountBZ13 { get; set; }
        public string AccountBZ14 { get; set; }
        public string AccountBZ15 { get; set; }
        public string AccountBZ16 { get; set; }
        public string AccountBZ17 { get; set; }

        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Description3 { get; set; }
        public string Description4 { get; set; }
        public string Description5 { get; set; }
        public string Description6 { get; set; }
        public string Description7 { get; set; }
        public string Description8 { get; set; }
        public string Description9 { get; set; }
        public string Description10 { get; set; }
        public string Description11 { get; set; }
        public string Description12 { get; set; }
        public string Description13 { get; set; }
        public string Description14 { get; set; }
        public string Description15 { get; set; }
        public string Description16 { get; set; }
        public string Description17 { get; set; }
    }

    public class USR_REQ_HY_BookingRoom_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Исполнитель")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер мобильного телефона")]
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Планируемый срок приезда")]
        public DateTime? DateArrival { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "с")]
        public DateTime? DateStart { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "по")]
        public DateTime? DateEnd { get; set; }

        [Display(Name = "Оплата")]
        public Payment Payment { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Название предполагаемой гостиницы")]
        public string HotelName { get; set; }
    }

    public class USR_REQ_HY_CreateStamp_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование печати/штампа")]
        public string StampName { get; set; }

        [Display(Name = "Количество")]
        public int Qty { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Обоснование (статья, бюджет)")]
        public string Reason { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Место хранения")]
        public string Location { get; set; }
    }

    public class USR_REQ_HY_FindApartment_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Исполнитель")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер мобильного телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Семейное положение")]
        public string MaritalStatus { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Кол-во детей, возраст")]
        public string KidsNumber { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Планируемый срок приезда")]
        public DateTime? DateArrival { get; set; }
    }

    public class USR_REQ_HY_RequestRepair_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Исполнитель")]
        public string UserChooseManual1 { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата возникновения поломки/неисправности")]
        public DateTime? DateIncident { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Описание поломки/неисправности")]
        public string Description { get; set; }
    }

    public class USR_REQ_HY_RequestTRU_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Представитель ФЭУ")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Исполнитель")]
        public string UserChooseManual2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цех, участок")]
        public string Department { get; set; }

        public string ItemName1 { get; set; }
        public string ItemName2 { get; set; }
        public string ItemName3 { get; set; }
        public string ItemName4 { get; set; }
        public string ItemName5 { get; set; }
        public string ItemName6 { get; set; }
        public string ItemName7 { get; set; }
        public string ItemName8 { get; set; }
        public string ItemName9 { get; set; }
        public string ItemName10 { get; set; }
        public string ItemName11 { get; set; }
        public string ItemName12 { get; set; }
        public string ItemName13 { get; set; }
        public string ItemName14 { get; set; }
        public string ItemName15 { get; set; }
        public string ItemName16 { get; set; }
        public string ItemName17 { get; set; }

        public string Unit1 { get; set; }
        public string Unit2 { get; set; }
        public string Unit3 { get; set; }
        public string Unit4 { get; set; }
        public string Unit5 { get; set; }
        public string Unit6 { get; set; }
        public string Unit7 { get; set; }
        public string Unit8 { get; set; }
        public string Unit9 { get; set; }
        public string Unit10 { get; set; }
        public string Unit11 { get; set; }
        public string Unit12 { get; set; }
        public string Unit13 { get; set; }
        public string Unit14 { get; set; }
        public string Unit15 { get; set; }
        public string Unit16 { get; set; }
        public string Unit17 { get; set; }

        public string Qty1 { get; set; }
        public string Qty2 { get; set; }
        public string Qty3 { get; set; }
        public string Qty4 { get; set; }
        public string Qty5 { get; set; }
        public string Qty6 { get; set; }
        public string Qty7 { get; set; }
        public string Qty8 { get; set; }
        public string Qty9 { get; set; }
        public string Qty10 { get; set; }
        public string Qty11 { get; set; }
        public string Qty12 { get; set; }
        public string Qty13 { get; set; }
        public string Qty14 { get; set; }
        public string Qty15 { get; set; }
        public string Qty16 { get; set; }
        public string Qty17 { get; set; }

        public string Price1 { get; set; }
        public string Price2 { get; set; }
        public string Price3 { get; set; }
        public string Price4 { get; set; }
        public string Price5 { get; set; }
        public string Price6 { get; set; }
        public string Price7 { get; set; }
        public string Price8 { get; set; }
        public string Price9 { get; set; }
        public string Price10 { get; set; }
        public string Price11 { get; set; }
        public string Price12 { get; set; }
        public string Price13 { get; set; }
        public string Price14 { get; set; }
        public string Price15 { get; set; }
        public string Price16 { get; set; }
        public string Price17 { get; set; }

        public string Amount1 { get; set; }
        public string Amount2 { get; set; }
        public string Amount3 { get; set; }
        public string Amount4 { get; set; }
        public string Amount5 { get; set; }
        public string Amount6 { get; set; }
        public string Amount7 { get; set; }
        public string Amount8 { get; set; }
        public string Amount9 { get; set; }
        public string Amount10 { get; set; }
        public string Amount11 { get; set; }
        public string Amount12 { get; set; }
        public string Amount13 { get; set; }
        public string Amount14 { get; set; }
        public string Amount15 { get; set; }
        public string Amount16 { get; set; }
        public string Amount17 { get; set; }

        public string Location1 { get; set; }
        public string Location2 { get; set; }
        public string Location3 { get; set; }
        public string Location4 { get; set; }
        public string Location5 { get; set; }
        public string Location6 { get; set; }
        public string Location7 { get; set; }
        public string Location8 { get; set; }
        public string Location9 { get; set; }
        public string Location10 { get; set; }
        public string Location11 { get; set; }
        public string Location12 { get; set; }
        public string Location13 { get; set; }
        public string Location14 { get; set; }
        public string Location15 { get; set; }
        public string Location16 { get; set; }
        public string Location17 { get; set; }

        public Months Month1 { get; set; }
        public Months Month2 { get; set; }
        public Months Month3 { get; set; }
        public Months Month4 { get; set; }
        public Months Month5 { get; set; }
        public Months Month6 { get; set; }
        public Months Month7 { get; set; }
        public Months Month8 { get; set; }
        public Months Month9 { get; set; }
        public Months Month10 { get; set; }
        public Months Month11 { get; set; }
        public Months Month12 { get; set; }
        public Months Month13 { get; set; }
        public Months Month14 { get; set; }
        public Months Month15 { get; set; }
        public Months Month16 { get; set; }
        public Months Month17 { get; set; }

        public string Reason1 { get; set; }
        public string Reason2 { get; set; }
        public string Reason3 { get; set; }
        public string Reason4 { get; set; }
        public string Reason5 { get; set; }
        public string Reason6 { get; set; }
        public string Reason7 { get; set; }
        public string Reason8 { get; set; }
        public string Reason9 { get; set; }
        public string Reason10 { get; set; }
        public string Reason11 { get; set; }
        public string Reason12 { get; set; }
        public string Reason13 { get; set; }
        public string Reason14 { get; set; }
        public string Reason15 { get; set; }
        public string Reason16 { get; set; }
        public string Reason17 { get; set; }

        public string AccountBZ1 { get; set; }
        public string AccountBZ2 { get; set; }
        public string AccountBZ3 { get; set; }
        public string AccountBZ4 { get; set; }
        public string AccountBZ5 { get; set; }
        public string AccountBZ6 { get; set; }
        public string AccountBZ7 { get; set; }
        public string AccountBZ8 { get; set; }
        public string AccountBZ9 { get; set; }
        public string AccountBZ10 { get; set; }
        public string AccountBZ11 { get; set; }
        public string AccountBZ12 { get; set; }
        public string AccountBZ13 { get; set; }
        public string AccountBZ14 { get; set; }
        public string AccountBZ15 { get; set; }
        public string AccountBZ16 { get; set; }
        public string AccountBZ17 { get; set; }

        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Description3 { get; set; }
        public string Description4 { get; set; }
        public string Description5 { get; set; }
        public string Description6 { get; set; }
        public string Description7 { get; set; }
        public string Description8 { get; set; }
        public string Description9 { get; set; }
        public string Description10 { get; set; }
        public string Description11 { get; set; }
        public string Description12 { get; set; }
        public string Description13 { get; set; }
        public string Description14 { get; set; }
        public string Description15 { get; set; }
        public string Description16 { get; set; }
        public string Description17 { get; set; }
    }

    public class USR_REQ_HY_EmergencyRequestTRU_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Исполнитель")]
        public string UserChooseManual1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цех, участок")]
        public string Department { get; set; }

        public string ItemName1 { get; set; }
        public string ItemName2 { get; set; }
        public string ItemName3 { get; set; }
        public string ItemName4 { get; set; }
        public string ItemName5 { get; set; }
        public string ItemName6 { get; set; }
        public string ItemName7 { get; set; }
        public string ItemName8 { get; set; }
        public string ItemName9 { get; set; }
        public string ItemName10 { get; set; }
        public string ItemName11 { get; set; }
        public string ItemName12 { get; set; }
        public string ItemName13 { get; set; }
        public string ItemName14 { get; set; }
        public string ItemName15 { get; set; }
        public string ItemName16 { get; set; }
        public string ItemName17 { get; set; }

        public string Unit1 { get; set; }
        public string Unit2 { get; set; }
        public string Unit3 { get; set; }
        public string Unit4 { get; set; }
        public string Unit5 { get; set; }
        public string Unit6 { get; set; }
        public string Unit7 { get; set; }
        public string Unit8 { get; set; }
        public string Unit9 { get; set; }
        public string Unit10 { get; set; }
        public string Unit11 { get; set; }
        public string Unit12 { get; set; }
        public string Unit13 { get; set; }
        public string Unit14 { get; set; }
        public string Unit15 { get; set; }
        public string Unit16 { get; set; }
        public string Unit17 { get; set; }

        public string Qty1 { get; set; }
        public string Qty2 { get; set; }
        public string Qty3 { get; set; }
        public string Qty4 { get; set; }
        public string Qty5 { get; set; }
        public string Qty6 { get; set; }
        public string Qty7 { get; set; }
        public string Qty8 { get; set; }
        public string Qty9 { get; set; }
        public string Qty10 { get; set; }
        public string Qty11 { get; set; }
        public string Qty12 { get; set; }
        public string Qty13 { get; set; }
        public string Qty14 { get; set; }
        public string Qty15 { get; set; }
        public string Qty16 { get; set; }
        public string Qty17 { get; set; }

        public string Price1 { get; set; }
        public string Price2 { get; set; }
        public string Price3 { get; set; }
        public string Price4 { get; set; }
        public string Price5 { get; set; }
        public string Price6 { get; set; }
        public string Price7 { get; set; }
        public string Price8 { get; set; }
        public string Price9 { get; set; }
        public string Price10 { get; set; }
        public string Price11 { get; set; }
        public string Price12 { get; set; }
        public string Price13 { get; set; }
        public string Price14 { get; set; }
        public string Price15 { get; set; }
        public string Price16 { get; set; }
        public string Price17 { get; set; }

        public string Amount1 { get; set; }
        public string Amount2 { get; set; }
        public string Amount3 { get; set; }
        public string Amount4 { get; set; }
        public string Amount5 { get; set; }
        public string Amount6 { get; set; }
        public string Amount7 { get; set; }
        public string Amount8 { get; set; }
        public string Amount9 { get; set; }
        public string Amount10 { get; set; }
        public string Amount11 { get; set; }
        public string Amount12 { get; set; }
        public string Amount13 { get; set; }
        public string Amount14 { get; set; }
        public string Amount15 { get; set; }
        public string Amount16 { get; set; }
        public string Amount17 { get; set; }

        public string Location1 { get; set; }
        public string Location2 { get; set; }
        public string Location3 { get; set; }
        public string Location4 { get; set; }
        public string Location5 { get; set; }
        public string Location6 { get; set; }
        public string Location7 { get; set; }
        public string Location8 { get; set; }
        public string Location9 { get; set; }
        public string Location10 { get; set; }
        public string Location11 { get; set; }
        public string Location12 { get; set; }
        public string Location13 { get; set; }
        public string Location14 { get; set; }
        public string Location15 { get; set; }
        public string Location16 { get; set; }
        public string Location17 { get; set; }

        public Months Month1 { get; set; }
        public Months Month2 { get; set; }
        public Months Month3 { get; set; }
        public Months Month4 { get; set; }
        public Months Month5 { get; set; }
        public Months Month6 { get; set; }
        public Months Month7 { get; set; }
        public Months Month8 { get; set; }
        public Months Month9 { get; set; }
        public Months Month10 { get; set; }
        public Months Month11 { get; set; }
        public Months Month12 { get; set; }
        public Months Month13 { get; set; }
        public Months Month14 { get; set; }
        public Months Month15 { get; set; }
        public Months Month16 { get; set; }
        public Months Month17 { get; set; }

        public string Reason1 { get; set; }
        public string Reason2 { get; set; }
        public string Reason3 { get; set; }
        public string Reason4 { get; set; }
        public string Reason5 { get; set; }
        public string Reason6 { get; set; }
        public string Reason7 { get; set; }
        public string Reason8 { get; set; }
        public string Reason9 { get; set; }
        public string Reason10 { get; set; }
        public string Reason11 { get; set; }
        public string Reason12 { get; set; }
        public string Reason13 { get; set; }
        public string Reason14 { get; set; }
        public string Reason15 { get; set; }
        public string Reason16 { get; set; }
        public string Reason17 { get; set; }

        public string AccountBZ1 { get; set; }
        public string AccountBZ2 { get; set; }
        public string AccountBZ3 { get; set; }
        public string AccountBZ4 { get; set; }
        public string AccountBZ5 { get; set; }
        public string AccountBZ6 { get; set; }
        public string AccountBZ7 { get; set; }
        public string AccountBZ8 { get; set; }
        public string AccountBZ9 { get; set; }
        public string AccountBZ10 { get; set; }
        public string AccountBZ11 { get; set; }
        public string AccountBZ12 { get; set; }
        public string AccountBZ13 { get; set; }
        public string AccountBZ14 { get; set; }
        public string AccountBZ15 { get; set; }
        public string AccountBZ16 { get; set; }
        public string AccountBZ17 { get; set; }

        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Description3 { get; set; }
        public string Description4 { get; set; }
        public string Description5 { get; set; }
        public string Description6 { get; set; }
        public string Description7 { get; set; }
        public string Description8 { get; set; }
        public string Description9 { get; set; }
        public string Description10 { get; set; }
        public string Description11 { get; set; }
        public string Description12 { get; set; }
        public string Description13 { get; set; }
        public string Description14 { get; set; }
        public string Description15 { get; set; }
        public string Description16 { get; set; }
        public string Description17 { get; set; }
    }
    #endregion

    #region Коммандировки
    public class USR_REQ_TRIP_RegistrationBusinessTripForeign_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник (ФИО)")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Должность")]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Подразделение")]
        public string Department { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Делопроизводитель")]
        public string UserChooseManual1 { get; set; }

        [Display(Name = "Категория командировки")]
        public CategoryTrip CategoryTrip { get; set; }

        [Display(Name = "Вид командировки")]
        public TypeTrip TypeTrip { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "по")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Количество дней")]
        public int Days { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель командировки")]
        public string Purpose { get; set; }

        [Display(Name = "Проезд")]
        public PassageTrip PassageTrip { get; set; }

        [Display(Name = "Организация")]
        public string Organization { get; set; }

        [Display(Name = "Проезд")]
        public PaymentTrip PaymentTripPassage { get; set; }

        [Display(Name = "Суточные")]
        public PaymentTrip PaymentTripDaily { get; set; }

        [Display(Name = "Проживание")]
        public PaymentTrip PaymentTripLive { get; set; }

        [Display(Name = "Примечание")]
        public string Description { get; set; }
    }

    public class USR_REQ_TRIP_RegistrationBusinessTripKZ_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Делопроизводитель")]
        public string UserChooseManual1 { get; set; }

        [Display(Name = "Категория командировки")]
        public CategoryTrip CategoryTrip { get; set; }

        [Display(Name = "Вид командировки")]
        public TypeTrip TypeTrip { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "по")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель командировки")]
        public string Purpose { get; set; }

        [Display(Name = "Организация")]
        public string Organization { get; set; }

        [Display(Name = "Примечание")]
        public string Description { get; set; }

        [Display(Name = "ФИО")]
        public string FIO1 { get; set; }

        [Display(Name = "ФИО")]
        public string FIO2 { get; set; }

        [Display(Name = "ФИО")]
        public string FIO3 { get; set; }

        [Display(Name = "ФИО")]
        public string FIO4 { get; set; }

        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType1 { get; set; }

        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType2 { get; set; }

        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType3 { get; set; }

        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType4 { get; set; }

        [Display(Name = "Направление")]
        public TripDirection TripDirection1 { get; set; }

        [Display(Name = "Направление")]
        public TripDirection TripDirection2 { get; set; }

        [Display(Name = "Направление")]
        public TripDirection TripDirection3 { get; set; }

        [Display(Name = "Направление")]
        public TripDirection TripDirection4 { get; set; }

        [Display(Name = "Количество суток")]
        public int Day1 { get; set; }

        [Display(Name = "Количество суток")]
        public int Day2 { get; set; }

        [Display(Name = "Количество суток")]
        public int Day3 { get; set; }

        [Display(Name = "Количество суток")]
        public int Day4 { get; set; }

        [Display(Name = "Количество суток проживания")]
        public int DayLive1 { get; set; }

        [Display(Name = "Количество суток проживания")]
        public int DayLive2 { get; set; }

        [Display(Name = "Количество суток проживания")]
        public int DayLive3 { get; set; }

        [Display(Name = "Количество суток проживания")]
        public int DayLive4 { get; set; }

        [Display(Name = "Проезд")]
        public TripPassage TripPassage1 { get; set; }

        [Display(Name = "Проезд")]
        public TripPassage TripPassage2 { get; set; }

        [Display(Name = "Проезд")]
        public TripPassage TripPassage3 { get; set; }

        [Display(Name = "Проезд")]
        public TripPassage TripPassage4 { get; set; }

        [Display(Name = "Сумма проезда")]
        public int TicketSum1 { get; set; }

        [Display(Name = "Сумма проезда")]
        public int TicketSum2 { get; set; }

        [Display(Name = "Сумма проезда")]
        public int TicketSum3 { get; set; }

        [Display(Name = "Сумма проезда")]
        public int TicketSum4 { get; set; }

        public int DayRate1 { get; set; }
        public int DayRate2 { get; set; }
        public int DayRate3 { get; set; }
        public int DayRate4 { get; set; }

        public int ResidenceRate1 { get; set; }
        public int ResidenceRate2 { get; set; }
        public int ResidenceRate3 { get; set; }
        public int ResidenceRate4 { get; set; }
    }

    public class USR_REQ_TRIP_RegistrationBusinessTripPP_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Делопроизводитель")]
        public string UserChooseManual1 { get; set; }

        [Display(Name = "Категория командировки")]
        public CategoryTrip CategoryTrip { get; set; }

        [Display(Name = "Вид командировки")]
        public TypeTrip TypeTrip { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "по")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель командировки")]
        public string Purpose { get; set; }

        [Display(Name = "Организация")]
        public string Organization { get; set; }

        [Display(Name = "Примечание")]
        public string Description { get; set; }

        [Display(Name = "ФИО")]
        public string FIO1 { get; set; }

        [Display(Name = "ФИО")]
        public string FIO2 { get; set; }

        [Display(Name = "ФИО")]
        public string FIO3 { get; set; }

        [Display(Name = "ФИО")]
        public string FIO4 { get; set; }

        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType1 { get; set; }

        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType2 { get; set; }

        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType3 { get; set; }

        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType4 { get; set; }

        [Display(Name = "Направление")]
        public TripDirection TripDirection1 { get; set; }

        [Display(Name = "Направление")]
        public TripDirection TripDirection2 { get; set; }

        [Display(Name = "Направление")]
        public TripDirection TripDirection3 { get; set; }

        [Display(Name = "Направление")]
        public TripDirection TripDirection4 { get; set; }

        [Display(Name = "Количество суток")]
        public int Day1 { get; set; }

        [Display(Name = "Количество суток")]
        public int Day2 { get; set; }

        [Display(Name = "Количество суток")]
        public int Day3 { get; set; }

        [Display(Name = "Количество суток")]
        public int Day4 { get; set; }

        [Display(Name = "Количество суток проживания")]
        public int DayLive1 { get; set; }

        [Display(Name = "Количество суток проживания")]
        public int DayLive2 { get; set; }

        [Display(Name = "Количество суток проживания")]
        public int DayLive3 { get; set; }

        [Display(Name = "Количество суток проживания")]
        public int DayLive4 { get; set; }

        [Display(Name = "Проезд")]
        public TripPassage TripPassage1 { get; set; }

        [Display(Name = "Проезд")]
        public TripPassage TripPassage2 { get; set; }

        [Display(Name = "Проезд")]
        public TripPassage TripPassage3 { get; set; }

        [Display(Name = "Проезд")]
        public TripPassage TripPassage4 { get; set; }

        [Display(Name = "Сумма проезда")]
        public int TicketSum1 { get; set; }

        [Display(Name = "Сумма проезда")]
        public int TicketSum2 { get; set; }

        [Display(Name = "Сумма проезда")]
        public int TicketSum3 { get; set; }

        [Display(Name = "Сумма проезда")]
        public int TicketSum4 { get; set; }

        public int DayRate1 { get; set; }
        public int DayRate2 { get; set; }
        public int DayRate3 { get; set; }
        public int DayRate4 { get; set; }

        public int ResidenceRate1 { get; set; }
        public int ResidenceRate2 { get; set; }
        public int ResidenceRate3 { get; set; }
        public int ResidenceRate4 { get; set; }
    }

    public class USR_REQ_TRIP_RegistrationBusinessTripPTY_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Сотрудник")]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Делопроизводитель")]
        public string UserChooseManual1 { get; set; }

        [Display(Name = "Категория командировки")]
        public CategoryTrip CategoryTrip { get; set; }

        [Display(Name = "Вид командировки")]
        public TypeTrip TypeTrip { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "с")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "по")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Цель командировки")]
        public string Purpose { get; set; }

        [Display(Name = "Организация")]
        public string Organization { get; set; }

        [Display(Name = "Примечание")]
        public string Description { get; set; }

        [Display(Name = "ФИО")]
        public string FIO1 { get; set; }

        [Display(Name = "ФИО")]
        public string FIO2 { get; set; }

        [Display(Name = "ФИО")]
        public string FIO3 { get; set; }

        [Display(Name = "ФИО")]
        public string FIO4 { get; set; }

        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType1 { get; set; }

        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType2 { get; set; }

        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType3 { get; set; }

        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType4 { get; set; }

        [Display(Name = "Направление")]
        public TripDirection TripDirection1 { get; set; }

        [Display(Name = "Направление")]
        public TripDirection TripDirection2 { get; set; }

        [Display(Name = "Направление")]
        public TripDirection TripDirection3 { get; set; }

        [Display(Name = "Направление")]
        public TripDirection TripDirection4 { get; set; }

        [Display(Name = "Количество суток")]
        public int Day1 { get; set; }

        [Display(Name = "Количество суток")]
        public int Day2 { get; set; }

        [Display(Name = "Количество суток")]
        public int Day3 { get; set; }

        [Display(Name = "Количество суток")]
        public int Day4 { get; set; }

        [Display(Name = "Количество суток проживания")]
        public int DayLive1 { get; set; }

        [Display(Name = "Количество суток проживания")]
        public int DayLive2 { get; set; }

        [Display(Name = "Количество суток проживания")]
        public int DayLive3 { get; set; }

        [Display(Name = "Количество суток проживания")]
        public int DayLive4 { get; set; }

        [Display(Name = "Проезд")]
        public TripPassage TripPassage1 { get; set; }

        [Display(Name = "Проезд")]
        public TripPassage TripPassage2 { get; set; }

        [Display(Name = "Проезд")]
        public TripPassage TripPassage3 { get; set; }

        [Display(Name = "Проезд")]
        public TripPassage TripPassage4 { get; set; }

        [Display(Name = "Сумма проезда")]
        public int TicketSum1 { get; set; }

        [Display(Name = "Сумма проезда")]
        public int TicketSum2 { get; set; }

        [Display(Name = "Сумма проезда")]
        public int TicketSum3 { get; set; }

        [Display(Name = "Сумма проезда")]
        public int TicketSum4 { get; set; }

        public int DayRate1 { get; set; }
        public int DayRate2 { get; set; }
        public int DayRate3 { get; set; }
        public int DayRate4 { get; set; }

        public int ResidenceRate1 { get; set; }
        public int ResidenceRate2 { get; set; }
        public int ResidenceRate3 { get; set; }
        public int ResidenceRate4 { get; set; }
    }
    public class USR_REQ_TRIP_RequestCalcDriveBTripCalsPP_View
    {
        public USR_REQ_TRIP_RequestCalcDriveBTripCalsPP_View(EmplTripType emplTripType, TripDirection tripDirection, int day, int dayLive, int ticketSum, int dayRate, int residenceRate)
        {
            EmplTripType = emplTripType;
            TripDirection = tripDirection;
            Day = day;
            DayLive = dayLive;
            TicketSum = ticketSum;
            DayRate = dayRate;
            ResidenceRate = residenceRate;
        }

        public EmplTripType EmplTripType { get; set; }
        public TripDirection TripDirection { get; set; }
        public int Day { get; set; }
        public int DayLive { get; set; }
        public int TicketSum { get; set; }
        public int DayRate { get; set; }
        public int ResidenceRate { get; set; }
    }

    public class USR_REQ_TRIP_RequestCalcDriveBTripCalsPTY_View
    {
        public USR_REQ_TRIP_RequestCalcDriveBTripCalsPTY_View(EmplTripType emplTripType, TripDirection tripDirection, int day, int dayLive, int ticketSum, int dayRate, int residenceRate)
        {
            EmplTripType = emplTripType;
            TripDirection = tripDirection;
            Day = day;
            DayLive = dayLive;
            TicketSum = ticketSum;
            DayRate = dayRate;
            ResidenceRate = residenceRate;
        }

        public EmplTripType EmplTripType { get; set; }
        public TripDirection TripDirection { get; set; }
        public int Day { get; set; }
        public int DayLive { get; set; }
        public int TicketSum { get; set; }
        public int DayRate { get; set; }
        public int ResidenceRate { get; set; }
    }

    public class USR_REQ_TRIP_RequestCalcDriveBTripCalsKZ_View
    {
        public USR_REQ_TRIP_RequestCalcDriveBTripCalsKZ_View(EmplTripType emplTripType, TripDirection tripDirection, int day, int dayLive, int ticketSum, int dayRate, int residenceRate)
        {
            EmplTripType = emplTripType;
            TripDirection = tripDirection;
            Day = day;
            DayLive = dayLive;
            TicketSum = ticketSum;
            DayRate = dayRate;
            ResidenceRate = residenceRate;
        }

        public EmplTripType EmplTripType { get; set; }
        public TripDirection TripDirection { get; set; }
        public int Day { get; set; }
        public int DayLive { get; set; }
        public int TicketSum { get; set; }
        public int DayRate { get; set; }
        public int ResidenceRate { get; set; }
    }
    #endregion

    #region Заявки Kazzink Holdings ИТ служба
    public class USK_REQ_IT_CTP_IncidentIT_View : BasicDocumentRequestView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Контактный номер телефона")]
        public string Phone { get; set; } 
    }
    #endregion

    #region Служебные записки

    public class USR_OFM_UIT_OfficeMemo_View : BasicDocumantOfficeMemoView
    {
    
    }
    #endregion

    #region Задачи

    public class USR_TAS_DailyTasks_View : BasicDailyTasksView
    {
        
    }

    public class USR_TAS_DailyTasksProlongation_View : BasicDocumentView
    {
        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Дата продления")]
        public DateTime? ProlongationDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }
       
        [Display(Name = "Текст задачи")]
        public string TextTask { get; set; }

        [DataType(DataType.Date)]        
        [Display(Name = "Дата исполнения")]
        public DateTime? ExecutionDate { get; set; }

        public Guid RefDocumentId { get; set; }

        [Display(Name = "Ссылка на исходный документ")]
        public string RefDocNum { get; set; }

    }
    #endregion
}