using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;

namespace RapidDoc.Models.Grids
{
    public class DelegationGrid : Grid<DelegationView>
    {
        public DelegationGrid(IEnumerable<DelegationView> items)
            : base(items)
        {
        }
    }

    public class DelegationAjaxPagingGrid : DelegationGrid
    {
        public DelegationAjaxPagingGrid(IEnumerable<DelegationView> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}