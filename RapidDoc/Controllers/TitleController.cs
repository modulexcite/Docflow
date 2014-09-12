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
    public class TitleController : BasicController
    {
        private readonly ITitleService _Service;
        private readonly IProfileService _ProfileService;

        public TitleController(ITitleService Service, IProfileService profileService, ICompanyService companyService, IAccountService accountService)
            : base(companyService, accountService)
        {
            _Service = Service;
            _ProfileService = profileService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            var grid = new TitleAjaxPagingGrid(_Service.GetAllView(), 1, false);
            return PartialView("_TitleGrid", grid);
        }

        public JsonResult GetTitleList(int page)
        {
            var grid = new TitleAjaxPagingGrid(_Service.GetAllView(), page, true);

            return Json(new
            {
                Html = RenderPartialViewToString("_TitleGrid", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.ProfileList = _ProfileService.GetDropListProfileNull(null);
            return View();
        }

        [HttpPost]
        public ActionResult Create(TitleView model)
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

            ViewBag.ProfileList = _ProfileService.GetDropListProfileNull(null);
            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            var model = _Service.FindView(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProfileList = _ProfileService.GetDropListProfileNull(model.ProfileTableId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TitleView model)
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

            ViewBag.ProfileList = _ProfileService.GetDropListProfileNull(model.ProfileTableId);
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
