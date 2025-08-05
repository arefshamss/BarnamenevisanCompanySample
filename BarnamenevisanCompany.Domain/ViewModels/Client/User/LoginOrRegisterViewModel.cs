using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.User;

public class LoginOrRegisterViewModel : GoogleReCaptchaViewModel
{
    [Display(Name = "شماره همراه")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(11, ErrorMessage = ErrorMessages.MaxLengthError)]
    [RegularExpression(SiteRegex.MobileRegex, ErrorMessage = ErrorMessages.NotValid)]
    public string Mobile { get; set; }
    
    [Display(Name = "رمز عبور")]
    [DataType(DataType.Password)]
    [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Password { get; set; }   
    
    public string? ReturnUrl { get; set; }
    public bool IsLoginByPassword { get; set; } 
}