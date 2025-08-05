using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Faq;

public class AdminFaqViewModel
{
    public short Id { get; set; }
    
    [Display(Name = "پرسش")]
    [MaxLength(580, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Question { get; set; }    
    
    [Display(Name = "تاریخ ایجاد")] public DateTime CreatedDate { get; set; }
    
    public bool IsDeleted { get; set; }
}