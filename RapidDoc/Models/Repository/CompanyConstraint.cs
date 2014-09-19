using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RapidDoc.Models.Services;

namespace RapidDoc.Models.Repository
{
    public class CompanyConstraint : IRouteConstraint
    {
        private string[] _values;
        public CompanyConstraint(params string[] values)
        {
            this._values = values;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName,
                  RouteValueDictionary values, RouteDirection routeDirection)
        {
            string value = values[parameterName].ToString();
            return _values.Contains(value);
        }
    }
}