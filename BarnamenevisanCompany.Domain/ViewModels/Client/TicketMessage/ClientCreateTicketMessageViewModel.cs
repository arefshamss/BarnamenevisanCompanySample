using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Enums.Ticket;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.TicketMessage;

public class ClientCreateTicketMessageViewModel
{
    public TicketStatus TicketStatus { get; set; }
    public int TicketId { get; set; }   
    public int SenderId { get; set; }
    
    [Display(Name = "پیام")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(1000, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Message { get; set; }
    
    [Display(Name = "ضمیمه")]
    public IFormFile? Attachment { get; set; }
}