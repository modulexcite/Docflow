using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;

namespace RapidDoc.Models.Grids
{
    public class EmplGrid : Grid<EmplView>
    {
        public EmplGrid(IEnumerable<EmplView> items)
            : base(items)
        {
        }
    }

    public class EmplAjaxPagingGrid : EmplGrid
    {
        public EmplAjaxPagingGrid(IEnumerable<EmplView> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}