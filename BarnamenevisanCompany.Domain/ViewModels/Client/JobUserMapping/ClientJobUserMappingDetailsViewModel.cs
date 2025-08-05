using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.JobUserMapping;

public class ClientJobUserMappingDetailsViewModel
{
    [Display(Name = "شناسه")] public int Id { get; set; }


    [Display(Name = "توضیحات")] public string? Description { get; set; }


    [Display(Name = "رزومه")] public string Attachment { get; set; }


    [Display(Name = "عنوان شغل")] public string JobTitle { get; set; }


    [Display(Name = "تاریخ ثبت درخواست")] public DateTime CreatedDate { get; set; }


    [Display(Name = "وضعیت اعتبار آگهی")] public DateTime ExpireDate { get; set; }
}