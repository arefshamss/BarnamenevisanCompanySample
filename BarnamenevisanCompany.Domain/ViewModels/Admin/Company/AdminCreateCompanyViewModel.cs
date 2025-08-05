using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Company;

public class AdminCreateCompanyViewModel
{
    [Display(Name = "نام شرکت")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Title { get; set; }


    [Display(Name = "آدرس سایت")]
    [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
    [RegularExpression(SiteRegex.SiteUrlRegex, ErrorMessage = ErrorMessages.NotValid)]
    public string? SiteUrl { get; set; }


    [Display(Name = "تصویر")] public IFormFile? Image { get; set; }


    [Display(Name = "توضیحات")]
    [MaxLength(700, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Description { get; set; }
}