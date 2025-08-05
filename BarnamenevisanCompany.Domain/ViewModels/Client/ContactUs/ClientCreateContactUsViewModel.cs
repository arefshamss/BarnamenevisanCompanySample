using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.ContactUs;

public class ClientCreateContactUsViewModel:GoogleReCaptchaViewModel
{
    [Display(Name = "عنوان")] 
    [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Title { get; set; }
    
    [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [Display(Name = "نام و نام خانوادگی")]
    public string FullName { get; set; }

    [Display(Name = "شماره تماس")]
    [MaxLength(11, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [RegularExpression(SiteRegex.MobileRegex, ErrorMessage = ErrorMessages.NotValid)]
    public string PhoneNumber { get; set; }

    [Display(Name = "ایمیل")]
    [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [RegularExpression(SiteRegex.EmailRegex, ErrorMessage = ErrorMessages.NotValid)]
    public string Email { get; set; }
    
    [Display(Name = "متن پیام")]
    [MaxLength(2000, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Message { get; set; }
}