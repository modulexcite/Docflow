﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RapidDoc.Models.Repository;

namespace RapidDoc.Models.DomainModels
{
    #region Заявки Связи
    public class USR_REQ_IT_CTS_DeliveryOfPinCode_Table : BasicDocumentRequestTable
    {
        [Required]
        public string Phone { get; set; }

        public bool International { get; set; }
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
        public bool AccessPublic { get; set; }
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
    #endregion

    #region Заявки ERP
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
    #endregion

    #region Заявки СТП
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

    public class USR_REQ_IT_CTP_IncidentIT_Table : BasicDocumentRequestTable
    {
        [Required]
        public string Phone { get; set; }

        public string ServiceName { get; set; }
        public ServiceIncidientPriority ServiceIncidientPriority { get; set; }
        public ServiceIncidientLevel ServiceIncidientLevel { get; set; }
        public ServiceIncidientLocation ServiceIncidientLocation { get; set; }
    }

    public class USR_REQ_IT_CTP_RequestTRU_Table : BasicDocumentTable
    {
        public string UserChooseManual1 { get; set; }

        [Required]
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

    #region Заявки САиИБ
    public class USR_REQ_IT_CAP_RemoveSignLotus_Table : BasicDocumentTable
    {
        public DeleteSignLotus DeleteSignLotus { get; set; }

        [Required]
        public string AuthorDocument { get; set; }

        [Required]
        public string DocumentName { get; set; }

        [Required]
        public string DocumentBase { get; set; }

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

        [Required]
        public string ContactPhone { get; set; }
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

        [Required]
        public string ContactPhone { get; set; }
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

        public bool Request { get; set; }
        public bool TreatmentPeople { get; set; }
        public bool MaterialMeetingsTehkomitet { get; set; }
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
        public AccessRightBasic AccessRight14 { get; set; }
        public AccessRightBasic AccessRight15 { get; set; }
        public AccessRightBasic AccessRight16 { get; set; }

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

    public class USR_REQ_IT_CAP_AccessRightInternetBGP_Table : BasicDocumentTable
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

        [Required]
        public string Description { get; set; }
    }

    public class USR_REQ_IT_CAP_CreateUserADFreelance_Table : BasicDocumentTable
    {
        public bool ActiveDirectory { get; set; }

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

        public DateTime ToDate { get; set; }

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
        public string Name { get; set; }

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

    public class USR_REQ_IT_CAP_ChangeRoute_Table : BasicDocumentTable
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string OldRoute { get; set; }

        [Required]
        public string NewRoute { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string Contact { get; set; }
    }

    public class USR_REQ_IT_CAP_CreateUserLotus_Table : BasicDocumentTable
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
    }

    public class USR_REQ_IT_CAP_AddOrChangeTemplate_Table : BasicDocumentTable
    {
        public AddOrChange ActionType { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Contact { get; set; }

        public string Description { get; set; }
    }

    public class USR_REQ_IT_CAP_ChangeOrder_Table : BasicDocumentTable
    {
        [Required]
        public string OrderNum { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string Contact { get; set; }
    }

    public class USR_REQ_IT_CAP_ChangeOrderWage_Table : BasicDocumentTable
    {
        [Required]
        public string OrderNum { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string Contact { get; set; }
    }
    public class USR_REQ_IT_CAP_RequestForITWeekend_Table : BasicDocumentTable
    {
        [Required]
        public string Reason { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Period { get; set; }

        public string UserChooseManual1 { get; set; }
    }
    #endregion

    #region Зиф

    public class USR_REQ_ZIF_RequestForFuel_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }
    }

    public class USR_REQ_ZIF_RequestForSIZ_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }
    }

    public class USR_REQ_ZIF_RequestForItems_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }
    }

    public class USR_REQ_ZIF_RequestForItemsMech_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }
    }

    public class USR_REQ_ZIF_RequestForItemsEnerg_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }
    }

    public class USR_REQ_ZIF_RequestForItemsASYTP_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }
    }

    public class USR_REQ_ZIF_RequestForCarpenterWork_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }

        [Required]
        public string Contact { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string Available { get; set; }
    }

    public class USR_REQ_ZIF_RequestForCreatingItemsJDE_Table : BasicDocumentTable
    {

    }
    #endregion

    #region ОКС

    public class USR_REQ_OKS_RequestForTranslate_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }
        [Required]
        public string  Contact{ get; set; }
        [Required]
        public string Purpose { get; set; }
        [Required]
        public TranslateDirection Direction { get; set; }
        [Required]
        public int CountPage { get; set; }
        [Required]
        public ServiceIncidientPriority Priority { get; set; }
        [Required]
        public string ExplanationPriority { get; set; }
        [Required]
        public string Users { get; set; }
    }

    public class USR_REQ_OKS_RequestForTranslateKAZ_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Purpose { get; set; }
        [Required]
        public TranslateDirectionKAZ Direction { get; set; }
        [Required]
        public int CountPage { get; set; }
        [Required]
        public ServiceIncidientPriority Priority { get; set; }
        [Required]
        public string ExplanationPriority { get; set; }
        [Required]
        public string Users { get; set; }
    }

    public class USR_REQ_OKS_RequestForPrintBlank_Table : BasicDocumentTable
    {
        [Required]
        public string DateAndNumber { get; set; }
        [Required]
        public string Purpose { get; set; }
    }

    public class USR_REQ_OKS_RequestForArchive_Table : BasicDocumentTable
    {
        [Required]
        public string DataDoument { get; set; }
        [Required]
        public string Period { get; set; }
        [Required]
        public string Reason { get; set; }
    }
    public class USR_REQ_OKS_RequestForVisa_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Country { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
    }

    public class USR_REQ_OKS_RequestForTicket_Table : BasicDocumentTable
    {
        [Required]
        public string NameRus { get; set; }
        [Required]
        public string NameEng { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime DateBirth { get; set; }
        [Required]
        public string PassportNumber { get; set; }
        [Required]
        public string Validity { get; set; }
        [Required]
        public string Route { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string TimeDeparture { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public PayType PayType { get; set; }
    }
    public class USR_REQ_OKS_RequestForTicketPermission_Table : BasicDocumentTable
    {
        [Required]
        public string NameRus { get; set; }
        [Required]
        public string NameEng { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime DateBirth { get; set; }
        [Required]
        public string PassportNumber { get; set; }
        [Required]
        public string Validity { get; set; }
        [Required]
        public string Route { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string TimeDeparture { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public PayType PayType { get; set; }
    }
    #endregion

    #region РОГР

    public class USR_REQ_ROGR_RequestForMiningVehicle_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string Volume { get; set; }

        [Required]
        public string Person { get; set; }

        [Required]
        public string VehicleType { get; set; }
    }

    #endregion

    #region СГЭ ЗИФ

    public class USR_REQ_SGMZIF_RequestForRepair_Table : BasicDocumentTable
    {
        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Contact { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string Equipment { get; set; }

        [Required]
        public string DescriptionFail { get; set; }

        [Required]
        public string TypeEngine { get; set; }

        [Required]
        public string PowerEngine { get; set; }

        [Required]
        public string Speed { get; set; }

        [Required]
        public string Current { get; set; }

        [Required]
        public string Voltage { get; set; }

        [Required]
        public string SchemeConnection { get; set; }

        [Required]
        public string RateDefense { get; set; }

        [Required]
        public string InstallPerformance { get; set; }

        [Required]
        public string SerialNumber { get; set; }
    }

    #endregion

    #region ЮУ

    public class USR_REQ_JU_RequestForAssurance_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public DateTime? DateAssurance { get; set; }

        [Required]
        public string AimAssurance { get; set; }

        [Required]
        public string WhereAndWhom { get; set; }

        [Required]
        public int CountExamples { get; set; }
    }

    public class USR_REQ_JU_RequestForProxyDoc_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string JuName { get; set; }

        [Required]
        public string PhyName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Term { get; set; }
    }

    public class USR_REQ_JU_RequestForArchiveContract_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string NumberContract { get; set; }

        [Required]
        public DateTime DateContract { get; set; }

        [Required]
        public string NameClient { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public TypeJUDocument TypeDocument { get; set; }
    }

    public class USR_REQ_JU_RequestForApproveDoc_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public DateTime DateApprove { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public int CountExamples { get; set; }
    }

    public class USR_REQ_JU_RequestForNPA_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string NormalAct { get; set; }

        [Required]
        public DateTime NPADate { get; set; }

        [Required]
        public string FullName { get; set; }
    }

    public class USR_REQ_JU_RequestForExplanationNormalAct_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string NumberDoc { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string FullName { get; set; }

        public string OwnMind { get; set; }
    }

    public class USR_REQ_JU_RequestForExpertise_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }
    }
    #endregion

    #region ФЭУ

    public class USR_REQ_FEU_RequestForFinExpertise_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }
    }

    public class USR_REQ_FEU_RequestForCorrectCalendar_Table : BasicDocumentTable
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Foundation { get; set; }

        [Required]
        public string NumberDate { get; set; }

        [Required]
        public string Appointment { get; set; }

        [Required]
        public string Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public string Article { get; set; }

        [Required]
        public string Reason { get; set; }
    }

    #endregion

    #region УЭ

    public class USR_REQ_UE_RequestForOutputElectricalEqu_Table : BasicDocumentTable
    {
        [Required]
        public string Equipment { get; set; }

        [Required]
        public string Consumers { get; set; }

        [Required]
        public string ConsumersOff { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public DateTime BeginDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string AccidentTime { get; set; }

        [Required]
        public string Responsible { get; set; }
    }

    public class USR_REQ_UE_RequestForDismantling_Table : BasicDocumentTable
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string AvailableEqu { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string Person { get; set; }
    }

    public class USR_REQ_UE_RequestForForecastWater_Table : BasicDocumentTable
    { 
        [Required]
        public string Department { get; set; }

        [Required]
        public PipeName Pipe { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public DateTime BeginDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Explanation { get; set; }
        
    }

    public class USR_REQ_UE_RequestForElectricRepairs_Table : BasicDocumentTable
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string ContactPerson { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string NameEqu { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Availability { get; set; }

        [Required]
        public string Person { get; set; }
    }

    #endregion

    #region УСХ

    public class USR_REQ_USH_RequestForReclassification_Table : BasicDocumentTable
    {
        [Required]
        public string Explanation { get; set; }
    }

    #endregion

    #region УПБ

    public class USR_REQ_UBP_RequestForExportWastes_Table : BasicDocumentTable
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

    public class USR_REQ_UBP_RequestForGetConclusion_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }

        [Required]
        public string Explanation { get; set; }
    }

    public class USR_REQ_UBP_RequestForInstructionBIOT_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }

        
        public string UserChooseManual1 { get; set; }
    }
    #endregion

    #region КД
    public class USR_REQ_KD_RequestForCompetitonProc_Table : BasicDocumentTable
    {
        [Required]
        public string NamePurchase { get; set; }

        [Required]
        public string BudgetArticle { get; set; }

        public string Project { get; set; }

        [Required]
        public string Amount { get; set; }

        [Required]
        public string Circumstance { get; set; }

        [Required]
        public string Destination { get; set; }

        public string UserChooseManual1 { get; set; }
    }

    public class USR_REQ_KD_RequestForCompetitonProcUZL_Table : BasicDocumentTable
    {
        [Required]
        public string NamePurchase { get; set; }

        [Required]
        public string BudgetArticle { get; set; }

        public string Project { get; set; }

        [Required]
        public string Amount { get; set; }

        [Required]
        public string Circumstance { get; set; }

        [Required]
        public string Destination { get; set; }

        public string UserChooseManual1 { get; set; }
    }

    public class USR_REQ_KD_RequestForCompetitonProcServices_Table : BasicDocumentTable
    {
        [Required]
        public string NamePurchase { get; set; }

        [Required]
        public string BudgetArticle { get; set; }

        public string Project { get; set; }

        [Required]
        public string Amount { get; set; }

        [Required]
        public string Circumstance { get; set; }

        [Required]
        public string Destination { get; set; }

        public string UserChooseManual1 { get; set; }
    }

    public class USR_REQ_KD_RequestForCompetitonProcServicesBGP_Table : BasicDocumentTable
    {
        [Required]
        public string NamePurchase { get; set; }

        [Required]
        public string BudgetArticle { get; set; }

        public string Project { get; set; }

        [Required]
        public string Amount { get; set; }

        [Required]
        public string Circumstance { get; set; }

        [Required]
        public string Destination { get; set; }

        public string UserChooseManual1 { get; set; }
    }
    #endregion

    #region СК

    public class USR_REQ_SK_RequestForRegContactNonresident_Table : BasicDocumentTable
    {

        [Required]
        public string Users { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string  Department { get; set; }

        [Required]
        public ContragentType ContragentType { get; set; }

        [Required]
        public string ContragentName { get; set; }

        [Required]
        public string DataMainContract { get; set; }

        [Required]
        public string DataAdditionalContract { get; set; }
    }

    #endregion

    #region УБ

    public class USR_REQ_UB_RequestForImportTMCZIF_Table : BasicDocumentTable
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Count { get; set; }

        [Required]
        public string DeparturePlace { get; set; }

        [Required]
        public string DestinationPlace { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string NamesPeople { get; set; }

        [Required]
        public string Aim { get; set; }

        [Required]
        public string Date { get; set; }

        public bool Post1 { get; set; }
        public bool Post2 { get; set; }
        public bool Post3 { get; set; }
        public bool Post4 { get; set; }
        public bool Post5 { get; set; }
        public bool Post6 { get; set; }
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForImportORZZIF_Table : BasicDocumentTable
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Count { get; set; }

        [Required]
        public string DeparturePlace { get; set; }

        [Required]
        public string DestinationPlace { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string NamesPeople { get; set; }

        [Required]
        public string Aim { get; set; }

        [Required]
        public string Date { get; set; }

        public bool Post1 { get; set; }
        public bool Post2 { get; set; }
        public bool Post3 { get; set; }
        public bool Post4 { get; set; }
        public bool Post5 { get; set; }
        public bool Post6 { get; set; }
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForImportTMCNoneZIF_Table : BasicDocumentTable
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Count { get; set; }

        [Required]
        public string DeparturePlace { get; set; }

        [Required]
        public string DestinationPlace { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string NamesPeople { get; set; }

        [Required]
        public string Aim { get; set; }

        [Required]
        public string Date { get; set; }

        public bool Post1 { get; set; }
        public bool Post2 { get; set; }
        public bool Post3 { get; set; }
        public bool Post4 { get; set; }
        public bool Post5 { get; set; }
        public bool Post6 { get; set; }
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForImportTMCUZL_Table : BasicDocumentTable
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Count { get; set; }

        [Required]
        public string DeparturePlace { get; set; }

        [Required]
        public string DestinationPlace { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string NamesPeople { get; set; }

        [Required]
        public string Aim { get; set; }

        [Required]
        public string Date { get; set; }

        public bool Post1 { get; set; }
        public bool Post2 { get; set; }
        public bool Post3 { get; set; }
        public bool Post4 { get; set; }
        public bool Post5 { get; set; }
        public bool Post6 { get; set; }
        public bool Post7 { get; set; }

    }

    public class USR_REQ_UB_RequestForInOutNotebook_Table : BasicDocumentTable
    {
        [Required]
        public string Organization { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ObjectAccess ObjectAccess { get; set; }

        [Required]
        public string Aim { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }
    }

    public class USR_REQ_UB_RequestForCarAccess_Table : BasicDocumentTable
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }
    }

    public class USR_REQ_UB_RequestForExportAsset_Table : BasicDocumentTable
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string Count { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string DeparturePlace { get; set; }

        [Required]
        public string DestinationPlace { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string NamesPeople { get; set; }

        [Required]
        public string Aim { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string DateReturnOC { get; set; }

        public bool Post1 { get; set; }
        public bool Post2 { get; set; }
        public bool Post3 { get; set; }
        public bool Post4 { get; set; }
        public bool Post5 { get; set; }
        public bool Post6 { get; set; }
        public bool Post7 { get; set; }

    }

    public class USR_REQ_UB_RequestForExportZIFOre_Table : BasicDocumentTable
    {
        [Required]
        public string Count { get; set; }

        [Required]
        public string DestinationPlace { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string NamesPeople { get; set; }

        [Required]
        public string Aim { get; set; }

        [Required]
        public string Date { get; set; }

        public bool Post1 { get; set; }
        public bool Post2 { get; set; }
        public bool Post3 { get; set; }
        public bool Post4 { get; set; }
        public bool Post5 { get; set; }
        public bool Post6 { get; set; }
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForExportOSPVHZIF_Table : BasicDocumentTable
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Count { get; set; }

        [Required]
        public string DeparturePlace { get; set; }

        [Required]
        public string DestinationPlace { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string NamesPeople { get; set; }

        [Required]
        public string Aim { get; set; }

        [Required]
        public string Date { get; set; }

        public bool Post1 { get; set; }
        public bool Post2 { get; set; }
        public bool Post3 { get; set; }
        public bool Post4 { get; set; }
        public bool Post5 { get; set; }
        public bool Post6 { get; set; }
        public bool Post7 { get; set; }
    }


    public class USR_REQ_UB_RequestForExportItems_Table : BasicDocumentTable
    {
        [Required]
        public string ItemName { get; set; }

        [Required]
        public string ItemNumber { get; set; }

        [Required]
        public string DeparturePlace { get; set; }

        [Required]
        public Warehouse Warehouse { get; set; }

        [Required]
        public string Count { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string NamesMOL { get; set; }

        [Required]
        public string DestinationPlace { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string NamesPeople { get; set; }

        [Required]
        public string Aim { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string DateReturnOC { get; set; }
        
        public bool Post1 { get; set; }  
        public bool Post2 { get; set; }
        public bool Post3 { get; set; }
        public bool Post4 { get; set; }
        public bool Post5 { get; set; }
        public bool Post6 { get; set; }
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForHU_Table : BasicDocumentTable
    {
        [Required]
        public string ItemName { get; set; }

        [Required]
        public string DeparturePlace { get; set; }

        [Required]
        public Warehouse Warehouse { get; set; }

        [Required]
        public string Count { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string NamesMOL { get; set; }

        [Required]
        public string DestinationPlace { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string NamesPeople { get; set; }

        [Required]
        public string Aim { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string DateReturnOC { get; set; }

        public bool Post1 { get; set; }
        public bool Post2 { get; set; }
        public bool Post3 { get; set; }
        public bool Post4 { get; set; }
        public bool Post5 { get; set; }
        public bool Post6 { get; set; }
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForMovementItems_Table : BasicDocumentTable
    {
        [Required]
        public string ItemName { get; set; }

        [Required]
        public string ItemNumber { get; set; }

        [Required]
        public string DeparturePlace { get; set; }

        [Required]
        public Warehouse Warehouse { get; set; }

        [Required]
        public string Count { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string DestinationPlace { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string NamesPeople { get; set; }

        [Required]
        public string Aim { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string DateReturnOC { get; set; }

        public bool Post1 { get; set; }
        public bool Post2 { get; set; }
        public bool Post3 { get; set; }
        public bool Post4 { get; set; }
        public bool Post5 { get; set; }
        public bool Post6 { get; set; }
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForMovementAssets_Table : BasicDocumentTable
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string Count { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string DeparturePlace { get; set; }

        [Required]
        public string DestinationPlace { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string NamesPeople { get; set; }

        [Required]
        public string Aim { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string DateReturnOC { get; set; }

        public bool Post1 { get; set; }
        public bool Post2 { get; set; }
        public bool Post3 { get; set; }
        public bool Post4 { get; set; }
        public bool Post5 { get; set; }
        public bool Post6 { get; set; }
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForTemporaryORZ_Table : BasicDocumentTable
    {
        [Required]
        public string Aim { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public string Time { get; set; }
    }

    public class USR_REQ_UB_RequestForTemporaryAccess_Table : BasicDocumentTable
    {
        [Required]
        public ObjectAccess ObjectAccess { get; set; }
        
        [Required]
        public string Aim { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

    }

    public class USR_REQ_UB_RequestForExportItemFromZIF_Table : BasicDocumentTable
    {
        [Required]
        public string ItemName { get; set; }

        [Required]
        public string ItemNumber { get; set; }

        [Required]
        public string DeparturePlace { get; set; }

        [Required]
        public string DestinationPlace { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string NamesPeople { get; set; }

        [Required]
        public string Aim { get; set; }

        [Required]
        public string Date { get; set; }


        public bool Post1 { get; set; }
        public bool Post2 { get; set; }
        public bool Post3 { get; set; }
        public bool Post4 { get; set; }
        public bool Post5 { get; set; }
        public bool Post6 { get; set; }
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForExportItemFromORZ_Table : BasicDocumentTable
    {
        [Required]
        public string ItemName { get; set; }

        [Required]
        public string ItemNumber { get; set; }

        [Required]
        public string DeparturePlace { get; set; }

        [Required]
        public string DestinationPlace { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string NamesPeople { get; set; }

        [Required]
        public string Aim { get; set; }

        [Required]
        public string Date { get; set; }

        public bool Post1 { get; set; }
        public bool Post2 { get; set; }
        public bool Post3 { get; set; }
        public bool Post4 { get; set; }
        public bool Post5 { get; set; }
        public bool Post6 { get; set; }
        public bool Post7 { get; set; }
    }

    public class USR_REQ_UB_RequestForPhotoRealization_Table : BasicDocumentTable
    {
        [Required]
        public DateTime FromPhotoDate { get; set; }

        [Required]
        public DateTime ToPhotoDate { get; set; }

        [Required]
        public PhotoType PhotoType { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public string Target { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Equipment { get; set; }
    }
    #endregion 

    #region УБУиО

    public class USR_REQ_UBUO_RequestForInspectionPropertyItems_Table : BasicDocumentTable
    { 
        
    }

    public class USR_REQ_UBUO_RequestForInspectionPropertyAssets_Table : BasicDocumentTable
    {

    }

    public class USR_REQ_UBUO_RequestForClearenceLetter_Table : BasicDocumentTable
    {
        [Required]
        public string User { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Contract { get; set; }

        [Required]
        public string NameItem { get; set; }

        [Required]
        public string Count { get; set; }

        [Required]
        public DateTime LetterDate { get; set; }

        [Required]
        public string Amount { get; set; }
    }

    public class USR_REQ_UBUO_RequestForReferenceTax_Table : BasicDocumentTable
    {
        [Required]
        public string Customer { get; set; }

        [Required]
        public string ForNotice { get; set; }
        
        [Required]
        public string Aim { get; set; }    
    }

    public class USR_REQ_UBUO_RequestCreateSettlView_Table : BasicDocumentTable
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

    public class USR_REQ_UBUO_RequestCalcDriveTrip_Table : BasicDocumentTable
    {
        public string OrderNum { get; set; }
        public DateTime OrderDate { get; set; }

        public string FIO1 { get; set; }
        public string FIO2 { get; set; }
        public string FIO3 { get; set; }
        public string FIO4 { get; set; }

        public EmplTripType EmplTripType1 { get; set; }
        public EmplTripType EmplTripType2 { get; set; }
        public EmplTripType EmplTripType3 { get; set; }
        public EmplTripType EmplTripType4 { get; set; }

        public TripDirection TripDirection1 { get; set; }
        public TripDirection TripDirection2 { get; set; }
        public TripDirection TripDirection3 { get; set; }
        public TripDirection TripDirection4 { get; set; }

        public int Day1 { get; set; }
        public int Day2 { get; set; }
        public int Day3 { get; set; }
        public int Day4 { get; set; }

        public int DayLive1 { get; set; }
        public int DayLive2 { get; set; }
        public int DayLive3 { get; set; }
        public int DayLive4 { get; set; }

        public TripPassage TripPassage1 { get; set; }
        public TripPassage TripPassage2 { get; set; }
        public TripPassage TripPassage3 { get; set; }
        public TripPassage TripPassage4 { get; set; }

        public int TicketSum1 { get; set; }
        public int TicketSum2 { get; set; }
        public int TicketSum3 { get; set; }
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
    #endregion

    #region УЗЛ

    public class USR_REQ_UZL_RequestForAccident_Table : BasicDocumentTable
    {
        [Required]
        public string Reason { get; set; }

        [Required]
        public string ReasonAbsent { get; set; }

        [Required]
        public string FinancialSource { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public string Amount { get; set; }
    }

    public class USR_REQ_UZL_RequestForUnscheduledIB_Table : BasicDocumentTable
    {
        [Required]
        public string Reason { get; set; }

        [Required]
        public string ReasonAbsent { get; set; }

        [Required]
        public string FinancialSource { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public string Amount { get; set; }
    }

    public class USR_REQ_UZL_RequestForUnscheduledOB_Table : BasicDocumentTable
    {
        [Required]
        public string Reason { get; set; }

        [Required]
        public string ReasonAbsent { get; set; }

        [Required]
        public string FinancialSource { get; set; }

        [Required]
        public DateTime? FromDate { get; set; }

        [Required]
        public DateTime? ToDate { get; set; }

        [Required]
        public string Amount { get; set; }
    }

    public class USR_REQ_UZL_RequestForIlliquid_Table : BasicDocumentTable
    {

        [Required]
        public string Department { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        public string ItemCount { get; set; }

        [Required]
        public string Event { get; set; }

        [Required]
        public DateTime? Date { get; set; }

    }

    public class USR_REQ_UZL_RequestForPeopleAcceptanceItems_Table : BasicDocumentTable
    {

        [Required]
        public string Iniciator { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string ContactPhone { get; set; }

        [Required]
        public string Theme { get; set; }

        public string UserChooseManual1 { get; set; }


        public string UserChooseManual2 { get; set; }


        public string UserChooseManual3 { get; set; }


        public string UserChooseManual4 { get; set; }


        public string Department1 { get; set; }


        public string Department2 { get; set; }


        public string Department3 { get; set; }


        public string Department4 { get; set; }


        public string UserChooseManual5 { get; set; }


        public string UserChooseManual6 { get; set; }


        public string UserChooseManual7 { get; set; }


        public string UserChooseManual8 { get; set; }


        public string Contact1 { get; set; }


        public string Contact2 { get; set; }


        public string Contact3 { get; set; }


        public string Contact4 { get; set; }

    }

    public class USR_REQ_UZL_RequestForContractNoneresident_Table : BasicDocumentTable
    {

        public string UserChooseManual1 { get; set; }

    }

    public class USR_REQ_UZL_RequestForContractNoneresidentCustoms_Table : BasicDocumentTable
    {

        public string UserChooseManual1 { get; set; }

    }

    public class USR_REQ_UZL_RequestForContractResident_Table : BasicDocumentTable
    {

        public string UserChooseManual1 { get; set; }

    }

    public class USR_REQ_UZL_RequestForrepresentationKD_Table : BasicDocumentTable
    {
        public string UserChooseManual1 { get; set; }
    }

    public class USR_REQ_UZL_RequestForUT_Table : BasicDocumentTable
    {
        [Required]
        public string Reason { get; set; }

        [Required]
        public string FinancialSource { get; set; }

    }

    public class USR_REQ_UZL_RequestForExtraIBBGP_Table : BasicDocumentTable
    {
        [Required]
        public string Explanation { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string FinancialSource { get; set; }

        [Required]
        public DateTime? FromDate { get; set; }

        [Required]
        public DateTime? ToDate { get; set; }

        [Required]
        public string Amount { get; set; }


    }

    public class USR_REQ_UZL_RequestForExtraIBZIF_Table : BasicDocumentTable
    {
        [Required]
        public string Explanation { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string FinancialSource { get; set; }

        [Required]
        public DateTime? FromDate { get; set; }

        [Required]
        public DateTime? ToDate { get; set; }

        [Required]
        public string Amount { get; set; }
    }

    public class USR_REQ_UZL_RequestForExtraOBBGP_Table : BasicDocumentTable
    {
        [Required]
        public string Explanation { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string FinancialSource { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public string Amount { get; set; }
    }

    public class USR_REQ_UZL_RequestForExtraOBZIF_Table : BasicDocumentTable
    {
        [Required]
        public string Explanation { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string FinancialSource { get; set; }

        [Required]
        public DateTime? FromDate { get; set; }

        [Required]
        public DateTime? ToDate { get; set; }

        [Required]
        public string Amount { get; set; }
    }

    public class USR_REQ_UZL_RequestForCrashedStone_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }

        [Required]
        public string Explanation { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public string Purpose { get; set; }

    }
    #endregion

    #region УКР

    public class USR_REQ_UKR_RequestForExpertiseDKU_Table : BasicDocumentTable
    {
        [Required]
        public string UserExecutive { get; set; }

        [Required]
        public string Department { get; set; }
    }

    public class USR_REQ_UKR_RequestForExpertiseInstruction_Table : BasicDocumentTable
    {
        [Required]
        public string UserExecutive { get; set; }

        [Required]
        public string Department { get; set; }

        public string UserChooseManual1 { get; set; }
    }

    public class USR_REQ_UKR_RequestForExpertiseDepartment_Table : BasicDocumentTable
    {
        [Required]
        public string UserExecutive { get; set; }

        [Required]
        public string Department { get; set; }

        public string UserChooseManual1 { get; set; }
    }


    #endregion

    #region УРП

    public class USR_REQ_URP_RequestForKindergarten_Table : BasicDocumentTable
    {
        public TeachGroup TeachGroup { get; set; }

        [Required]
        public string ChildName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public RelateRate RelateRate { get; set; }

        [Required]
        public string FatherName { get; set; }

        [Required]
        public string FatherWorkPlace { get; set; }

        [Required]
        public string MotherName { get; set; }

        [Required]
        public string MotherWorkPlace { get; set; }

        [Required]
        public string ActualAddress { get; set; }

        [Required]
        public string Contact { get; set; }
    }

    public class USR_REQ_URP_RequestForResponsibilities_Table : BasicDocumentTable
    {
        [Required]
        public string Reason { get; set; }

        [Required]
        public string UsersOff { get; set; }

        [Required]
        public string DepartmentOff { get; set; }

        [Required]
        public string ScheduleOff{ get; set; }

        [Required]
        public string UsersOn { get; set; }

        [Required]
        public string DepartmentOn { get; set; }

        [Required]
        public string ScheduleOn { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public string Amount { get; set; }

        public string UserChooseManual1 { get; set; }
    }

    public class USR_REQ_URP_RequestForResponsibilitiesSOTB_Table : BasicDocumentTable
    {
        [Required]
        public string Reason { get; set; }

        [Required]
        public string UsersOff { get; set; }

        [Required]
        public string DepartmentOff { get; set; }

        [Required]
        public string ScheduleOff { get; set; }

        [Required]
        public string UsersOn { get; set; }

        [Required]
        public string DepartmentOn { get; set; }

        [Required]
        public string ScheduleOn { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public string Amount { get; set; }

        public string UserChooseManual1 { get; set; }
    }

    public class USR_REQ_URP_RequestForPrepayMaster_Table : BasicDocumentTable
    {

        [Required]
        public string Reason { get; set; }

        [Required]
        public string FIO { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Schedule { get; set; }

        [Required]
        public DateTime DocDate { get; set; }

        [Required]
        public string Percent { get; set; }

        [Required]
        public string Department { get; set; }
    }

    public class USR_REQ_URP_RequestForAccrualExceptZIFBGP_Table : BasicDocumentTable
    {

        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string Period { get; set; }
    }

    public class USR_REQ_URP_RequestForAccrualBGP_Table : BasicDocumentTable
    {

        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string Period { get; set; }
    }

    public class USR_REQ_URP_RequestForAccrualZIF_Table : BasicDocumentTable
    {

        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string Period { get; set; }
    }

    public class USR_REQ_URP_RequestForAccrualSM_Table : BasicDocumentTable
    {

        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string Period { get; set; }
    }

    public class USR_REQ_URP_RequestForAccrualPTU_Table : BasicDocumentTable
    {

        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string Period { get; set; }
    }

    public class USR_REQ_URP_RequestForTeaching_Table : BasicDocumentTable
    {
        [Required]
        public string ToUsers { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string Theme { get; set; }

        [Required]
        public DateTime DocDate { get; set; }

        [Required]
        public string Place { get; set; }

        public string Company { get; set; }
    }

    public class USR_REQ_URP_RequestForWithdrawVocationITR1_Table : BasicDocumentTable
    {
        [Required]
        public string Employee { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForWithdrawVocationITR2_Table : BasicDocumentTable
    {
        [Required]
        public string Employee { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForWVAuxiliaryBlock_Table : BasicDocumentTable
    {
        [Required]
        public string Employee { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForWVMiningBlock_Table : BasicDocumentTable
    {
        [Required]
        public string Employee { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForWVFinBlock_Table : BasicDocumentTable
    {
        [Required]
        public string Employee { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForWVStraight_Table : BasicDocumentTable
    {
        [Required]
        public string Employee { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForForeignVisa_Table : BasicDocumentTable
    {

        [Required]
        public string Department { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string DateAndPlace { get; set; }

        [Required]
        public string Citizenship { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        public string Passport { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PlaceVisa { get; set; }

        [Required]
        public string Period { get; set; }

        [Required]
        public string Multiplicity { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public string Route { get; set; }
    }

    public class USR_REQ_URP_RequestForTransferVocationITR1_Table : BasicDocumentTable
    {
        [Required]
        public string Employee { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string PlanDate { get; set; }

        [Required]
        public string TransferDate { get; set; }

        [Required]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForTransferVocationITR2_Table : BasicDocumentTable
    {
        [Required]
        public string Employee { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string PlanDate { get; set; }

        [Required]
        public string TransferDate { get; set; }

        [Required]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForTransfVacWorkerMining_Table : BasicDocumentTable
    {

        [Required]
        public string Employee { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string PlanDate { get; set; }

        [Required]
        public string TransferDate { get; set; }

        [Required]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForTransfVacWorkerStraight_Table : BasicDocumentTable
    {

        [Required]
        public string Employee { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string PlanDate { get; set; }

        [Required]
        public string TransferDate { get; set; }

        [Required]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForTransfVacWorkerFin_Table : BasicDocumentTable
    {

        [Required]
        public string Employee { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string PlanDate { get; set; }

        [Required]
        public string TransferDate { get; set; }

        [Required]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForTransfVacWorkerAuxiliary_Table : BasicDocumentTable
    {

        [Required]
        public string Employee { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string PlanDate { get; set; }

        [Required]
        public string TransferDate { get; set; }

        [Required]
        public string Reason { get; set; }

    }

    public class USR_REQ_URP_RequestForProvisionGraphVac_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        public string UserChooseManual2 { get; set; }
    }

    public class USR_REQ_URP_RequestForProvisionGraphExit_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string Period { get; set; }
    }

    public class USR_REQ_URP_RequestForWeekend_Table : BasicDocumentTable
    {
        [Required]
        public string Reason { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Employee { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Period { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }
    }

    public class USR_REQ_URP_RequestForReduction_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }

        [Required]
        public string Period { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

    }

    public class USR_REQ_URP_RequestForReductionCandidate_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }

        [Required]
        public string Period { get; set; }

    }

    public class USR_REQ_URP_RequestForReductionTB_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string Bulk { get; set; }

        [Required]
        public string Period { get; set; }

        [Required]
        public string Reason { get; set; }


    }

    public class USR_REQ_URP_RequestForReductionThird_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        [Required]
        public string UserChooseManual2 { get; set; }

        [Required]
        public string UserChooseManual3 { get; set; }

        [Required]
        public string Period { get; set; }
    }

    public class USR_REQ_URP_RequestForSelectionPersonal_Table : BasicDocumentTable
    {
        [Required]
        public string Position { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public DateTime ClosedDate { get; set; }

        [Required]
        public string Possibility { get; set; }

        [Required]
        public string Executive { get; set; }

        [Required]
        public string ContactPerson { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Age { get; set; }

        [Required]
        public string Education { get; set; }

        [Required]
        public string Experience { get; set; }

        [Required]
        public string Requirement { get; set; }

        [Required]
        public string Skills { get; set; }

        [Required]
        public string Character { get; set; }

        [Required]
        public string Additional { get; set; }

        [Required]
        public string FuncDuties { get; set; }

        [Required]
        public string AddDuties { get; set; }

        [Required]
        public string DirectExecutive { get; set; }

        [Required]
        public string Management { get; set; }

        [Required]
        public string Salary { get; set; }

        [Required]
        public string AddSalary { get; set; }

        [Required]
        public string Schedule { get; set; }

        [Required]
        public string BusinessTrip { get; set; }
    }

    public class USR_REQ_URP_RequestForHRCardITR1_Table : BasicDocumentTable
    {

        [Required]
        public string OnPosition { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }
    
        public string Tutor { get; set; }

        [Required]
        public string UserChooseManual2 { get; set; }

        [Required]
        public string Report1 { get; set; }

        [Required]
        public DateTime DateInterview1 { get; set; }

        [Required]
        public HROpinion Opinion1 { get; set; }

        [Required]
        public string Reason1 { get; set; }

        public string Report2 { get; set; }

        public DateTime? DateInterview2 { get; set; }

        public HROpinion Opinion2 { get; set; }

        public string Reason2 { get; set; }

        public string Report3 { get; set; }

        public DateTime? DateInterview3 { get; set; }

        public HROpinion Opinion3 { get; set; }

        public string Reason3 { get; set; }

        public string Report4 { get; set; }

        public DateTime? DateInterview4 { get; set; }

        public HROpinion Opinion4 { get; set; }

        public string Reason4 { get; set; }
     
        public string Circumstances { get; set; }

        public string RecommendationOZTB { get; set; }

        [Required]
        public string SalaryCode { get; set; }

        [Required]
        public string Period { get; set; }

        [Required]
        public string Schedule { get; set; }

        [Required]
        public string Solution { get; set; }

        [Required]
        public StatusResidence Status { get; set; }

        [Required]
        public bool External { get; set; }
    }

    public class USR_REQ_URP_RequestForHRCardITR2_Table : BasicDocumentTable
    {

        [Required]
        public string OnPosition { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        public string Tutor { get; set; }

        [Required]
        public string UserChooseManual2 { get; set; }

        [Required]
        public string UserChooseManual3 { get; set; }

        [Required]
        public string Report1 { get; set; }

        [Required]
        public DateTime DateInterview1 { get; set; }

        [Required]
        public HROpinion Opinion1 { get; set; }

        [Required]
        public string Reason1 { get; set; }

        public string Report2 { get; set; }

        public DateTime? DateInterview2 { get; set; }

        public HROpinion Opinion2 { get; set; }

        public string Reason2 { get; set; }

        public string Report3 { get; set; }

        public DateTime? DateInterview3 { get; set; }

        public HROpinion Opinion3 { get; set; }

        public string Reason3 { get; set; }

        public string Report4 { get; set; }

        public DateTime? DateInterview4 { get; set; }

        public HROpinion Opinion4 { get; set; }

        public string Reason4 { get; set; }

        public string Circumstances { get; set; }

        public string RecommendationOZTB { get; set; }

        [Required]
        public string SalaryCode { get; set; }

        [Required]
        public string Period { get; set; }

        [Required]
        public string Schedule { get; set; }

        [Required]
        public string Solution { get; set; }

        [Required]
        public StatusResidence Status { get; set; }

        [Required]
        public bool External { get; set; }
    
    }

    public class USR_REQ_URP_RequestForHRCardITRZIF_Table : BasicDocumentTable
    {

        [Required]
        public string OnPosition { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }
       
        public string Tutor { get; set; }

        [Required]
        public string UserChooseManual2 { get; set; }

        [Required]
        public string Report1 { get; set; }

        [Required]
        public DateTime DateInterview1 { get; set; }

        [Required]
        public HROpinion Opinion1 { get; set; }

        [Required]
        public string Reason1 { get; set; }

        public string Report2 { get; set; }

        public DateTime? DateInterview2 { get; set; }

        public HROpinion Opinion2 { get; set; }

        public string Reason2 { get; set; }

        public string Report3 { get; set; }

        public DateTime? DateInterview3 { get; set; }

        public HROpinion Opinion3 { get; set; }

        public string Reason3 { get; set; }

        public string Circumstances { get; set; }

        public string RecommendationOZTB { get; set; }

        [Required]
        public string SalaryCode { get; set; }

        [Required]
        public string Period { get; set; }

        [Required]
        public string Schedule { get; set; }

        [Required]
        public string Solution { get; set; }

        [Required]
        public StatusResidence Status { get; set; }

        [Required]
        public bool External { get; set; }
    
    }

    public class USR_REQ_URP_RequestForHRCardWork_Table : BasicDocumentTable
    {
        [Required]
        public string OnPosition { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        public string Tutor { get; set; }

        [Required]
        public string UserChooseManual2 { get; set; }

        [Required]
        public string UserChooseManual3 { get; set; }

        [Required]
        public string Report1 { get; set; }

        [Required]
        public DateTime DateInterview1 { get; set; }

        [Required]
        public HROpinion Opinion1 { get; set; }

        [Required]
        public string Reason1 { get; set; }

        public string Report2 { get; set; }

        public DateTime? DateInterview2 { get; set; }

        public HROpinion Opinion2 { get; set; }

        public string Reason2 { get; set; }

        public string Report3 { get; set; }

        public DateTime? DateInterview3 { get; set; }

        public HROpinion Opinion3 { get; set; }

        public string Reason3 { get; set; }

        public string Report4 { get; set; }

        public DateTime? DateInterview4 { get; set; }

        public HROpinion Opinion4 { get; set; }

        public string Reason4 { get; set; }

        public string Circumstances { get; set; }

        public string RecommendationOZTB { get; set; }

        [Required]
        public string SalaryCode { get; set; }

        [Required]
        public string Period { get; set; }

        [Required]
        public string Schedule { get; set; }

        [Required]
        public string Solution { get; set; }

        [Required]
        public StatusResidence Status { get; set; }

        [Required]
        public bool External { get; set; }
    


    }

    public class USR_REQ_URP_RequestForHRCardWorkZIF_Table : BasicDocumentTable
    {

        [Required]
        public string OnPosition { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string UserChooseManual1 { get; set; }

        public string Tutor { get; set; }

        [Required]
        public string UserChooseManual2 { get; set; }

        [Required]
        public string Report1 { get; set; }

        [Required]
        public DateTime DateInterview1 { get; set; }

        [Required]
        public HROpinion Opinion1 { get; set; }

        [Required]
        public string Reason1 { get; set; }

        public string Report2 { get; set; }

        public DateTime? DateInterview2 { get; set; }

        public HROpinion Opinion2 { get; set; }

        public string Reason2 { get; set; }

        public string Report3 { get; set; }

        public DateTime? DateInterview3 { get; set; }

        public HROpinion Opinion3 { get; set; }

        public string Reason3 { get; set; }

        public string Circumstances { get; set; }

        public string RecommendationOZTB { get; set; }

        [Required]
        public string SalaryCode { get; set; }

        [Required]
        public string Period { get; set; }

        [Required]
        public string Schedule { get; set; }

        [Required]
        public string Solution { get; set; }

        [Required]
        public StatusResidence Status { get; set; }

        [Required]
        public bool External { get; set; }
    
    }

    public class USR_REQ_URP_RequestForHRChGraphTime_Table : BasicDocumentTable
    {
        [Required]
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

        public DateTime? StartDate1 { get; set; }
        public DateTime? StartDate2 { get; set; }
        public DateTime? StartDate3 { get; set; }
        public DateTime? StartDate4 { get; set; }
        public DateTime? StartDate5 { get; set; }
        public DateTime? StartDate6 { get; set; }
        public DateTime? StartDate7 { get; set; }
        public DateTime? StartDate8 { get; set; }
        public DateTime? StartDate9 { get; set; }
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

    public class USR_REQ_URP_RequestForHRChGraphConst_Table : BasicDocumentTable
    {
        [Required]
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

        public DateTime? StartDate1 { get; set; }
        public DateTime? StartDate2 { get; set; }
        public DateTime? StartDate3 { get; set; }
        public DateTime? StartDate4 { get; set; }
        public DateTime? StartDate5 { get; set; }
        public DateTime? StartDate6 { get; set; }
        public DateTime? StartDate7 { get; set; }
        public DateTime? StartDate8 { get; set; }
        public DateTime? StartDate9 { get; set; }
        public DateTime? StartDate10 { get; set; }
    }

    public class USR_REQ_URP_RequestForPaymentFirstDay_Table : BasicDocumentTable
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

        public DateTime? StartDate1 { get; set; }
        public DateTime? StartDate2 { get; set; }
        public DateTime? StartDate3 { get; set; }
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
    public class USR_REQ_YT_AuxiliaryTransportOCTMCInvest_Table : BasicDocumentTable
    {
        [Required]
        public string Transport { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string ItemId { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public TimeSpan TimeDeparture { get; set; }

        [Required]
        public string NumberProject { get; set; }

        [Required]
        public string NumberObject { get; set; }

        public string Terms { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_AuxiliaryTransportOCTMCOper_Table : BasicDocumentTable
    {
        [Required]
        public string Transport { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string ItemId { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public TimeSpan TimeDeparture { get; set; }

        public string Terms { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_AuxiliaryTransportDayOff_Table : BasicDocumentTable
    {
        [Required]
        public string Transport { get; set; }

        public PurposeAuxiliaryTransportTrip PurposeAuxiliaryTransportTrip { get; set; }

        [Required]
        public string ExecutionWork { get; set; }

        [Required]
        public string EmplName { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        public TimeSpan EndWorkTime { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_AuxiliaryTransportWorkDays_Table : BasicDocumentTable
    {
        [Required]
        public string Transport { get; set; }

        public PurposeAuxiliaryTransportTrip PurposeAuxiliaryTransportTrip { get; set; }

        [Required]
        public string ExecutionWork { get; set; }

        [Required]
        public string EmplName { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        public TimeSpan EndWorkTime { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_AuxiliaryTransportOutABK_Table : BasicDocumentTable
    {
        [Required]
        public string Transport { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public string ExecutionWork { get; set; }

        [Required]
        public string EmplName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        public TimeSpan EndWorkTime { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_StandbyTransport_Table : BasicDocumentTable
    {
        [Required]
        public string Client { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        [Required]
        public string Route { get; set; }

        [Required]
        public string Purpose { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_StandbyTransportUIT_Table : BasicDocumentTable
    {
        [Required]
        public string Client { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        [Required]
        public string Route { get; set; }

        [Required]
        public string Purpose { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_LightTransportTripManage_Table : BasicDocumentTable
    {
        [Required]
        public string Place { get; set; }

        public PurposeTrip PurposeTrip { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public EmplTrip EmplTrip { get; set; }

        [Required]
        public string EmplName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public TimeSpan TimeDeparture { get; set; }

        [Required]
        public string Flight { get; set; }

        public string Terms { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_LightTransportTripATK_Table : BasicDocumentTable
    {
        [Required]
        public string Place { get; set; }

        public PurposeTrip PurposeTrip { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Users { get; set; }

        public EmplTrip EmplTrip { get; set; }

        [Required]
        public string EmplName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public TimeSpan TimeDeparture { get; set; }

        public string Terms { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_LightTransportOCTMCInvest_Table : BasicDocumentTable
    {
        [Required]
        public string Place { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string ItemId { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public TimeSpan TimeDeparture { get; set; }

        [Required]
        public string NumberProject { get; set; }

        [Required]
        public string NumberObject { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_LightTransportOCTMCOper_Table : BasicDocumentTable
    {
        [Required]
        public string Place { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string ItemId { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public TimeSpan TimeDeparture { get; set; }

        public string Terms { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_LightTransportOutOrganizationInvest_Table : BasicDocumentTable
    {
        [Required]
        public string Place { get; set; }

        public PurposeTrip PurposeTrip { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public EmplTrip EmplTrip { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string EmplName { get; set; }

        public string Organization { get; set; }

        public string Purpose { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public string Flight { get; set; }

        [Required]
        public string NumberProject { get; set; }

        [Required]
        public string NumberObject { get; set; }

        public bool Account { get; set; }

        public string Terms { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_LightTransportOutOrganizationOper_Table : BasicDocumentTable
    {
        [Required]
        public string Place { get; set; }

        public PurposeTrip PurposeTrip { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public EmplTrip EmplTrip { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string EmplName { get; set; }

        public string Organization { get; set; }

        public string Purpose { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public string Flight { get; set; }

        public bool Account { get; set; }

        public string Terms { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_LightTransportTripDayOff_Table : BasicDocumentTable
    {
        [Required]
        public string Place { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        public TimeSpan EndWorkTime { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_PassangerTransportTrip_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        public TimeSpan EndWorkTime { get; set; }

        [Required]
        public string Route { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_PassangerTransportTripManage_Table : BasicDocumentTable
    {
        [Required]
        public string Place { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        public TimeSpan EndWorkTime { get; set; }

        public string Terms { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_PassangerTransportTripATK_Table : BasicDocumentTable
    {
        [Required]
        public string Place { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Users { get; set; }

        [Required]
        public string EmplName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public TimeSpan TimeArrive { get; set; }

        [Required]
        public string Flight { get; set; }

        public string Terms { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_PassangerTransportOutOrganizationInvest_Table : BasicDocumentTable
    {
        [Required]
        public string Place { get; set; }

        public PurposeTrip PurposeTrip { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public EmplTrip EmplTrip { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string EmplName { get; set; }

        public string Organization { get; set; }

        public string Purpose { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public string Flight { get; set; }

        [Required]
        public string NumberProject { get; set; }

        [Required]
        public string NumberObject { get; set; }

        public bool Account { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_PassangerTransportOutOrganizationOper_Table : BasicDocumentTable
    {
        [Required]
        public string Place { get; set; }

        public PurposeTrip PurposeTrip { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public EmplTrip EmplTrip { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string EmplName { get; set; }

        public string Organization { get; set; }

        public string Purpose { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public string Flight { get; set; }

        public bool Account { get; set; }

        public string Terms { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_PassangerTransportDayOff_Table : BasicDocumentTable
    {
        [Required]
        public string Place { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        public TimeSpan EndWorkTime { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_PassangerTransportDayOffZIF_Table : BasicDocumentTable
    {
        [Required]
        public string Place { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        public TimeSpan EndWorkTime { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_PassangerTransportCorporate_Table : BasicDocumentTable
    {
        [Required]
        public string Client { get; set; }

        [Required]
        public string Users { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime DateCorporate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        public TimeSpan EndWorkTime { get; set; }

        [Required]
        public string Route { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_RequestForEmergAuxiliaryTransportZIF_Table : BasicDocumentTable
    {
        [Required]
        public string Transport { get; set; }

        [Required]
        public string ExecutionWork { get; set; }

        [Required]
        public string EmplName { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        public TimeSpan EndWorkTime { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_RequestForAuxiliaryTransportDayOffZIF_Table : BasicDocumentTable
    {
        [Required]
        public string Transport { get; set; }

        public PurposeAuxiliaryTransportTrip PurposeAuxiliaryTransportTrip { get; set; }

        [Required]
        public string ExecutionWork { get; set; }

        [Required]
        public string EmplName { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        public TimeSpan EndWorkTime { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_RequestForAuxiliaryTransportWorkDaysZIF_Table : BasicDocumentTable
    {
        [Required]
        public string Transport { get; set; }

        public PurposeAuxiliaryTransportTrip PurposeAuxiliaryTransportTrip { get; set; }

        [Required]
        public string ExecutionWork { get; set; }

        [Required]
        public string EmplName { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        public TimeSpan EndWorkTime { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_RequestForStandbyTransportZIF_Table : BasicDocumentTable
    {
        [Required]
        public string Client { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        [Required]
        public string Route { get; set; }

        [Required]
        public string Purpose { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }
    }

    public class USR_REQ_YT_RequestForLightTransportTripManageZIF_Table : BasicDocumentTable
    {
        [Required]
        public string Place { get; set; }

        public PurposeTrip PurposeTrip { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public EmplTrip EmplTrip { get; set; }

        [Required]
        public string EmplName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public TimeSpan TimeDeparture { get; set; }

        [Required]
        public string Flight { get; set; }

        public string Terms { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }        
    }

    public class USR_REQ_YT_RequestForLightTransportTripDayOffZIF_Table : BasicDocumentTable
    {
        [Required]
        public string Place { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        public TimeSpan EndWorkTime { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }            
    }

    public class USR_REQ_YT_RequestForPassangerTransportZIF_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        public TimeSpan EndWorkTime { get; set; }

        [Required]
        public string Route { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }    
    }

    public class USR_REQ_YT_RequestForPassangerTransportTripZIF_Table : BasicDocumentTable
    {
        [Required]
        public string Place { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Users { get; set; }

        [Required]
        public string EmplName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string PlaceDeparture { get; set; }

        [Required]
        public TimeSpan TimeArrive { get; set; }

        [Required]
        public string Flight { get; set; }

        public string Terms { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }    
    }

    public class USR_REQ_YT_RequestForStandbyTransportUZL_Table : BasicDocumentTable
    {
        [Required]
        public string Client { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        [Required]
        public string Route { get; set; }

        [Required]
        public string Purpose { get; set; }

        public string NumberCar { get; set; }

        public string NameDriverCar { get; set; }    
    }
    #endregion

    #region ХУ
    public class USR_REQ_HY_EmergencyPurposeTRU_Table : BasicDocumentTable
    {
        public string UserChooseManual1 { get; set; }
        public string UserChooseManual2 { get; set; }

        [Required]
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

    public class USR_REQ_HY_BookingRoom_Table : BasicDocumentTable
    {
        public string UserChooseManual1 { get; set; }

        [Required]
        public string Users { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public DateTime DateArrival { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }

        public Payment Payment { get; set; }

        [Required]
        public string HotelName { get; set; }
    }

    public class USR_REQ_HY_CreateStamp_Table : BasicDocumentTable
    {
        [Required]
        public string Department { get; set; }

        [Required]
        public string StampName { get; set; }

        public int Qty { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string Location { get; set; }
    }

    public class USR_REQ_HY_FindApartment_Table : BasicDocumentTable
    {
        public string UserChooseManual1 { get; set; }

        [Required]
        public string Users { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string MaritalStatus { get; set; }

        [Required]
        public string KidsNumber { get; set; }

        [Required]
        public DateTime DateArrival { get; set; }
    }

    public class USR_REQ_HY_RequestRepair_Table : BasicDocumentTable
    {
        public string UserChooseManual1 { get; set; }

        [Required]
        public DateTime DateIncident { get; set; }

        [Required]
        public string Description { get; set; }
    }

    public class USR_REQ_HY_RequestTRU_Table : BasicDocumentTable
    {
        public string UserChooseManual1 { get; set; }
        public string UserChooseManual2 { get; set; }

        [Required]
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

    public class USR_REQ_HY_EmergencyRequestTRU_Table : BasicDocumentTable
    {
        public string UserChooseManual1 { get; set; }

        [Required]
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
    public class USR_REQ_TRIP_RegistrationBusinessTripForeign_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Department { get; set; }

        public string UserChooseManual1 { get; set; }

        public CategoryTrip CategoryTrip { get; set; }

        public TypeTrip TypeTrip { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int Days { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Purpose { get; set; }

        public PassageTrip PassageTrip { get; set; }

        public string Organization { get; set; }

        public PaymentTrip PaymentTripPassage { get; set; }

        public PaymentTrip PaymentTripDaily { get; set; }

        public PaymentTrip PaymentTripLive { get; set; }

        public string Description { get; set; }
    }

    public class USR_REQ_TRIP_RegistrationBusinessTripKZ_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        public string UserChooseManual1 { get; set; }

        public CategoryTrip CategoryTrip { get; set; }

        public TypeTrip TypeTrip { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Purpose { get; set; }

        public string Organization { get; set; }

        public string Description { get; set; }

        public string FIO1 { get; set; }
        public string FIO2 { get; set; }
        public string FIO3 { get; set; }
        public string FIO4 { get; set; }

        public EmplTripType EmplTripType1 { get; set; }
        public EmplTripType EmplTripType2 { get; set; }
        public EmplTripType EmplTripType3 { get; set; }
        public EmplTripType EmplTripType4 { get; set; }

        public TripDirection TripDirection1 { get; set; }
        public TripDirection TripDirection2 { get; set; }
        public TripDirection TripDirection3 { get; set; }
        public TripDirection TripDirection4 { get; set; }

        public int Day1 { get; set; }
        public int Day2 { get; set; }
        public int Day3 { get; set; }
        public int Day4 { get; set; }

        public int DayLive1 { get; set; }
        public int DayLive2 { get; set; }
        public int DayLive3 { get; set; }
        public int DayLive4 { get; set; }

        public TripPassage TripPassage1 { get; set; }
        public TripPassage TripPassage2 { get; set; }
        public TripPassage TripPassage3 { get; set; }
        public TripPassage TripPassage4 { get; set; }

        public int TicketSum1 { get; set; }
        public int TicketSum2 { get; set; }
        public int TicketSum3 { get; set; }
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

    public class USR_REQ_TRIP_RegistrationBusinessTripPP_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        public string UserChooseManual1 { get; set; }

        public CategoryTrip CategoryTrip { get; set; }

        public TypeTrip TypeTrip { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Purpose { get; set; }

        public string Organization { get; set; }

        public string Description { get; set; }

        public string FIO1 { get; set; }
        public string FIO2 { get; set; }
        public string FIO3 { get; set; }
        public string FIO4 { get; set; }

        public EmplTripType EmplTripType1 { get; set; }
        public EmplTripType EmplTripType2 { get; set; }
        public EmplTripType EmplTripType3 { get; set; }
        public EmplTripType EmplTripType4 { get; set; }

        public TripDirection TripDirection1 { get; set; }
        public TripDirection TripDirection2 { get; set; }
        public TripDirection TripDirection3 { get; set; }
        public TripDirection TripDirection4 { get; set; }

        public int Day1 { get; set; }
        public int Day2 { get; set; }
        public int Day3 { get; set; }
        public int Day4 { get; set; }

        public int DayLive1 { get; set; }
        public int DayLive2 { get; set; }
        public int DayLive3 { get; set; }
        public int DayLive4 { get; set; }

        public TripPassage TripPassage1 { get; set; }
        public TripPassage TripPassage2 { get; set; }
        public TripPassage TripPassage3 { get; set; }
        public TripPassage TripPassage4 { get; set; }

        public int TicketSum1 { get; set; }
        public int TicketSum2 { get; set; }
        public int TicketSum3 { get; set; }
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

    public class USR_REQ_TRIP_RegistrationBusinessTripPTY_Table : BasicDocumentTable
    {
        [Required]
        public string Users { get; set; }

        public string UserChooseManual1 { get; set; }

        public CategoryTrip CategoryTrip { get; set; }

        public TypeTrip TypeTrip { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Purpose { get; set; }

        public string Organization { get; set; }

        public string Description { get; set; }

        public string FIO1 { get; set; }
        public string FIO2 { get; set; }
        public string FIO3 { get; set; }
        public string FIO4 { get; set; }

        public EmplTripType EmplTripType1 { get; set; }
        public EmplTripType EmplTripType2 { get; set; }
        public EmplTripType EmplTripType3 { get; set; }
        public EmplTripType EmplTripType4 { get; set; }

        public TripDirection TripDirection1 { get; set; }
        public TripDirection TripDirection2 { get; set; }
        public TripDirection TripDirection3 { get; set; }
        public TripDirection TripDirection4 { get; set; }

        public int Day1 { get; set; }
        public int Day2 { get; set; }
        public int Day3 { get; set; }
        public int Day4 { get; set; }

        public int DayLive1 { get; set; }
        public int DayLive2 { get; set; }
        public int DayLive3 { get; set; }
        public int DayLive4 { get; set; }

        public TripPassage TripPassage1 { get; set; }
        public TripPassage TripPassage2 { get; set; }
        public TripPassage TripPassage3 { get; set; }
        public TripPassage TripPassage4 { get; set; }

        public int TicketSum1 { get; set; }
        public int TicketSum2 { get; set; }
        public int TicketSum3 { get; set; }
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
    #endregion

    #region Заявки Kazzink Holdings ИТ служба
    public class USK_REQ_IT_CTP_IncidentIT_Table : BasicDocumentRequestTable
    {
        [Required]
        public string Phone { get; set; }
    }
    #endregion

    #region Служебные записки

    public class USR_OFM_UIT_OfficeMemo_Table : BasicDocumantOfficeMemoTable
    {

    }
    #endregion

    #region Задачи

    public class USR_TAS_DailyTasks_Table : BasicDailyTasksTable
    {
    
    }

    public class USR_TAS_DailyTasksProlongation_Table : BasicDocumentTable
    {
        [Required]
        public DateTime ProlongationDate { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public Guid RefDocumentId { get; set; }

        [Required]
        public string RefDocNum { get; set; }

        [Required]
        public DateTime ExecutionDate { get; set; }

        [Required]
        public string TextTask { get; set; }
    }
    #endregion
}
    
