using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BarnamenevisanCompany.Web.Helpers.TagHelpers;

[HtmlTargetElement("userpanel-bage")]    
public sealed class UserPanelBadgeTagHelper : TagHelper
{
    #region Attribute Names

    private const string BadgeTypeAttributeName = "type";
    private const string BadgeModeAttributeName = "mode";        

    #endregion

    #region Attributes

    [HtmlAttributeName(BadgeTypeAttributeName)]
    public required BadgeType Type { get; set; }

    [HtmlAttributeName(BadgeModeAttributeName)]
    public required BadgeMode Mode { get; set; } = BadgeMode.Outline; 

    #endregion
    
    public override void Process(TagHelperContext context, TagHelperOutput output)      
    {
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        StringBuilder classes = new();
        classes.Append("rounded-3xl px-3 py-1 text-sm ");
        
        switch (Mode)
        {
            case BadgeMode.Normal:
            {
                switch (Type)
                {
                    case BadgeType.Danger:
                        classes.Append("bg-red-400 text-red-950 ");
                        break;
                    case BadgeType.Success:
                        classes.Append("bg-green-400 text-green-950 ");
                        break;
                    case BadgeType.Info:
                        classes.Append("bg-sky-400 text-sky-950 ");
                        break;
                    case BadgeType.Warning:
                        classes.Append("bg-yellow-400 text-yellow-950 ");
                        break;
                }
                break;
            }
            
            case BadgeMode.Outline:
            {
                switch (Type)
                {
                    case BadgeType.Danger:
                        classes.Append("text-red-400 border border-red-400 ");
                        break;
                    case BadgeType.Success:
                        classes.Append("text-green-400 border border-green-400 ");
                        break;
                    case BadgeType.Info:
                        classes.Append("text-sky-400 border border-sky-400 ");
                        break;
                    case BadgeType.Warning:
                        classes.Append("text-yellow-400 border border-yellow-400 ");
                        break;
                }
                break;
            }
        }
        
        output.Attributes.SetAttribute("class", classes.ToString());
        
        base.Process(context, output);
    }
}

public enum BadgeType : byte
{
    Success,
    Warning,    
    Danger,
    Info,
}

public enum BadgeMode : byte    
{
    Normal,
    Outline
}

