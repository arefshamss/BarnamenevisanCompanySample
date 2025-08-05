using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.Job;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.JobUserMapping;

public class AdminFilterJobUserMappingViewModel : BasePaging<AdminJobUserMappingViewModel>
{
    [Display(Name="عنوان شغل"),FilterInput]
    public string? JobTitle { get; set; }
    
    
    [Display(Name="درخواست‌کننده"), FilterInput]
    public int? UserId { get; set; }
    public string? FullName { get; set; }
    

    [Display(Name="وضعیت اعتبار"), FilterInput]
    public JobExpirationStatus JobExpirationStatus {get; set;}
    
    
    [Display(Name = "مرتب سازی بر اساس"), FilterInput]
    public FilterOrderBy FilterOrderBy { get; set; }
    
    
    [Display(Name="وضعیت حذف"), FilterInput]
    public DeleteStatus DeleteStatus { get; set; }
}