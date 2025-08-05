using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.OurService;

public class AdminFilterOurServiceViewModel : BasePaging<AdminOurServiceViewModel>
{
    [FilterInput]
    [Display(Name = "وضعیت حذف")]
    public DeleteStatus DeleteStatus { get; set; }

    [FilterInput]
    [Display(Name = "مرتب سازی بر اساس")]
    public FilterOrderBy FilterOrderBy { get; set; }

    [FilterInput]
    [Display(Name = "عنوان")]
    public string? Title { get; set; }
}