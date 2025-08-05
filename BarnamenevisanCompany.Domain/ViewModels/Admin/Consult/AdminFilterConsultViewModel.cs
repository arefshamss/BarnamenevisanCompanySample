using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.Consult;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Consult;

public class AdminFilterConsultViewModel:BasePaging<AdminConsultViewModel>
{
    [Display(Name = "عنوان")]
    [FilterInput]
    public string? Title { get; set; }
    
    [Display(Name = "مرتب سازی بر اساس")]
    [FilterInput]
    public FilterOrderBy FilterOrderBy { get; set; }
    
    [Display(Name = "وضعیت حذف")]
    [FilterInput]
    public DeleteStatus DeleteStatus { get; set; }
    
    [Display(Name = "بر اساس پاسخ")]
    [FilterInput]
    public ConsultStatus ConsultStatus { get; set; }
    
    
    [FilterInput]
    [Display(Name = "کاربر")]
    public int? UserId { get; set; }    
}