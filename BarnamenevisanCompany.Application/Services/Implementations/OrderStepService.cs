using BarnamenevisanCompany.Application.Mappers.OrderMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.Order;
using BarnamenevisanCompany.Domain.Models.Order;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.OrderStep;
using BarnamenevisanCompany.Domain.ViewModels.Client.OrderStep;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class OrderStepService(IOrderStepRepository orderStepRepository) : IOrderStepService
{
    #region FitlerAsync

    public async Task<AdminFilterOrderStepViewModel> FilterAsync(AdminFilterOrderStepViewModel filter)
    {
        filter ??= new AdminFilterOrderStepViewModel();

        var condition = Filter.GenerateConditions<OrderStep>();
        var order = Filter.GenerateOrders<OrderStep>();

        if (!string.IsNullOrEmpty(filter.Title))
            condition.Add(s => EF.Functions.Like(s.Title, $"%{filter.Title}%"));

        switch (filter.OrderStepFilter)
        {
            case OrderStepFilter.None:
                break;
            case OrderStepFilter.Priority:
                order.Add(s => s.PriorityId);
                break;
        }

        switch (filter.DeleteStatus)
        {
            case DeleteStatus.All:
                break;
            case DeleteStatus.Deleted:
                condition.Add(s => s.IsDeleted);
                break;
            case DeleteStatus.NotDeleted:
                condition.Add(s => !s.IsDeleted);
                break;
        }

        await orderStepRepository.FilterAsync(filter, condition, s => s.MapToAdminOrderStepViewModel(), order);
        return filter;
    }

    #endregion

    #region Create

    public async Task<Result> CrateAsync(AdminCreateOrderStepViewModel model)
    {
        if (await orderStepRepository.AnyAsync(s => s.PriorityId == model.PriorityId))
            return Result.Failure(ErrorMessages.PriorityOrderStepExist);
        await orderStepRepository.InsertAsync(model.MapToOrderStep());
        await orderStepRepository.SaveChangesAsync();
        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    #endregion

    #region Update

    public async Task<Result> UpdateAsync(AdminUpdateOrderStepViewModel model)
    {
        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var orderStep = await orderStepRepository.GetByIdAsync(model.Id);
        if (orderStep == null)
            return Result.Failure(ErrorMessages.NotFoundError);
        if (model.PriorityId != orderStep.PriorityId)
        {
            var exist = await orderStepRepository.AnyAsync(s => s.PriorityId == model.PriorityId);
            if (exist)
                return Result.Failure(ErrorMessages.PriorityOrderStepExist);
        }

        orderStep.MapToOrderStep(model);
        orderStepRepository.Update(orderStep);
        await orderStepRepository.SaveChangesAsync();
        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region FillModelForUpdateAsync

    public async Task<Result<AdminUpdateOrderStepViewModel>> FillModelForUpdateAsync(short orderStepId)
    {
        if (orderStepId < 1)
            return Result.Failure<AdminUpdateOrderStepViewModel>(ErrorMessages.BadRequestError);

        var orderStep = await orderStepRepository.GetByIdAsync(orderStepId);
        if (orderStep == null)
            return Result.Failure<AdminUpdateOrderStepViewModel>(ErrorMessages.NotFoundError);

        return orderStep.MapToAdminUpdateOrderStepViewModel();
    }

    #endregion

    #region DeleteOrRecover

    public async Task<Result> DeleteOrRecoverAsync(short orderStepId)
    {
        if (orderStepId < 1)
            return Result.Failure(ErrorMessages.BadRequestError);
        var orderStep = await orderStepRepository.GetByIdAsync(orderStepId);
        if (orderStep == null)
            return Result.Failure(ErrorMessages.NotFoundError);

        var result = orderStepRepository.SoftDeleteOrRecover(orderStep);
        await orderStepRepository.SaveChangesAsync();

        if (result)
            return Result.Success(SuccessMessages.DeleteSuccess);

        return Result.Success(SuccessMessages.RecoverSuccess);
    }

    #endregion

    #region GetAllAsync

    public async Task<Result<List<ClientOrderStepViewModel>>> GetClientOrderStepAsync()
    {
        return await orderStepRepository.GetAllAsync(s => s.MapToClientOrderStepViewModel(), s => !s.IsDeleted);
    }

    #endregion
}