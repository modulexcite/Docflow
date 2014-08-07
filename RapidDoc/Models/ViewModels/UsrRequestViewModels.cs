using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RapidDoc.Models.Repository;

namespace RapidDoc.Models.ViewModels
{
    #region Заявки Связи
    public class USR_REQ_IT_CTS_DeliveryOfPinCode_View : BasicDocumentRequestView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
    }

    public class USR_REQ_IT_CTS_DeliveryOfWS_View : BasicDocumentView
    {
        [Display(Name = "Тип радиостанции")]
        public AllWSType WSType { get; set; }

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

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }

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

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Место установки")]
        public string RequestText { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Users", ResourceType = typeof(CustomRes.Custom))]
        public string Users { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Причина")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CTS_ReplacementWorkPlace_View : BasicDocumentView
    {
        [Display(Name = "Что необходимо сделать?")]
        public WorkPlaceMovement WorkPlaceMovementType { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "С рабочего места")]
        public string FromPlace { get; set; }

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

    #region Заявки ERP
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

        [Display(Name = "Другое")]
        public string Other { get; set; }
    }
    #endregion

    #region Заявки СТП
    public class USR_REQ_IT_CTP_EquipmentInstallation_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование оборудования")]
        public string NameEquipment { get; set; }

        [Display(Name = "Количество")]
        public int NumberEquipment { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Местоположение")]
        public string Location { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
    }

    public class USR_REQ_IT_CTP_InstallNewComputer_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Местоположение")]
        public string Location { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пользователи")]
        public string Users { get; set; }
    }

    public class USR_REQ_IT_CTP_InstallSoftware_View : BasicDocumentView
    {
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

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Местоположение")]
        public string Location { get; set; }
    }

    public class USR_REQ_IT_CTP_ReplaceComputer_View : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Пользователи")]
        public string Users { get; set; }

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
    #endregion

    #region Заявки САиИБ
    public class USR_REQ_IT_CAP_RemoveSignLotus_View : BasicDocumentView
    {
        [Display(Name = "Что необходимо удалить")]
        public DeleteSignLotus DeleteSignLotus { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Автор документа")] 
        public string AuthorDocument { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Наименование документа и база")]       
        public string DocumentName { get; set; }

        [Display(Name = "Дата/время документа")]
        public DateTime? DocumentDate { get; set; }

         [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ФИО согласовавшего")]  
        public string SignName { get; set; }

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

        [Display(Name = "Состав группы рассылки")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string GroupUsers { get; set; }

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

        [Display(Name = "Причина")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }

        [Display(Name = "Действует с")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "Действует по")]
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

        [Display(Name = "Описание")]
        public string Description { get; set; }
    }

    public class USR_REQ_IT_CAP_ChangePassAD_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [Display(Name = "Причина")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_ChangePassLotus_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [Display(Name = "Причина")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_UnlockUserAD_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [Display(Name = "Причина")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }
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

        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessRightInternet_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessRightInternetZIF_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

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
        public DateTime? FromDate { get; set; }

        [Display(Name = "До даты")]
        public DateTime? ToDate { get; set; }

        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }

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
        public string Users { get; set; }

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

        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_NoLinkInternet_View : BasicDocumentView
    {
        [Display(Name = "Пользователь")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Users { get; set; }

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

        [Display(Name = "Обоснование")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_HardSoftwareMaintenance_View : BasicDocumentView
    {
        [Display(Name = "Объект обслуживания")]
        public HardSoftwareMaintenance HardSoftwareMaintenance { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public string Description { get; set; }
    }
    #endregion
}