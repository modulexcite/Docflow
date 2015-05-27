using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RapidDoc.Models.ViewModels
{
    public class TitleView : BasicView
    {
        [StringLength(256, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "TitleName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string TitleName { get; set; }

        public Guid? ProfileTableId { get; set; }

        [Display(Name = "ProfileName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string ProfileName { get; set; }
    }

    public class ProfileView : BasicView
    {
        [StringLength(20, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ProfileName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string ProfileName { get; set; }
    }

    public class DepartmentView : BasicCompanyNullView
    {
        [StringLength(256, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "DepartmentName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string DepartmentName { get; set; }

        public Guid? ParentDepartmentId { get; set; }

        [Display(Name = "DepartmentNameParent", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string ParentDepartmentName { get; set; }

        [Display(Name = "Обязательные роли")]
        public string RequiredRoles { get; set; }
    }

    public class EmplView : BasicCompanyNullView
    {
        [StringLength(70, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "FirstName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string FirstName { get; set; }

        [StringLength(70, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "SecondName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string SecondName { get; set; }

        [StringLength(70, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Display(Name = "MiddleName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string MiddleName { get; set; }

        [Display(Name = "Enable", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public bool Enable { get; set; }

        public Guid? TitleTableId { get; set; }
        public Guid? ProfileTableId { get; set; }
        public Guid? DepartmentTableId { get; set; }
        public Guid? ManageId { get; set; }
        public Guid WorkScheduleTableId { get; set; }

        public string ApplicationUserId { get; set; }

        [Display(Name = "TitleName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string TitleName { get; set; }

        [Display(Name = "ProfileName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string ProfileName { get; set; }

        [Display(Name = "DepartmentName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string DepartmentName { get; set; }

        [Display(Name = "UserName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string UserName { get; set; }

        [Display(Name = "WorkScheduleName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string WorkScheduleName { get; set; }

        [Display(Name = "Manage", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string FullNameParent { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string FullName
        {
            get
            {
                return (SecondName + " " + FirstName + " " + MiddleName);
            }
        }

        [Display(Name = "FirstName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string FullNameAddon
        {
            get
            {
                return ("(" + AliasCompanyName + ") " + SecondName + " " + FirstName + " " + MiddleName);
            }
        }
    }

    public class DelegationView : BasicCompanyNullView
    {
        [Display(Name = "EmplNameFrom", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public Guid EmplTableFromId { get; set; }
        public string EmplNameFrom { get; set; }

        [Display(Name = "EmplNameTo", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public Guid EmplTableToId { get; set; }
        public string EmplNameTo { get; set; }

        [Display(Name = "DateFrom", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        [DataType(DataType.Date)]
        public DateTime? DateFrom { get; set; }

        [Display(Name = "DateTo", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        [DataType(DataType.Date)]
        public DateTime? DateTo { get; set; }

        [Display(Name = "GroupProcessName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public Guid? GroupProcessTableId { get; set; }
        public string GroupProcessName { get; set; }

        [Display(Name = "ProcessName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public Guid? ProcessTableId { get; set; }
        public string ProcessName { get; set; }
    }

    public class DualListView
    {
        public Guid DocumentId { get; set; }
        public List<RapidDoc.Models.ViewModels.EmplDualListView> EmplList { get; set; }
    }

    public class EmplDualListView
    {
        public string AliasCompanyName { get; set; }
        public string FullName { get; set; }
        public string ApplicationUserId { get; set; }
        public bool isActiveDualList { get; set; }
    }
}