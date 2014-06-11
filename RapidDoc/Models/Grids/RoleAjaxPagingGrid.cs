using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;

namespace RapidDoc.Models.Grids
{
    public class RoleGrid : Grid<RoleViewModel>
    {
        public RoleGrid(IEnumerable<RoleViewModel> items)
            : base(items)
        {
        }
    }

    public class RoleAjaxPagingGrid : RoleGrid
    {
        public RoleAjaxPagingGrid(IEnumerable<RoleViewModel> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}