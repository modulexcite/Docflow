using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;

namespace RapidDoc.Models.Grids
{
    public class TripSettingsGrid : Grid<TripSettingsView>
    {
        public TripSettingsGrid(IEnumerable<TripSettingsView> items)
            : base(items)
        {
        }
    }

    public class TripSettingsAjaxPagingGrid : TripSettingsGrid
    {
        public TripSettingsAjaxPagingGrid(IEnumerable<TripSettingsView> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; 
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}