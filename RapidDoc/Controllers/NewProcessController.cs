﻿using System;
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

        protected UserManager<ApplicationUser> UserManager { get; private set; }
        protected RoleManager<ApplicationRole> RoleManager { get; private set; }

        public NewProcessController(IProcessService processService, IGroupProcessService groupProcessService, IEmplService emplService, IDocumentService documentService, IAccountService accountService, ICompanyService companyService)
            : base(companyService, accountService)
        {
            _ProcessService = processService;
            _GroupProcessService = groupProcessService;
            _EmplService = emplService;
            _DocumentService = documentService;

            ApplicationDbContext dbContext = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbContext));
            RoleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(dbContext));
        }

        [OutputCache(Duration = 86400, VaryByParam = "id")]
        public ActionResult Index(string id)
        {
            ApplicationUser user = _AccountService.Find(id);
            EmplTable emplTable = _EmplService.FirstOrDefault(x => x.ApplicationUserId == user.Id && x.Enable == true );
            if (emplTable == null)
            {
                ModelState.AddModelError(string.Empty, String.Format(ValidationRes.ValidationResource.ErrorEmplNotFound, User.Identity.Name));
            }

            var allCompanyProcesses = _ProcessService.GetPartialIntercompany(x => x.CompanyTableId == user.CompanyTableId && x.isApproved == true).Select(z => z.Id).ToList();
            List<ProcessView> topProcess = new List<ProcessView>();
            DateTime startDateTopProcess = DateTime.UtcNow.AddDays(-30);
            var processes = _DocumentService.GetPartial(x => x.CreatedDate >= startDateTopProcess).GroupBy(x => x.ProcessTableId).Where(z => allCompanyProcesses.Contains(z.Key)).Select(g => new { ProcessTableId = g.Key, Count = g.Count() }).OrderByDescending(i => i.Count).Select(y => y.ProcessTableId).ToList();         
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

                    if (process != null && CheckCreateProcess(process, user.Id))
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
            if ((startTime > date || date > endTime) && process.StartWorkTime != process.EndWorkTime) return false;

            if (!String.IsNullOrEmpty(process.RoleId))
            {
                string roleName = RoleManager.FindById(process.RoleId).Name;
                if (!UserManager.IsInRole(UserId, roleName))
                {
                    return false;
                }
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
                List<GroupProcessView> resultListGroup = new List<GroupProcessView>();
                foreach (var item in groupChildItems)
                {
                    if (_ProcessService.Contains(x => x.GroupProcessTableId == item.Id && x.isApproved == true) || _GroupProcessService.Contains(x => x.GroupProcessParentId == item.Id))
                        resultListGroup.Add(item);
                }

                return View("ChildGroup", resultListGroup);
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