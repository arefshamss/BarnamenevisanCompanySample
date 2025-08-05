using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Models.Common;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Blog;

public class AdminFilterBlogViewModel : BasePaging<AdminBlogViewModel>
{
    [Display(Name="نام مقاله"),FilterInput]
    public string? Title { get; set; }
    
    
    [Display(Name="نویسنده"), FilterInput]
    public int? UserId { get; set; }
    public string? Author { get; set; }
    
    [Display(Name= "از تاریخ") , FilterInput]
    public string? FromDate { get; set; }
    
    
    [Display(Name= "تا تاریخ") , FilterInput]
    public string? ToDate { get; set; }
    
    
    [Display(Name = "مرتب سازی بر اساس"), FilterInput]
    public FilterOrderBy FilterOrderBy { get; set; }
    
    
    [Display(Name="وضعیت حذف"), FilterInput]
    public DeleteStatus DeleteStatus { get; set; }
}