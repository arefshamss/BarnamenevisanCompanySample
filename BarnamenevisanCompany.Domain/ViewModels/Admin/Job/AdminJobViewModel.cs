using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Job;

public class AdminJobViewModel
{
    [Display(Name = "شناسه")] public int Id { get; set; }
    
    [Display(Name = "عنوان")] public string Title { get; set; }
    
    [Display(Name = "حقوق از")] public string? SalaryFrom { get; set; }
    
    [Display(Name = "حقوق تا")]  public string? SalaryTo { get; set; }

    [Display(Name = "تاریخ انقضا")] public DateTime ExpireDate { get; set; }

    [Display(Name = "تاریخ ایجاد")] public DateTime CreatedDate { get; set; }

    public bool IsDeleted { get; set; }
}