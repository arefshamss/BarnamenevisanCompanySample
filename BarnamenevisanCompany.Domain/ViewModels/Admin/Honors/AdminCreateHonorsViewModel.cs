using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Honors;

public class AdminCreateHonorsViewModel
{
    [Display(Name = "عنوان")]
    [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    [MaxLength(1500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Description { get; set; }

    [Display(Name = "تصویر")] 
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public IFormFile Image { get; set; }
    
    [Display(Name = "پوستر ویدئو")] 
    public IFormFile? TeaserPoster { get; set; }
    
    [Display(Name = "آدرس صفحه")]
    [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Slug { get; set; }
    
    public string? ImageName { get; set; }
    
    [Display(Name = "تیزر")]
    public string? TeaserName { get; set; }

    public string? TeaserPosterName { get; set; }
    
    [Display(Name = "شرکت")] 
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public short CompanyId { get; set; }
    
}