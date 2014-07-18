using AutoMapper;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Services;
using RapidDoc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Extensions;
using RapidDoc.Models.Repository;
using RapidDoc.Models.Grids;

namespace RapidDoc.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class WorkScheduleController : BasicController
    {
        private readonly IWorkScheduleService _Service;

        public WorkScheduleController(IWorkScheduleService Service)
        {
            _Service = Service;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            var grid = new WorkScheduleAjaxPagingGrid(_Service.GetAllView(), 1, false);
            return PartialView("_WorkScheduleGrid", grid);
        }

        public JsonResult GetWorkScheduleList(int page)
        {
            var grid = new WorkScheduleAjaxPagingGrid(_Service.GetAllView(), page, true);

            return Json(new
            {
                Html = RenderPartialViewToString("_WorkScheduleGrid", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(WorkScheduleView model)
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

            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            var model = _Service.FindView(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(WorkScheduleView model)
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

        public ActionResult Calendar(Guid id)
        {
            ViewBag.WorkScheduleId = id;
            return View();
        }

        [HttpPost]
        public JsonResult Calendar(Guid id, DateTime date)
        {
            _Service.SaveDayToCalendar(id, date);
            return Json(new { });
        }

        public JsonResult GetDaysOff(Guid id)
        {
            DateTime[] days = _Service.GetDaysOff(id);
            return Json(new { days = days }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckDayOff(Guid id, int day, int month, int year)
        {
            DateTime date = new DateTime(year, month, day);

            if (_Service.CheckDayType(id, date))
            {
                return Json(new { dayOff = "true" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { dayOff = "false" }, JsonRequestBehavior.AllowGet);
        }
    }
}
