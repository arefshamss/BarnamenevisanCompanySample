using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace BarnamenevisanCompany.Application.Extensions;

public static class ObjectExtensions
{
    public static string GetDisplayName<T, TProperty>(this T obj, Expression<Func<T, TProperty>> propertyExpression)
    {
        if (propertyExpression.Body is not MemberExpression memberExpression)
            throw new ArgumentException("The expression is not a valid member expression.");
        var propertyInfo = memberExpression.Member as PropertyInfo;

        if (propertyInfo == null)
            throw new ArgumentException("The expression is not a valid property.");

        var displayNameAttribute = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();
        if (displayNameAttribute != null)
        {
            return displayNameAttribute.DisplayName;
        }

        var displayAttribute = propertyInfo.GetCustomAttribute<DisplayAttribute>();
        if (displayAttribute != null)
        {
            return displayAttribute.Name ?? propertyInfo.Name;
        }
            
        return propertyInfo.Name;

    }
   
}