using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using RapidDoc.Models.DomainModels;
using System.ComponentModel.DataAnnotations;
using RapidDoc.Attributes.Validation;
using System;

namespace RapidDoc.Models.Infrastructure
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<CompanyTable> CompanyTable { get; set; }
        public DbSet<TitleTable> TitleTable { get; set; }
        public DbSet<EmplTable> EmplTable { get; set; }
        public DbSet<ProfileTable> ProfileTable { get; set; }
        public DbSet<GroupProcessTable> GroupProcessTable { get; set; }
        public DbSet<ProcessTable> ProcessTable { get; set; }
        public DbSet<DomainTable> DomainTable { get; set; }
        public DbSet<DelegationTable> DelegationTable { get; set; }
        public DbSet<DepartmentTable> DepartmentTable { get; set; }
        public DbSet<NumberSeriesTable> NumberSeriesTable { get; set; }
        public DbSet<WFTrackerTable> WFTrackerTable { get; set; }
        public DbSet<WFTrackerUsersTable> WFTrackerUsersTable { get; set; }
        public DbSet<ReviewDocLogTable> ReviewDocLogTable { get; set; }
        public DbSet<DocumentReaderTable> DocumentReaderTable { get; set; }
        public DbSet<CommentTable> CommentTable { get; set; }
        public DbSet<WorkScheduleTable> WorkScheduleTable { get; set; }
        public DbSet<СalendarTable> СalendarTable { get; set; }
        public DbSet<FileTable> FileTable { get; set; }
        public DbSet<EmailParameterTable> EmailParameterTable { get; set; }
        public DbSet<HistoryUserTable> HistoryUserTable { get; set; }
        public DbSet<SearchTable> SearchTable { get; set; }
        public DbSet<ServiceIncidentTable> ServiceIncidentTable { get; set; }
        public DbSet<DocumentTable> DocumentTable { get; set; }

        //Custom Requests
        public DbSet<USR_REQ_IT_CTS_DeliveryOfPinCode_Table> USR_REQ_IT_CTS_DeliveryOfPinCode_Table { get; set; }
        public DbSet<USR_REQ_IT_CTS_DeliveryOfWS_Table> USR_REQ_IT_CTS_DeliveryOfWS_Table { get; set; }
        public DbSet<USR_REQ_IT_CTS_DeliveryOfComponentsWS_Table> USR_REQ_IT_CTS_DeliveryOfComponentsWS_Table { get; set; }
        public DbSet<USR_REQ_IT_CTS_DisassemblingOfWS_Table> USR_REQ_IT_CTS_DisassemblingOfWS_Table { get; set; }
        public DbSet<USR_REQ_IT_CTS_ReplacementPhone_Table> USR_REQ_IT_CTS_ReplacementPhone_Table { get; set; }
        public DbSet<USR_REQ_IT_CTS_ReplacementWorkPlace_Table> USR_REQ_IT_CTS_ReplacementWorkPlace_Table { get; set; }
        public DbSet<USR_REQ_IT_CTS_ReregistrationUserInPhoneSystem_Table> USR_REQ_IT_CTS_ReregistrationUserInPhoneSystem_Table { get; set; }
        public DbSet<USR_REQ_IT_CTS_DeleteRezervationNumber_Table> USR_REQ_IT_CTS_DeleteRezervationNumber_Table { get; set; }
        public DbSet<USR_REQ_IT_CTS_SetUpPhone_Table> USR_REQ_IT_CTS_SetUpPhone_Table { get; set; }
        public DbSet<USR_REQ_IT_CTS_ProblemWithPhone_Table> USR_REQ_IT_CTS_ProblemWithPhone_Table { get; set; }
        public DbSet<USR_REQ_IT_CTS_SetUpDVO_Table> USR_REQ_IT_CTS_SetUpDVO_Table { get; set; }
        public DbSet<USR_REQ_IT_CTS_DeliveryOfService_Table> USR_REQ_IT_CTS_DeliveryOfService_Table { get; set; }
        public DbSet<USR_REQ_IT_CTS_SetPersonalButton_Table> USR_REQ_IT_CTS_SetPersonalButton_Table { get; set; }
        public DbSet<USR_REQ_IT_ERP_RequestPermission1C8Salary_Table> USR_REQ_IT_ERP_RequestPermission1C8Salary_Table { get; set; }
        public DbSet<USR_REQ_IT_ERP_RequestPermission1C77_Table> USR_REQ_IT_ERP_RequestPermission1C77_Table { get; set; }
        public DbSet<USR_REQ_IT_ERP_RequestPermissionAccountingDAX_Table> USR_REQ_IT_ERP_RequestPermissionAccountingDAX_Table { get; set; }
        public DbSet<USR_REQ_IT_ERP_RequestPermissionTreasuryDAX_Table> USR_REQ_IT_ERP_RequestPermissionTreasuryDAX_Table { get; set; }
        public DbSet<USR_REQ_IT_ERP_ModificationDAX_Table> USR_REQ_IT_ERP_ModificationDAX_Table { get; set; }
        public DbSet<USR_REQ_IT_ERP_ChangeAnalyticalModel_Table> USR_REQ_IT_ERP_ChangeAnalyticalModel_Table { get; set; }
        public DbSet<USR_REQ_IT_CTP_EquipmentInstallation_Table> USR_REQ_IT_CTP_EquipmentInstallation_Table { get; set; }
        public DbSet<USR_REQ_IT_CTP_InstallNewComputer_Table> USR_REQ_IT_CTP_InstallNewComputer_Table { get; set; }
        public DbSet<USR_REQ_IT_CTP_InstallSoftware_Table> USR_REQ_IT_CTP_InstallSoftware_Table { get; set; }
        public DbSet<USR_REQ_IT_CTP_RecoverySimCard_Table> USR_REQ_IT_CTP_RecoverySimCard_Table { get; set; }
        public DbSet<USR_REQ_IT_CTP_IssueSimCard_Table> USR_REQ_IT_CTP_IssueSimCard_Table { get; set; }
        public DbSet<USR_REQ_IT_CTP_IssueMaterial_Table> USR_REQ_IT_CTP_IssueMaterial_Table { get; set; }
        public DbSet<USR_REQ_IT_CTP_IssueStorage_Table> USR_REQ_IT_CTP_IssueStorage_Table { get; set; }
        public DbSet<USR_REQ_IT_CTP_ReplaceCartridge_Table> USR_REQ_IT_CTP_ReplaceCartridge_Table { get; set; }
        public DbSet<USR_REQ_IT_CTP_ReplaceComputer_Table> USR_REQ_IT_CTP_ReplaceComputer_Table { get; set; }
        public DbSet<USR_REQ_IT_CTP_RequestEquipment_Table> USR_REQ_IT_CTP_RequestEquipment_Table { get; set; }
        public DbSet<USR_REQ_IT_CTP_ReissueComputer_Table> USR_REQ_IT_CTP_ReissueComputer_Table { get; set; }
        public DbSet<USR_REQ_IT_CTP_IncidentIT_Table> USR_REQ_IT_CTP_IncidentIT_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_RemoveSignLotus_Table> USR_REQ_IT_CAP_RemoveSignLotus_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_CreateSubscription_Table> USR_REQ_IT_CAP_CreateSubscription_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_CreateNetworkFolder_Table> USR_REQ_IT_CAP_CreateNetworkFolder_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_DelegationExchServ_Table> USR_REQ_IT_CAP_DelegationExchServ_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_AddUserSubscription_Table> USR_REQ_IT_CAP_AddUserSubscription_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_ChangePassAD_Table> USR_REQ_IT_CAP_ChangePassAD_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_ChangePassLotus_Table> USR_REQ_IT_CAP_ChangePassLotus_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_UnlockUserAD_Table> USR_REQ_IT_CAP_UnlockUserAD_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_AccessRightParagraf_Table> USR_REQ_IT_CAP_AccessRightParagraf_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_AccessRightLotus_Table> USR_REQ_IT_CAP_AccessRightLotus_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_AccessSendLotus_Table> USR_REQ_IT_CAP_AccessSendLotus_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_AccessRightFTP_Table> USR_REQ_IT_CAP_AccessRightFTP_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_AccessRightNetworkFolder_Table> USR_REQ_IT_CAP_AccessRightNetworkFolder_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_AccessRightInternet_Table> USR_REQ_IT_CAP_AccessRightInternet_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_AccessRightInternetZIF_Table> USR_REQ_IT_CAP_AccessRightInternetZIF_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_DelegationDocflow_Table> USR_REQ_IT_CAP_DelegationDocflow_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_CreateUserAD_Table> USR_REQ_IT_CAP_CreateUserAD_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_CreateUserADFreelance_Table> USR_REQ_IT_CAP_CreateUserADFreelance_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_RecoveryData_Table> USR_REQ_IT_CAP_RecoveryData_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_ArchiveMail_Table> USR_REQ_IT_CAP_ArchiveMail_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_CreateUserLync_Table> USR_REQ_IT_CAP_CreateUserLync_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_CreateUserExchange_Table> USR_REQ_IT_CAP_CreateUserExchange_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_CreateUserAutograf_Table> USR_REQ_IT_CAP_CreateUserAutograf_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_NoLinkInternet_Table> USR_REQ_IT_CAP_NoLinkInternet_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_CapacityMail_Table> USR_REQ_IT_CAP_CapacityMail_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_HardSoftwareMaintenance_Table> USR_REQ_IT_CAP_HardSoftwareMaintenance_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_ChangeRoute_Table> USR_REQ_IT_CAP_ChangeRoute_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_CreateUserLotus_Table> USR_REQ_IT_CAP_CreateUserLotus_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_AddOrChangeTemplate_Table> USR_REQ_IT_CAP_AddOrChangeTemplate_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_ChangeOrder_Table> USR_REQ_IT_CAP_ChangeOrder_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_ChangeOrderWage_Table> USR_REQ_IT_CAP_ChangeOrderWage_Table { get; set; }
        public DbSet<USR_REQ_ZIF_RequestForFuel_Table> USR_REQ_ZIF_RequestForFuel_Table { get; set; }
        public DbSet<USR_REQ_ZIF_RequestForSIZ_Table> USR_REQ_ZIF_RequestForSIZ_Table { get; set; }
        public DbSet<USR_REQ_OKS_RequestForTranslate_Table> USR_REQ_OKS_RequestForTranslate_Table { get; set; }
        public DbSet<USR_REQ_OKS_RequestForTranslateKAZ_Table> USR_REQ_OKS_RequestForTranslateKAZ_Table { get; set; }
        public DbSet<USR_REQ_OKS_RequestForPrintBlank_Table> USR_REQ_OKS_RequestForPrintBlank_Table { get; set; }
        public DbSet<USR_REQ_OKS_RequestForArchive_Table> USR_REQ_OKS_RequestForArchive_Table { get; set; }
        public DbSet<USR_REQ_OKS_RequestForVisa_Table> USR_REQ_OKS_RequestForVisa_Table { get; set; }
        public DbSet<USR_REQ_ROGR_RequestForMiningVehicle_Table> USR_REQ_ROGR_RequestForMiningVehicle_Table { get; set; }
        public DbSet<USR_REQ_SGMZIF_RequestForRepair_Table> USR_REQ_SGMZIF_RequestForRepair_Table { get; set; }
        public DbSet<USR_REQ_JU_RequestForAssurance_Table> USR_REQ_JU_RequestForAssurance_Table { get; set; }
        public DbSet<USR_REQ_JU_RequestForProxyDoc_Table> USR_REQ_JU_RequestForProxyDoc_Table { get; set; }
        public DbSet<USR_REQ_JU_RequestForArchiveContract_Table> USR_REQ_JU_RequestForArchiveContract_Table { get; set; }
        public DbSet<USR_REQ_JU_RequestForApproveDoc_Table> USR_REQ_JU_RequestForApproveDoc_Table { get; set; }
        public DbSet<USR_REQ_JU_RequestForNPA_Table> USR_REQ_JU_RequestForNPA_Table { get; set; }
        public DbSet<USR_REQ_JU_RequestForExplanationNormalAct_Table> USR_REQ_JU_RequestForExplanationNormalAct_Table { get; set; }
        public DbSet<USR_REQ_JU_RequestForExpertise_Table> USR_REQ_JU_RequestForExpertise_Table { get; set; }
        public DbSet<USR_REQ_FEU_RequestForFinExpertise_Table> USR_REQ_FEU_RequestForFinExpertise_Table { get; set; }
        public DbSet<USR_REQ_FEU_RequestForCorrectCalendar_Table> USR_REQ_FEU_RequestForCorrectCalendar_Table { get; set; }
        public DbSet<USR_REQ_UE_RequestForOutputElectricalEqu_Table> USR_REQ_UE_RequestForOutputElectricalEqu_Table { get; set; }
        public DbSet<USR_REQ_UE_RequestForDismantling_Table> USR_REQ_UE_RequestForDismantling_Table { get; set; }
        public DbSet<USR_REQ_UE_RequestForForecastWater_Table> USR_REQ_UE_RequestForForecastWater_Table { get; set; }
        public DbSet<USR_REQ_UE_RequestForElectricRepairs_Table> USR_REQ_UE_RequestForElectricRepairs_Table { get; set; }
        
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}