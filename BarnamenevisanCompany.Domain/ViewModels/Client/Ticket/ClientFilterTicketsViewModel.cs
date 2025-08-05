using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Ticket;

public class ClientFilterTicketsViewModel : BasePaging<ClientTicketViewModel>
{
    public int UserId { get; set; } 
}