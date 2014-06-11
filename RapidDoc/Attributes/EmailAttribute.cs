using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RapidDoc.Attributes.Validation
{
    public class EmailAttribute : RegularExpressionAttribute
    {
        public EmailAttribute() : base("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$") { }
    }
}