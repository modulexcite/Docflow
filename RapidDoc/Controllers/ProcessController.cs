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
using Microsoft.AspNet.Identity.EntityFramework;

namespace RapidDoc.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProcessController : BasicController
    {
        private readonly IProcessService _Service;
        private readonly IGroupProcessService _GroupProcessService;
        private readonly IWorkScheduleService _WorkScheduleService;

        public ProcessController(IProcessService service, IGroupProcessService groupProcessService, IWorkScheduleService workScheduleService, ICompanyService companyService, IAccountService accountService)
            : base(companyService, accountService)
        {
            _Service = service;
            _GroupProcessService = groupProcessService;
            _WorkScheduleService = workScheduleService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            var grid = new ProcessAjaxPagingGrid(_Service.GetAllView(), 1, false);
            return PartialView("_ProcessGrid", grid);
        }

        public JsonResult GetProcessList(int page)
        {
            var grid = new ProcessAjaxPagingGrid(_Service.GetAllView(), page, true);

            return Json(new
            {
                Html = RenderPartialViewToString("_ProcessGrid", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var items = Mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(context.Roles).ToList();
            items.Insert(0, new RoleViewModel { Name = UIElementRes.UIElement.NoValue, Id = String.Empty });
            ViewBag.RolesList = new SelectList(items, "Id", "Name", null);
            context.Dispose();

            ViewBag.GroupProcessList = _GroupProcessService.GetDropListGroupProcess(null);
            ViewBag.WorkScheduleList = _WorkScheduleService.GetDropListWorkSchedule(null);
            return View();
        }


        [HttpPost]
        public ActionResult Create(ProcessView model)
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

            ApplicationDbContext context = new ApplicationDbContext();
            var items = Mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(context.Roles).ToList();
            items.Insert(0, new RoleViewModel { Name = UIElementRes.UIElement.NoValue, Id = String.Empty });
            ViewBag.RolesList = new SelectList(items, "Id", "Name", null);
            context.Dispose();

            ViewBag.GroupProcessList = _GroupProcessService.GetDropListGroupProcess(null);
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

            ApplicationDbContext context = new ApplicationDbContext();
            var items = Mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(context.Roles).ToList();
            items.Insert(0, new RoleViewModel { Name = UIElementRes.UIElement.NoValue, Id = String.Empty });
            ViewBag.RolesList = new SelectList(items, "Id", "Name", model.RoleId);
            context.Dispose();

            ViewBag.GroupProcessList = _GroupProcessService.GetDropListGroupProcess(model.GroupProcessTableId);
            ViewBag.WorkScheduleList = _WorkScheduleService.GetDropListWorkSchedule(model.WorkScheduleTableId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProcessView model)
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

            ApplicationDbContext context = new ApplicationDbContext();
            var items = Mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(context.Roles).ToList();
            items.Insert(0, new RoleViewModel { Name = UIElementRes.UIElement.NoValue, Id = String.Empty });
            ViewBag.RolesList = new SelectList(items, "Id", "Name", model.RoleId);
            context.Dispose();

            ViewBag.GroupProcessList = _GroupProcessService.GetDropListGroupProcess(model.GroupProcessTableId);
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
