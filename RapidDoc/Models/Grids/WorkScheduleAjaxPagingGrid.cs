using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;

namespace RapidDoc.Models.Grids
{
    public class WorkScheduleGrid : Grid<WorkScheduleView>
    {
        public WorkScheduleGrid(IEnumerable<WorkScheduleView> items)
            : base(items)
        {
        }
    }

    public class WorkScheduleAjaxPagingGrid : WorkScheduleGrid
    {
        public WorkScheduleAjaxPagingGrid(IEnumerable<WorkScheduleView> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}