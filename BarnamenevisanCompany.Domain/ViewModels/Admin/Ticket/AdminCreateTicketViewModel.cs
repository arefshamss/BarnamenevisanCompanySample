using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.TicketMessage;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Ticket;

public class AdminCreateTicketViewModel
{
    [Display(Name = "موضوع")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(60, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Title { get; set; }
    
    [Display(Name = "کاربر")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public int UserId { get; set; }
    public string UserDisplay { get; set; } 
    
    [Display(Name = "اولویت")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public short PriorityId { get; set; }
    
    public AdminCreateTicketMessageViewModel TicketMessage { get; set; }    
}