using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Services;
using RapidDoc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Grids;
using System.DirectoryServices.AccountManagement;

namespace RapidDoc.Controllers
{
    [Authorize(Roles = "Administrator, SetupAdministrator")]
    public class UserController : BasicController
    {
        private readonly IDomainService _DomainService;
        private readonly IEmplService _EmplService;

        protected UserManager<ApplicationUser> UserManager { get; private set; }
        protected RoleManager<IdentityRole> RoleManager { get; private set; }

        public UserController(ICompanyService companyService, IAccountService accountService, IDomainService domainService, IEmplService emplService)
            : base(companyService, accountService)
        {
            _DomainService = domainService;
            _EmplService = emplService;

            ApplicationDbContext dbContext = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbContext));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbContext));
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            var items = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserViewModel>>(UserManager.Users);
            var grid = new UserAjaxPagingGrid(items, 1, false);
            return PartialView("_UserGrid", grid);
        }

        public JsonResult GetUserList(int page)
        {
            var items = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserViewModel>>(UserManager.Users);
            var grid = new UserAjaxPagingGrid(items, page, true);

            return Json(new
            {
                Html = RenderPartialViewToString("_UserGrid", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.CompanyList = _CompanyService.GetDropListCompany(null);
            ViewBag.TimeZoneList = _AccountService.GetTimeZoneList(null);
            ViewBag.DomainList = _DomainService.GetDropListDomainNull(null);

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel viewModel)
        {
            if (viewModel.isDomainUser == true && viewModel.DomainTableId == null)
            {
                ModelState.AddModelError("", ValidationRes.ValidationResource.ErrorNoDomain);
            }

            if (ModelState.IsValid)
            {
                string domainSID = String.Empty;

                if (viewModel.isDomainUser == true && viewModel.DomainTableId != null)
                {
                    DomainTable domain = _DomainService.Find(GuidNull2Guid(viewModel.DomainTableId));
                    if (domain != null)
                    {
                        PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domain.LDAPServer, domain.LDAPBaseDN, domain.LDAPLogin, domain.LDAPPassword);
                        UserPrincipal user = UserPrincipal.FindByIdentity(ctx, viewModel.AccountDomainName);
                        domainSID = user.Sid.ToString();
                    }
                }

                var domainModel = new ApplicationUser();
                domainModel.UserName = viewModel.UserName;
                domainModel.Email = viewModel.Email;
                domainModel.TimeZoneId = viewModel.TimeZoneId;
                domainModel.Lang = viewModel.Lang;
                domainModel.CompanyTableId = viewModel.CompanyTableId;
                domainModel.isDomainUser = viewModel.isDomainUser;
                domainModel.Enable = true;
                domainModel.AccountDomainName = viewModel.AccountDomainName;
                domainModel.DomainTableId = viewModel.DomainTableId;

                var result = await UserManager.CreateAsync(domainModel);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First().ToString());
                }
                else
                {
                    if (domainModel.isDomainUser == true && domainSID != String.Empty)
                    {
                        var loginInfo = new UserLoginInfo("Windows", domainSID);

                        var resultLogin = await UserManager.AddLoginAsync(domainModel.Id, loginInfo);
                        if (!resultLogin.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First().ToString());

                            ViewBag.CompanyList = _CompanyService.GetDropListCompany(null);
                            ViewBag.TimeZoneList = _AccountService.GetTimeZoneList(null);
                            ViewBag.DomainList = _DomainService.GetDropListDomainNull(null);
                            return View();
                        }
                    }
                }

                if (ModelState.IsValid)
                    return RedirectToAction("Index");
            }

            ViewBag.CompanyList = _CompanyService.GetDropListCompany(null);
            ViewBag.TimeZoneList = _AccountService.GetTimeZoneList(null);
            ViewBag.DomainList = _DomainService.GetDropListDomainNull(null);
            return View();
        }

        public async Task<ActionResult> Edit(string id)
        {
            var domainModel = await UserManager.FindByIdAsync(id);
            if (domainModel == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<ApplicationUser, UserViewModel>(domainModel);
            ViewBag.CompanyList = _CompanyService.GetDropListCompany(viewModel.CompanyTableId);
            ViewBag.TimeZoneList = _AccountService.GetTimeZoneList(viewModel.TimeZoneId);
            ViewBag.DomainList = _DomainService.GetDropListDomainNull(viewModel.DomainTableId);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var domainModel = await UserManager.FindByIdAsync(viewModel.Id);
                if (domainModel == null)
                {
                    return HttpNotFound();
                }

                string domainSID = String.Empty;
                if (viewModel.isDomainUser == true)
                {
                    if (domainModel.DomainTable != null)
                    {
                        PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domainModel.DomainTable.LDAPServer, domainModel.DomainTable.LDAPBaseDN, domainModel.DomainTable.LDAPLogin, domainModel.DomainTable.LDAPPassword);
                        UserPrincipal user = UserPrincipal.FindByIdentity(ctx, viewModel.AccountDomainName);
                        domainSID = user.Sid.ToString();
                    }
                }

                domainModel.UserName = viewModel.UserName;
                domainModel.Email = viewModel.Email;
                domainModel.CompanyTableId = viewModel.CompanyTableId;
                domainModel.TimeZoneId = viewModel.TimeZoneId;
                domainModel.Lang = viewModel.Lang;
                domainModel.isDomainUser = viewModel.isDomainUser;
                domainModel.Enable = viewModel.Enable;
                domainModel.AccountDomainName = viewModel.AccountDomainName;
                domainModel.DomainTableId = viewModel.DomainTableId;

                var result = await UserManager.UpdateAsync(domainModel);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First().ToString());
                }
                else
                {
                    if (domainModel.isDomainUser == true && domainSID != String.Empty && UserManager.GetLogins(domainModel.Id).Count == 0)
                    {
                        var loginInfo = new UserLoginInfo("Windows", domainSID);

                        var resultLogin = await UserManager.AddLoginAsync(domainModel.Id, loginInfo);
                        if (!resultLogin.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.FirstOrDefault().ToString());
                        }
                    }
                }

                if (ModelState.IsValid == true)
                {
                    if (viewModel.Enable == false)
                    {
                        var allRoles = Mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(RoleManager.Roles);
                        foreach (var role in allRoles)
                        {
                            UserManager.RemoveFromRole(viewModel.Id, role.Name);
                        }

                        var emplTables = _EmplService.GetPartialIntercompany(x => x.Enable == true && x.ApplicationUserId == viewModel.Id).ToList();
                        if (emplTables != null)
                        {
                            foreach (var empl in emplTables)
                            {
                                empl.Enable = false;
                                _EmplService.SaveDomain(empl);
                            }
                        }

                    }
                    else
                    {
                        UserManager.AddToRole(viewModel.Id, "ActiveUser");
                    }

                    return RedirectToAction("Index");
                }
            }

            ViewBag.CompanyList = _CompanyService.GetDropListCompany(viewModel.CompanyTableId);
            ViewBag.TimeZoneList = _AccountService.GetTimeZoneList(viewModel.TimeZoneId);
            ViewBag.DomainList = _DomainService.GetDropListDomainNull(viewModel.DomainTableId);
            return View();
        }

        public async Task<ActionResult> AddRoles(string id)
        {
            var userTable = await UserManager.FindByIdAsync(id);
            if (userTable == null)
            {
                return HttpNotFound();
            }

            var roles = Mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(RoleManager.Roles);

            foreach (var role in roles)
            {
                if (UserManager.IsInRole(userTable.Id, role.Name) == true)
                {
                    role.isUserRole = true;
                }
            }

            return View(roles);
        }

        [HttpPost]
        public async Task<ActionResult> AddRoles(string id, string[] listdata, bool? isAjax)
        {
            ApplicationUser userTable  = await UserManager.FindByIdAsync(id);
            List<string> roleList = new List<string>();
            bool isDeletedUser = true;

            if (userTable == null)
            {
                return HttpNotFound();
            }

            if (isAjax == true)
            {
                var allRoles = Mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(RoleManager.Roles);

                foreach (var role in allRoles)
                {
                    UserManager.RemoveFromRole(userTable.Id, role.Name);
                }
            }

            if (listdata != null)
            {
                foreach (string roleId in listdata)
                {
                    var roleTable = await RoleManager.FindByIdAsync(roleId);
                    if (roleTable == null)
                    {
                        return HttpNotFound();
                    }

                    if (roleTable.Name == "ActiveUser")
                    {
                        isDeletedUser = false;
                    }

                    roleList.Add(roleTable.Name);
                }
            }

            if (listdata == null || isDeletedUser == true)
            {
                userTable.Enable = false;
                UserManager.Update(userTable);

                var emplTables = _EmplService.GetPartialIntercompany(x => x.Enable == true && x.ApplicationUserId == userTable.Id).ToList();
                if (emplTables != null)
                {
                    foreach (var empl in emplTables)
                    {
                        empl.Enable = false;
                        _EmplService.SaveDomain(empl);
                    }
                }
            }
            else
            {
                userTable.Enable = true;
                UserManager.Update(userTable);

                foreach(var roleName in roleList)
                {
                    UserManager.AddToRole(userTable.Id, roleName);
                }
            }

            return Json(new { result = "Redirect", url = Url.Action("Index") });
        }

        public async Task<ActionResult> ChangePassword(string id)
        {
            var userTable = await UserManager.FindByIdAsync(id);
            if (userTable == null)
            {
                return HttpNotFound();
            }

            var model = new ChangePassword();
            model.Id = userTable.Id;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var userTable = await UserManager.FindByIdAsync(model.Id);
                if (userTable == null)
                {
                    return HttpNotFound();
                }

                bool setPassword = await UserManager.HasPasswordAsync(userTable.Id);

                if (setPassword)
                {
                    IdentityResult resultRemove = await UserManager.RemovePasswordAsync(userTable.Id);
                    if (!resultRemove.Succeeded)
                    {
                        ModelState.AddModelError("", resultRemove.Errors.First().ToString());
                        return View(model);
                    }
                }

                IdentityResult result = await UserManager.AddPasswordAsync(userTable.Id, model.Password);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First().ToString());
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            if (disposing && RoleManager != null)
            {
                RoleManager.Dispose();
                RoleManager = null;
            }
            base.Dispose(disposing);
        }
	}
}