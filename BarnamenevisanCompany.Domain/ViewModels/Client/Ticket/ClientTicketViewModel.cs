using BarnamenevisanCompany.Domain.Enums.Ticket;
using BarnamenevisanCompany.Domain.ViewModels.Client.TicketMessage;
using BarnamenevisanCompany.Domain.ViewModels.Client.TicketPriority;
using BarnamenevisanCompany.Domain.ViewModels.Client.User;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Ticket;

public class ClientTicketViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public TicketStatus TicketStatus { get; set; }
    public bool ReadBySupporter { get; set; }
    public bool ReadByUser { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }
    public ClientTicketPriorityViewModel Priority { get; set; }
    public ClientUserViewModel User { get; set; }
    public List<ClientTicketMessageViewModel> Messages { get; set; }    
}