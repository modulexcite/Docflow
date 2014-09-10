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
    public class DocumentGrid : Grid<DocumentTable>
    {
        private IEnumerable<DocumentTable> _displayingItems;

        public DocumentGrid(IQueryable<DocumentTable> items)
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
            ISearchService _SearchService = DependencyResolver.Current.GetService<ISearchService>();
            IEmplService _EmplService = DependencyResolver.Current.GetService<IEmplService>();

            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name);

            foreach (var displayedItem in _displayingItems)
            {
                displayedItem.isNotReview = _ReviewDocLogService.isNotReviewDocCurrentUser(displayedItem.Id, "", user);
                displayedItem.SLAStatus = _DocumentService.SLAStatus(displayedItem.Id, "", user);

                EmplTable empl = _EmplService.FirstOrDefault(x => x.ApplicationUserId == displayedItem.ApplicationUserCreatedId && x.CompanyTableId == displayedItem.CompanyTableId);
                displayedItem.FullName = empl.FullName;
                displayedItem.TitleName = empl.TitleName;
                displayedItem.DepartmentName = empl.DepartmentName;

                if (displayedItem.DocumentText != null && displayedItem.DocumentText.Length > 80)
                {
                    displayedItem.DocumentText = displayedItem.DocumentText.Substring(0, 80) + "...";
                }
            }

            return _displayingItems;
        }
    }

    public class DocumentAjaxPagingGrid : DocumentGrid
    {
        public DocumentAjaxPagingGrid(IQueryable<DocumentTable> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}