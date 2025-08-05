using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.HonorsGallery;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Honors;

public class AdminUpdateHonorsViewModel
{
    public short HonorsId { get; set; }
    
    [Display(Name = "عنوان")]
    [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    [MaxLength(1500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Description { get; set; }
    
    [Display(Name = "آدرس صفحه")]
    [MaxLength(1500, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Slug { get; set; }
    
    [Display(Name = "تصویر")] 

    public IFormFile? Image { get; set; }
    
    [Display(Name = "پوستر ویدئو")] 
    public IFormFile? TeaserPoster { get; set; }
    
    public string? ImageName { get; set; }

    public string? TeaserPosterImageName { get; set; }
    
    [Display(Name = "شرکت")] 
    [Required(ErrorMessage = ErrorMessages.RequiredError)]  
    public short CompanyId { get; set; }
    
    [Display(Name = "تیزر")]
    public string? TeaserName { get; set; }



}