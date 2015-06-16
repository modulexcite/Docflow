using RapidDoc.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidDoc.Models.DomainModels
{
    public class TitleTable : BasicTable
    {
        [StringLength(256)]
        [Required]
        public string TitleName { get; set; }

        public bool isIntegratedLDAP { get; set; }

        public Guid? ProfileTableId { get; set; }
        public virtual ProfileTable ProfileTable { get; set; }

        public string ProfileName
        {
            get
            {
                if (this.ProfileTable != null)
                    return this.ProfileTable.ProfileName;

                return string.Empty;
            }
        }

        public IEnumerable<EmplTable> EmplTables { get; set; }
    }

    public class ProfileTable : BasicTable
    {
        [StringLength(20)]
        [Required]
        public string ProfileName { get; set; }

        public IEnumerable<EmplTable> EmplTables { get; set; }
    }

    public class DepartmentTable : BasicCompanyNullTable
    {
        [StringLength(256)]
        [Required]
        public string DepartmentName { get; set; }

        [ForeignKey("DepartmentTableParent")]
        public Guid? ParentDepartmentId { get; set; }
        public virtual DepartmentTable DepartmentTableParent { get; set; }

        public string ParentDepartmentName
        {
            get
            {
                if (this.DepartmentTableParent != null)
                    return this.DepartmentTableParent.DepartmentName;

                return string.Empty;
            }
        }
        public string RequiredRoles { get; set; }
    }

    public class EmplTable : BasicCompanyNullTable
    {
        [StringLength(70)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(70)]
        [Required]
        public string SecondName { get; set; }

        [StringLength(70)]
        public string MiddleName { get; set; }

        public bool isIntegratedLDAP { get; set; }

        public Guid? LDAPGlobalId { get; set; }

        public bool Enable { get; set; }

        public Guid? TitleTableId { get; set; }
        public virtual TitleTable TitleTable { get; set; }

        public Guid? ProfileTableId { get; set; }
        public virtual ProfileTable ProfileTable { get; set; }

        public Guid? DepartmentTableId { get; set; }
        public virtual DepartmentTable DepartmentTable { get; set; }

        [ForeignKey("EmplTableParent")]
        public Guid? ManageId { get; set; }
        public virtual EmplTable EmplTableParent { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public Guid WorkScheduleTableId { get; set; }
        public virtual WorkScheduleTable WorkScheduleTable { get; set; }

        public string ProfileName
        {
            get
            {
                if (this.ProfileTable != null)
                    return this.ProfileTable.ProfileName;

                return string.Empty;
            }
        }

        public string TitleName
        {
            get
            {
                if (this.TitleTable != null)
                    return this.TitleTable.TitleName;

                return string.Empty;
            }
        }

        public string AliasCompanyName
        {
            get
            {
                if (this.CompanyTable != null)
                    return this.CompanyTable.AliasCompanyName;

                return string.Empty;
            }
        }

        public string DepartmentName
        {
            get
            {
                if (this.DepartmentTable != null)
                    return this.DepartmentTable.DepartmentName;

                return string.Empty;
            }
        }

        public string FullNameParent
        {
            get
            {
                if (this.EmplTableParent != null)
                    return this.EmplTableParent.FullName;

                return string.Empty;
            }
        }

        public string WorkScheduleName
        {
            get
            {
                if (this.WorkScheduleTable != null)
                    return this.WorkScheduleTable.WorkScheduleName;

                return string.Empty;
            }
        }

        public string UserName
        {
            get
            {
                if (this.ApplicationUser != null)
                    return this.ApplicationUser.UserName;

                return string.Empty;
            }
        }

        public string FullName
        {
            get
            {
                return (SecondName + " " + FirstName + " " + MiddleName);
            }
        }

        public string ShortFullName
        {
            get
            {
                return (SecondName + "_" + FirstName.Substring(0, 1) + "." + MiddleName.Substring(0, 1));
            }
        }
    }

    public class DelegationTable : BasicCompanyNullTable
    {
        public Guid EmplTableFromId { get; set; }
        public virtual EmplTable EmplTableFrom { get; set; }

        public Guid EmplTableToId { get; set; }
        public virtual EmplTable EmplTableTo { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public Guid? GroupProcessTableId { get; set; }
        public virtual GroupProcessTable GroupProcessTable { get; set; }

        public Guid? ProcessTableId { get; set; }
        public virtual ProcessTable ProcessTable { get; set; }

        public bool isArchive { get; set; }
    }
}