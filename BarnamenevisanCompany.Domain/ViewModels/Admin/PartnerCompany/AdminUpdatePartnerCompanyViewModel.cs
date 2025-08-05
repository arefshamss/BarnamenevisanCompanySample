using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.PartnerCompany;

public class AdminUpdatePartnerCompanyViewModel
{
    public short Id { get; set; }

    
    [Display(Name = "عنوان سایت")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Title { get; set; }

    
    [Display(Name = "آدرس سایت")]
    [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
    [RegularExpression(SiteRegex.SiteUrlRegex, ErrorMessage = ErrorMessages.NotValid)]
    public string? SiteUrl { get; set; }
    
    
    [Display(Name = "تصویر")]
    public IFormFile? Image { get; set; }
    
    
    public string? ImageUrl { get; set; }
    
    
    [Display(Name = "توضیحات کوتاه")]
    [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? ShortDescription { get; set; }
}