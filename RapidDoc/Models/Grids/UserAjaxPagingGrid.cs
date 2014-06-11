using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;

namespace RapidDoc.Models.Grids
{
    public class UserGrid : Grid<UserViewModel>
    {
        public UserGrid(IEnumerable<UserViewModel> items)
            : base(items)
        {
        }
    }

    public class UserAjaxPagingGrid : UserGrid
    {
        public UserAjaxPagingGrid(IEnumerable<UserViewModel> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}