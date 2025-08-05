using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Project;

public class AdminCreateProjectViewModel
{
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

    [Display(Name = "تصویر")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public IFormFile Image { get; set; }

    public string? ImageName { get; set; }

    [Display(Name = "آدرس صفحه")]
    [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Slug { get; set; }

    [Display(Name = "تیزر پروژه")] public IFormFile? Teaser { get; set; }

    [Display(Name = "درصد پیشرفت")]
    [Range(1, 100, ErrorMessage = "بازه انتخای باید بین 1 تا 100 باشد")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public byte ProgressByte { get; set; }

    [Display(Name = "تیزر پروژه")] public string? TeaserName { get; set; }

    [Display(Name = "پوستر ویدئو")] public IFormFile? TeaserPoster { get; set; }

    public string? TeaserPosterName { get; set; }

    [Display(Name = "توضیحات")]
    [MaxLength(3000, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Description { get; set; }

    [Display(Name = "تیم فنی پروژه")] public string? Programmer { get; set; }

    public string? ProgrammerPositions { get; set; }


    [Display(Name = "تکنولوژی‌های استفاده شده")]
    public string? Technology { get; set; }
}