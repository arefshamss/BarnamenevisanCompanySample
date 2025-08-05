using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.DynamicPage;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.DynamicPage;

public class AdminFilterDynamicPageViewModel : BasePaging<AdminDynamicPageViewModel>
{
    [Display(Name="عنوان صفحه"),FilterInput]
    public string? Title { get; set; }
    
    
    [Display(Name = "مرتب سازی بر اساس"), FilterInput]
    public FilterOrderBy FilterOrderBy { get; set; }
    
    
    [Display(Name="وضعیت حذف"), FilterInput]
    public DeleteStatus DeleteStatus { get; set; }    
    
    [Display(Name="وضعیت صفحه"), FilterInput]
    public DynamicPageActiveStatus ActiveStatus { get; set; }
}