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
        private readonly IDocumentService _DocumentService;

        public NewProcessController(IProcessService processService, IGroupProcessService groupProcessService, IEmplService emplService, IDocumentService documentService, IAccountService accountService, ICompanyService companyService)
            : base(companyService, accountService)
        {
            _ProcessService = processService;
            _GroupProcessService = groupProcessService;
            _EmplService = emplService;
            _DocumentService = documentService;
        }

        public ActionResult Index()
        {
            ApplicationUser user = _AccountService.Find(User.Identity.GetUserId());
            EmplTable emplTable = _EmplService.FirstOrDefault(x => x.ApplicationUserId == user.Id && x.CompanyTableId == user.CompanyTableId );
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

                    List<ProcessView> result = new List<ProcessView>();
                    ProcessView process = _ProcessService.FindView(processId);

                    if (CheckCreateProcess(process, user.Id))
                    {
                        num++;
                        topProcess.Add(process);
                    }
                }
            }

            ViewBag.TopProcess = topProcess;

            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(user.TimeZoneId);
            ViewBag.CurrentTimeZoneOffset = timeZoneInfo.BaseUtcOffset;
            return View(_GroupProcessService.GetPartialView(x => x.GroupProcessParentId == null));
        }

        public bool CheckCreateProcess(ProcessView process, String UserId)
        {
            DateTime date = DateTime.UtcNow;
            DateTime startTime = new DateTime(date.Year, date.Month, date.Day) + process.StartWorkTime;
            DateTime endTime = new DateTime(date.Year, date.Month, date.Day) + process.EndWorkTime;
            if ((startTime < date || date > endTime) && process.StartWorkTime != process.EndWorkTime) return false;

            if (!String.IsNullOrEmpty(process.RoleId))
            {
                ApplicationDbContext context = new ApplicationDbContext();
                UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                string roleName = RoleManager.FindById(process.RoleId).Name;
                if (!UserManager.IsInRole(UserId, roleName))
                {
                    context.Dispose();
                    return false;
                }
                context.Dispose();
            }

            return true;
        }

        public ActionResult ProcessList(Guid groupProcessId)
        {
            List<GroupProcessTable> breadCrumbsList = new List<GroupProcessTable>();
            breadCrumbsList = GetBreadCrumbs(breadCrumbsList, groupProcessId);
            ViewBag.BreadCrumbs = breadCrumbsList;
            ApplicationUser user = _AccountService.Find(User.Identity.GetUserId());
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(user.TimeZoneId);
            ViewBag.CurrentTimeZoneOffset = timeZoneInfo.BaseUtcOffset;

            var groupChildItems = _GroupProcessService.GetPartialView(x => x.GroupProcessParentId == groupProcessId);
            if(groupChildItems.Count() == 0)
            {
                var model = _ProcessService.GetPartialView(x => x.GroupProcessTableId == groupProcessId && x.isApproved == true).OrderBy(x => x.ProcessName);

                ApplicationDbContext context = new ApplicationDbContext();
                UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                List<ProcessView> result = new List<ProcessView>();

                foreach (var item in model)
                {
                    if (CheckCreateProcess(item, user.Id))
                    {
                        result.Add(item);
                    }
                }

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

            if (searchString.Length >= 1)
            {
                var model = _ProcessService.GetPartialView(x => x.ProcessName.Contains(searchString));
                
                ApplicationDbContext context = new ApplicationDbContext();
                UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                List<ProcessView> result = new List<ProcessView>();

                ApplicationUser user = _AccountService.Find(User.Identity.GetUserId());
                var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(user.TimeZoneId);
                ViewBag.CurrentTimeZoneOffset = timeZoneInfo.BaseUtcOffset;

                foreach (var item in model)
                {
                    if (CheckCreateProcess(item, user.Id))
                    {
                        result.Add(item);
                    }
                }

                return PartialView("_SearchResultProcess", result);
            }

            return null;
        }
	}
}