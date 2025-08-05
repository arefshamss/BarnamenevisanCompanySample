using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.Project;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Project;

public class AdminFilterProjectViewModel : BasePaging<AdminProjectViewModel>
{
    [Display(Name = "عنوان")]
    [FilterInput]
    public string? Title { get; set; }
    [Display(Name = "مرتب سازی بر اساس اولیت نمایش")]
    [FilterInput]
    public ProjectStatus ProjectStatus { get; set; }

    [Display(Name = "وضعیت حذف")]
    [FilterInput]
    public DeleteStatus DeleteStatus { get; set; }

    [Display(Name = "مرتب سازی بر اساس")]
    [FilterInput]
    public FilterOrderBy FilterOrderBy { get; set; }
}