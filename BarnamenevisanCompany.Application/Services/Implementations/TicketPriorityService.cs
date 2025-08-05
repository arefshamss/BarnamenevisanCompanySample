using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.TicketPriorityMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Models.Ticket;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.TicketPriority;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class TicketPriorityService(ITicketPriorityRepository ticketPriorityRepository) : ITicketPriorityService
{
    public async Task<Result<AdminFilterTicketPrioritiesViewModel>> FilterAsync(
        AdminFilterTicketPrioritiesViewModel filter)
    {
        filter ??= new();

        var conditions = Filter.GenerateConditions<TicketPriority>();
        var orders = Filter.GenerateOrders<TicketPriority>();

        #region Filters

        if (!filter.Title.IsNullOrEmptyOrWhiteSpace())
            conditions.Add(x => EF.Functions.Like(x.Title, $"%{filter.Title}%"));

        switch (filter.FilterOrderBy)
        {
            case FilterOrderBy.Descending:
                orders.Add(x => x.CreatedDate, FilterOrderBy.Descending);
                break;

            case FilterOrderBy.Ascending:
                orders.Add(x => x.CreatedDate);
                break;
        }

        switch (filter.DeleteStatus)
        {
            case DeleteStatus.Deleted:
                conditions.Add(x => x.IsDeleted);
                break;

            case DeleteStatus.NotDeleted:
                conditions.Add(x => !x.IsDeleted);
                break;
        }

        #endregion

        await ticketPriorityRepository.FilterAsync(filter, conditions, x => x.MapToAdminTicketPriorityViewModel(),
            orders);
        return filter;
    }

    public async Task<Result<AdminUpdateTicketPriorityViewModel>> FillModelForUpdateAsync(short id)
    {
        if (id < 4)
            return Result.Failure<AdminUpdateTicketPriorityViewModel>(ErrorMessages.BadRequestError);

        var ticket = await ticketPriorityRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (ticket is null)
            return Result.Failure<AdminUpdateTicketPriorityViewModel>(ErrorMessages.NotFoundError);

        return ticket.MapToAdminUpdateTicketPriorityViewModel();
    }

    public async Task<Result<SelectList>> GetSelectListAsync() =>   
        new SelectList(await ticketPriorityRepository.GetAllAsync(x => x.MapToSelectListItem(), x => !x.IsDeleted), "Value", "Text");

    public async Task<Result> CreateAsync(AdminCreateTicketPriorityViewModel model)
    {
        var ticket = model.MapToTicketPriority();

        await ticketPriorityRepository.InsertAsync(ticket);
        await ticketPriorityRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    public async Task<Result> UpdateAsync(AdminUpdateTicketPriorityViewModel model)
    {
        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var ticketPriority = await ticketPriorityRepository.FirstOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);

        if (ticketPriority is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        ticketPriority.MapToTicketPriority(model);

        ticketPriorityRepository.Update(ticketPriority);
        await ticketPriorityRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    public async Task<Result> DeleteOrRecoverAsync(short id)
    {
        if (id < 4)
            return Result.Failure(ErrorMessages.BadRequestError);

        var ticketPriority = await ticketPriorityRepository.GetByIdAsync(id);

        if (ticketPriority is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        var result = ticketPriorityRepository.SoftDeleteOrRecover(ticketPriority);
        await ticketPriorityRepository.SaveChangesAsync();

        return Result.Success(result ? SuccessMessages.DeleteSuccess : SuccessMessages.RecoverSuccess);
    }
}