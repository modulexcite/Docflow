using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RapidDoc.Models.Repository;

namespace RapidDoc.Models.ViewModels
{
    public class ServiceIncidentView : BasicCompanyNullView
    {
        [StringLength(70, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ProcessName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string ServiceName { get; set; }

        [StringLength(256, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Display(Name = "Description", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string Description { get; set; }

        [Display(Name = "Приоритет")]
        public ServiceIncidientPriority ServiceIncidientPriority { get; set; }

        [Display(Name = "Уровень поддержки")]
        public ServiceIncidientLevel ServiceIncidientLevel { get; set; }

        [Display(Name = "Местоположение")]
        public ServiceIncidientLocation ServiceIncidientLocation { get; set; }

        [Display(Name = "SLA инцидентов")]
        public int SLAIncident { get; set; }

        public string RoleTableId { get; set; }

        [Display(Name = "Роль")]
        public string RoleName { get; set; }
    }

    public class TripSettingsView : BasicCompanyNullView
    {
        [Display(Name = "Категория сотрудника")]
        public EmplTripType EmplTripType { get; set; }

        [Display(Name = "Направления")]
        public TripDirection TripDirection { get; set; }

        [Display(Name = "Суточные норма")]
        public int DayRate { get; set; }

        [Display(Name = "Проживание норма")]
        public int ResidenceRate { get; set; }
    }

    public class ItemCauseView: BasicCompanyNullView
    {
        [Display(Name = "№ дела")]
        public string CaseNumber { get; set; }

        [Display(Name = "Название дела")]
        public string CaseName { get; set; }

        public Guid? DepartmentTableId { get; set; }

        [Display(Name = "DepartmentName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string DepartmentName { get; set; }
        public bool IsCurrentUserDepartment { get; set; }
        
        [Display(Name = "Включено")]
        public bool Enable { get; set; }
    }

    public class TaskDelegationView
    {
        public string DocumentNum { get; set; }
        public Guid DocumentId { get; set; }
    }
}