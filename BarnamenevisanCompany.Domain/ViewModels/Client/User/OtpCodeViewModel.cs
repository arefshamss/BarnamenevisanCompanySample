using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.User;

public class OtpCodeViewModel : GoogleReCaptchaViewModel
{
    public int UserId { get; set; }
    public string Mobile { get; set; }  
    public string? ReturnUrl { get; set; }
    

    [Display(Name = "کد تایید")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Code { get; set; }
}