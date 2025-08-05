using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.Job;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Job;

public class AdminFilterJobViewModel : BasePaging<AdminJobViewModel>
{
    
    [Display(Name="عنوان موقعیت شغلی"),FilterInput]
    public string? Title { get; set; }
    
    
    [Display(Name = "مرتب سازی بر اساس"), FilterInput]
    public FilterOrderBy FilterOrderBy { get; set; }
    
    
    [Display(Name="وضعیت حذف"), FilterInput]
    public DeleteStatus DeleteStatus { get; set; }
    
    
    [Display(Name="وضعیت اعتبار"), FilterInput]
    public JobExpirationStatus JobExpirationStatus {get; set;}
}