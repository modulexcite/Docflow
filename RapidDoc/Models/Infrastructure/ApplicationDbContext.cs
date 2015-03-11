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
        public DbSet<TripSettingsTable> TripSettingsTable { get; set; }
        public DbSet<ItemCauseTable> ItemCauseTable { get; set; }

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
        public DbSet<USR_REQ_USH_RequestForReclassification_Table> USR_REQ_USH_RequestForReclassification_Table { get; set; }
        public DbSet<USR_REQ_UBP_RequestForExportWastes_Table> USR_REQ_UBP_RequestForExportWastes_Table { get; set; }
        public DbSet<USR_REQ_UBP_RequestForGetConclusion_Table> USR_REQ_UBP_RequestForGetConclusion_Table { get; set; }
        public DbSet<USR_REQ_KD_RequestForCompetitonProc_Table> USR_REQ_KD_RequestForCompetitonProc_Table { get; set; }
        public DbSet<USR_REQ_KD_RequestForCompetitonProcUZL_Table> USR_REQ_KD_RequestForCompetitonProcUZL_Table { get; set; }
        public DbSet<USR_REQ_KD_RequestForCompetitonProcServices_Table> USR_REQ_KD_RequestForCompetitonProcServices_Table { get; set; }
        public DbSet<USR_REQ_KD_RequestForCompetitonProcServicesBGP_Table> USR_REQ_KD_RequestForCompetitonProcServicesBGP_Table { get; set; }
        public DbSet<USR_REQ_SK_RequestForRegContactNonresident_Table> USR_REQ_SK_RequestForRegContactNonresident_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForImportTMCZIF_Table> USR_REQ_UB_RequestForImportTMCZIF_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForImportORZZIF_Table> USR_REQ_UB_RequestForImportORZZIF_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForImportTMCNoneZIF_Table> USR_REQ_UB_RequestForImportTMCNoneZIF_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForImportTMCUZL_Table> USR_REQ_UB_RequestForImportTMCUZL_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForInOutNotebook_Table> USR_REQ_UB_RequestForInOutNotebook_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForCarAccess_Table> USR_REQ_UB_RequestForCarAccess_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForExportAsset_Table> USR_REQ_UB_RequestForExportAsset_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForExportZIFOre_Table> USR_REQ_UB_RequestForExportZIFOre_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForExportOSPVHZIF_Table> USR_REQ_UB_RequestForExportOSPVHZIF_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForExportItems_Table> USR_REQ_UB_RequestForExportItems_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForHU_Table> USR_REQ_UB_RequestForHU_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForMovementItems_Table> USR_REQ_UB_RequestForMovementItems_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForMovementAssets_Table> USR_REQ_UB_RequestForMovementAssets_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForTemporaryORZ_Table> USR_REQ_UB_RequestForTemporaryORZ_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForTemporaryAccess_Table> USR_REQ_UB_RequestForTemporaryAccess_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForExportItemFromZIF_Table> USR_REQ_UB_RequestForExportItemFromZIF_Table { get; set; }
        public DbSet<USR_REQ_UB_RequestForExportItemFromORZ_Table> USR_REQ_UB_RequestForExportItemFromORZ_Table { get; set; }
        public DbSet<USR_REQ_UBUO_RequestForInspectionPropertyItems_Table> USR_REQ_SK_RequestForInspectionPropertyItems_Table { get; set; }
        public DbSet<USR_REQ_UBUO_RequestForInspectionPropertyAssets_Table> USR_REQ_SK_RequestForInspectionPropertyAssets_Table { get; set; }
        public DbSet<USR_REQ_UBUO_RequestForClearenceLetter_Table> USR_REQ_SK_RequestForClearenceLetter_Table { get; set; }
        public DbSet<USR_REQ_UBUO_RequestForReferenceTax_Table> USR_REQ_SK_RequestForReferenceTax_Table { get; set; }
        public DbSet<USR_REQ_UBUO_RequestCreateSettlView_Table> USR_REQ_UBUO_RequestCreateSettlView_Table { get; set; }
        public DbSet<USR_REQ_UBUO_RequestCalcDriveTrip_Table> USR_REQ_UBUO_RequestCalcDriveTrip_Table { get; set; }
        public DbSet<USR_REQ_UBP_RequestForInstructionBIOT_Table> USR_REQ_UBP_RequestForInstructionBIOT_Table { get; set; }
        public DbSet<USR_REQ_UZL_RequestForAccident_Table> USR_REQ_UZL_RequestForAccident_Table { get; set; }
        public DbSet<USR_REQ_UZL_RequestForUnscheduledIB_Table> USR_REQ_UZL_RequestForUnscheduledIB_Table { get; set; }
        public DbSet<USR_REQ_UZL_RequestForUnscheduledOB_Table> USR_REQ_UZL_RequestForUnscheduledOB_Table { get; set; }
        public DbSet<USR_REQ_UZL_RequestForIlliquid_Table> USR_REQ_UZL_RequestForIlliquid_Table { get; set; }
        public DbSet<USR_REQ_UZL_RequestForPeopleAcceptanceItems_Table> USR_REQ_UZL_RequestForPeopleAcceptanceItems_Table { get; set; }
        public DbSet<USR_REQ_UZL_RequestForContractNoneresident_Table> USR_REQ_UZL_RequestForContractNoneresident_Table { get; set; }
        public DbSet<USR_REQ_UZL_RequestForContractResident_Table> USR_REQ_UZL_RequestForContractResident_Table { get; set; }
        public DbSet<USR_REQ_UZL_RequestForrepresentationKD_Table> USR_REQ_UZL_RequestForrepresentationKD_Table { get; set; }
        public DbSet<USR_REQ_UZL_RequestForUT_Table> USR_REQ_UZL_RequestForUT_Table { get; set; }
        public DbSet<USR_REQ_UZL_RequestForExtraIBBGP_Table> USR_REQ_UZL_RequestForExtraIBBGP_Table { get; set; }
        public DbSet<USR_REQ_UZL_RequestForExtraIBZIF_Table> USR_REQ_UZL_RequestForExtraIBZIF_Table { get; set; }
        public DbSet<USR_REQ_UZL_RequestForExtraOBBGP_Table> USR_REQ_UZL_RequestForExtraOBBGP_Table { get; set; }
        public DbSet<USR_REQ_UZL_RequestForExtraOBZIF_Table> USR_REQ_UZL_RequestForExtraOBZIF_Table { get; set; }
        public DbSet<USR_REQ_UKR_RequestForExpertiseDKU_Table> USR_REQ_UZL_RequestForExpertiseDKU_Table { get; set; }
        public DbSet<USR_REQ_UKR_RequestForExpertiseInstruction_Table> USR_REQ_UKR_RequestForExpertiseInstruction_Table { get; set; }
        public DbSet<USR_REQ_UKR_RequestForExpertiseDepartment_Table> USR_REQ_UKR_RequestForExpertiseDepartment_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForKindergarten_Table> USR_REQ_URP_RequestForKindergarten_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForResponsibilities_Table> USR_REQ_URP_RequestForResponsibilities_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForResponsibilitiesSOTB_Table> USR_REQ_URP_RequestForResponsibilitiesSOTB_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForPrepayMaster_Table> USR_REQ_URP_RequestForPrepayMaster_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForAccrualExceptZIFBGP_Table> USR_REQ_URP_RequestForAccrualExceptZIFBGP_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForAccrualBGP_Table> USR_REQ_URP_RequestForAccrualBGP_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForAccrualZIF_Table> USR_REQ_URP_RequestForAccrualZIF_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForWithdrawVocationITR1_Table> USR_REQ_URP_RequestForWithdrawVocationITR1_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForWithdrawVocationITR2_Table> USR_REQ_URP_RequestForWithdrawVocationITR2_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForWVAuxiliaryBlock_Table> USR_REQ_URP_RequestForWVAuxiliaryBlock_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForWVMiningBlock_Table> USR_REQ_URP_RequestForWVMiningBlock_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForWVFinBlock_Table> USR_REQ_URP_RequestForWVFinBlock_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForWVStraight_Table> USR_REQ_URP_RequestForWVStraight_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForTeaching_Table> USR_REQ_URP_RequestForTeaching_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForForeignVisa_Table> USR_REQ_URP_RequestForForeignVisa_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForTransferVocationITR1_Table> USR_REQ_URP_RequestForTransferVocationITR1_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForTransferVocationITR2_Table> USR_REQ_URP_RequestForTransferVocationITR2_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForTransfVacWorkerMining_Table> USR_REQ_URP_RequestForTransfVacWorkerMining_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForTransfVacWorkerStraight_Table> USR_REQ_URP_RequestForTransfVacWorkerStraight_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForTransfVacWorkerFin_Table> USR_REQ_URP_RequestForTransfVacWorkerFin_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForTransfVacWorkerAuxiliary_Table> USR_REQ_URP_RequestForTransfVacWorkerAuxiliary_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForProvisionGraphVac_Table> USR_REQ_URP_RequestForProvisionGraphVac_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForProvisionGraphExit_Table> USR_REQ_URP_RequestForProvisionGraphExit_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForWeekend_Table> USR_REQ_URP_RequestForWeekend_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForReduction_Table> USR_REQ_URP_RequestForReduction_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForReductionCandidate_Table> USR_REQ_URP_RequestForReductionCandidate_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForReductionTB_Table> USR_REQ_URP_RequestForReductionTB_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForReductionThird_Table> USR_REQ_URP_RequestForReductionThird_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForSelectionPersonal_Table> USR_REQ_URP_RequestForSelectionPersonal_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForHRCardITR1_Table> USR_REQ_URP_RequestForHRCardITR1_Table { get; set; }

        public DbSet<USR_REQ_URP_RequestForHRCardITR2_Table> USR_REQ_URP_RequestForHRCardITR2_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForHRCardITRZIF_Table> USR_REQ_URP_RequestForHRCardITRZIF_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForHRCardWork_Table> USR_REQ_URP_RequestForHRCardWork_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForHRCardWorkZIF_Table> USR_REQ_URP_RequestForHRCardWorkZIF_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForHRChGraphTime_Table> USR_REQ_URP_RequestForHRChGraphTime_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForHRChGraphConst_Table> USR_REQ_URP_RequestForHRChGraphConst_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForPaymentFirstDay_Table> USR_REQ_URP_RequestForPaymentFirstDay_Table { get; set; }
        public DbSet<USR_REQ_YT_AuxiliaryTransportOCTMCInvest_Table> USR_REQ_YT_AuxiliaryTransportOCTMCInvest_Table { get; set; }
        public DbSet<USR_REQ_YT_AuxiliaryTransportOCTMCOper_Table> USR_REQ_YT_AuxiliaryTransportOCTMCOper_Table { get; set; }
        public DbSet<USR_REQ_YT_AuxiliaryTransportDayOff_Table> USR_REQ_YT_AuxiliaryTransportDayOff_Table { get; set; }
        public DbSet<USR_REQ_YT_AuxiliaryTransportWorkDays_Table> USR_REQ_YT_AuxiliaryTransportWorkDays_Table { get; set; }
        public DbSet<USR_REQ_YT_AuxiliaryTransportOutABK_Table> USR_REQ_YT_AuxiliaryTransportOutABK_Table { get; set; }
        public DbSet<USR_REQ_YT_StandbyTransport_Table> USR_REQ_YT_StandbyTransport_Table { get; set; }
        public DbSet<USR_REQ_YT_StandbyTransportUIT_Table> USR_REQ_YT_StandbyTransportUIT_Table { get; set; }
        public DbSet<USR_REQ_YT_LightTransportTripManage_Table> USR_REQ_YT_LightTransportTripManage_Table { get; set; }
        public DbSet<USR_REQ_YT_LightTransportTripATK_Table> USR_REQ_YT_LightTransportTripATK_Table { get; set; }
        public DbSet<USR_REQ_YT_LightTransportOCTMCInvest_Table> USR_REQ_YT_LightTransportOCTMCInvest_Table { get; set; }
        public DbSet<USR_REQ_YT_LightTransportOCTMCOper_Table> USR_REQ_YT_LightTransportOCTMCOper_Table { get; set; }
        public DbSet<USR_REQ_YT_LightTransportOutOrganizationInvest_Table> USR_REQ_YT_LightTransportOutOrganizationInvest_Table { get; set; }
        public DbSet<USR_REQ_YT_LightTransportOutOrganizationOper_Table> USR_REQ_YT_LightTransportOutOrganizationOper_Table { get; set; }
        public DbSet<USR_REQ_YT_LightTransportTripDayOff_Table> USR_REQ_YT_LightTransportTripDayOff_Table { get; set; }
        public DbSet<USR_REQ_YT_PassangerTransportTrip_Table> USR_REQ_YT_PassangerTransportTrip_Table { get; set; }
        public DbSet<USR_REQ_YT_PassangerTransportTripManage_Table> USR_REQ_YT_PassangerTransportTripManage_Table { get; set; }
        public DbSet<USR_REQ_YT_PassangerTransportTripATK_Table> USR_REQ_YT_PassangerTransportTripATK_Table { get; set; }
        public DbSet<USR_REQ_YT_PassangerTransportOutOrganizationInvest_Table> USR_REQ_YT_PassangerTransportOutOrganizationInvest_Table { get; set; }
        public DbSet<USR_REQ_YT_PassangerTransportOutOrganizationOper_Table> USR_REQ_YT_PassangerTransportOutOrganizationOper_Table { get; set; }
        public DbSet<USR_REQ_YT_PassangerTransportDayOff_Table> USR_REQ_YT_PassangerTransportDayOff_Table { get; set; }
        public DbSet<USR_REQ_YT_PassangerTransportDayOffZIF_Table> USR_REQ_YT_PassangerTransportDayOffZIF_Table { get; set; }
        public DbSet<USR_REQ_YT_PassangerTransportCorporate_Table> USR_REQ_YT_PassangerTransportCorporate_Table { get; set; }
        public DbSet<USR_REQ_HY_EmergencyPurposeTRU_Table> USR_REQ_HY_EmergencyPurposeTRU_Table { get; set; }
        public DbSet<USR_REQ_HY_BookingRoom_Table> USR_REQ_HY_BookingRoom_Table { get; set; }
        public DbSet<USR_REQ_HY_CreateStamp_Table> USR_REQ_HY_CreateStamp_Table { get; set; }
        public DbSet<USR_REQ_HY_FindApartment_Table> USR_REQ_HY_FindApartment_Table { get; set; }
        public DbSet<USR_REQ_HY_RequestRepair_Table> USR_REQ_HY_RequestRepair_Table { get; set; }
        public DbSet<USR_REQ_HY_RequestTRU_Table> USR_REQ_HY_RequestTRU_Table { get; set; }
        public DbSet<USR_REQ_HY_EmergencyRequestTRU_Table> USR_REQ_HY_EmergencyRequestTRU_Table { get; set; }
        public DbSet<USR_REQ_TRIP_RegistrationBusinessTripForeign_Table> USR_REQ_TRIP_RegistrationBusinessTripForeign_Table { get; set; }
        public DbSet<USR_REQ_TRIP_RegistrationBusinessTripKZ_Table> USR_REQ_TRIP_RegistrationBusinessTripKZ_Table { get; set; }
        public DbSet<USR_REQ_TRIP_RegistrationBusinessTripPP_Table> USR_REQ_TRIP_RegistrationBusinessTripPP_Table { get; set; }
        public DbSet<USK_REQ_IT_CTP_IncidentIT_Table> USK_REQ_IT_CTP_IncidentIT_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_AccessRightInternetBGP_Table> USR_REQ_IT_CAP_AccessRightInternetBGP_Table { get; set; }
        public DbSet<USR_REQ_IT_CAP_RequestForITWeekend_Table> USR_REQ_IT_CAP_RequestForITWeekend_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForAccrualSM_Table> USR_REQ_URP_RequestForAccrualSM_Table { get; set; }
        public DbSet<USR_REQ_URP_RequestForAccrualPTU_Table> USR_REQ_URP_RequestForAccrualPTU_Table { get; set; }
        public DbSet<USR_REQ_ZIF_RequestForItems_Table> USR_REQ_ZIF_RequestForItems_Table { get; set; }
        public DbSet<USR_REQ_ZIF_RequestForItemsMech_Table> USR_REQ_ZIF_RequestForItemsMech_Table { get; set; }
        public DbSet<USR_REQ_ZIF_RequestForItemsEnerg_Table> USR_REQ_ZIF_RequestForItemsEnerg_Table { get; set; }
        public DbSet<USR_REQ_ZIF_RequestForItemsASYTP_Table> USR_REQ_ZIF_RequestForItemsASYTP_Table { get; set; }
        public DbSet<USR_REQ_OKS_RequestForTicketPermission_Table> USR_REQ_OKS_RequestForTicketPermission_Table { get; set; }
        public DbSet<USR_REQ_OKS_RequestForTicket_Table> USR_REQ_OKS_RequestForTicket_Table { get; set; }
        public DbSet<USR_REQ_YT_RequestForEmergAuxiliaryTransportZIF_Table> USR_REQ_YT_RequestForEmergAuxiliaryTransportZIF_Table { get; set; }
        public DbSet<USR_REQ_YT_RequestForAuxiliaryTransportDayOffZIF_Table> USR_REQ_YT_RequestForAuxiliaryTransportDayOffZIF_Table { get; set; }
        public DbSet<USR_REQ_YT_RequestForAuxiliaryTransportWorkDaysZIF_Table> USR_REQ_YT_RequestForAuxiliaryTransportWorkDaysZIF_Table { get; set; }
        public DbSet<USR_REQ_YT_RequestForStandbyTransportZIF_Table> USR_REQ_YT_RequestForStandbyTransportZIF_Table { get; set; }
        public DbSet<USR_REQ_YT_RequestForLightTransportTripManageZIF_Table> USR_REQ_YT_RequestForLightTransportTripManageZIF_Table { get; set; }
        public DbSet<USR_REQ_YT_RequestForLightTransportTripDayOffZIF_Table> USR_REQ_YT_RequestForLightTransportTripDayOffZIF_Table { get; set; }
        public DbSet<USR_REQ_YT_RequestForPassangerTransportZIF_Table> USR_REQ_YT_RequestForPassangerTransportZIF_Table { get; set; }
        public DbSet<USR_REQ_YT_RequestForPassangerTransportTripZIF_Table> USR_REQ_YT_RequestForPassangerTransportTripZIF_Table { get; set; }
        public DbSet<USR_REQ_YT_RequestForStandbyTransportUZL_Table> USR_REQ_YT_RequestForStandbyTransportUZL_Table { get; set; }
        public DbSet<USR_REQ_ZIF_RequestForCarpenterWork_Table> USR_REQ_ZIF_RequestForCarpenterWork_Table { get; set; }
        public DbSet<USR_REQ_ZIF_RequestForCreatingItemsJDE_Table> USR_REQ_ZIF_RequestForCreatingItemsJDE_Table { get; set; }
        public DbSet<USR_REQ_UZL_RequestForCrashedStone_Table> USR_REQ_UZL_RequestForCrashedStone_Table { get; set; }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}