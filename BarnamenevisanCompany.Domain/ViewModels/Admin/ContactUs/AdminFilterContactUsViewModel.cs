using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.ContactUs;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.ContactUs;

public class AdminFilterContactUsViewModel:BasePaging<AdminContactUsViewModel>
{
    [FilterInput]
    [Display(Name = "عنوان")]
    public string? Title { get; set; }
    [Display(Name = "ایمیل")]

    [FilterInput]
    public string Email { get; set; }
    [Display(Name = "وضعیت حذف ")]
        
    [FilterInput]
    public DeleteStatus DeleteStatus { get; set; }
    [Display(Name = "مرتب سازی بر اساس")]

    [FilterInput]
    public FilterOrderBy FilterOrderBy { get; set; }
    [Display(Name = "وضعیت")]
    [FilterInput]
    public ContactUsStatus ContactUsStatus { get; set; }
    
}