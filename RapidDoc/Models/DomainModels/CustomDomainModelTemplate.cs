using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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

    public class BasicDocumantOfficeMemoTable : BasicDocumentTable
    {
        public Folder Folder { get; set; }

        public Guid? ItemCauseTableId { get; set; }
        public virtual ItemCauseTable ItemCauseTable { get; set; }

        [Required]
        public string ItemCauseNumber
        {
            get {
                if (this.ItemCauseTable != null)
                    return this.ItemCauseTable.CaseNumber;

                return String.Empty;
            }
        }

        [Required]
        public string DocumentTitle { get; set; }

        [Required]
        public string MainField { get; set; }
    }
}