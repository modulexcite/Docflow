using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RapidDoc.Models.Repository;

namespace RapidDoc.Models.DomainModels
{
    public class USR_REQ_IT_CTS_DeliveryOfPinCode_Table : BasicDocumentRequestTable
    {
        [Required]
        public string Phone { get; set; }
    }

    public class USR_REQ_IT_CTS_DeliveryOfWS_Table : BasicDocumentRequestTable
    {
        public AllWSType WSType { get; set; }
    }

    public class USR_REQ_IT_CTS_DeliveryOfComponentsWS_Table : BasicDocumentRequestTable
    {
        public WSComponents WSComponents { get; set; }
    }

    public class USR_REQ_IT_CTS_DisassemblingOfWS_Table : BasicDocumentRequestTable
    {
        public WSType WSType { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CTS_ReplacementPhone_Table : BasicDocumentRequestTable
    {
        public PhoneType FromPhoneType { get; set; }
        public PhoneType ToPhoneType { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string Phone { get; set; }
    }

    public class USR_REQ_IT_CTS_ReplacementWorkPlace_Table : BasicDocumentTable
    {
        public WorkPlaceMovement WorkPlaceMovementType { get; set; }
        
        [Required]
        public string FromPlace { get; set; }

        [Required]
        public string ToPlace { get; set; }

        [Required]
        public string Users { get; set; }

        [Required]
        public string Phone { get; set; }
    }

    public class USR_REQ_IT_CTS_ReregistrationUserInPhoneSystem_Table : BasicDocumentTable
    {
        [Required]
        public string Phone { get; set; }

        [Required]
        public string Users { get; set; }
    }

    public class USR_REQ_IT_CTS_DeleteRezervationNumber_Table : BasicDocumentTable
    {
        [Required]
        public string Phone { get; set; }
        public ActionsPhone ActionsPhone { get; set; }

        [Required]
        public string PeriodTime { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CTS_SetUpPhone_Table : BasicDocumentRequestTable
    {
        public PhoneType PhoneType { get; set; }
        bool AccessPublic { get; set; }
    }

    public class USR_REQ_IT_CTS_ProblemWithPhone_Table : BasicDocumentTable
    {
        public ProblemTypeCTS ProblemType { get; set; }

        [Required]
        public string Problem { get; set; }

        [Required]
        public string Users { get; set; }

        [Required]
        public string Phone { get; set; }
    }

    public class USR_REQ_IT_CTS_SetUpDVO_Table : BasicDocumentTable
    {
        public ForwardType ForwardType { get; set; }
        public string ForwardPhone { get; set; }
        public int ForwardNumber { get; set; }

        [Required]
        public string Users { get; set; }

        [Required]
        public string Phone { get; set; }
    }

    public class USR_REQ_IT_CTS_DeliveryOfService_Table : BasicDocumentTable
    {
        public CTS_ServiceList ServiceList { get; set; }

        [Required]
        public string RequestText { get; set; }
    }

    public class USR_REQ_IT_CTS_SetPersonalButton_Table : BasicDocumentTable
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

        public string RequestText { get; set; }

        [Required]
        public string Users { get; set; }

        [Required]
        public string Phone { get; set; }
    }

    public class USR_REQ_IT_ERP_RequestPermission1C8Salary_Table : BasicDocumentRequestTable
    {

    }

    public class USR_REQ_IT_ERP_RequestPermission1C77_Table : BasicDocumentTable
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
        public AccessRightBasic AccessRight17 { get; set; }
        public AccessRightBasic AccessRight18 { get; set; }
        public AccessRightBasic AccessRight19 { get; set; }
        public AccessRightBasic AccessRight20 { get; set; }
        public AccessRightBasic AccessRight21 { get; set; }
        public AccessRightBasic AccessRight22 { get; set; }
        public AccessRightBasic AccessRight23 { get; set; }
        public AccessRightBasic AccessRight24 { get; set; }
        public AccessRightBasic AccessRight25 { get; set; }

        [Required]
        public string Users { get; set; }
    }

    public class USR_REQ_IT_ERP_RequestPermissionAccountingDAX_Table : BasicDocumentRequestTable
    {

    }

    public class USR_REQ_IT_ERP_RequestPermissionTreasuryDAX_Table : BasicDocumentRequestTable
    {

    }

    public class USR_REQ_IT_ERP_ModificationDAX_Table : BasicDocumentTable
    {
        [Required]
        public string RequestText { get; set; }

        public bool Treasury { get; set; }
        public bool Purchases { get; set; }
        public bool Finance { get; set; }
    }

    public class USR_REQ_IT_ERP_ChangeAnalyticalModel_Table : BasicDocumentTable
    {
        public string AccountNum { get; set; }
        public string TypeCosts { get; set; }
        public string TypeActivity { get; set; }
        public string Department { get; set; }
        public string Other { get; set; }
    }

    // Служба тех. поддержки
    public class USR_REQ_IT_CTP_EquipmentInstallation_Table : BasicDocumentTable
    {
        [Required]
        public string NameEquipment { get; set; }

        public int NumberEquipment { get; set; }

        [Required]
        public string Location { get; set; }

        public string Description { get; set; }
    }

    public class USR_REQ_IT_CTP_InstallNewComputer_Table : BasicDocumentTable
    {
        [Required]
        public string Location { get; set; }

        [Required]
        public string Users { get; set; }
    }

    public class USR_REQ_IT_CTP_InstallSoftware_Table : BasicDocumentTable
    {
        [Required]
        public string Software { get; set; }

        [Required]
        public string Users { get; set; }
    }

    public class USR_REQ_IT_CTP_RecoverySimCard_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Pnone { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CTP_IssueSimCard_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        public int Limit { get; set; }
    }

    public class USR_REQ_IT_CTP_IssueMaterial_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string ItemId { get; set; }

        [Required]
        public string ItemName { get; set; }

        public int Qty { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CTP_IssueStorage_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        public StorageType StorageType { get; set; }
        public StorageVolume StorageVolume { get; set; }

        public int Qty { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CTP_ReplaceCartridge_Table : BasicDocumentTable
    {
        [Required]
        public string ModelPrinter { get; set; }

        [Required]
        public string RassetId { get; set; }

        public int Qty { get; set; }

        [Required]
        public string Location { get; set; }
    }

    public class USR_REQ_IT_CTP_ReplaceComputer_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CTP_RequestEquipment_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
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

    public class USR_REQ_IT_CTP_ReissueComputer_Table : BasicDocumentTable
    {
        [Required]
        public string FromUsers { get; set; }

        [Required]
        public string ToUsers { get; set; }

        [Required]
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

    public class USR_REQ_IT_CTP_IncidentIT_Table : BasicDocumentRequestTable
    {
        [Required]
        public string Phone { get; set; }
    }

    // Служба администрирования и информационной безопасности
    public class USR_REQ_IT_CAP_RemoveSignLotus_Table : BasicDocumentTable
    {
        public DeleteSignLotus DeleteSignLotus { get; set; }

        [Required]
        public string AuthorDocument { get; set; }

        [Required]
        public string DocumentName { get; set; }

        public DateTime DocumentDate { get; set; }

        [Required]
        public string SignName { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_CreateSubscription_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string GroupName { get; set; }

        [Required]
        public string GroupUsers { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_CreateNetworkFolder_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string NetworkFolder { get; set; }

        [Required]
        public string Path { get; set; }

        public AccessRightBasic AccessRight { get; set; }
    }

    public class USR_REQ_IT_CAP_DelegationExchServ_Table : BasicDocumentTable
    {
        [Required]
        public string FromUsers { get; set; }

        [Required]
        public string ToUsers { get; set; }

        [Required]
        public string Reason { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class USR_REQ_IT_CAP_AddUserSubscription_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        public bool RTDUET_GMO { get; set; }
        public bool RTDUET_II_ST_DOIZM { get; set; } 
        public bool RTDUET_KKD { get; set; }
        public bool RTDUET_KSMD { get; set; } 
        public bool RTDUET_OSPViHH { get; set; }
        public bool RTDUET_OTDiI { get; set; }
        public bool RTDUET_RAZDFLOT { get; set; } 
        public bool GuideZIF { get; set; }
        public bool HR_Kokshetau { get; set; }
        public bool MeetingTB_ZIF { get; set; }
        public bool DispetcherGroup { get; set; }
        public bool InternalAuditGroup { get; set; }
        public bool Reservist2014 { get; set; }
        public bool RT_Duet_Comments { get; set; }
        public bool dailydigest { get; set; }
        public bool raigorodok { get; set; }
        public bool PISystemClients { get; set; }
        public bool Manager { get; set; }
        public bool IspolnitelnyeDirektoraATK { get; set; } 
        public bool Otan { get; set; }
        public bool NachalnikiUpravleniyATK { get; set; }
        public bool NachalnikiSluzhbATK { get; set; } 
        public bool SvodkaOTK { get; set; }
        public bool OperationalNotificationKazzinc { get; set; }
        public bool DispatchersDailyReports { get; set; }
        public bool ChiefAccountantATK { get; set; }

        public string Description { get; set; }
    }

    public class USR_REQ_IT_CAP_ChangePassAD_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_ChangePassLotus_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_UnlockUserAD_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessRightParagraf_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string IP { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessRightLotus_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        public bool Agreement { get; set; }
        public bool Orders { get; set; }
        public bool Control { get; set; }
        public bool Trips { get; set; }
        public bool Correspondence { get; set; }
        public bool MaterialsMeeting { get; set; }
        public bool MaterialsMeetingDirectorate { get; set; }
        public bool ProtocolSolutions { get; set; }
        public bool ProtocolSolutionsDirectorate { get; set; }
        public bool ProtocolManagementSolution { get; set; }
        public bool ProtocolSolutionsCommission { get; set; }
        public bool AgreementsSVD { get; set; }
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

        public string Description { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessSendLotus_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        public bool AllATKEmployees { get; set; }
        public bool ATKEmployees { get; set; }
        public bool TopManagement { get; set; }
        public bool ATKDirectionGroup { get; set; }
        public bool VicePresident { get; set; }
        public bool President { get; set; }
        public bool ATKManagementChiefs { get; set; }
        public bool ATKManagementGroup { get; set; }

        public AccessRightBasic AccessRight01 { get; set; }
        public AccessRightBasic AccessRight02 { get; set; }
        public AccessRightBasic AccessRight03 { get; set; }
        public AccessRightBasic AccessRight04 { get; set; }
        public AccessRightBasic AccessRight05 { get; set; }
        public AccessRightBasic AccessRight06 { get; set; }
        public AccessRightBasic AccessRight07 { get; set; }
        public AccessRightBasic AccessRight08 { get; set; }

        public string Description { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessRightFTP_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string FTP { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessRightNetworkFolder_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Path { get; set; }

        public AccessRightBasic AccessRight { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessRightInternet_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_AccessRightInternetZIF_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_DelegationDocflow_Table : BasicDocumentTable
    {
        [Required]
        public string FromUsers { get; set; }

        [Required]
        public string ToUsers { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string DocumentName { get; set; }
    }

    public class USR_REQ_IT_CAP_CreateUserAD_Table : BasicDocumentTable
    {
        public bool ActiveDirectory { get; set; }
        public bool Exchange { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime BirthDay { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Users { get; set; }

        public BlocksATK BlocksATK { get; set; }

        [Required]
        public string Department { get; set; }

        public DateTime? ToDate { get; set; }

        [Required]
        public string Contact { get; set; }
    }

    public class USR_REQ_IT_CAP_RecoveryData_Table : BasicDocumentTable
    {
        [Required]
        public string PathFile { get; set; }

        [Required]
        public string FileName { get; set; }

        public DateTime LastDate { get; set; }

        [Required]
        public string Contact { get; set; }
    }

    public class USR_REQ_IT_CAP_ArchiveMail_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Description { get; set; }
    }

    public class USR_REQ_IT_CAP_CreateUserLync_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string InternalPhone { get; set; }
    }

    public class USR_REQ_IT_CAP_CreateUserExchange_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Contact { get; set; }
    }

    public class USR_REQ_IT_CAP_CreateUserAutograf_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Contact { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_NoLinkInternet_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Problem { get; set; }

        [Required]
        public string Site { get; set; }
    }

    public class USR_REQ_IT_CAP_CapacityMail_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    public class USR_REQ_IT_CAP_HardSoftwareMaintenance_Table : BasicDocumentTable
    {
        public HardSoftwareMaintenance HardSoftwareMaintenance { get; set; }

        [Required]
        public string Description { get; set; }
    }
}