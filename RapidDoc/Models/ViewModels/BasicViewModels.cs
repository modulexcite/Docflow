using RapidDoc.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RapidDoc.Models.Interfaces;

namespace RapidDoc.Models.ViewModels
{
    public abstract class BasicView
    {
        public Guid? Id { get; set; }

        [Display(Name = "CreatedBy", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string CreatedBy { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "ModifiedBy", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string ModifiedBy { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public DateTime ModifiedDate { get; set; }
    }

    public abstract class BasicCompanyNullView : BasicView
    {
        public Guid? CompanyTableId { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string AliasCompanyName { get; set; }
    }

    public abstract class BasicCompanyNotNullView : BasicView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public Guid CompanyTableId { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string AliasCompanyName { get; set; }
    }

    public abstract class BasicDocumentView : BasicCompanyNullView, IDocument
    {
        public Guid DocumentTableId { get; set; }
    }

    public abstract class BasicDocumentRequestView : BasicDocumentView
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "RequestText", ResourceType = typeof(CustomRes.Custom))]
        public string RequestText { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Users", ResourceType = typeof(CustomRes.Custom))]
        public string Users { get; set; }
    }
}