using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BarnamenevisanCompany.Web.Helpers.TagHelpers;

[HtmlTargetElement("team-member")]
public class UserPositionTagHelper(IUserPositionService permissionService) : TagHelper
{
    [ViewContext] [HtmlAttributeNotBound] public ViewContext ViewContextData { get; set; }


    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var userId = ViewContextData.HttpContext.User.GetUserId();

        if (userId < 1 || userId == null)
            return;

        if (!await permissionService.IsUserProgrammer(userId))
        {
            output.SuppressOutput();
            return;
        }

        output.TagName = null;
    }
}