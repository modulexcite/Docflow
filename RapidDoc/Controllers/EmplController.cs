using AutoMapper;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Services;
using RapidDoc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Extensions;
using RapidDoc.Models.Grids;

namespace RapidDoc.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class EmplController : BasicController
    {
        private readonly IEmplService _Service;
        private readonly ITitleService _TitleService;
        private readonly IProfileService _ProfileService;
        private readonly IDepartmentService _DepartmentService;
        private readonly IWorkScheduleService _WorkScheduleService;

        public EmplController(IEmplService service, ITitleService titleService, IProfileService profileService, ICompanyService companyService,
            IDepartmentService departmentService, IAccountService accountService, IWorkScheduleService workScheduleService)
            : base(companyService, accountService)            
        {
            _Service = service;
            _TitleService = titleService;
            _ProfileService = profileService;
            _DepartmentService = departmentService;
            _WorkScheduleService = workScheduleService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            var grid = new EmplAjaxPagingGrid(_Service.GetAllView(), 1, false);
            return PartialView("_EmplGrid", grid);
        }

        public JsonResult GetEmplList(int page)
        {
            var grid = new EmplAjaxPagingGrid(_Service.GetAllView(), page, true);

            return Json(new
            {
                Html = RenderPartialViewToString("_EmplGrid", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.TitleList = _TitleService.GetDropListTitleNull(null);
            ViewBag.ProfileList = _ProfileService.GetDropListProfileNull(null);
            ViewBag.ManageList = _Service.GetDropListEmplNull(null);
            ViewBag.CompanyList = _CompanyService.GetDropListCompanyNull(null);
            ViewBag.DepartmentList = _DepartmentService.GetDropListDepartmentNull(null);
            ViewBag.UserList = _AccountService.GetDropListUserNull(null);
            ViewBag.WorkScheduleList = _WorkScheduleService.GetDropListWorkSchedule(null);
            return View();
        }


        [HttpPost]
        public ActionResult Create(EmplView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _Service.Save(model);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.GetOriginalException().Message);
                }
            }
            ViewBag.TitleList = _TitleService.GetDropListTitleNull(null);
            ViewBag.ProfileList = _ProfileService.GetDropListProfileNull(null);
            ViewBag.ManageList = _Service.GetDropListEmplNull(null);
            ViewBag.CompanyList = _CompanyService.GetDropListCompanyNull(null);
            ViewBag.DepartmentList = _DepartmentService.GetDropListDepartmentNull(null);
            ViewBag.UserList = _AccountService.GetDropListUserNull(null);
            ViewBag.WorkScheduleList = _WorkScheduleService.GetDropListWorkSchedule(null);

            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            var model = _Service.FindView(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.TitleList = _TitleService.GetDropListTitleNull(model.TitleTableId);
            ViewBag.ProfileList = _ProfileService.GetDropListProfileNull(model.ProfileTableId);
            ViewBag.ManageList = _Service.GetDropListEmplNull(model.ManageId);
            ViewBag.CompanyList = _CompanyService.GetDropListCompanyNull(model.CompanyTableId);
            ViewBag.DepartmentList = _DepartmentService.GetDropListDepartmentNull(model.DepartmentTableId);
            ViewBag.UserList = _AccountService.GetDropListUserNull(model.ApplicationUserId);
            ViewBag.WorkScheduleList = _WorkScheduleService.GetDropListWorkSchedule(model.WorkScheduleTableId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EmplView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _Service.Save(model);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.GetOriginalException().Message);
                }
            }
            ViewBag.TitleList = _TitleService.GetDropListTitleNull(model.TitleTableId);
            ViewBag.ProfileList = _ProfileService.GetDropListProfileNull(model.ProfileTableId);
            ViewBag.ManageList = _Service.GetDropListEmplNull(model.ManageId);
            ViewBag.CompanyList = _CompanyService.GetDropListCompanyNull(model.CompanyTableId);
            ViewBag.DepartmentList = _DepartmentService.GetDropListDepartmentNull(model.DepartmentTableId);
            ViewBag.UserList = _AccountService.GetDropListUserNull(model.ApplicationUserId);
            ViewBag.WorkScheduleList = _WorkScheduleService.GetDropListWorkSchedule(model.WorkScheduleTableId);

            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            var model = _Service.FindView(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                _Service.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.GetOriginalException().Message);
            }

            var model = _Service.FindView(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        public ActionResult Detail(Guid id)
        {
            var model = _Service.FindView(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
    }
}
