﻿using AutoMapper;
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

namespace RapidDoc.Controllers
{
    [Authorize(Roles = "Administrator, SetupAdministrator")]
    public class CompanyController : BasicController
    {
        private readonly IDomainService _DomainService;

        public CompanyController(ICompanyService companyService, IDomainService DomainService, IAccountService accountService)
            : base(companyService, accountService)
        {
            _DomainService = DomainService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            var grid = new CompanyAjaxPagingGrid(_CompanyService.GetAllView(), 1, false);
            return PartialView("_CompanyGrid", grid);
        }

        public JsonResult GetCompanyList(int page)
        {
            var grid = new CompanyAjaxPagingGrid(_CompanyService.GetAllView(), page, true);

            return Json(new
            {
                Html = RenderPartialViewToString("_CompanyGrid", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.DomainList = _DomainService.GetDropListDomainNull(null);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompanyView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _CompanyService.Save(model);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.GetOriginalException().Message);
                }
            }
            ViewBag.DomainList = _DomainService.GetDropListDomainNull(null);
            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            var model = _CompanyService.FindView(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.DomainList = _DomainService.GetDropListDomainNull(model.DomainTableId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _CompanyService.Save(model);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.GetOriginalException().Message);
                }
            }
            ViewBag.DomainList = _DomainService.GetDropListDomainNull(model.DomainTableId);
            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            var model = _CompanyService.FindView(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                _CompanyService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.GetOriginalException().Message);
            }

            var model = _CompanyService.FindView(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        public ActionResult Detail(Guid id)
        {
            var model = _CompanyService.FindView(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
    }
}
