using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Repository;
using RapidDoc.Models.Services;

namespace RapidDoc.Controllers
{
    public class LDAPIntegrationController : ApiController
    {
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");

            ICompanyService _Companyservice = DependencyResolver.Current.GetService<ICompanyService>();
            CompanyTable company = _Companyservice.FirstOrDefault(x => x.AliasCompanyName == "ATK");
            BuildTreeLDAP(company, company.DomainTable.LDAPBaseDN, "");
            BuildTreeLDAP(company, company.DomainTable.LDAPBaseDN, "", true);
            return response;
        }

        private void BuildTreeLDAP(CompanyTable _item, string _LDAPpath, string _parentDepartmentName = "", bool afterUpdate = false)
        {
            DirectoryEntry entry = new DirectoryEntry("LDAP://" + _item.DomainTable.LDAPServer + ":" + Convert.ToString(_item.DomainTable.LDAPPort) + "/" + _LDAPpath, _item.DomainTable.LDAPLogin, _item.DomainTable.LDAPPassword, AuthenticationTypes.None);
            DirectorySearcher ds = new DirectorySearcher(entry);

            ds.SearchScope = SearchScope.OneLevel;
            ds.Filter = "(&(objectCategory=organizationalUnit)(!(name=Computers))(!(name=Disable))(!(name=Improvers))(!(name=Service&App))(!(name=Labs))(!(name=Servers))(!(name=Users))(!(name=Groups))(!(name=Disabled)))";
            ds.PropertiesToLoad.Add("distinguishedname");
            ds.PropertiesToLoad.Add("name");

            foreach (SearchResult result in ds.FindAll())
            {
                if (result.Properties.Contains("distinguishedname") && result.Properties.Contains("name"))
                {
                    Guid depId = Guid.Empty;
                    if (afterUpdate == false)
                    {
                        depId = DepartmentIntegration(result.Properties["name"][0].ToString(), _parentDepartmentName, _item.Id);
                    }
                    getUsersDepartment(_item, result.Properties["distinguishedname"][0].ToString(), depId, afterUpdate);
                    BuildTreeLDAP(_item, result.Properties["distinguishedname"][0].ToString(), result.Properties["name"][0].ToString(), afterUpdate);
                }
            }
        }

        private void getUsersDepartment(CompanyTable _item, string _LDAPpath, Guid _department, bool afterUpdate)
        {
            DirectoryEntry entry = new DirectoryEntry("LDAP://" + _item.DomainTable.LDAPServer + ":" 
                + Convert.ToString(_item.DomainTable.LDAPPort) + "/" 
                + _LDAPpath, _item.DomainTable.LDAPLogin, _item.DomainTable.LDAPPassword, AuthenticationTypes.None);
            DirectorySearcher ds = new DirectorySearcher(entry);
            string ldapData;
            Guid titleId = Guid.Empty;

            ds.SearchScope = SearchScope.OneLevel;
            ds.Filter = "(&(objectCategory=user)(!(userAccountControl:1.2.840.113556.1.4.803:=2)))";
            ds.PropertiesToLoad.Add("title");
            ds.PropertiesToLoad.Add("mail");
            ds.PropertiesToLoad.Add("telephonenumber");
            ds.PropertiesToLoad.Add("mobile");
            ds.PropertiesToLoad.Add("description");
            ds.PropertiesToLoad.Add("cn");
            ds.PropertiesToLoad.Add("sn");
            ds.PropertiesToLoad.Add("manager");
            ds.PropertiesToLoad.Add("sAMAccountName");

            foreach (SearchResult result in ds.FindAll())
            {
                if (result.Properties.Contains("title") && afterUpdate == false)
                {
                    ldapData = result.Properties["title"][0].ToString();
                    titleId = TitleIntegration(ldapData);
                }

                if (result.Properties.Contains("description") && result.Properties.Contains("mail"))
                {
                    string telephone = string.Empty;
                    string mobile = string.Empty;
                    string mail = string.Empty;
                    string userid = string.Empty;
                    string userName = string.Empty;
                    string manager = string.Empty;
                    string[] emplName = new string[3];

                    ldapData = result.Properties["description"][0].ToString();
                    //mail = result.Properties["mail"][0].ToString();
                    mail = "igor.dmitrov@altyntau.com";

                    emplName = DecodeLDAPNameUsers(ldapData, 0);

                    if (result.Properties.Contains("telephonenumber"))
                    {
                        telephone = result.Properties["telephonenumber"][0].ToString();
                    }
                    if (result.Properties.Contains("mobile"))
                    {
                        mobile = result.Properties["mobile"][0].ToString();
                    }
                    if (result.Properties.Contains("cn"))
                    {
                        userName = result.Properties["cn"][0].ToString();
                    }
                    if (result.Properties.Contains("sAMAccountName"))
                    {
                        userid = result.Properties["sAMAccountName"][0].ToString();
                    }
                    if (result.Properties.Contains("manager"))
                    {
                        manager = result.Properties["manager"][0].ToString();
                    }

                    if (emplName[0] != null && emplName[1] != null)
                    {
                        if (afterUpdate == true)
                        {
                            try
                            {
                                DirectoryEntry entryManager = new DirectoryEntry("LDAP://" + _item.DomainTable.LDAPServer + ":"
                                    + Convert.ToString(_item.DomainTable.LDAPPort) + "/"
                                    + manager, _item.DomainTable.LDAPLogin, _item.DomainTable.LDAPPassword, AuthenticationTypes.None);

                                if (entryManager.Properties.Contains("sAMAccountName"))
                                {
                                    string ManagerUserId = entryManager.Properties["sAMAccountName"].Value.ToString();

                                    ApplicationDbContext context = new ApplicationDbContext();
                                    var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                                    var userTable = um.FindByName(ManagerUserId);
                                    manager = userTable != null ? userTable.Id : String.Empty;

                                    EmplUpdateIntegration(emplName[1], emplName[0], emplName[2], _item.Id, manager);
                                }
                            }
                            catch 
                            { 
                                return; 
                            }
                        }
                        else
                        {
                            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
                            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, userid);
                            string domainSID = user.Sid.ToString();

                            String ApplicationUserId = UserIntegration(userid, mail, domainSID, _item.Id);
                            EmplIntegration(emplName[1], emplName[0], emplName[2], telephone, mobile, ApplicationUserId, _department, titleId, _item.Id, manager);
                        }
                    }
                }
            }
        }

        private void EmplUpdateIntegration(string _firstname, string _secondname, string _middlename, Guid _company, string _managerUserId)
        {
            IEmplService _EmplService = DependencyResolver.Current.GetService<IEmplService>();
            Guid? manageId = null;

            if (_managerUserId != String.Empty)
            {
                if (_EmplService.Contains(x => x.CompanyTableId == _company
                    && x.ApplicationUserId == _managerUserId))
                {
                    manageId = _EmplService.FirstOrDefault(x => x.CompanyTableId == _company
                        && x.ApplicationUserId == _managerUserId).Id;

                    if (_EmplService.Contains(x => x.CompanyTableId == _company
                        && x.FirstName == _firstname
                        && x.MiddleName == _middlename
                        && x.SecondName == _secondname))
                    {
                        EmplTable empl = _EmplService.FirstOrDefault(x => x.CompanyTableId == _company
                            && x.FirstName == _firstname
                            && x.MiddleName == _middlename
                            && x.SecondName == _secondname);

                        empl.ManageId = manageId;
                        _EmplService.SaveDomain(empl, "Admin");
                    }
                }
            }
        }

        private void EmplIntegration(string _firstname, string _secondname, string _middlename, string _workphone, string _mobilephone, string _userId, Guid _departmentId, Guid _titleId, Guid _company, string _managerUserId)
        {
            IEmplService _EmplService = DependencyResolver.Current.GetService<IEmplService>();
            IWorkScheduleService _WorkScheduleService = DependencyResolver.Current.GetService<IWorkScheduleService>();
            Guid? manageId = null;

            if(_managerUserId != String.Empty)
            {
                if (_EmplService.Contains(x => x.CompanyTableId == _company
                    && x.ApplicationUserId == _managerUserId))
                {
                    manageId = _EmplService.FirstOrDefault(x => x.CompanyTableId == _company
                        && x.ApplicationUserId == _managerUserId).Id;
                }
            }

            if (!_EmplService.Contains(x => x.CompanyTableId == _company 
                && x.FirstName == _firstname
                && x.MiddleName == _middlename
                && x.SecondName == _secondname))
            {
                _EmplService.SaveDomain(new EmplTable()
                {
                    isIntegratedLDAP = true,
                    FirstName = _firstname,
                    SecondName = _secondname,
                    MiddleName = _middlename,
                    ApplicationUserId = _userId,
                    DepartmentTableId = _departmentId,
                    TitleTableId = _titleId,
                    CompanyTableId = _company,
                    WorkScheduleTableId = _WorkScheduleService.FirstOrDefault(x => x.Id != null).Id,
                    ManageId = manageId
                }, "Admin");
            }
            else
            {
                EmplTable empl = _EmplService.FirstOrDefault(x => x.CompanyTableId == _company
                    && x.FirstName == _firstname
                    && x.MiddleName == _middlename
                    && x.SecondName == _secondname);

                empl.FirstName = _firstname;
                empl.SecondName = _secondname;
                empl.MiddleName = _middlename;
                empl.DepartmentTableId = _departmentId;
                empl.TitleTableId = _titleId;
                empl.isIntegratedLDAP = true;
                empl.ManageId = manageId;
                _EmplService.SaveDomain(empl, "Admin");
            }
        }

        private string UserIntegration(string _userId, string _email, string _sid, Guid _companyId)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var domainModel = um.FindByName(_userId);
            if(domainModel == null)
            {
                domainModel = new ApplicationUser();
                domainModel.UserName = _userId;
                domainModel.Email = _email;
                domainModel.TimeZoneId = "Central Asia Standard Time";
                domainModel.isEnable = true;
                domainModel.Lang = "ru-RU";
                domainModel.isDomainUser = true;
                domainModel.CompanyTableId = _companyId;
                um.Create(domainModel);

                var loginInfo = new UserLoginInfo("Windows", _sid);
                um.AddLogin(domainModel.Id, loginInfo);

                domainModel = um.FindByName(_userId);
            }
            return domainModel == null ? "" : domainModel.Id;
        }

        private Guid TitleIntegration(string _title)
        {
            ITitleService _Titleservice = DependencyResolver.Current.GetService<ITitleService>();

            if (!_Titleservice.Contains(x => x.TitleName == _title))
            {
                _Titleservice.SaveDomain(new TitleTable() { TitleName = _title, isIntegratedLDAP = true }, "Admin");
            }

            TitleTable titleTable = _Titleservice.FirstOrDefault(x => x.TitleName == _title);
            return titleTable == null ? Guid.Empty : titleTable.Id;
        }

        private Guid DepartmentIntegration(string _department, string _parentDepartmentName, Guid _companyId)
        {
            Guid? guid = null;
            _department = _department.Trim();

            IDepartmentService _DepartmentService = DependencyResolver.Current.GetService<IDepartmentService>();

            if (!_DepartmentService.Contains(x => x.DepartmentName == _department && x.CompanyTableId == _companyId))
            {
                if (_parentDepartmentName != string.Empty)
                {
                    guid = _DepartmentService.FirstOrDefault(x => x.CompanyTableId == _companyId && x.DepartmentName == _parentDepartmentName).Id;
                }

                _DepartmentService.SaveDomain(new DepartmentTable() { DepartmentName = _department, ParentDepartmentId = guid, CompanyTableId = _companyId }, "Admin");
            }

            guid = _DepartmentService.FirstOrDefault(x => x.DepartmentName == _department && x.CompanyTableId == _companyId).Id;
            return guid == null ? Guid.Empty : (Guid)guid;
        }

        private String[] DecodeLDAPNameUsers(string _name, int _type = 0)
        {
            String[] results = new string[3];

            if (_name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Count() > 1)
            {
                var list = new List<string>();
                list = _name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (list.Count == 2)
                {
                    list.Add(string.Empty);
                }

                switch (_type)
                {
                    case 0: //SecondName FirstName MiddleName
                        results[0] = list[0];
                        results[1] = list[1];
                        results[2] = list[2];
                        break;
                    case 1: //FirstName SecondName MiddleName 
                        results[0] = list[1];
                        results[1] = list[0];
                        results[2] = list[2];
                        break;
                }
            }

            return results;
        }

    }
}