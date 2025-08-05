using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Project;

public class AdminUpdateProjectViewModel
{
    public short Id { get; set; }

    [Display(Name = "عنوان")]
    [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Title { get; set; }
    
    [Display(Name = "اولویت نمایش")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [Range(1, short.MaxValue, ErrorMessage = ErrorMessages.RangeError)]
    public short Priority { get; set; }

    [Display(Name = "آدرس سایت")]
    [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [RegularExpression(SiteRegex.SiteUrlRegex, ErrorMessage = ErrorMessages.NotValid)]
    public string SiteUrl { get; set; }

    [Display(Name = "تصویر")] public IFormFile? Image { get; set; }

    [Display(Name = "آدرس صفحه")]
    [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Slug { get; set; }

    public string ImageName { get; set; }

    [Display(Name = "تیزر")] public IFormFile? Teaser { get; set; }

    [Display(Name = "درصد پیشرفت")]
    [Range(1, 100, ErrorMessage = "بازه انتخابی باید بین 1 تا 100 باشد")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public byte ProgressByte { get; set; }

    [Display(Name = "تیزر")] public string? TeaserName { get; set; }

    [Display(Name = "پوستر تیزر")] public IFormFile? TeaserPoster { get; set; }
    public string? TeaserPosterName { get; set; }

    [Display(Name = "توضیحات")]
    [MaxLength(3000, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Description { get; set; }

    [Display(Name = "تیم فنی پروژه")]

    public string? Programmer { get; set; }

    public string? ProgrammerPositions { get; set; }

    [Display(Name = "تکنولوژی‌های استفاده شده")]
    public string? Technology { get; set; }

   
}