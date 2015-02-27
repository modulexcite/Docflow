using RapidDoc.Models.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RapidDoc.Attributes.Validation;

namespace RapidDoc.Models.ViewModels
{
    public class CompanyView : BasicView
    {
        [StringLength(20, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Display(Name = "AliasCompanyName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        [Required(ErrorMessageResourceName = "ErrorFieldisNull", ErrorMessageResourceType = typeof(ValidationRes.ValidationResource))]
        public string AliasCompanyName { get; set; }

        [StringLength(256, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Display(Name = "CompanyName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        [Required(ErrorMessageResourceName = "ErrorFieldisNull", ErrorMessageResourceType = typeof(ValidationRes.ValidationResource))]
        public string CompanyName { get; set; }

        [Display(Name = "DomainName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string DomainName { get; set; }

        public Guid? DomainTableId { get; set; }
    }

    public class DomainView : BasicView
    {
        [StringLength(20, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Display(Name = "DomainName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        [Required(ErrorMessageResourceName = "ErrorFieldisNull", ErrorMessageResourceType = typeof(ValidationRes.ValidationResource))]
        public string DomainName { get; set; }

        [StringLength(256, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Display(Name = "LDAPServer", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string LDAPServer { get; set; }

        [Range(0, 65535, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorRangePort")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Port", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public int LDAPPort { get; set; }

        [StringLength(256, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Display(Name = "Login", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string LDAPLogin { get; set; }

        [StringLength(256, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Display(Name = "Password", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        [DataType(DataType.Password)]
        public string LDAPPassword { get; set; }

        [StringLength(256, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Display(Name = "LDAPBaseDN", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string LDAPBaseDN { get; set; }
    }

    public class NumberSeriesView : BasicView
    {
        [StringLength(20, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "NumberSeriesName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string NumberSeriesName { get; set; }

        [StringLength(5, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "NumberSeriesPrefix", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string Prefix { get; set; }

        [Range(3, 10, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorRangeNumberSeqSize")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "NumberSeriesSize", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public int Size { get; set; }

        [Display(Name = "NumberSeriesLastNum", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public int LastNum { get; set; }
    }

    public class WorkScheduleView : BasicView
    {
        [StringLength(20, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "WorkScheduleName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string WorkScheduleName { get; set; }

        [Display(Name = "WorkStartTime", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public TimeSpan WorkStartTime { get; set; }

        [Display(Name = "WorkEndTime", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public TimeSpan WorkEndTime { get; set; }
    }

    public class EmailParameterView : BasicView
    {
        [StringLength(256, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "SmtpServer", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string SmtpServer { get; set; }

        [Range(0, 65535, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorRangePort")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Port", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public int SmtpPort { get; set; }

        [StringLength(254, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Email", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        [DataType(DataType.EmailAddress)]
        [Email(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorInvalidEmail")]
        public string Email { get; set; }

        [StringLength(256, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "UserName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string UserName { get; set; }

        [StringLength(256, ErrorMessageResourceName = "ErrorFieldSize", ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string Password { get; set; }

        [Display(Name = "EnableSsl", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public bool EnableSsl { get; set; }

        [Display(Name = "Timeout", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public int Timeout { get; set; }
    }

    public class ViewDataUploadFilesResult
    {
        public string id { get; set; }
        public string name { get; set; }
        public string error { get; set; }
        public int size { get; set; }
        public string url { get; set; }
        public string deleteUrl { get; set; }
        public string thumbnailUrl { get; set; }
        public string deleteType { get; set; }
        public string createdUser { get; set; }
        public string createdDate { get; set; }
        public string versionName { get; set; }
        public bool isReplaceFile { get; set; }
        public bool isClosed { get; set; }
    }
}