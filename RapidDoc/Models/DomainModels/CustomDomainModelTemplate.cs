using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.Interfaces;
using RapidDoc.Models.Repository;

namespace RapidDoc.Models.DomainModels
{
    public abstract class BasicDocumentTable : BasicTable, IDocument
    {
        public Guid DocumentTableId { get; set; }
        public virtual DocumentTable DocumentTable { get; set; }
    }

    public abstract class BasicDocumentRequestTable : BasicDocumentTable
    {
        [Required]
        public string RequestText { get; set; }

        [Required]
        public string Users { get; set; }
    }

    public abstract class BasicDocumantOfficeMemoTable : BasicDocumentTable
    {
        public Folder Folder { get; set; }

        [Required]
        public Guid? ItemCauseTableId { get; set; }
        public virtual ItemCauseTable ItemCauseTable { get; set; }
     
        public string ItemCauseNumber
        {
            get {
                if (this.ItemCauseTable != null)
                    return this.ItemCauseTable.CaseNumber + " - " + this.ItemCauseTable.CaseName;

                return String.Empty;
            }
        }

        public string DocumentWhom { get; set; }

        public string DocumentCopy { get; set; }

        [Required]
        public string DocumentTitle { get; set; }

        [Required]
        public string MainField { get; set; }
        public bool Parallel { get; set; }
    }

    public abstract class BasicDailyTasksTable : BasicDocumentTable
    {
        [Required]
        public string MainField { get; set; }

        [Required]
        public string Users { get; set; }

        [Required]
        public DateTime ExecutionDate { get; set; }

        public string ReportText { get; set; }
    
    }
}