using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BarnamenevisanCompany.Web.Helpers.TagHelpers;

[HtmlTargetElement("a", Attributes = "title")]
public class                                  TitleTagHelper : TagHelper
{
    private const string TitleAttributeName = "title";

    [HtmlAttributeName(TitleAttributeName)]
    public string Title { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.SetAttribute("data-bs-toggle", "tooltip");
        output.Attributes.SetAttribute("data-bs-original-title", Title);
        base.Process(context, output);
    }
}