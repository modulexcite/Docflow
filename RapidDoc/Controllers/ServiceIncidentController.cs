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

namespace RapidDoc.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ServiceIncidentController : BasicController
    {
        private readonly IServiceIncidentService _Service;
        public ApplicationDbContext context { get; private set; }

        public ServiceIncidentController(IServiceIncidentService Service)
        {
            context = new ApplicationDbContext();
            _Service = Service;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            var grid = new ServiceIncidentAjaxPagingGrid(_Service.GetAllView(), 1, false);
            return PartialView("_ServiceIncidentGrid", grid);
        }

        public JsonResult GetServiceIncidentList(int page)
        {
            var grid = new ServiceIncidentAjaxPagingGrid(_Service.GetAllView(), page, true);

            return Json(new
            {
                Html = RenderPartialViewToString("_ServiceIncidentGrid", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View(_Service.GetNewModel());
        }

        [HttpPost]
        public ActionResult Create(ServiceIncidentView model)
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

	}
}