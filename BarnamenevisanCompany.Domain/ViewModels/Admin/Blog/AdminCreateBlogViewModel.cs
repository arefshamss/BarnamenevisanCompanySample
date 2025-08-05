using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Blog;

public class AdminCreateBlogViewModel
{
    [Display(Name = "نویسنده")]
    public int? UserId { get; set; }
    
    
    public string? Author { get; set; }
    
    
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(300,ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Title { get; set; }

    
    [Display(Name = "آدرس صفحه")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(350,ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Slug { get; set; }

    
    [Display(Name = "توضیح کوتاه")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(500,ErrorMessage = ErrorMessages.MaxLengthError)]
    public string ShortDescription { get; set; }

    
    [Display(Name = "متن اصلی")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Description { get; set; }

    
    [Display(Name = "تصویر")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public IFormFile Image { get; set; }
}