using BarnamenevisanCompany.Domain.Enums.Ticket;
using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Ticket;

public sealed class Ticket : BaseEntity
{
    #region Properties

    public required string Title { get; set; }
    public TicketStatus TicketStatus { get; set; }
    public int UserId { get; set; }
    public short PriorityId { get; set; }
    public bool ReadBySupporter { get; set; }
    public bool ReadByUser { get; set; }

    #endregion

    #region Relations

    public User.User User { get; set; }
    public TicketPriority Priority { get; set; }
    public ICollection<TicketMessage> TicketMessages { get; set; }  

    #endregion
}