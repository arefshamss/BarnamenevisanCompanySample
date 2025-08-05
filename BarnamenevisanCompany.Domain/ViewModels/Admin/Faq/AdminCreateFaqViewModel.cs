using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Faq;

public class AdminCreateFaqViewModel
{
    [Display(Name = "پرسش")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(600, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Question { get; set; }    
    
    
    [Display(Name = "پاسخ")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(700, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Answer { get; set; }
}