using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.OrderStep;

public class AdminCreateOrderStepViewModel
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Title { get; set; }
    [Display(Name = "اولویت نمایش")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [Range(1,250,ErrorMessage = ErrorMessages.RangeError)]
    public short PriorityId { get; set; }
}