using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;

namespace RapidDoc.Models.Grids
{
    public class NumberSeriesGrid : Grid<NumberSeriesView>
    {
        public NumberSeriesGrid(IEnumerable<NumberSeriesView> items)
            : base(items)
        {
        }
    }

    public class NumberSeriesAjaxPagingGrid : NumberSeriesGrid
    {
        public NumberSeriesAjaxPagingGrid(IEnumerable<NumberSeriesView> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}