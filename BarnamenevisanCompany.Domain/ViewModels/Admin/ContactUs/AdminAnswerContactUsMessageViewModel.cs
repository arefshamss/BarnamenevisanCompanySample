using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.ContactUs;

public class AdminAnswerContactUsMessageViewModel
{
    public short Id { get; set; }

    [Display(Name = "عنوان")] public string Title { get; set; }

    [Display(Name = "ایمیل")] public string Email { get; set; }

    [Display(Name = "پیام کاربر")]
    [MaxLength(2500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string UserMessage { get; set; }

    [Display(Name = "پاسخ")]
    [MaxLength(2500, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string AdminMessage { get; set; }    

    [Display(Name = "تاریخ ایجاد")] public string Createdate { get; set; }
}