using RapidDoc.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RapidDoc.Models.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [StringLength(100, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "UserName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [DataType(DataType.Password)]
        [Display(Name = "CurrentPassword", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [StringLength(100, ErrorMessageResourceName = "ErrorFieldSize", ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorPasswordsDoNotMatch")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [StringLength(100, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "UserName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [StringLength(256, ErrorMessageResourceName = "ErrorFieldSize", ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [StringLength(100, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "UserName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string UserName { get; set; }

        [StringLength(254, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Email", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        [DataType(DataType.EmailAddress)]
        [Email(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorInvalidEmail")]
        public virtual string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [StringLength(256, ErrorMessageResourceName = "ErrorFieldisLong", ErrorMessageResourceType = typeof(ValidationRes.ValidationResource))]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-ZА-ЯЁ])(?=.*[a-zа-яё])(?=.*[0-9])[A-Za-zа-яА-ЯёЁ0-9]{8,}$", ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorInvalidPassword")]
        [Display(Name = "Password", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        [Compare("Password", ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorPasswordsDoNotMatch")]
        public string ConfirmPassword { get; set; }
    }

    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "RoleName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string Name { get; set; }

        public bool isUserRole { get; set; }
    }

    public class UserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "UserName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string UserName { get; set; }

        [StringLength(254, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Email", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        [DataType(DataType.EmailAddress)]
        [Email(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorInvalidEmail")]
        public virtual string Email { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string AliasCompanyName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        public Guid? CompanyTableId { get; set; }

        public Guid? DomainTableId { get; set; }

        public string AccountDomainName { get; set; }

        [Display(Name = "TimeZoneName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string TimeZoneName
        {
            get
            {
                return TimeZoneId != null ? TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId).DisplayName : String.Empty;
            }
        }

        public string TimeZoneId { get; set; }

        [StringLength(5, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "Language", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string Lang { get; set; }

        [Display(Name = "isDomainUser", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public bool isDomainUser { get; set; }

        public bool isRoleUser { get; set; }
    }

    public class ChangePassword
    {
        public string Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [StringLength(256, ErrorMessageResourceName = "ErrorFieldisLong", ErrorMessageResourceType = typeof(ValidationRes.ValidationResource))]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-ZА-ЯЁ])(?=.*[a-zа-яё])(?=.*[0-9])[A-Za-zа-яА-ЯёЁ0-9]{8,}$", ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorInvalidPassword")]
        [Display(Name = "Password", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        [Compare("Password", ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorPasswordsDoNotMatch")]
        public string ConfirmPassword { get; set; }
    }

    public class WindowsLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}
