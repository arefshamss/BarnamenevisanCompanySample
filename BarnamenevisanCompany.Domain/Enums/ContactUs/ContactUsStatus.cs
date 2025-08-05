using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.Enums.ContactUs;

public enum ContactUsStatus : byte
{
    [Display(Name = "همه")]
    All,
    [Display(Name = "بدون پاسخ")]
    InProcess,
    [Display(Name = "پاسخ داده شده")]
    IsAnswered,
}