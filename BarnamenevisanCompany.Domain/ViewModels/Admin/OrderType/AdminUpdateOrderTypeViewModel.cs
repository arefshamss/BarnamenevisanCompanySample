using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.OrderType;

public class AdminUpdateOrderTypeViewModel
{
    public short Id { get; set; }

    
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(100, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Title { get; set; }
    
    
    [Display(Name = "تصویر")]
    public IFormFile? Image { get; set; }
    
    
    public string? ImageUrl { get; set; }
    
    
    [Display(Name = "توضیحات")]
    [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Description { get; set; }
}