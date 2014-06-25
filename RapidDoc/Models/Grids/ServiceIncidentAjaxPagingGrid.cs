using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using GridMvc;

namespace RapidDoc.Models.Grids
{
    public class ServiceIncidentGrid : Grid<ServiceIncidentView>
    {
        public ServiceIncidentGrid(IEnumerable<ServiceIncidentView> items)
            : base(items)
        {
        }
    }

    public class ServiceIncidentAjaxPagingGrid : ServiceIncidentGrid
    {
        public ServiceIncidentAjaxPagingGrid(IEnumerable<ServiceIncidentView> items, int page, bool renderOnlyRows)
            : base(items)
        {
            Pager = new AjaxGridPager(this) { CurrentPage = page }; 
            RenderOptions.RenderRowsOnly = renderOnlyRows;
            EnablePaging = true;
        }
    }
}