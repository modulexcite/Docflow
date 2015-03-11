using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;

namespace RapidDoc.Models.Grids
{
    public class ItemCauseGrid: Grid<ItemCauseView>
    {
        public ItemCauseGrid(IEnumerable<ItemCauseView> items)
            : base(items)
        {
        }
    }

    public class ItemCauseAjaxPagingGrid : ItemCauseGrid
    {
        public ItemCauseAjaxPagingGrid(IEnumerable<ItemCauseView> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page };
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }

}