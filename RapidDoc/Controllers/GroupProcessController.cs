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
using RapidDoc.Models.Grids;

namespace RapidDoc.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class GroupProcessController : BasicController
    {
        private readonly IGroupProcessService _Service;
        private readonly INumberSeqService _NumberSeqService;

        public GroupProcessController(IGroupProcessService Service, INumberSeqService NumberSeqService)
        {
            _Service = Service;
            _NumberSeqService = NumberSeqService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            var grid = new GroupProcessAjaxPagingGrid(_Service.GetAllView(), 1, false);
            return PartialView("_GroupProcessGrid", grid);
        }

        public JsonResult GetGroupProcessList(int page)
        {
            var grid = new GroupProcessAjaxPagingGrid(_Service.GetAllView(), page, true);

            return Json(new
            {
                Html = RenderPartialViewToString("_GroupProcessGrid", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.NumberSeqList = _NumberSeqService.GetDropListNumberSeqNull(null);
            ViewBag.GroupProcessParent = _Service.GetDropListGroupProcessNull(null);
            return View();
        }

        [HttpPost]
        public ActionResult Create(GroupProcessView model)
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

            ViewBag.NumberSeqList = _NumberSeqService.GetDropListNumberSeqNull(null);
            ViewBag.GroupProcessParent = _Service.GetDropListGroupProcessNull(null);
            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            var model = _Service.FindView(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.NumberSeqList = _NumberSeqService.GetDropListNumberSeqNull(model.NumberSeriesTableId);
            ViewBag.GroupProcessParent = _Service.GetDropListGroupProcessNull(model.GroupProcessParentId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(GroupProcessView model)
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
            ViewBag.NumberSeqList = _NumberSeqService.GetDropListNumberSeqNull(model.NumberSeriesTableId);
            ViewBag.GroupProcessParent = _Service.GetDropListGroupProcessNull(model.GroupProcessParentId);
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
