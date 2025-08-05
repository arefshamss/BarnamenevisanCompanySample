using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.SocialNetwork;

public class AdminSocialNetworkUpdateViewModel
{
    public byte Id { get; set; }
    
    [Display(Name = "عنوان")] 
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(150, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Title { get; set; }

    [Display(Name = "آدرس")] 
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
    [RegularExpression(SiteRegex.SiteUrlRegex, ErrorMessage = ErrorMessages.NotValid)]
    public string Url { get; set; }

    public string IconName { get; set; }

    [Display(Name = "تصویر")] 
    public IFormFile? Icon { get; set; }
}