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
    [Authorize(Roles = "Administrator, SetupAdministrator")]
    public class DepartmentController : BasicController
    {
        private readonly IDepartmentService _Service;

        public DepartmentController(IUnitOfWork uow, IDepartmentService Service, ICompanyService companyService, IAccountService accountService)
            : base(uow, companyService, accountService)
        {
            _Service = Service;
        }
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            var grid = new DepartmentAjaxPagingGrid(_Service.GetAllView(), 1, false);
            return PartialView("_DepartmentGrid", grid);
        }

        public JsonResult GetDepartmentList(int page)
        {
            var grid = new DepartmentAjaxPagingGrid(_Service.GetAllView(), page, true);

            return Json(new
            {
                Html = RenderPartialViewToString("_DepartmentGrid", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.DepartmentList = _Service.GetDropListDepartmentNull(null);
            return View();
        }

        [HttpPost]
        public ActionResult Create(DepartmentView model)
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
            ViewBag.DepartmentList = _Service.GetDropListDepartmentNull(null);
            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            var model = _Service.FindView(id);
            
            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.DepartmentList = _Service.GetDropListDepartmentNull(model.ParentDepartmentId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DepartmentView model)
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
            ViewBag.DepartmentList = _Service.GetDropListDepartmentNull(model.ParentDepartmentId);
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
