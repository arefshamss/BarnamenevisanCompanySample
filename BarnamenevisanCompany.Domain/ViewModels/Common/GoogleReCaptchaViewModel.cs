using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Common;

public class GoogleReCaptchaViewModel
{
    [Required(ErrorMessage = ErrorMessages.CaptchaError)]
    public string Captcha { get; set; }
}