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
using System.Reflection;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RapidDoc.Controllers
{
    [Authorize(Roles = "Administrator, SetupAdministrator")]
    public class ItemCauseController : BasicController
    {
        private readonly IItemCauseService _Service;
        private readonly IDepartmentService _DepartmentService;

        public ItemCauseController(IItemCauseService Service, ICompanyService companyService, IAccountService accountService, IDepartmentService departmentService)
            : base(companyService, accountService)
        {
            _Service = Service;
            _DepartmentService = departmentService;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            var grid = new ItemCauseAjaxPagingGrid(_Service.GetAllView(), 1, false);
            return PartialView("_ItemCauseGrid", grid);
        }

        public JsonResult GetItemCausesList(int page)
        {
            var grid = new ItemCauseAjaxPagingGrid(_Service.GetAllView(), page, true);

            return Json(new
            {
                Html = RenderPartialViewToString("_ItemCauseGrid", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.DepartmentList = _DepartmentService.GetDropListDepartmentNull(null);

            return View();
        }

        [HttpPost]
        public ActionResult Create(ItemCauseView model)
        {
            ViewBag.DepartmentList = _DepartmentService.GetDropListDepartmentNull(null);
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

            ViewBag.DepartmentList = _DepartmentService.GetDropListDepartmentNull(null);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ItemCauseView model)
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

            ViewBag.DepartmentList = _DepartmentService.GetDropListDepartmentNull(null);

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