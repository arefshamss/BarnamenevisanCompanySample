using BarnamenevisanCompany.Domain.ViewModels.Client.User;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.TicketMessage;

public class ClientTicketMessageViewModel
{
    public int Id { get; set; } 
    public int TicketId { get; set; }   
    public int SenderId { get; set; }
    public string Message { get; set; }
    public string? AttachmentUrl { get; set; }     
    public bool ReadBySupporter { get; set; }
    public bool ReadByUser { get; set; }
    public DateTime CreatedDate { get; set; }
    public ClientUserViewModel Sender { get; set; }  
}