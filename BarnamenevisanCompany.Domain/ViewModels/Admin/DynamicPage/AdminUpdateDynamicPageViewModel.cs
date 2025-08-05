using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.DynamicPage;

public class AdminUpdateDynamicPageViewModel
{
    public short Id { get; set; }
    
    
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(300, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Title { get; set; }


    [Display(Name = "آدرس صفحه")] 
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(400, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Slug { get; set; }

    
    [Display(Name = "شرح مختصر")] 
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(1000, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string ShortDescription { get; set; }

    
    [Display(Name = "شرح کامل")] 
    public string? Description { get; set; }


    [Display(Name = "صفحه فعال است؟")] public bool IsActive { get; set; }
}