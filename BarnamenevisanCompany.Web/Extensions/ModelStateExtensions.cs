using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BarnamenevisanCompany.Web.Extensions;

internal static class ModelStateExtensions
{
    internal static string GetModelErrorsAsString(this ModelStateDictionary modelState)
    {
        StringBuilder errors = new();
        
        foreach (var modelStateValue in modelState.Values)
        {
            foreach (var error in modelStateValue.Errors)
            {
                errors.Append($"<span> {error.ErrorMessage} </span><br>");
                ;
            }
        }
        return errors.ToString();
    }
}