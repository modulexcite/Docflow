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
    [Authorize(Roles = "Administrator")]
    public class UserController : BasicController
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public ApplicationDbContext context { get; private set; }
        private readonly IDomainService _DomainService;

        public UserController(ICompanyService companyService, IAccountService accountService, IDomainService domainService)
            : base(companyService, accountService)
        {
            _DomainService = domainService;
            context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ICompanyService companyService, IAccountService accountService)
            : base(companyService, accountService)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            var items = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserViewModel>>(context.Users);
            var grid = new UserAjaxPagingGrid(items, 1, false);
            return PartialView("_UserGrid", grid);
        }

        public JsonResult GetUserList(int page)
        {
            var items = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserViewModel>>(context.Users);
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

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string domainSID = String.Empty;

                if (viewModel.isDomainUser == true)
                {
                    PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
                    UserPrincipal user = UserPrincipal.FindByIdentity(ctx, viewModel.UserName.ToString());
                    domainSID = user.Sid.ToString();
                }

                var domainModel = new ApplicationUser();
                domainModel.UserName = viewModel.UserName;
                domainModel.Email = viewModel.Email;
                domainModel.TimeZoneId = viewModel.TimeZoneId;
                domainModel.Lang = viewModel.Lang;
                domainModel.CompanyTableId = viewModel.CompanyTableId;
                domainModel.isDomainUser = viewModel.isDomainUser;

                var result = await UserManager.CreateAsync(domainModel);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First().ToString());

                    ViewBag.CompanyList = _CompanyService.GetDropListCompany(null);
                    ViewBag.TimeZoneList = _AccountService.GetTimeZoneList(null);
                    return View();
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
                            return View();
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CompanyList = _CompanyService.GetDropListCompany(null);
                ViewBag.TimeZoneList = _AccountService.GetTimeZoneList(null);
                return View();
            }
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
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string domainSID = String.Empty;

                if (viewModel.isDomainUser == true)
                {
                    PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
                    UserPrincipal user = UserPrincipal.FindByIdentity(ctx, viewModel.UserName.ToString());
                    domainSID = user.Sid.ToString();
                }

                var domainModel = await UserManager.FindByIdAsync(viewModel.Id.ToString());
                if (domainModel == null)
                {
                    return HttpNotFound();
                }

                domainModel.UserName = viewModel.UserName;
                domainModel.Email = viewModel.Email;
                domainModel.CompanyTableId = viewModel.CompanyTableId;
                domainModel.TimeZoneId = viewModel.TimeZoneId;
                domainModel.Lang = viewModel.Lang;
                domainModel.isDomainUser = viewModel.isDomainUser;

                var result = await UserManager.UpdateAsync(domainModel);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First().ToString());
                    return View();
                }

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CompanyList = _CompanyService.GetDropListCompany(viewModel.CompanyTableId);
                ViewBag.TimeZoneList = _AccountService.GetTimeZoneList(viewModel.TimeZoneId);
                return View();
            }
        }

        public async Task<ActionResult> AddRoles(string id)
        {
            var userTable = await UserManager.FindByIdAsync(id);
            if (userTable == null)
            {
                return HttpNotFound();
            }

            var roles = Mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(context.Roles);

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

            if (userTable == null)
            {
                return HttpNotFound();
            }

            if (isAjax == true)
            { 
                var allRoles = Mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(context.Roles);

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

                    UserManager.AddToRole(userTable.Id, roleTable.Name);
                }
            }

            return RedirectToAction("Index");
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
	}
}