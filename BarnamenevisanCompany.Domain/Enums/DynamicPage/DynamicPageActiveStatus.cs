using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.Enums.DynamicPage;

public enum DynamicPageActiveStatus : byte
{
    [Display(Name="همه")]
    All,
    [Display(Name="فعال")]
    Active,
    [Display(Name="غیرفعال")]
    NotActive
}

