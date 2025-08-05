using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Common;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Order;

public class ClientCreateOrderViewModel:GoogleReCaptchaViewModel
{
    [Display(Name = "عنوان سفارش")] 
    [MaxLength(280, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Title { get; set; }

    
    [Display(Name = "توضیحات سفارش")] 
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Description { get; set; }

    
    [Display(Name = "فایل پیوست")] 
    public IFormFile? Attachment { get; set; }
    

    [Display(Name = "نوع سفارش")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public List<short> OrderTypeIds { get; set; } 
    
    public int UserId { get; set; }
}