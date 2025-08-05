using BarnamenevisanCompany.Domain.Enums.Ticket;
using BarnamenevisanCompany.Domain.ViewModels.Admin.TicketMessage;
using BarnamenevisanCompany.Domain.ViewModels.Admin.TicketPriority;
using BarnamenevisanCompany.Domain.ViewModels.Admin.User;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Ticket;

public class AdminTicketViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public TicketStatus TicketStatus { get; set; }
    public bool ReadBySupporter { get; set; }
    public bool ReadByUser { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }
    public AdminTicketPriorityViewModel Priority { get; set; }
    public AdminUserViewModel User { get; set; }
    public List<AdminTicketMessageViewModel> Messages { get; set; } 
}