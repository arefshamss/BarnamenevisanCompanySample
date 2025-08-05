using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.OurService;

public class AdminCreateOurServiceViewModel
{
    [Display(Name = "عنوان")] 
    [MaxLength(150,ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Title { get; set; }


    [Display(Name = "تصویر")] 
 
    [Required(ErrorMessage = ErrorMessages.ImageIsRequired)]
    public IFormFile Image { get; set; }

    public string? ImageName { get; set; }
    [Display(Name = "آدرس صفحه")] 
    [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Slug { get; set; }


    [Display(Name = "توضیحات کوتاه")] 
    [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string ShortDescription { get; set; }

    [Display(Name = "توضیحات اصلی")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string LongDescription { get; set; }
}