using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Enums.Order;
using BarnamenevisanCompany.Domain.ViewModels.Admin.OrderType;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.JobUserMapping;

public class AdminJobUserMappingDetailsViewModel
{
    [Display(Name = "شناسه")] public int Id { get; set; }


    [Display(Name = "توضیحات")] public string? Description { get; set; }


    [Display(Name = "رزومه")] public string Attachment { get; set; }


    [Display(Name = "درخواست‌کننده")] public string RequesterFullName { get; set; }


    [Display(Name = "شماره موبایل")] public string RequesterMobile { get; set; }


    [Display(Name = "عنوان شغل")] public string JobTitle { get; set; }


    [Display(Name = "تاریخ ثبت درخواست")] public DateTime CreatedDate { get; set; }


    [Display(Name = "وضعیت اعتبار آگهی")] public DateTime ExpireDate { get; set; }
}