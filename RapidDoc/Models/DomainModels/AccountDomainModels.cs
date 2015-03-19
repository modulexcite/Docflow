using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using RapidDoc.Attributes.Validation;
using RapidDoc.Models.Repository;

namespace RapidDoc.Models.DomainModels
{
    public class ApplicationUser : IdentityUser
    {
        public Guid? CompanyTableId { get; set; }
        public virtual CompanyTable CompanyTable { get; set; }

        public Guid? DomainTableId { get; set; }
        public virtual DomainTable DomainTable { get; set; }

        public string TimeZoneId { get; set; }

        [StringLength(5)]
        [Required]
        public string Lang { get; set; }

        public bool isDomainUser { get; set; }

        public bool Enable { get; set; }

        public string AccountDomainName { get; set; }

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

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string name)
            : base()
        {
            this.Name = name;
        }

        public ApplicationRole(string name, string description, RoleType roleType)
            : base()
        {
            this.Name = name;
            this.Description = description;
            this.RoleType = roleType;
        }

        public string Description { get; set; }

        public RoleType RoleType { get; set; }
    }
}