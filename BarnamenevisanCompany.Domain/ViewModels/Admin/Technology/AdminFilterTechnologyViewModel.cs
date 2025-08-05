using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;


namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Technology;

public class AdminFilterTechnologyViewModel:BasePaging<AdminTechnologyViewModel>
{
    [Display(Name = "عنوان ")]
    [FilterInput]
    public string? Title { get; set; }
    
    [Display(Name = "وضعیت حذف")]
    [FilterInput]
    public DeleteStatus DeleteStatus { get; set; }
    
    [Display(Name = "مرتب سازی بر اساس")]
    [FilterInput]
    public FilterOrderBy FilterOrderBy { get; set; }
}