using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RapidDoc.Extensions
{
    public static class ExceptionExtensions
    {
        public static Exception GetOriginalException(this Exception ex)
        {
            if (ex.InnerException == null) return ex;

            return ex.InnerException.GetOriginalException();
        }
    }
}