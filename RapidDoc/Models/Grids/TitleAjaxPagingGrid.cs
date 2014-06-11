using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;

namespace RapidDoc.Models.Grids
{
    public class TitleGrid : Grid<TitleView>
    {
        public TitleGrid(IEnumerable<TitleView> items)
            : base(items)
        {
        }
    }

    public class TitleAjaxPagingGrid : TitleGrid
    {
        public TitleAjaxPagingGrid(IEnumerable<TitleView> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; //override  default pager
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}