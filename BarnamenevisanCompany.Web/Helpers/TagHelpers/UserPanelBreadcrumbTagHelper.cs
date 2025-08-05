using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BarnamenevisanCompany.Web.Helpers.TagHelpers;

public class UserPanelBreadcrumbTagHelper : TagHelper
{
    public string PageTitle { get; set; }   
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var cc = await output.GetChildContentAsync();
        output.TagName = "div";
        output.Attributes.SetAttribute("class", "col-span-full");
        output.Content.SetHtmlContent($"<div class=\"bg-white dark:bg-dark-200 rounded-medium p-2.5 shadow-nav\">\n<div class=\"border border-dashed rounded border-gray-100 dark:border-borderColour-dark py-3 px-5\">\n<div class=\"flex justify-start items-center\">\n<div>\n<nav>\n<ul class=\"flex items-center space-x-reverse space-x-1 *:flex\">\n<li>\n<a href=\"/account/dashboard\">داشبورد</a>\n</li>\n{cc.GetContent()}\n</ul>\n</nav>\n</div>\n</div>\n</div>\n</div>");
    }
}