using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.TicketPriority;

public class AdminCreateTicketPriorityViewModel
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(60, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Title { get; set; }
}