using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;

namespace RapidDoc.Models.Grids
{
    public class DomainGrid : Grid<DomainView>
    {
        public DomainGrid(IEnumerable<DomainView> items)
            : base(items)
        {
        }
    }

    public class DomainAjaxPagingGrid : DomainGrid
    {
        public DomainAjaxPagingGrid(IEnumerable<DomainView> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}