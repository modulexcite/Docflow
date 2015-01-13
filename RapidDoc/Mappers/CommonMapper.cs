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
                .ForMember(x => x.CompanyTableId, opt => opt.Ignore())
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
                .ForMember(x => x.ApplicationCreatedUser, o => o.MapFrom(m => m.DocumentTable.CreatedBy))
                .ForMember(x => x.ProcessName, o => o.MapFrom(m => m.DocumentTable.ProcessTable.ProcessName));

            Mapper.CreateMap<SearchTable, SearchView>()
                .ForMember(x => x.DocumentNum, o => o.MapFrom(m => m.DocumentTable.DocumentNum))
                .ForMember(x => x.ProcessName, o => o.MapFrom(m => m.DocumentTable.ProcessTable.ProcessName));

            Mapper.CreateMap<ServiceIncidentTable, ServiceIncidentView>()
                .ForMember(x => x.RoleName, o => o.MapFrom(m => m.IdentityRole.Name));
            Mapper.CreateMap<ServiceIncidentView, ServiceIncidentTable>()
                .ForMember(x => x.CompanyTableId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<TripSettingsTable, TripSettingsView>();
            Mapper.CreateMap<TripSettingsView, TripSettingsTable>()
                .ForMember(x => x.CompanyTableId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserCreatedId, opt => opt.Ignore())
                .ForMember(x => x.ApplicationUserModifiedId, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDate, opt => opt.Ignore());

            Mapper.CreateMap<DocumentTable, DocumentView>();
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }
    }
}