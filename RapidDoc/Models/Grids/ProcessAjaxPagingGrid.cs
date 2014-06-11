using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;

namespace RapidDoc.Models.Grids
{
    public class ProcessGrid : Grid<ProcessView>
    {
        public ProcessGrid(IEnumerable<ProcessView> items)
            : base(items)
        {
        }
    }

    public class ProcessAjaxPagingGrid : ProcessGrid
    {
        public ProcessAjaxPagingGrid(IEnumerable<ProcessView> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}