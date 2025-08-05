using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Ticket;

public sealed class TicketPriority : BaseEntity<short>
{
    #region Properties

    public required string Title { get; set; }   

    #endregion

    #region Relations

    public ICollection<Ticket> Tickets { get; set; }    

    #endregion
}