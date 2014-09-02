using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.Services;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Interfaces;
using RapidDoc.Models.ViewModels;
using RapidDoc.Models.DomainModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RapidDoc.Controllers
{
    public class NewProcessController : BasicController
    {
        private readonly IProcessService _ProcessService;
        private readonly IGroupProcessService _GroupProcessService;
        private readonly IEmplService _EmplService;
        private readonly IAccountService _AccountService;
        private readonly IDocumentService _DocumentService;

        public NewProcessController(IProcessService processService, IGroupProcessService groupProcessService, IEmplService emplService, IAccountService accountService, IDocumentService documentService)
        {
            _ProcessService = processService;
            _GroupProcessService = groupProcessService;
            _EmplService = emplService;
            _AccountService = accountService;
            _DocumentService = documentService;
        }

        public ActionResult Index()
        {
            ApplicationUser userTable = _AccountService.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (userTable == null)
            {
                ModelState.AddModelError(string.Empty, String.Format(ValidationRes.ValidationResource.ErrorUserNotFound, User.Identity.Name));
            }

            EmplTable emplTable = _EmplService.FirstOrDefault(x => x.ApplicationUserId == userTable.Id);
            if (emplTable == null)
            {
                ModelState.AddModelError(string.Empty, String.Format(ValidationRes.ValidationResource.ErrorEmplNotFound, User.Identity.Name));
            }

            List<ProcessView> topProcess = new List<ProcessView>();
            var processes = _DocumentService.GetAll().GroupBy(x => x.ProcessTableId).Select(g => new { ProcessTableId = g.Key, Count = g.Count() }).OrderByDescending(i => i.Count).Select(y => y.ProcessTableId).ToList();

            int num = 0;
            if (processes != null)
            {
                foreach (var processId in processes)
                {
                    if(num > 8)
                    {
                        break;
                    }

                    ApplicationDbContext context = new ApplicationDbContext();
                    UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                    RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                    List<ProcessView> result = new List<ProcessView>();
                    string userId = User.Identity.GetUserId();
                    ProcessView process = _ProcessService.FindView(processId);

                    if (!String.IsNullOrEmpty(process.RoleId))
                    {
                        string roleName = RoleManager.FindById(process.RoleId).Name;
                        if (UserManager.IsInRole(userId, roleName))
                        {
                            num++;
                            topProcess.Add(process);
                        }
                    }
                    else
                    {
                        num++;
                        topProcess.Add(process);
                    }
                    context.Dispose();
                }
            }

            ViewBag.TopProcess = topProcess;
            return View(_GroupProcessService.GetPartialView(x => x.GroupProcessParentId == null));
        }

        public ActionResult ProcessList(Guid groupProcessId)
        {
            List<GroupProcessTable> breadCrumbsList = new List<GroupProcessTable>();
            breadCrumbsList = GetBreadCrumbs(breadCrumbsList, groupProcessId);
            ViewBag.BreadCrumbs = breadCrumbsList;

            var groupChildItems = _GroupProcessService.GetPartialView(x => x.GroupProcessParentId == groupProcessId);
            if(groupChildItems.Count() == 0)
            {
                var model = _ProcessService.GetPartialView(x => x.GroupProcessTableId == groupProcessId && x.isApproved == true).OrderBy(x => x.ProcessName);

                ApplicationDbContext context = new ApplicationDbContext();
                UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                List<ProcessView> result = new List<ProcessView>();
                string userId = User.Identity.GetUserId();
                foreach (var item in model)
                {
                    if (!String.IsNullOrEmpty(item.RoleId))
                    {
                        string roleName = RoleManager.FindById(item.RoleId).Name;
                        if (UserManager.IsInRole(userId, roleName))
                        {
                            result.Add(item);
                        }
                    }
                    else
                    {
                        result.Add(item);
                    }
                }
                context.Dispose();

                return View(result);
            }
            else
            {
                return View("ChildGroup", groupChildItems);
            }
        }

        private List<GroupProcessTable> GetBreadCrumbs(List<GroupProcessTable> breadCrumbsList, Guid groupProcessId)
        {
            var item = _GroupProcessService.Find(groupProcessId);

            if (item.GroupProcessParentId != null && item.GroupProcessParentId != Guid.Empty)
            {
                breadCrumbsList = GetBreadCrumbs(breadCrumbsList, item.GroupProcessParentId.Value);
            }
            breadCrumbsList.Add(item);

            return breadCrumbsList;
        }

        public ActionResult SearchProcess(string searchText = "")
        {
            string searchString = searchText.Trim();

            if (searchString.Length > 2)
            {
                var model = _ProcessService.GetPartialView(x => x.ProcessName.Contains(searchString));
                
                ApplicationDbContext context = new ApplicationDbContext();
                UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                List<ProcessView> result = new List<ProcessView>();
                string userId = User.Identity.GetUserId();
                foreach (var item in model)
                {
                    if (!String.IsNullOrEmpty(item.RoleId))
                    {
                        string roleName = RoleManager.FindById(item.RoleId).Name;
                        if (UserManager.IsInRole(userId, roleName))
                        {
                            result.Add(item);
                        }
                    }
                    else
                    {
                        result.Add(item);
                    }
                }
                context.Dispose();

                return PartialView("_SearchResultProcess", result);
            }

            return null;
        }
	}
}