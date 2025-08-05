using BarnamenevisanCompany.Application.Mappers.TicketMessageMappings;
using BarnamenevisanCompany.Application.Mappers.TicketPriorityMappings;
using BarnamenevisanCompany.Application.Mappers.UserMappings;
using BarnamenevisanCompany.Domain.Enums.Ticket;
using BarnamenevisanCompany.Domain.Models.Ticket;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Ticket;
using BarnamenevisanCompany.Domain.ViewModels.Client.Ticket;

namespace BarnamenevisanCompany.Application.Mappers.TicketMappings;

public static class TicketMapper
{
    #region Admin

    public static void MapToTicket(this Ticket ticket, AdminUpdateTicketViewModel model)
    {
        ticket.Id = model.Id;
        ticket.Title = model.Title;
        ticket.PriorityId = model.PriorityId;
    }

    public static AdminTicketViewModel MapToAdminTicketViewModel(this Ticket model) =>
        new()
        {
            Id = model.Id,
            TicketStatus = model.TicketStatus,
            Priority = model.Priority.MapToAdminTicketPriorityViewModel(),
            CreatedDate = model.CreatedDate,
            Title = model.Title,
            IsDeleted = model.IsDeleted,
            User = model.User.MapToAdminUserViewModel(),
            ReadBySupporter = model.ReadBySupporter,
            ReadByUser = model.ReadByUser,
            Messages = model.TicketMessages?.Select(x => x.MapToAdminTicketMessageViewModel()).ToList()
        };

    public static AdminUpdateTicketViewModel MapToAdminUpdateTicketViewModel(this Ticket model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            PriorityId = model.PriorityId,
        };

    public static Ticket MapToTicket(this AdminCreateTicketViewModel ticket) =>
        new()
        {
            Title = ticket.Title,
            UserId = ticket.UserId,
            PriorityId = ticket.PriorityId,
            ReadBySupporter = true,
            TicketStatus = TicketStatus.PendingForUserAnswer
        };

    #endregion

    #region Client

    public static Ticket MapToTicket(this ClientCreateTicketViewModel ticket) =>    
        new()
        {
            Title = ticket.Title,
            UserId = ticket.UserId,
            PriorityId = ticket.PriorityId,
            ReadBySupporter = true,
            TicketStatus = TicketStatus.InProgress
        };
    
    public static ClientTicketViewModel MapToClientTicketViewModel(this Ticket model) =>
        new()
        {
            Id = model.Id,
            TicketStatus = model.TicketStatus,
            Priority = model.Priority.MapToClientTicketPriorityViewModel(),
            CreatedDate = model.CreatedDate,
            Title = model.Title,
            IsDeleted = model.IsDeleted,
            User = model.User.MapToClientUserViewModel(),
            ReadBySupporter = model.ReadBySupporter,
            ReadByUser = model.ReadByUser,
            Messages = model.TicketMessages?.Select(x => x.MapToClientTicketMessageViewModel()).ToList()
        };

    #endregion
}