using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidDoc.Models.Interfaces
{
    public interface IDocument
    {
        Guid DocumentTableId { get; set; }
    }
}
