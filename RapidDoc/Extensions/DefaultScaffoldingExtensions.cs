using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RapidDoc.Extensions
{
    public static class DefaultScaffoldingExtensions
    {
        public static PropertyInfo[] VisibleProperties(this IEnumerable Model)
        {
            var elementType = Model.GetType().GetElementType();
            if (elementType == null)
            {
                elementType = Model.GetType().GetGenericArguments()[0];
            }
            return elementType.GetProperties().Where(info => info.Name != elementType.IdentifierPropertyName()).Where(x => x.Name != "CreatedDate" && x.Name != "ModifiedDate" && x.Name != "ApplicationUserCreatedId" && x.Name != "ApplicationUserModifiedId" && x.Name != "CompanyTableId" && x.Name != "DocumentTableId" && x.Name != "AliasCompanyName" && x.Name != "CreatedBy" && x.Name != "ModifiedBy").ToArray();
        }

        public static PropertyInfo[] VisibleProperties(this Object model)
        {
            return model.GetType().GetProperties().Where(info => info.Name != model.IdentifierPropertyName()).Where(x => x.Name != "CreatedDate" && x.Name != "ModifiedDate" && x.Name != "ApplicationUserCreatedId" && x.Name != "ApplicationUserModifiedId" && x.Name != "CompanyTableId" && x.Name != "DocumentTableId" && x.Name != "AliasCompanyName" && x.Name != "CreatedBy" && x.Name != "ModifiedBy").ToArray();
        }

        public static string IdentifierPropertyName(this Object model)
        {
            return IdentifierPropertyName(model.GetType());
        }

        public static string IdentifierPropertyName(this Type type)
        {
            if (type.GetProperties().Any(info => info.PropertyType.AttributeExists<System.ComponentModel.DataAnnotations.KeyAttribute>()))
            {
                return
                    type.GetProperties().First(
                        info => info.PropertyType.AttributeExists<System.ComponentModel.DataAnnotations.KeyAttribute>())
                        .Name;
            }
            else if (type.GetProperties().Any(p => p.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase)))
            {
                return
                    type.GetProperties().First(
                        p => p.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase)).Name;
            }

            return "";
        }
    }

    public static class PropertyInfoExtensions
    {
        public static bool AttributeExists<T>(this PropertyInfo propertyInfo) where T : class
        {
            var attribute = propertyInfo.GetCustomAttributes(typeof(T), false)
                                .FirstOrDefault() as T;
            if (attribute == null)
            {
                return false;
            }
            return true;
        }

        public static bool AttributeExists<T>(this Type type) where T : class
        {
            var attribute = type.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
            if (attribute == null)
            {
                return false;
            }
            return true;
        }
    }
}