using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.UserSocialNetworkMapper;

public class ClientCreateSocialNetworkMapperViewModel
{
    [Display(Name = "آدرس")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [RegularExpression(SiteRegex.SiteUrlRegex, ErrorMessage = ErrorMessages.NotValid)]
    
    public string SocialLink { get; set; }
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [Display(Name = "شبکه اجتماعی")]
    public byte SocialNetworkId { get; set; }

    public int UserId { get; set; }
    
    
}