using GridMvc;
using GridMvc.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RapidDoc.Models.Grids
{
    public class AjaxGridPager : IGridPager
    {
        private readonly IGrid _grid;

        public AjaxGridPager(IGrid grid)
        {
            _grid = grid;
        }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public string TemplateName
        {
            get
            {
                //Custom view name to render this pager
                return "_AjaxGridPager";
            }
        }

        /// <summary>
        ///     Returns true if the pager has pages
        /// </summary>
        public bool HasPages
        {
            get
            {
                return _grid.ItemsToDisplay.Count() >= PageSize;
            }
        }

        public void Initialize<T>(IQueryable<T> items)
        {
            //do nothing
        }
    }
}