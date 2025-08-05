using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Client.TicketMessage;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Ticket;

public class ClientCreateTicketViewModel
{
    [Display(Name = "موضوع")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(60, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Title { get; set; }
    
    [Display(Name = "اولویت")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public short PriorityId { get; set; }

    public int UserId { get; set; } 
    
    public ClientCreateTicketMessageViewModel Message { get; set; } 
}