using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;

namespace RapidDoc.Models.Grids
{
    public class CompanyGrid : Grid<CompanyView>
    {
        public CompanyGrid(IEnumerable<CompanyView> items)
            : base(items)
        {
        }
    }

    public class CompanyAjaxPagingGrid : CompanyGrid
    {
        public CompanyAjaxPagingGrid(IEnumerable<CompanyView> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}