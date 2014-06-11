using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RapidDoc.Models.DomainModels;

namespace RapidDoc.Models.Repository
{
    public class WFUserFunctionResult
    {
        public List<WFTrackerUsersTable> Users { get; set; }
        public bool Skip { get; set; }
    }
}