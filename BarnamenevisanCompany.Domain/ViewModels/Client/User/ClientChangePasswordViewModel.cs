using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.User;

public class ClientChangePasswordViewModel
{
    public int UserId { get; set; }         
    
    [Display(Name = "رمز عبور جدید")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MinLength(8, ErrorMessage = ErrorMessages.MinLengthError)]
    [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string NewPassword { get; set; }
    
    [Display(Name = "تکرار رمز عبور جدید")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [Compare(nameof(NewPassword), ErrorMessage = ErrorMessages.PasswordCompareError)]
    [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string ConfirmNewPassword { get; set; }  
    
    
    [Display(Name = "شماره همراه")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(11, ErrorMessage = ErrorMessages.MaxLengthError)]
    [RegularExpression(SiteRegex.MobileRegex, ErrorMessage = ErrorMessages.NotValid)]
    public string Mobile { get; set; }  
    

    [Display(Name = "کد تایید")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(6, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Code { get; set; }
}