using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.UserSocialNetworkMapper;

public class ClientUpdateUserSocialNetworkViewModel
{
    public byte Id { get; set; }
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Title { get; set; }
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [Display(Name = "عکس")]
    public string ImageName { get; set; }
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [Display(Name = "آدرس")]
    [RegularExpression(SiteRegex.SiteUrlRegex, ErrorMessage = ErrorMessages.NotValid)]
    public string SocialUrl { get; set; }
}