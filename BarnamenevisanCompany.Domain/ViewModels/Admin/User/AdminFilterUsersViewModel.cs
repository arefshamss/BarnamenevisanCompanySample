using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.User;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.User;

public class AdminFilterUsersViewModel : BasePaging<AdminUserViewModel>
{
    [Display(Name="نام")]
    [FilterInput]
    public string? FirstName { get; set; }  
    
    [Display(Name="نام خانوادگی")]
    [FilterInput]
    public string? LastName { get; set; }
    
    [Display(Name="شماره همراه")]
    [FilterInput]
    public string? Mobile { get; set; }

    [Display(Name = "وضعیت حساب کاربری")]
    [FilterInput]
    public UserActiveStatus UserActiveStatus { get; set; }

    [Display(Name = "مرتب سازی بر اساس")]
    [FilterInput]
    public FilterOrderBy FilterOrderBy { get; set; }
    
    [Display(Name = "وضعیت حذف")]
    [FilterInput]
    public DeleteStatus DeleteStatus { get; set; }  
}