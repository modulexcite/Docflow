using GridMvc;
using RapidDoc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RapidDoc.Models.Grids
{
    public class ProfileGrid : Grid<ProfileView>
    {
        public ProfileGrid(IEnumerable<ProfileView> items)
            : base(items)
        {
        }
    }

    public class ProfileAjaxPagingGrid : ProfileGrid
    {
        public ProfileAjaxPagingGrid(IEnumerable<ProfileView> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}