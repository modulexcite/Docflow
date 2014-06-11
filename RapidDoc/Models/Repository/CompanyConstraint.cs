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
            // Get the value called "parameterName" from the 
            // RouteValueDictionary called "value"
            string value = values[parameterName].ToString();
            // Return true is the list of allowed values contains 
            // this value.
            return _values.Contains(value);
        }
    }
}