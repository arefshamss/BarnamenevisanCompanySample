using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Models.Ticket;

namespace BarnamenevisanCompany.Domain.Contracts;

public interface ITicketMessageRepository : IEfRepository<TicketMessage>;