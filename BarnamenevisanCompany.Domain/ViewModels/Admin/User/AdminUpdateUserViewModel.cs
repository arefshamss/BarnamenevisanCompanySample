using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.User;

public class AdminUpdateUserViewModel
{
    public int Id { get; set; } 
    
    [Display(Name = "تصویر پروفایل")]
    public IFormFile? AvatarImageFile { get; set; }
    public string? AvatarImageName { get; set; }    
    
    [Display(Name = "نام")]
    [MaxLength(50, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? FirstName { get; set; }
    
    [Display(Name = "نام خانوادگی")]
    [MaxLength(50, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? LastName { get; set; }
    
    [Display(Name = "شماره همراه")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [RegularExpression(SiteRegex.MobileRegex, ErrorMessage = ErrorMessages.NotValid)]
    public string Mobile { get; set; }

    [Display(Name = "رمزعبور")]
    [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Password { get; set; } 
    
    [Display(Name = "وضعیت فعال بودن حساب کاربری")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public bool IsActive { get; set; }
}