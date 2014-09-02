using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;
using System.Web.Mvc;
using RapidDoc.Models.Services;

namespace RapidDoc.Models.Grids
{
    public class AgreedDocumentGrid : Grid<DocumentTable>
    {
        private IEnumerable<DocumentTable> _displayingItems;

        public AgreedDocumentGrid(IQueryable<DocumentTable> items)
            : base(items)
        {
        }

        protected override IEnumerable<DocumentTable> GetItemsToDisplay()
        {
            if (_displayingItems != null)
                return _displayingItems;

            _displayingItems = base.GetItemsToDisplay().ToList();

            IReviewDocLogService _ReviewDocLogService = DependencyResolver.Current.GetService<IReviewDocLogService>();
            IDocumentService _DocumentService = DependencyResolver.Current.GetService<IDocumentService>();
            IAccountService _AccountService = DependencyResolver.Current.GetService<IAccountService>();
            IEmplService _EmplService = DependencyResolver.Current.GetService<IEmplService>();

            foreach (var displayedItem in _displayingItems)
            {
                EmplTable empl = _EmplService.FirstOrDefault(x => x.ApplicationUserId == displayedItem.ApplicationUserCreatedId && x.CompanyTableId == displayedItem.CompanyTableId);
                displayedItem.FullName = empl.FullName;
                displayedItem.TitleName = empl.TitleName;
                displayedItem.DepartmentName = empl.DepartmentName;

                displayedItem.isNotReview = false;
                displayedItem.SLAStatus = _DocumentService.SLAStatus(displayedItem.Id);
            }

            return _displayingItems;
        }
    }

    public class AgreedDocumentAjaxPagingGrid : DocumentGrid
    {
        public AgreedDocumentAjaxPagingGrid(IQueryable<DocumentTable> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}