using RapidDoc.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RapidDoc.Models.Repository;
using System.ComponentModel.DataAnnotations.Schema;
using RapidDoc.Models.Services;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using RapidDoc.Models.Interfaces;

namespace RapidDoc.Models.DomainModels
{
    public class GroupProcessTable : BasicCompanyNullTable
    {
        [StringLength(40)] 
        [Required]
        public string GroupProcessName { get; set; }

        public Guid? NumberSeriesTableId { get; set; }
        public virtual NumberSeriesTable NumberSeriesTable { get; set; }

        [ForeignKey("GroupProcessTableParent")]
        public Guid? GroupProcessParentId { get; set; }
        public virtual GroupProcessTable GroupProcessTableParent { get; set; }

        public string NumberSeriesName
        {
            get
            {
                if (this.NumberSeriesTable != null)
                    return this.NumberSeriesTable.NumberSeriesName;

                return string.Empty;
            }
        }

        public IEnumerable<ProcessTable> ProcessTables { get; set; }
    }

    public class ProcessTable : BasicCompanyNullTable
    {
        [StringLength(70)]
        [Required]
        public string ProcessName { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [StringLength(256)]
        [Required]
        public string TableName { get; set; }

        public bool isApproved { get; set; }

        public string RoleId { get; set; }
        public virtual IdentityRole IdentityRole { get; set; }

        public string StartReaderRoleId { get; set; }
        public virtual IdentityRole StartReaderIdentityRole { get; set; }

        public string AfterEndReaderRoleId { get; set; }
        public virtual IdentityRole AfterEndReaderIdentityRole { get; set; }

        public int MandatoryNumberFiles { get; set; }
        public string MandatoryFileTypes { get; set; }

        public TimeSpan StartWorkTime { get; set; }

        public TimeSpan EndWorkTime { get; set; }

        public DateTime? MandatoryDocDate { get; set; }

        public int DocSize { get; set; }

        public string GroupProcessName
        {
            get
            {
                if (this.GroupProcessTable != null)
                    return this.GroupProcessTable.GroupProcessName;

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

        public Guid? WorkScheduleTableId { get; set; }
        public virtual WorkScheduleTable WorkScheduleTable { get; set; }

        public Guid? GroupProcessTableId { get; set; }
        public virtual GroupProcessTable GroupProcessTable { get; set; }
    }

    public class DocumentTable : BasicCompanyNullTable
    {
        [StringLength(256)]
        [Required]
        public string DocumentNum { get; set; }
        public Guid ProcessTableId { get; set; }
        public virtual ProcessTable ProcessTable { get; set; }
        public string DocumentText { get; set; }
        public Guid RefDocumentId { get; set; }
        public Guid WWFInstanceId { get; set; }
        public Guid FileId { get; set; }
        public DocumentState DocumentState { get; set; }
        public string ActivityName { get; set; }

        public string GroupProcessName 
        {
            get
            {
                if (this.ProcessTable != null && this.ProcessTable.GroupProcessTable != null)
                    return this.ProcessTable.GroupProcessTable.GroupProcessName;

                return string.Empty;
            }
        }

        public string ProcessName
        {
            get
            {
                if (this.ProcessTable != null)
                    return this.ProcessTable.ProcessName;

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
    }

    public class CommentTable : BasicCompanyNullTable
    {
        public Guid DocumentTableId { get; set; }
        public virtual DocumentTable DocumentTable { get; set; }

        public string Comment { get; set; }
    }

    public class WFTrackerTable : BasicTable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineNum { get; set; }

        public Guid DocumentTableId { get; set; }
        public virtual DocumentTable DocumentTable { get; set; }

        [Required]
        public string ActivityName { get; set; }

        public string ActivityID { get; set; }

        public string ParallelID { get; set; }

        public string SignUserId { get; set; }

        public DateTime? SignDate { get; set; }

        public TrackerType TrackerType { get; set; }

        public bool ManualExecutor { get; set; }

        public DateTime? StartDateSLA { get; set; }

        public int SLAOffset { get; set; }

        public bool ExecutionStep { get; set; }

        public DateTime? PerformToDate()
        {
            if (SLAOffset > 0)
            {
                IDocumentService _service = DependencyResolver.Current.GetService<IDocumentService>();
                return _service.GetSLAPerformDate(DocumentTableId, StartDateSLA, SLAOffset);
            }

            return null;
        }

        public virtual List<WFTrackerUsersTable> Users { get; set; }
    }

    public class WFTrackerUsersTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Timestamp]
        public Byte[] TimeStamp { get; set; }

        public string InitiatorUserId { get; set; }

        [Required]
        public string UserId { get; set; }
    }

    public class ReviewDocLogTable : BasicTable
    {
        public Guid DocumentTableId { get; set; }
        public virtual DocumentTable DocumentTable { get; set; }
        public bool isArchive { get; set; }
    }

    public class DocumentReaderTable : BasicTable
    {
        public Guid DocumentTableId { get; set; }
        public virtual DocumentTable DocumentTable { get; set; }

        [Required]
        public string UserId { get; set; }
    }

    public class HistoryUserTable : BasicTable
    {
        public HistoryType HistoryType { get; set; }
        public Guid DocumentTableId { get; set; }
        public virtual DocumentTable DocumentTable { get; set; }
        public string Description { get; set; }
    }

    public class SearchTable : BasicTable
    {
        public string DocumentText { get; set; }
        public Guid DocumentTableId { get; set; }
        public virtual DocumentTable DocumentTable { get; set; }
    }
}