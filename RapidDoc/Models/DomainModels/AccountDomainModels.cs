using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using RapidDoc.Attributes.Validation;

namespace RapidDoc.Models.DomainModels
{
    public class ApplicationUser : IdentityUser
    {
        public Guid? CompanyTableId { get; set; }
        public virtual CompanyTable CompanyTable { get; set; }

        public string TimeZoneId { get; set; }

        [StringLength(5)]
        [Required]
        public string Lang { get; set; }

        public bool isDomainUser { get; set; }

        public string AliasCompanyName
        {
            get
            {
                if (this.CompanyTable != null)
                    return this.CompanyTable.AliasCompanyName;

                return string.Empty;
            }
        }
    }
}