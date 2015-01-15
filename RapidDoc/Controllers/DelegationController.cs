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
    public class DelegationController : BasicController
    {
        private readonly IDelegationService _Service;
        private readonly IGroupProcessService _GroupProcessService;
        private readonly IProcessService _ProcessService;
        private readonly IEmplService _EmplService;
        private readonly IEmailService _EmailService;

        public DelegationController(IUnitOfWork uow, IDelegationService service, ICompanyService companyService,
            IGroupProcessService groupProcessService, IProcessService processService, IEmplService emplService, IEmailService emailService, IAccountService accountService)
            : base(uow, companyService, accountService)
        {
            _Service = service;
            _GroupProcessService = groupProcessService;
            _ProcessService = processService;
            _EmplService = emplService;
            _EmailService = emailService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            var grid = new DelegationAjaxPagingGrid(_Service.GetAllView(), 1, false);
            return PartialView("_DelegationGrid", grid);
        }

        public JsonResult GetDelegationList(int page)
        {
            var grid = new DelegationAjaxPagingGrid(_Service.GetAllView(), page, true);

            return Json(new
            {
                Html = RenderPartialViewToString("_DelegationGrid", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.CompanyList = _CompanyService.GetDropListCompany(null);
            ViewBag.GroupProcessList = _GroupProcessService.GetDropListGroupProcessNull(null);
            ViewBag.ProcessList = _ProcessService.GetDropListProcessNull(null);
            var droplistTmp = _EmplService.GetDropListEmplNull(null);
            ViewBag.EmplToList = droplistTmp;
            ViewBag.EmplFromList = droplistTmp;
            return View();
        }

        [HttpPost]
        public ActionResult Create(DelegationView model)
        {
            if (ModelState.IsValid)
            {
                model.DateFrom = model.DateFrom.HasValue ? model.DateFrom : model.DateFrom = DateTime.UtcNow;
                model.DateTo = model.DateTo.HasValue ? model.DateTo : model.DateTo = DateTime.UtcNow.AddDays(1);

                try
                {
                    _Service.Save(model);
                    _EmailService.SendDelegationEmplEmail(model);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.GetOriginalException().Message);
                }
            }

            ViewBag.CompanyList = _CompanyService.GetDropListCompany(null);
            ViewBag.GroupProcessList = _GroupProcessService.GetDropListGroupProcessNull(null);
            ViewBag.ProcessList = _ProcessService.GetDropListProcessNull(null);
            var droplistTmp = _EmplService.GetDropListEmplNull(null);
            ViewBag.EmplToList = droplistTmp;
            ViewBag.EmplFromList = droplistTmp;
            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            var model = _Service.FindView(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.CompanyList = _CompanyService.GetDropListCompany(model.CompanyTableId);
            ViewBag.GroupProcessList = _GroupProcessService.GetDropListGroupProcessNull(model.GroupProcessTableId);
            ViewBag.ProcessList = _ProcessService.GetDropListProcessNull(model.ProcessTableId);
            ViewBag.EmplToList = _EmplService.GetDropListEmplNull(model.EmplTableToId);
            ViewBag.EmplFromList = _EmplService.GetDropListEmplNull(model.EmplTableFromId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DelegationView model)
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

            ViewBag.CompanyList = _CompanyService.GetDropListCompany(model.CompanyTableId);
            ViewBag.GroupProcessList = _GroupProcessService.GetDropListGroupProcessNull(model.GroupProcessTableId);
            ViewBag.ProcessList = _ProcessService.GetDropListProcessNull(model.ProcessTableId);
            ViewBag.EmplToList = _EmplService.GetDropListEmplNull(model.EmplTableToId);
            ViewBag.EmplFromList = _EmplService.GetDropListEmplNull(model.EmplTableFromId);
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
    }
}
