using RapidDoc.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
}