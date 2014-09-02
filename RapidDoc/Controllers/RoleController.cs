using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Grids;

namespace RapidDoc.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : BasicController
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public ApplicationDbContext context { get; private set; }

        public RoleController()
        {
            context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
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
            var items = Mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(context.Roles);
            var grid = new RoleAjaxPagingGrid(items, 1, false);
            return PartialView("_RoleGrid", grid);
        }

        public JsonResult GetRoleList(int page)
        {
            var items = Mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(context.Roles);
            var grid = new RoleAjaxPagingGrid(items, page, true);

            return Json(new
            {
                Html = RenderPartialViewToString("_RoleGrid", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var domainModel = new IdentityRole(viewModel.Name);
                var roleresult = await RoleManager.CreateAsync(domainModel);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First().ToString());
                    return View();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var domainModel = await RoleManager.FindByIdAsync(id.ToString());
            if (domainModel == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<IdentityRole, RoleViewModel>(domainModel);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var domainModel = await RoleManager.FindByIdAsync(viewModel.Id);
                if (domainModel == null)
                {
                    return HttpNotFound();
                }

                domainModel.Name = viewModel.Name;
                var result = await RoleManager.UpdateAsync(domainModel);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First().ToString());
                    return View();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public async Task<ActionResult> AddUsers(string id)
        {
            var roleTable = await RoleManager.FindByIdAsync(id);
            if (roleTable == null)
            {
                return HttpNotFound();
            }

            var users = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserViewModel>>(context.Users);

            foreach (var user in users)
            {
                foreach (var userRole in roleTable.Users)
                {
                    if (userRole.UserId == user.Id)
                    {
                        user.isRoleUser = true;
                    }
                }
            }

            return View(users);
        }

        [HttpPost]
        public async Task<ActionResult> AddUsers(string id, string[] listdata, bool? isAjax)
        {
            var roleTable = await RoleManager.FindByIdAsync(id);

            if (roleTable == null)
            {
                return HttpNotFound();
            }

            if (isAjax == true)
            {
                var allUsers = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserViewModel>>(context.Users);

                foreach (var user in allUsers)
                {
                    UserManager.RemoveFromRole(user.Id, roleTable.Name);
                }
            }

            if (listdata != null)
            {
                foreach (string userId in listdata)
                {
                    var userTable = await UserManager.FindByIdAsync(userId);
                    if (userTable == null)
                    {
                        return HttpNotFound();
                    }

                    UserManager.AddToRole(userTable.Id, roleTable.Name);
                }
            }

            return RedirectToAction("Index");
        }
	}
}