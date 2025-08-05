using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.JobUserMapping;

public class ClientJobUserMappingViewModel
{
    [Display(Name = "شناسه")] public int Id { get; set; }

    public int UserId { get; set; }

    [Display(Name = "عنوان شغل")] public string JobTitle { get; set; }


    [Display(Name = "تاریخ ثبت درخواست")] public DateTime CreatedDate { get; set; }
}