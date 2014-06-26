using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Repository;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RapidDoc.Models.ViewModels
{
    public class GroupProcessView : BasicView
    {
        [StringLength(40, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "GroupProcessName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string GroupProcessName { get; set; }

        [Display(Name = "NumberSeriesName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string NumberSeriesName { get; set; }
        public Guid? NumberSeriesTableId { get; set; }

        public Guid? GroupProcessParentId { get; set; }

        [Display(Name = "GroupProcessParentName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string GroupProcessParentName { get; set; }
    }

    public class ProcessView : BasicCompanyNullView
    {
        [StringLength(70, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ProcessName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string ProcessName { get; set; }

        [StringLength(256, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Display(Name = "Description", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string Description { get; set; }

        [StringLength(256, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "TableName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string TableName { get; set; }

        public Guid? GroupProcessTableId { get; set; }
        public Guid? WorkScheduleTableId { get; set; }

        [Display(Name = "Approved", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public bool isApproved { get; set; }

        [Display(Name = "GroupProcessName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string GroupProcessName { get; set; }

        [Display(Name = "WorkScheduleName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string WorkScheduleName { get; set; }
    }

    public class DocumentComposite
    {
        public ProcessView ProcessView { get; set; }
        public DocumentTable DocumentTable { get; set; }
        public IEnumerable<WFTrackerListView> WFTrackerItems { get; set; }
        public dynamic docData { get; set; }
        public Guid fileId { get; set; }
    }

    public class DocumentListView : BasicCompanyNullView
    {
        [StringLength(256, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "DocumentNum", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string DocumentNum { get; set; }

        [Display(Name = "GroupProcesses", ResourceType = typeof(UIElementRes.UIElement))]
        public string GroupProcessName { get; set; }

        [Display(Name = "Processes", ResourceType = typeof(UIElementRes.UIElement))]
        public string ProcessName { get; set; }
        public Guid? ProcessTableId { get; set; }

        [Display(Name = "DocumentState", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public DocumentState DocumentState { get; set; }

        [Display(Name = "CurrentActivityName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string ActivityName { get; set; }

        public SLAStatusList SLAStatus { get; set; }

        public bool isNotReview { get; set; }

        public bool isArchive { get; set; }

        public bool isShow { get; set; }
    }

    public class WFTrackerListView : BasicCompanyNullView
    {
        public int RowNum { get; set; }

        [Display(Name = "ActivityName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string ActivityName { get; set; }

        public string ActivityID { get; set; }

        [Display(Name = "Executor", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string Executors { get; set; }

        [Display(Name = "PerformToDate", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public DateTime? PerformToDate { get; set; }

        public int SLAOffset { get; set; }

        [Display(Name = "Date", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public DateTime? SignDate { get; set; }

        public bool ManualExecutor { get; set; }

        public TrackerType TrackerType { get; set; }
    }

    public class CommentView : BasicCompanyNullView
    {
        [Display(Name = "FirstName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string EmplName { get; set; }

        [Display(Name = "TitleName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string TitleName { get; set; }

        [Display(Name = "Comment", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string Comment { get; set; }
    }

    public class HistoryUserView : BasicView
    {
        public HistoryType HistoryType { get; set; }
        public string DocumentNum { get; set; }
        public string ProcessName { get; set; }
        public Guid DocumentTableId { get; set; }
    }

    public class SearchView : BasicView
    {
        public string DocumentText { get; set; }
        public Guid DocumentTableId { get; set; }
        public string DocumentNum { get; set; }
        public string ProcessName { get; set; }
        public bool isShow { get; set; }
    }

    public class SearchFormView
    {
        public string SearchText { get; set; }
    }

    public class ServiceIncidentView : BasicCompanyNullView
    {
        [StringLength(70, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisNull")]
        [Display(Name = "ProcessName", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string ServiceName { get; set; }

        [StringLength(256, ErrorMessageResourceType = typeof(ValidationRes.ValidationResource), ErrorMessageResourceName = "ErrorFieldisLong")]
        [Display(Name = "Description", ResourceType = typeof(FieldNameRes.FieldNameResource))]
        public string Description { get; set; }

        [Display(Name = "Приоритеты инцидентов")]
        public PriorityIncident PriorityIncident { get; set; }

        [Display(Name = "SLA инцидентов")]
        public int SLAIncident { get; set; }

        public string FirstRoleTableId { get; set; }
        public string SecondRoleTableId { get; set; }

        [Display(Name = "2 линия IT")]
        public string SecondRoleName { get; set; }

        [Display(Name = "1 линия IT")]
        public string FirstRoleName { get; set; }
    }
}