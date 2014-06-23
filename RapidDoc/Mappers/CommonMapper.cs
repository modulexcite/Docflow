using AutoMapper;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using RapidDoc.Models.Infrastructure;

namespace RapidDoc.Mappers
{
    public class CommonMapper : IMapper
    {
        static CommonMapper()
        {
            Mapper.CreateMap<DomainTable, DomainView>();
            Mapper.CreateMap<DomainView, DomainTable>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<CompanyTable, CompanyView>();
            Mapper.CreateMap<CompanyView, CompanyTable>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<ProfileTable, ProfileView>();
            Mapper.CreateMap<ProfileView, ProfileTable>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<TitleTable, TitleView>();
            Mapper.CreateMap<TitleView, TitleTable>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<GroupProcessTable, GroupProcessView>()
                .ForMember(x => x.GroupProcessParentName, o => o.MapFrom(m => m.GroupProcessTableParent.GroupProcessName));
            Mapper.CreateMap<GroupProcessView, GroupProcessTable>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<ProcessTable, ProcessView>();
            Mapper.CreateMap<ProcessView, ProcessTable>()
                .ForMember(x => x.CompanyTableId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<EmplTable, EmplView>();
            Mapper.CreateMap<EmplView, EmplTable>()
                .ForMember(x => x.CompanyTableId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<DelegationTable, DelegationView>()
                .ForMember(x => x.AliasCompanyName, o => o.MapFrom(m => m.CompanyTable.AliasCompanyName))
                .ForMember(x => x.GroupProcessName, o => o.MapFrom(m => m.GroupProcessTable.GroupProcessName))
                .ForMember(x => x.ProcessName, o => o.MapFrom(m => m.ProcessTable.ProcessName))
                .ForMember(x => x.EmplNameFrom, o => o.MapFrom(m => m.EmplTableFrom.FullName))
                .ForMember(x => x.EmplNameTo, o => o.MapFrom(m => m.EmplTableTo.FullName));
            Mapper.CreateMap<DelegationView, DelegationTable>()
                .ForMember(x => x.CompanyTableId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<DepartmentTable, DepartmentView>();
            Mapper.CreateMap<DepartmentView, DepartmentTable>()
                .ForMember(x => x.CompanyTableId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<DocumentTable, DocumentListView>();

            Mapper.CreateMap<NumberSeriesTable, NumberSeriesView>();
            Mapper.CreateMap<NumberSeriesView, NumberSeriesTable>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<IdentityRole, RoleViewModel>();
            Mapper.CreateMap<ApplicationUser, UserViewModel>();

            Mapper.CreateMap<WorkScheduleTable, WorkScheduleView>();
            Mapper.CreateMap<WorkScheduleView, WorkScheduleTable>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<EmailParameterTable, EmailParameterView>();
            Mapper.CreateMap<EmailParameterView, EmailParameterTable>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<HistoryUserTable, HistoryUserView>()
                .ForMember(x => x.DocumentNum, o => o.MapFrom(m => m.DocumentTable.DocumentNum))
                .ForMember(x => x.ProcessName, o => o.MapFrom(m => m.DocumentTable.ProcessTable.ProcessName));

            Mapper.CreateMap<SearchTable, SearchView>()
                .ForMember(x => x.DocumentNum, o => o.MapFrom(m => m.DocumentTable.DocumentNum))
                .ForMember(x => x.ProcessName, o => o.MapFrom(m => m.DocumentTable.ProcessTable.ProcessName));

            //Custom Requests
            Mapper.CreateMap<USR_REQ_IT_CTS_DeliveryOfPinCode_Table, USR_REQ_IT_CTS_DeliveryOfPinCode_View>();
            Mapper.CreateMap<USR_REQ_IT_CTS_DeliveryOfPinCode_View, USR_REQ_IT_CTS_DeliveryOfPinCode_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTS_DeliveryOfWS_Table, USR_REQ_IT_CTS_DeliveryOfWS_View>();
            Mapper.CreateMap<USR_REQ_IT_CTS_DeliveryOfWS_View, USR_REQ_IT_CTS_DeliveryOfWS_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTS_DeliveryOfComponentsWS_Table, USR_REQ_IT_CTS_DeliveryOfComponentsWS_View>();
            Mapper.CreateMap<USR_REQ_IT_CTS_DeliveryOfComponentsWS_View, USR_REQ_IT_CTS_DeliveryOfComponentsWS_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTS_DisassemblingOfWS_Table, USR_REQ_IT_CTS_DisassemblingOfWS_View>();
            Mapper.CreateMap<USR_REQ_IT_CTS_DisassemblingOfWS_View, USR_REQ_IT_CTS_DisassemblingOfWS_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTS_ReplacementPhone_Table, USR_REQ_IT_CTS_ReplacementPhone_View>();
            Mapper.CreateMap<USR_REQ_IT_CTS_ReplacementPhone_View, USR_REQ_IT_CTS_ReplacementPhone_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTS_ReplacementWorkPlace_Table, USR_REQ_IT_CTS_ReplacementWorkPlace_View>();
            Mapper.CreateMap<USR_REQ_IT_CTS_ReplacementWorkPlace_View, USR_REQ_IT_CTS_ReplacementWorkPlace_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTS_ReregistrationUserInPhoneSystem_Table, USR_REQ_IT_CTS_ReregistrationUserInPhoneSystem_View>();
            Mapper.CreateMap<USR_REQ_IT_CTS_ReregistrationUserInPhoneSystem_View, USR_REQ_IT_CTS_ReregistrationUserInPhoneSystem_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTS_DeleteRezervationNumber_Table, USR_REQ_IT_CTS_DeleteRezervationNumber_View>();
            Mapper.CreateMap<USR_REQ_IT_CTS_DeleteRezervationNumber_View, USR_REQ_IT_CTS_DeleteRezervationNumber_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTS_SetUpPhone_Table, USR_REQ_IT_CTS_SetUpPhone_View>();
            Mapper.CreateMap<USR_REQ_IT_CTS_SetUpPhone_View, USR_REQ_IT_CTS_SetUpPhone_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTS_ProblemWithPhone_Table, USR_REQ_IT_CTS_ProblemWithPhone_View>();
            Mapper.CreateMap<USR_REQ_IT_CTS_ProblemWithPhone_View, USR_REQ_IT_CTS_ProblemWithPhone_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTS_SetUpDVO_Table, USR_REQ_IT_CTS_SetUpDVO_View>();
            Mapper.CreateMap<USR_REQ_IT_CTS_SetUpDVO_View, USR_REQ_IT_CTS_SetUpDVO_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTS_DeliveryOfService_Table, USR_REQ_IT_CTS_DeliveryOfService_View>();
            Mapper.CreateMap<USR_REQ_IT_CTS_DeliveryOfService_View, USR_REQ_IT_CTS_DeliveryOfService_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTS_SetPersonalButton_Table, USR_REQ_IT_CTS_SetPersonalButton_View>();
            Mapper.CreateMap<USR_REQ_IT_CTS_SetPersonalButton_View, USR_REQ_IT_CTS_SetPersonalButton_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            //----
            Mapper.CreateMap<USR_REQ_IT_ERP_RequestPermission1C8Salary_Table, USR_REQ_IT_ERP_RequestPermission1C8Salary_View>();
            Mapper.CreateMap<USR_REQ_IT_ERP_RequestPermission1C8Salary_View, USR_REQ_IT_ERP_RequestPermission1C8Salary_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_ERP_RequestPermission1C77_Table, USR_REQ_IT_ERP_RequestPermission1C77_View>();
            Mapper.CreateMap<USR_REQ_IT_ERP_RequestPermission1C77_View, USR_REQ_IT_ERP_RequestPermission1C77_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_ERP_RequestPermissionAccountingDAX_Table, USR_REQ_IT_ERP_RequestPermissionAccountingDAX_View>();
            Mapper.CreateMap<USR_REQ_IT_ERP_RequestPermissionAccountingDAX_View, USR_REQ_IT_ERP_RequestPermissionAccountingDAX_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_ERP_RequestPermissionTreasuryDAX_Table, USR_REQ_IT_ERP_RequestPermissionTreasuryDAX_View>();
            Mapper.CreateMap<USR_REQ_IT_ERP_RequestPermissionTreasuryDAX_View, USR_REQ_IT_ERP_RequestPermissionTreasuryDAX_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_ERP_ModificationDAX_Table, USR_REQ_IT_ERP_ModificationDAX_View>();
            Mapper.CreateMap<USR_REQ_IT_ERP_ModificationDAX_View, USR_REQ_IT_ERP_ModificationDAX_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_ERP_ChangeAnalyticalModel_Table, USR_REQ_IT_ERP_ChangeAnalyticalModel_View>();
            Mapper.CreateMap<USR_REQ_IT_ERP_ChangeAnalyticalModel_View, USR_REQ_IT_ERP_ChangeAnalyticalModel_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            //----
            Mapper.CreateMap<USR_REQ_IT_CTP_EquipmentInstallation_Table, USR_REQ_IT_CTP_EquipmentInstallation_View>();
            Mapper.CreateMap<USR_REQ_IT_CTP_EquipmentInstallation_View, USR_REQ_IT_CTP_EquipmentInstallation_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTP_InstallNewComputer_Table, USR_REQ_IT_CTP_InstallNewComputer_View>();
            Mapper.CreateMap<USR_REQ_IT_CTP_InstallNewComputer_View, USR_REQ_IT_CTP_InstallNewComputer_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTP_InstallSoftware_Table, USR_REQ_IT_CTP_InstallSoftware_View>();
            Mapper.CreateMap<USR_REQ_IT_CTP_InstallSoftware_View, USR_REQ_IT_CTP_InstallSoftware_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTP_RecoverySimCard_Table, USR_REQ_IT_CTP_RecoverySimCard_View>();
            Mapper.CreateMap<USR_REQ_IT_CTP_RecoverySimCard_View, USR_REQ_IT_CTP_RecoverySimCard_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTP_IssueSimCard_Table, USR_REQ_IT_CTP_IssueSimCard_View>();
            Mapper.CreateMap<USR_REQ_IT_CTP_IssueSimCard_View, USR_REQ_IT_CTP_IssueSimCard_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTP_IssueMaterial_Table, USR_REQ_IT_CTP_IssueMaterial_View>();
            Mapper.CreateMap<USR_REQ_IT_CTP_IssueMaterial_View, USR_REQ_IT_CTP_IssueMaterial_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTP_IssueStorage_Table, USR_REQ_IT_CTP_IssueStorage_View>();
            Mapper.CreateMap<USR_REQ_IT_CTP_IssueStorage_View, USR_REQ_IT_CTP_IssueStorage_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTP_ReplaceCartridge_Table, USR_REQ_IT_CTP_ReplaceCartridge_View>();
            Mapper.CreateMap<USR_REQ_IT_CTP_ReplaceCartridge_View, USR_REQ_IT_CTP_ReplaceCartridge_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTP_ReplaceComputer_Table, USR_REQ_IT_CTP_ReplaceComputer_View>();
            Mapper.CreateMap<USR_REQ_IT_CTP_ReplaceComputer_View, USR_REQ_IT_CTP_ReplaceComputer_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTP_RequestEquipment_Table, USR_REQ_IT_CTP_RequestEquipment_View>();
            Mapper.CreateMap<USR_REQ_IT_CTP_RequestEquipment_View, USR_REQ_IT_CTP_RequestEquipment_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTP_ReissueComputer_Table, USR_REQ_IT_CTP_ReissueComputer_View>();
            Mapper.CreateMap<USR_REQ_IT_CTP_ReissueComputer_View, USR_REQ_IT_CTP_ReissueComputer_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CTP_IncidentIT_Table, USR_REQ_IT_CTP_IncidentIT_View>();
            Mapper.CreateMap<USR_REQ_IT_CTP_IncidentIT_View, USR_REQ_IT_CTP_IncidentIT_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            //---
            Mapper.CreateMap<USR_REQ_IT_CAP_RemoveSignLotus_Table, USR_REQ_IT_CAP_RemoveSignLotus_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_RemoveSignLotus_View, USR_REQ_IT_CAP_RemoveSignLotus_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_CreateSubscription_Table, USR_REQ_IT_CAP_CreateSubscription_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_CreateSubscription_View, USR_REQ_IT_CAP_CreateSubscription_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_CreateNetworkFolder_Table, USR_REQ_IT_CAP_CreateNetworkFolder_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_CreateNetworkFolder_View, USR_REQ_IT_CAP_CreateNetworkFolder_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_DelegationExchServ_Table, USR_REQ_IT_CAP_DelegationExchServ_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_DelegationExchServ_View, USR_REQ_IT_CAP_DelegationExchServ_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_AddUserSubscription_Table, USR_REQ_IT_CAP_AddUserSubscription_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_AddUserSubscription_View, USR_REQ_IT_CAP_AddUserSubscription_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_ChangePassLotus_Table, USR_REQ_IT_CAP_ChangePassLotus_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_ChangePassLotus_View, USR_REQ_IT_CAP_ChangePassLotus_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_ChangePassAD_Table, USR_REQ_IT_CAP_ChangePassAD_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_ChangePassAD_View, USR_REQ_IT_CAP_ChangePassAD_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_UnlockUserAD_Table, USR_REQ_IT_CAP_UnlockUserAD_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_UnlockUserAD_View, USR_REQ_IT_CAP_UnlockUserAD_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_AccessRightParagraf_Table, USR_REQ_IT_CAP_AccessRightParagraf_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_AccessRightParagraf_View, USR_REQ_IT_CAP_AccessRightParagraf_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_AccessRightLotus_Table, USR_REQ_IT_CAP_AccessRightLotus_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_AccessRightLotus_View, USR_REQ_IT_CAP_AccessRightLotus_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_AccessSendLotus_Table, USR_REQ_IT_CAP_AccessSendLotus_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_AccessSendLotus_View, USR_REQ_IT_CAP_AccessSendLotus_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_AccessRightFTP_Table, USR_REQ_IT_CAP_AccessRightFTP_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_AccessRightFTP_View, USR_REQ_IT_CAP_AccessRightFTP_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_AccessRightNetworkFolder_Table, USR_REQ_IT_CAP_AccessRightNetworkFolder_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_AccessRightNetworkFolder_View, USR_REQ_IT_CAP_AccessRightNetworkFolder_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_AccessRightInternet_Table, USR_REQ_IT_CAP_AccessRightInternet_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_AccessRightInternet_View, USR_REQ_IT_CAP_AccessRightInternet_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_AccessRightInternetZIF_Table, USR_REQ_IT_CAP_AccessRightInternetZIF_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_AccessRightInternetZIF_View, USR_REQ_IT_CAP_AccessRightInternetZIF_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_DelegationDocflow_Table, USR_REQ_IT_CAP_DelegationDocflow_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_DelegationDocflow_View, USR_REQ_IT_CAP_DelegationDocflow_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_CreateUserAD_Table, USR_REQ_IT_CAP_CreateUserAD_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_CreateUserAD_View, USR_REQ_IT_CAP_CreateUserAD_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_RecoveryData_Table, USR_REQ_IT_CAP_RecoveryData_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_RecoveryData_View, USR_REQ_IT_CAP_RecoveryData_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_ArchiveMail_Table, USR_REQ_IT_CAP_ArchiveMail_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_ArchiveMail_View, USR_REQ_IT_CAP_ArchiveMail_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_CreateUserLync_Table, USR_REQ_IT_CAP_CreateUserLync_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_CreateUserLync_View, USR_REQ_IT_CAP_CreateUserLync_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_CreateUserExchange_Table, USR_REQ_IT_CAP_CreateUserExchange_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_CreateUserExchange_View, USR_REQ_IT_CAP_CreateUserExchange_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_CreateUserAutograf_Table, USR_REQ_IT_CAP_CreateUserAutograf_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_CreateUserAutograf_View, USR_REQ_IT_CAP_CreateUserAutograf_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_NoLinkInternet_Table, USR_REQ_IT_CAP_NoLinkInternet_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_NoLinkInternet_View, USR_REQ_IT_CAP_NoLinkInternet_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_CapacityMail_Table, USR_REQ_IT_CAP_CapacityMail_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_CapacityMail_View, USR_REQ_IT_CAP_CapacityMail_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<USR_REQ_IT_CAP_HardSoftwareMaintenance_Table, USR_REQ_IT_CAP_HardSoftwareMaintenance_View>();
            Mapper.CreateMap<USR_REQ_IT_CAP_HardSoftwareMaintenance_View, USR_REQ_IT_CAP_HardSoftwareMaintenance_Table>()
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }
    }
}