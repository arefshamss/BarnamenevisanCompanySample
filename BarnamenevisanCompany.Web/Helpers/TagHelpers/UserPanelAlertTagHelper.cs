using BarnamenevisanCompany.Application.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BarnamenevisanCompany.Web.Helpers.TagHelpers;

public class UserPanelAlertTagHelper : TagHelper
{
    #region Attribute Names
    
    private const string AlertModeAttributeName = "mode";           
    private const string AlertTitleAttributeName = "title";            

    #endregion

    #region Attributes

    [HtmlAttributeName(AlertModeAttributeName)]
    public required BadgeType Mode { get; set; }
    
    [HtmlAttributeName(AlertTitleAttributeName)]

    public string? Title { get; set; }     

    #endregion
    
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)   
    {
        output.SuppressOutput();
        var content = (await output.GetChildContentAsync()).GetContent();
        switch (Mode)
        {
            case BadgeType.Success:
                output.Content.SetHtmlContent($"<div class=\"bg-green-400 text-green-950 rounded-md p-2 flex items-center space-x-3 space-x-reverse\">\n<div class=\"flex *:text-green-950\">\n<iconify-icon icon=\"mdi:success-bold\" width=\"45\" height=\"45\"></iconify-icon>\n</div>\n<div class=\"*:text-green-950 flex flex-col\">\n<h6 class=\"font-semibold text-lg\">{Title ?? "موفقیت آمیز"}</h6>\n<p class=\"text-base\">{content}</p>\n</div>\n</div>");
                break;
            case BadgeType.Danger:
                output.Content.SetHtmlContent($"<div class=\"bg-red-400 text-red-950 rounded-md p-2 flex items-center space-x-3 space-x-reverse\">\n<div class=\"flex *:text-red-950\">\n<iconify-icon icon=\"gravity-ui:circle-xmark\" width=\"45\" height=\"45\"></iconify-icon>\n</div>\n<div class=\"*:text-red-950 flex flex-col\">\n<h6 class=\"font-semibold text-lg\">{Title ?? "خطا"}</h6>\n<p class=\"text-base\">{content}</p>\n</div>\n</div>");
                break;
            case BadgeType.Info:
                output.Content.SetHtmlContent($"<div class=\"bg-sky-400 text-sky-950 rounded-md p-2 flex items-center space-x-3 space-x-reverse\">\n<div class=\"flex *:text-sky-950\">\n<iconify-icon icon=\"material-symbols:info-outline-rounded\" width=\"45\" height=\"45\"></iconify-icon>\n</div>\n<div class=\"*:text-sky-950 flex flex-col\">\n<h6 class=\"font-semibold text-lg\">{Title ?? "اطلاع رسانی"}</h6>\n<p class=\"text-base\">{content}</p>\n</div>\n</div>");
                break;
            case BadgeType.Warning:
                output.Content.SetHtmlContent($"<div class=\"bg-yellow-400 text-yellow-950 rounded-md p-2 flex items-center space-x-3 space-x-reverse\">\n<div class=\"flex *:text-yellow-950\">\n<iconify-icon icon=\"jam:triangle-danger\" width=\"45\" height=\"45\"></iconify-icon>\n</div>\n<div class=\"*:text-yellow-950 flex flex-col\">\n<h6 class=\"font-semibold text-lg\">{Title ?? "هشدار"}</h6>\n<p class=\"text-base\">{content}</p>\n</div>\n</div>");
                break;
        }
        
        await base.ProcessAsync(context, output);
    }
}

public enum AlertMode : byte        
{
    Success,
    Warning,    
    Danger,
    Info,
}

