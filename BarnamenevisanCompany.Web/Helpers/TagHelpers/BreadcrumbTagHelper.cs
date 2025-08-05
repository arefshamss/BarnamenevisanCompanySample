using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BarnamenevisanCompany.Web.Helpers.TagHelpers;

public class BreadcrumbTagHelper : TagHelper
{
    private const string PageTitleAttributeName = "page-title";

    /// <summary>
    /// title of the page
    /// </summary>
    [HtmlAttributeName(PageTitleAttributeName)]
    public string PageTitle { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var cc = await output.GetChildContentAsync();
        output.TagName = "section";
        output.Attributes.SetAttribute("class", "hero overflow-hidden relative pt-48 pb-24 z-40");
        output.Content.SetHtmlContent($"<div class=\"container\" dir=\"rtl\">\n    <div class=\"max-w-[948px] mx-auto text-center aos-init aos-animate\" data-aos=\"fade-up\" data-aos-offset=\"200\" \n data-aos-duration=\"1000\" data-aos-once=\"true\">\n<ul class=\"mb-4 font-medium uppercase flex justify-center items-center space-x-1 space-x-reverse\">\n{cc.GetContent()}\n</ul>\n<h1 class=\"max-lg:mb-10 mb-10 text-[40px]\">{PageTitle}</h1>\n</div>\n</div>");
    }
}