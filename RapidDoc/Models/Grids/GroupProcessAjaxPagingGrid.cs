using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;

namespace RapidDoc.Models.Grids
{
    public class GroupProcessGrid : Grid<GroupProcessView>
    {
        public GroupProcessGrid(IEnumerable<GroupProcessView> items)
            : base(items)
        {
        }
    }

    public class GroupProcessAjaxPagingGrid : GroupProcessGrid
    {
        public GroupProcessAjaxPagingGrid(IEnumerable<GroupProcessView> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}