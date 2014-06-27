using RapidDoc.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Http;
using System.Data.SqlClient;

namespace RapidDoc.Models.Infrastructure
{
    public class DataDBInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            #region Roles Initialize
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            rm.Create(new IdentityRole("Administrator"));
            rm.Create(new IdentityRole("Delegations"));

            rm.Create(new IdentityRole("ExecutorCTS_ATC"));
            rm.Create(new IdentityRole("ExecutorCTS_RADIO"));
            rm.Create(new IdentityRole("ExecutorCTS_PHONE"));
            rm.Create(new IdentityRole("AdministratorCTS"));
            rm.Create(new IdentityRole("PhoneCatalogExecutor"));
            rm.Create(new IdentityRole("ExecutorERPAccess"));
            rm.Create(new IdentityRole("ExecutorERPDevelop"));
            rm.Create(new IdentityRole("ExecutorCTPMaterial"));
            rm.Create(new IdentityRole("ExecutorCTPMobile"));
            rm.Create(new IdentityRole("ExecutorCTP"));
            rm.Create(new IdentityRole("AdministratorCTP"));
            rm.Create(new IdentityRole("ExecutorLotus"));
            rm.Create(new IdentityRole("ExecutorSysAdmin"));
            rm.Create(new IdentityRole("ExecutorDelegation"));
            #endregion

            #region Database Initialize
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser() { UserName = "Admin", Email = "admin@mail.com", CompanyTableId = null, TimeZoneId = "Central Asia Standard Time", isEnable = true, Lang = "ru-RU" };
            um.Create(user, "123456aA");
            um.AddToRole(user.Id, "Administrator");

            var user2 = new ApplicationUser() { UserName = "MaksatKulchikov", Email = "igor.dmitrov@altyntau.com", CompanyTableId = null, TimeZoneId = "Central Asia Standard Time", isEnable = true, Lang = "ru-RU" };
            um.Create(user2, "123456aA");

            var user3 = new ApplicationUser() { UserName = "IlyaFilimonov", Email = "igor.dmitrov@altyntau.com", CompanyTableId = null, TimeZoneId = "Central Asia Standard Time", isEnable = true, Lang = "ru-RU" };
            um.Create(user3, "123456aA");

            context.DomainTable.Add(new DomainTable { DomainName = "altyntau.com", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id, LDAPBaseDN = "OU=ATK,OU=Altyntau,DC=altyntau,DC=com", LDAPLogin = "ldapuser@altyntau.com", LDAPPort = 389, LDAPPassword = "iHFh6JKm", LDAPServer = "ATK-S-100" });
            context.SaveChanges();

            context.ProfileTable.Add(new ProfileTable { ProfileName = "ИД", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id });
            context.ProfileTable.Add(new ProfileTable { ProfileName = "НУ", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id });
            context.SaveChanges();

            context.TitleTable.Add(new TitleTable { TitleName = "И.о. главного бухгалтера", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id });
            context.TitleTable.Add(new TitleTable { TitleName = "Управляющий Директор по Коммерции", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id });
            context.SaveChanges();

            context.NumberSeriesTable.Add(new NumberSeriesTable { NumberSeriesName = "Заявка", Prefix = "RD", Size = 7, LastNum = 1, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id });
            context.SaveChanges();

            context.GroupProcessTable.Add(new GroupProcessTable { GroupProcessName = "Заявки", NumberSeriesTableId = context.NumberSeriesTable.FirstOrDefault().Id, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id });
            context.GroupProcessTable.Add(new GroupProcessTable { GroupProcessName = "Служебные записки", NumberSeriesTableId = context.NumberSeriesTable.FirstOrDefault().Id, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id });
            context.GroupProcessTable.Add(new GroupProcessTable { GroupProcessName = "Приказы", NumberSeriesTableId = context.NumberSeriesTable.FirstOrDefault().Id, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id });
            context.SaveChanges();

            context.GroupProcessTable.Add(new GroupProcessTable { GroupProcessName = "ИТ сервисы", NumberSeriesTableId = context.NumberSeriesTable.FirstOrDefault().Id, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id, GroupProcessParentId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "Заявки").Id });
            context.SaveChanges();

            context.GroupProcessTable.Add(new GroupProcessTable { GroupProcessName = "ERP", NumberSeriesTableId = context.NumberSeriesTable.FirstOrDefault().Id, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id, GroupProcessParentId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "ИТ сервисы").Id });
            context.GroupProcessTable.Add(new GroupProcessTable { GroupProcessName = "Документооборот", NumberSeriesTableId = context.NumberSeriesTable.FirstOrDefault().Id, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id, GroupProcessParentId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "ИТ сервисы").Id });
            context.GroupProcessTable.Add(new GroupProcessTable { GroupProcessName = "Почта", NumberSeriesTableId = context.NumberSeriesTable.FirstOrDefault().Id, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id, GroupProcessParentId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "ИТ сервисы").Id });
            context.GroupProcessTable.Add(new GroupProcessTable { GroupProcessName = "Интернет", NumberSeriesTableId = context.NumberSeriesTable.FirstOrDefault().Id, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id, GroupProcessParentId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "ИТ сервисы").Id });
            context.GroupProcessTable.Add(new GroupProcessTable { GroupProcessName = "Аппаратное обеспечение", NumberSeriesTableId = context.NumberSeriesTable.FirstOrDefault().Id, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id, GroupProcessParentId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "ИТ сервисы").Id });
            context.GroupProcessTable.Add(new GroupProcessTable { GroupProcessName = "Радиосвязь и телефония", NumberSeriesTableId = context.NumberSeriesTable.FirstOrDefault().Id, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id, GroupProcessParentId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "ИТ сервисы").Id });
            context.GroupProcessTable.Add(new GroupProcessTable { GroupProcessName = "Программное обеспечение", NumberSeriesTableId = context.NumberSeriesTable.FirstOrDefault().Id, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id, GroupProcessParentId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "ИТ сервисы").Id });
            context.GroupProcessTable.Add(new GroupProcessTable { GroupProcessName = "Техническая поддержка", NumberSeriesTableId = context.NumberSeriesTable.FirstOrDefault().Id, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id, GroupProcessParentId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "ИТ сервисы").Id });
            context.SaveChanges();

            context.WorkScheduleTable.Add(new WorkScheduleTable { WorkScheduleName = "8x5", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id, WorkStartTime = TimeSpan.FromHours(2), WorkEndTime = TimeSpan.FromHours(11) });
            context.SaveChanges();

            var numId = context.DomainTable.FirstOrDefault().Id;
            context.CompanyTable.Add(new CompanyTable { AliasCompanyName = "ATK", CompanyName = "Altyntau Kokshetau", DomainTableId = numId, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id });
            context.CompanyTable.Add(new CompanyTable { AliasCompanyName = "ATR", CompanyName = "Altyntau Resources", DomainTableId = numId, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id });
            context.SaveChanges();

            user.CompanyTableId = context.CompanyTable.FirstOrDefault().Id;
            um.Update(user);
            context.SaveChanges();

            Guid ATRId = context.CompanyTable.Where(x => x.AliasCompanyName == "ATR").FirstOrDefault().Id;
            user2.CompanyTableId = ATRId;
            um.Update(user2);
            context.SaveChanges();

            user3.CompanyTableId = ATRId;
            um.Update(user3);
            context.SaveChanges();

            context.DepartmentTable.Add(new DepartmentTable { DepartmentName = "Управляющий Директор по Коммерции", CompanyTableId = ATRId, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id, ApplicationUserModifiedId = user.Id });
            context.DepartmentTable.Add(new DepartmentTable { DepartmentName = "Департамент бухгалтерского учета и отчетности", CompanyTableId = ATRId, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id, ApplicationUserModifiedId = user.Id });
            context.SaveChanges();

            context.EmplTable.Add(new EmplTable { FirstName = "Илья", SecondName = "Филимонов", MiddleName = "Викторович", TitleTableId = context.TitleTable.FirstOrDefault(x => x.TitleName == "Управляющий Директор по Коммерции").Id, DepartmentTableId = context.DepartmentTable.FirstOrDefault(x => x.DepartmentName == "Управляющий Директор по Коммерции").Id, CompanyTableId = ATRId, ApplicationUserId = context.Users.FirstOrDefault(x => x.UserName == "IlyaFilimonov").Id, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id, ApplicationUserModifiedId = user.Id, WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id });
            context.EmplTable.Add(new EmplTable { FirstName = "Максат", SecondName = "Кульчиков", MiddleName = "Жазылканович", TitleTableId = context.TitleTable.FirstOrDefault(x => x.TitleName == "И.о. главного бухгалтера").Id, DepartmentTableId = context.DepartmentTable.FirstOrDefault(x => x.DepartmentName == "Департамент бухгалтерского учета и отчетности").Id, CompanyTableId = ATRId, ApplicationUserId = context.Users.FirstOrDefault(x => x.UserName == "MaksatKulchikov").Id, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id, ApplicationUserModifiedId = user.Id, WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id });
            context.SaveChanges();

            context.EmailParameterTable.Add(new EmailParameterTable { SmtpServer = "192.168.254.14", SmtpPort = 25, Email = "workflow.altyntau@altyntau.com", UserName = "altyntau.workflow", Password = "dctdjkjl123456aA", Timeout = 5000, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow, ApplicationUserCreatedId = user.Id });
            context.SaveChanges();
            #endregion

            #region Радиосвязь и телефония
            numId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "Радиосвязь и телефония").Id;
            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на выдачу Pin-кода",
                Description = "Нужен новый Pin-код или Вы забыли старый?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTS_DeliveryOfPinCode",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на выдачу, установку радиостанций",
                Description = "Требуется радиостанция?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTS_DeliveryOfWS",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на установку комплектующих радиостанций",
                Description = "Требуются комплектующие для радиостанций?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTS_DeliveryOfComponentsWS",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на демонтаж радиостанций",
                Description = "Требуются демонтировать радиостанцию?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTS_DisassemblingOfWS",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на замену телефона",
                Description = "Требуются заменить телефон?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTS_ReplacementPhone",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на перемещение рабочего места",
                Description = "Вы переезжаете?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTS_ReplacementWorkPlace",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на перерегистрацию пользователя в системе телефонной связи",
                Description = "Поменялся пользователь?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTS_ReregistrationUserInPhoneSystem",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на удаление/резервирование номера",
                Description = "",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTS_DeleteRezervationNumber",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на установку телефона",
                Description = "Требуеться установить телефон?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTS_SetUpPhone",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Проблемы с телефонной связью",
                Description = "У вас не работает телефонная связь?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTS_ProblemWithPhone",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Настройка переадресации звонков",
                Description = "Хотите настроить переадресацию?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTS_SetUpDVO",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Настройка персональных кнопок",
                Description = "Только если у Вас Цифровой или IP телефон.",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTS_SetPersonalButton",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на обслуживание (Связь)",
                Description = "У вас плановое обслуживание оборудования средств связи?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTS_DeliveryOfService",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });
            #endregion

            #region ERP
            numId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "ERP").Id;
            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на предоставление прав доступа к 1С8: Заработная плата",
                Description = "Требуется доступ к 1С8: Заработная плата?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_ERP_RequestPermission1C8Salary",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на предоставление прав доступа к 1С7.7: Бухгалтерия",
                Description = "Требуется история за старые периоды?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_ERP_RequestPermission1C77",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на предоставление прав доступа Бух. учет DAX (Axapta)",
                Description = "Требуется доступ к ERP?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_ERP_RequestPermissionAccountingDAX",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на предоставление прав доступа Казначейство MS DAX (Axapta)",
                Description = "Требуется доступ к ERP?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_ERP_RequestPermissionTreasuryDAX",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на модификацию\\разработку MS DAX (Axapta)",
                Description = "Необходимо внести изменения в программу?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_ERP_ModificationDAX",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на изменение аналитической модели",
                Description = "Требуется внести изменения в аналитическую модель?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_ERP_ChangeAnalyticalModel",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });
            #endregion

            #region Аппаратное обеспечение
            numId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "Аппаратное обеспечение").Id;
            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на установку оборудования",
                Description = "Нужно помочь подключить оборудование?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTP_EquipmentInstallation",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на установку нового компьютера и подключения к сети",
                Description = "Установить новый компьютер?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTP_InstallNewComputer",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на выдачу материалов",
                Description = "Нужны материалы?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTP_IssueMaterial",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на выдачу носителей информации",
                Description = "Необходима флешка или жесткий диск?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTP_IssueStorage",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на замену картриджа",
                Description = "Закончился картридж?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTP_ReplaceCartridge",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на замену компьютера",
                Description = "Заменить старый компьютер на новый?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTP_ReplaceComputer",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на оборудование",
                Description = "Требуется новое оборудование?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTP_RequestEquipment",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на переоформление компьютера",
                Description = "Поменялся владелец компьютера?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTP_ReissueComputer",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            numId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "Программное обеспечение").Id;
            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на установку (переустановку) программного обеспечения",
                Description = "Установить новое ПО?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTP_InstallSoftware",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            numId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "Радиосвязь и телефония").Id;
            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на восстановление SIM карты",
                Description = "Потеряли SIM карту?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTP_RecoverySimCard",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Запрос на выдачу SIM карты",
                Description = "Нужна корпоративная SIM карта?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTP_IssueSimCard",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            numId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "Техническая поддержка").Id;
            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "УИТ Регистрация инцидента",
                Description = "У вас есть проблемы с использованием ИТ сервиса?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CTP_IncidentIT",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });
            #endregion

            #region Программное обеспечение
            numId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "Программное обеспечение").Id;
            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на создание сетевой папки",
                Description = "Создать новую папку на файловом сервере?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_CreateNetworkFolder",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на смену пароля учетной записи в Active Directory",
                Description = "Забыли или потеряли пароль?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_ChangePassAD",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на разблокировку учетной записи Active Directory",
                Description = "Заблокированна учетная запись?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_UnlockUserAD",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на предоставление прав доступа в ИС ПАРАГРАФ",
                Description = "Нужен доступ к ИС ПАРАГРАФ?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_AccessRightParagraf",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на предоставление доступа к сетевым папкам",
                Description = "Нужен доступ к сетевым папкам?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_AccessRightNetworkFolder",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на заведение учетной записи MS Active Directory, MS Exchange",
                Description = "Завести нового пользователя?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_CreateUserAD",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на восстановление данных",
                Description = "Случайно удалили нужны файл?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_RecoveryData",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на создание пользователя в Lync",
                Description = "Завести нового пользователя?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_CreateUserLync",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на предоставление прав доступа в систему Автограф",
                Description = "Нужен доступ к системе Автограф?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_CreateUserAutograf",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на обслуживание программного обеспечения и оборудования",
                Description = "Проводите плановое обслуживание?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_HardSoftwareMaintenance",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });
            #endregion

            #region Документооборот
            numId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "Документооборот").Id;
            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на удаление подписи/документа в Lotus Notes",
                Description = "Ошиблись при согласовании в Lotus?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_RemoveSignLotus",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на смену пароля в Lotus Notes",
                Description = "Забыли или потеряли пароль?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_ChangePassLotus",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на предоставление прав доступа в Lotus Notes",
                Description = "Нужен доступ к Lotus Notes?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_AccessRightLotus",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на предоставление доступа по отправке на рассылки в Lotus Notes",
                Description = "Требуется доступ к рассылке?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_AccessSendLotus",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на делегирование Документооборот",
                Description = "",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_DelegationDocflow",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });
            #endregion

            #region Почта
            numId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "Почта").Id;
            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на создание списка рассылки в MS Outlook",
                Description = "Нужна новая группа рассылки?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_CreateSubscription",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на делегирования писем в Exchange server",
                Description = "",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_DelegationExchServ",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на добавление пользователей в группу рассылки",
                Description = "Включить вас в группу рассылки",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_AddUserSubscription",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на архивацию почты",
                Description = "У вас полный ящик писем?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_ArchiveMail",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на увеличение размера почтового ящика в MS Exchange (Outlook)",
                Description = "Нужно увеличить размер почтового ящика?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_CapacityMail",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на создание контакта в Exchange",
                Description = "Завести новый контакт?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_CreateUserExchange",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });
            #endregion

            #region Интернет
            numId = context.GroupProcessTable.FirstOrDefault(x => x.GroupProcessName == "Интернет").Id;
            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на предоставление доступа к FTP ресурсу",
                Description = "Нужен доступ к FTP ресурсу?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_AccessRightFTP",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Нет доступа к интернету",
                Description = "Не доступен сайт?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_NoLinkInternet",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на предоставление доступа в Internet",
                Description = "Нужен доступ к интернету?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_AccessRightInternet",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.ProcessTable.Add(new ProcessTable
            {
                ProcessName = "Запрос на предоставление доступа в Internet (Для сотрудников ЗИФ)",
                Description = "Нужен доступ к интернету?",
                GroupProcessTableId = numId,
                TableName = "USR_REQ_IT_CAP_AccessRightInternetZIF",
                isApproved = true,
                CreatedDate = DateTime.UtcNow,
                WorkScheduleTableId = context.WorkScheduleTable.FirstOrDefault().Id,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });

            context.SaveChanges();
            #endregion
            context.ServiceIncidentTable.Add(new ServiceIncidentTable
            {
                ServiceName = "ERP сервис",
                Description = "Система учета ресурсов предприятия",
                PriorityIncident = Repository.PriorityIncident.Hight,
                SLAIncident = 2,
                RoleTableId = rm.FindByName("ExecutorCTS_ATC").Id,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                CompanyTableId = context.CompanyTable.FirstOrDefault().Id,
                ApplicationUserCreatedId = user.Id
            });
            base.Seed(context);

            GetLDAPData().Wait(30000);
        }

        static async System.Threading.Tasks.Task GetLDAPData()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57203/");
                System.Net.Http.HttpResponseMessage response = await client.GetAsync("api/LDAPIntegration");
            }
        }
    }
}