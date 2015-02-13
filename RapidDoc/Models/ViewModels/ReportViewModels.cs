using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Repository;
using System.Drawing;

namespace RapidDoc.Models.ViewModels
{
    public class ReportParametersBasicView
    {
        [Display(Name = "StartDate", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public DateTime StartDate { get; set; }

        [Display(Name = "EndDate", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public DateTime EndDate { get; set; }

        [Display(Name = "DepartmentName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public String DepartmentName { get; set; }
        public Guid? DepartmentTableId { get; set; }
    }

    public class ReportProcessesView
    {
        public ProcessTable Process { get; set; }

        public string StageName { get; set; }
        public FilterType FilterType { get; set; }
        public string FilterText { get; set; }
        public Color Color { get; set; }
        
        public List<EmplTable> Names { get; set; }

    }
}