using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.SocialNetwork;

public class AdminFilterSocialNetworkViewModel : BasePaging<AdminSocialNetworkViewModel>
{
    [Display(Name = "عنوان")]
    [FilterInput]
    public string? Title { get; set; }
    
    [Display(Name = "مرتب سازی بر اساس")]
    [FilterInput]
    public FilterOrderBy FilterOrderBy { get; set; }
}