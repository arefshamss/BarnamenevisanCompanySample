using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BarnamenevisanCompany.Web.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class InvokePermissionAttribute(string permissionName) : Attribute, IAsyncAuthorizationFilter
{
    public string PermissionName { get; set; } = permissionName;

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var permissionService = context.HttpContext.RequestServices.GetRequiredService<IPermissionService>();
        
        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            var userId = context.HttpContext.User.GetUserId();
            bool userHaveAccess = await permissionService.CheckUserPermissionAsync(userId, PermissionName);
            if (!userHaveAccess)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.HttpContext.Response.Redirect("/access-denied");
            }
        }
        else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.HttpContext.Response.Redirect("/access-denied");
        }
    }
}