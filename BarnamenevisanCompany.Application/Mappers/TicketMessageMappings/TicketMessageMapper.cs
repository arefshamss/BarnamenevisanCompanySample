using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.UserMappings;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Models.Ticket;
using BarnamenevisanCompany.Domain.ViewModels.Admin.TicketMessage;
using BarnamenevisanCompany.Domain.ViewModels.Client.TicketMessage;

namespace BarnamenevisanCompany.Application.Mappers.TicketMessageMappings;

public static class TicketMessageMapper
{
    public static AdminTicketMessageViewModel MapToAdminTicketMessageViewModel(this TicketMessage model) =>
        new()
        {
            Id = model.Id,
            Message = model.Message.Trim(),
            TicketId = model.TicketId,
            SenderId = model.SenderId,
            AttachmentUrl = !model.Attachment.IsNullOrEmptyOrWhiteSpace() ? FilePaths.TicketMessageAttachmentFile + model.Attachment : null,
            ReadByUser = model.ReadByUser,
            ReadBySupporter = model.ReadBySupporter,
            CreatedDate = model.CreatedDate,
            Sender = model.Sender.MapToAdminUserViewModel()
        };

    public static ClientTicketMessageViewModel MapToClientTicketMessageViewModel(this TicketMessage model) =>
        new()
        {
            Id = model.Id,
            Message = model.Message.Trim(),
            TicketId = model.TicketId,
            SenderId = model.SenderId,
            AttachmentUrl = !model.Attachment.IsNullOrEmptyOrWhiteSpace() ? FilePaths.TicketMessageAttachmentFile + model.Attachment : null,
            ReadByUser = model.ReadByUser,
            ReadBySupporter = model.ReadBySupporter,
            CreatedDate = model.CreatedDate,
            Sender = model.Sender.MapToClientUserViewModel()
        };

    public static TicketMessage MapTicketMessage(this AdminCreateTicketMessageViewModel model) =>
        new()
        {
            SenderId = model.SenderId,
            TicketId = model.TicketId,
            Message = model.Message,
            ReadBySupporter = true
        };
    
    public static TicketMessage MapTicketMessage(this ClientCreateTicketMessageViewModel model) =>  
        new()
        {
            SenderId = model.SenderId,
            TicketId = model.TicketId,
            Message = model.Message,
            ReadByUser = true
        };
}