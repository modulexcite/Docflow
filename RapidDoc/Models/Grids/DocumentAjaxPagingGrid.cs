using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;

namespace RapidDoc.Models.Grids
{
    public class DocumentGrid : Grid<DocumentTable>
    {
        public DocumentGrid(IEnumerable<DocumentTable> items)
            : base(items)
        {
        }
    }

    public class DocumentAjaxPagingGrid : DocumentGrid
    {
        public DocumentAjaxPagingGrid(IEnumerable<DocumentTable> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}