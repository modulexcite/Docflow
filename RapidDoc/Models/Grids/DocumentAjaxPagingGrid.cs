using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;

namespace RapidDoc.Models.Grids
{
    public class DocumentGrid : Grid<DocumentListView>
    {
        public DocumentGrid(IEnumerable<DocumentListView> items)
            : base(items)
        {
        }
    }

    public class DocumentAjaxPagingGrid : DocumentGrid
    {
        public DocumentAjaxPagingGrid(IEnumerable<DocumentListView> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}