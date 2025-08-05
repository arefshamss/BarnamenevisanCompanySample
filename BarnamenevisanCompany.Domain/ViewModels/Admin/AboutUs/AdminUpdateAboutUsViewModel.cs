using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.AboutUs;

public class AdminUpdateAboutUsViewModel
{

    [Display(Name = "عنوان اصلی")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(200,ErrorMessage = ErrorMessages.MaxLengthError)]
    public string TopTitle { get; set; }

    [Display(Name = "توضیحات کوتاه عنوان اصلی")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(1000,ErrorMessage = ErrorMessages.MaxLengthError)]
    public string TopDescription { get; set; }

    [Display(Name = "عنوان وسط صفحه")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(600,ErrorMessage = ErrorMessages.MaxLengthError)]
    public string MainDescriptionLeft { get; set; }

    [Display(Name = "توضیحات کوتاه وسط صفحه")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(600,ErrorMessage = ErrorMessages.MaxLengthError)]
    public string MainDescriptionRight { get; set; }

    [Display(Name = "عنوان ارزش‌های ما")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(200,ErrorMessage = ErrorMessages.MaxLengthError)]
    public string OurValuesTitle { get; set; }

    [Display(Name = "توضیحات اشتیاق ما")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(700,ErrorMessage = ErrorMessages.MaxLengthError)]
    public string OurPassionDescription { get; set; }

    [Display(Name = "توضیحات شفافیت")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(1000,ErrorMessage = ErrorMessages.MaxLengthError)]
    public string TransparencyDescription { get; set; }

    [Display(Name = "توضیحات ارزش ما")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(1000,ErrorMessage = ErrorMessages.MaxLengthError)]
    public string OurValuesDescription { get; set; }


    [Display(Name = "توضیحات ماموریت ما")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(1000,ErrorMessage = ErrorMessages.MaxLengthError)]
    public string OurMissionDescription { get; set; }
}