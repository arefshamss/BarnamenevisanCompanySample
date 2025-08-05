using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.Ticket;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class TicketPriorityRepository(BarnamenevisanContext context) : EfRepository<TicketPriority, short>(context), ITicketPriorityRepository;