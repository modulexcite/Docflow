using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using RapidDoc.Models.Repository;

namespace RapidDoc.Models.DomainModels
{
    public class ServiceIncidentTable : BasicCompanyNullTable
    {
        [StringLength(70)]
        [Required]
        public string ServiceName { get; set; }

        [StringLength(256)]
        [Required]
        public string Description { get; set; }
        public ServiceIncidientPriority ServiceIncidientPriority { get; set; }
        public ServiceIncidientLevel ServiceIncidientLevel { get; set; }
        public ServiceIncidientLocation ServiceIncidientLocation { get; set; }
        public int SLAIncident { get; set; }

        [Required]
        public string RoleTableId { get; set; }

        [ForeignKey("RoleTableId")]
        public virtual IdentityRole IdentityRole { get; set; }
    }

    public class TripSettingsTable : BasicCompanyNullTable
    {
        public EmplTripType EmplTripType { get; set; }
        public TripDirection TripDirection { get; set; }
        public int DayRate { get; set; }
        public int ResidenceRate { get; set; }
    }

    public class ItemCauseTable: BasicCompanyNullTable
    {
        public string CaseNumber { get; set; }
        public string CaseName { get; set; }

        public Guid? DepartmentTableId { get; set; }
        public virtual DepartmentTable DepartmentTable { get; set; }

        public  string Departmentname
        {
            get {
                    if (this.DepartmentTable != null)
                        return this.DepartmentTable.DepartmentName;

                    return String.Empty;
                }
        }
        
    }
}