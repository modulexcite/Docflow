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

        private readonly IReviewDocLogService _ReviewDocLogService;
        private readonly IDocumentService _DocumentService;
        private readonly IAccountService _AccountService;
        private readonly ISearchService _SearchService;
        private readonly IEmplService _EmplService;

        public AgreedDocumentGrid(IQueryable<DocumentTable> items, IReviewDocLogService reviewDocLogService, IDocumentService documentService, IAccountService accountService, ISearchService searchService, IEmplService emplService)
            : base(items)
        {
            _ReviewDocLogService = reviewDocLogService;
            _DocumentService = documentService;
            _AccountService = accountService;
            _SearchService = searchService;
            _EmplService = emplService;
        }

        protected override IEnumerable<DocumentTable> GetItemsToDisplay()
        {
            if (_displayingItems != null)
                return _displayingItems;

            _displayingItems = base.GetItemsToDisplay().ToList();

            /*
            IReviewDocLogService _ReviewDocLogService = DependencyResolver.Current.GetService<IReviewDocLogService>();
            IDocumentService _DocumentService = DependencyResolver.Current.GetService<IDocumentService>();
            IAccountService _AccountService = DependencyResolver.Current.GetService<IAccountService>();
            IEmplService _EmplService = DependencyResolver.Current.GetService<IEmplService>();
            */
 
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
        public AgreedDocumentAjaxPagingGrid(IQueryable<DocumentTable> items, int page, bool renderOnlyRows, IReviewDocLogService reviewDocLogService, IDocumentService documentService, IAccountService accountService, ISearchService searchService, IEmplService emplService)
            : base(items, reviewDocLogService, documentService, accountService, searchService, emplService)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}