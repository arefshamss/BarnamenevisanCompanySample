using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.TicketMessageMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Ticket;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.TicketMessage;
using BarnamenevisanCompany.Domain.ViewModels.Client.TicketMessage;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class TicketMessageService(ITicketMessageRepository ticketMessageRepository, ITicketRepository ticketRepository) : ITicketMessageService
{
    #region Admin

    public async Task<Result> CreateAsync(AdminCreateTicketMessageViewModel model)
    {
        #region Validations

        if (model.SenderId < 1 || model.TicketId < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        if (await ticketRepository.AnyAsync(x => x.Id == model.TicketId && x.TicketStatus == TicketStatus.Closed))
            return Result.Failure(ErrorMessages.TicketIsClosed);

        #endregion

        var ticketMessage = model.MapTicketMessage();

        #region Attachment

        if (model.Attachment is not null)
        {
            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(model.Attachment.FileName);
            var result = await model.Attachment.AddFilesToServer(fileName, FilePaths.TicketMessageAttachmentFile);
            if (result.IsFailure)
                return Result.Failure(result.Message);

            ticketMessage.Attachment = result.Value;
        }

        #endregion

        #region Update Ticket Status

        bool ticketIsPendingForUserAnswer = await ticketRepository.AnyAsync(x => x.Id == model.TicketId
                                                                                 && !x.TicketMessages.Any(s => s.SenderId != model.SenderId));
        await ticketRepository.ExecuteUpdateAsync(
            x => x.Id == model.TicketId,
            x => x.SetProperty(c => c.TicketStatus, ticketIsPendingForUserAnswer ? TicketStatus.PendingForUserAnswer : TicketStatus.SupporterAnswered));

        #endregion

        await ticketMessageRepository.InsertAsync(ticketMessage);
        await ticketMessageRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    #endregion

    #region Client

    public async Task<Result> CreateAsync(ClientCreateTicketMessageViewModel model)
    {
        #region Validations

        if (model.SenderId < 1 || model.TicketId < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        if (await ticketRepository.AnyAsync(x => x.Id == model.TicketId && x.TicketStatus == TicketStatus.Closed))
            return Result.Failure(ErrorMessages.TicketIsClosed);

        #endregion

        var ticketMessage = model.MapTicketMessage();

        #region Attachment

        if (model.Attachment is not null)
        {
            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(model.Attachment.FileName);
            var result = await model.Attachment.AddFilesToServer(fileName, FilePaths.TicketMessageAttachmentFile);
            if (result.IsFailure)
                return Result.Failure(result.Message);

            ticketMessage.Attachment = result.Value;
        }

        #endregion

        #region Update Ticket Status

        await ticketRepository.ExecuteUpdateAsync(
            x => x.Id == model.TicketId,
            x => x.SetProperty(c => c.TicketStatus, TicketStatus.InProgress));

        #endregion

        await ticketMessageRepository.InsertAsync(ticketMessage);
        await ticketMessageRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    #endregion
}