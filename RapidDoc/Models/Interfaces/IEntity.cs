using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidDoc.Models.DomainModels
{
    public interface IEntity
    {
        Guid Id { get; set; }
        Byte[] TimeStamp { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}
