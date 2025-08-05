using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Enums.Job;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.JobUserMapping;

public class AdminJobUserMappingViewModel
{
    public int Id { get; set; }


    [Display(Name = "درخواست‌کننده")] public string RequesterFullName { get; set; }


    [Display(Name = "عنوان شغل")] public string JobTitle { get; set; }


    [Display(Name = "تاریخ ثبت درخواست")] public DateTime CreatedDate { get; set; }


    [Display(Name = "وضعیت اعتبار")] public DateTime ExpireDate { get; set; }


    public bool IsDeleted { get; set; }
}