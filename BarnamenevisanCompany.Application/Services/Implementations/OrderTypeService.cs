using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.OrderMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Models.Order;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.OrderType;
using BarnamenevisanCompany.Domain.ViewModels.Client.OrderType;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class OrderTypeService(
    IOrderTypeRepository orderTypeRepository
) : IOrderTypeService
{
    #region Admin

    #region FilterAsync

    public async Task<Result<AdminFilterOrderTypeViewModel>> FilterAsync(AdminFilterOrderTypeViewModel filter)
    {
        filter ??= new();

        var conditions = Filter.GenerateConditions<OrderType>();

        #region Filter

        if (!string.IsNullOrEmpty(filter.Title))
            conditions.Add(x => EF.Functions.Like(x.Title, $"%{filter.Title}%"));

        switch (filter.DeleteStatus)
        {
            case DeleteStatus.NotDeleted:
                conditions.Add(x => !x.IsDeleted);
                break;
            case DeleteStatus.All:
                break;
            case DeleteStatus.Deleted:
                conditions.Add(x => x.IsDeleted);
                break;
        }

        #endregion

        string[] includes =
        [
            nameof(OrderType.OrderTypeMapping)
        ];
        
        await orderTypeRepository.FilterAsync(filter, conditions, x => x.MapToAdminOrderTypeViewModel(), includes: includes);
        return filter;
    }

    #endregion

    #region CreateAsync

    public async Task<Result> CreateAsync(AdminCreateOrderTypeViewModel model)
    {
        var orderType = model.MapToOrderType();

        if (model.Image is not null)
        {
            var result = await model.Image.AddImageToServer(FilePaths.OrderTypeOriginalImage);
            if (result.IsFailure)
                return Result.Failure(result.Message);
            orderType.ImageUrl = result.Value;
        }
        else
        {
            orderType.ImageUrl = SiteTools.DefaultImageName;
        }

        await orderTypeRepository.InsertAsync(orderType);
        await orderTypeRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    #endregion

    #region FillModelForUpdateAsync

    public async Task<Result<AdminUpdateOrderTypeViewModel>> FillModelForUpdateAsync(short id)
    {
        if (id < 1)
            return Result.Failure<AdminUpdateOrderTypeViewModel>(ErrorMessages.BadRequestError);

        string[] includes =
        [
            nameof(OrderType.OrderTypeMapping),
        ];
        
        var orderType = await orderTypeRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, includes: includes);

        if (orderType is null)
            return Result.Failure<AdminUpdateOrderTypeViewModel>(ErrorMessages.NotFoundError);

        return orderType.MapToAdminUpdateOrderTypeViewModel();
    }

    #endregion

    #region UpdateAsync

    public async Task<Result> UpdateAsync(AdminUpdateOrderTypeViewModel model)
    {
        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var orderType = await orderTypeRepository.FirstOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);

        if (orderType is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        orderType.UpdateOrderType(model);

        if (model.Image is not null)
        {
            var result = await model.Image.AddImageToServer(FilePaths.OrderTypeOriginalImage, deleteFileName: orderType.ImageUrl);
            if (result.IsFailure)
                return Result.Failure(result.Message);
            orderType.ImageUrl = result.Value;
        }

        orderTypeRepository.Update(orderType);
        await orderTypeRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region DeleteOrRecoverAsync

    public async Task<Result> DeleteOrRecoverAsync(short id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.SomethingWentWrong);

        var orderType = await orderTypeRepository.GetByIdAsync(id);

        if (orderType is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        var result = orderTypeRepository.SoftDeleteOrRecover(orderType);
        await orderTypeRepository.SaveChangesAsync();

        return result ? Result.Success(SuccessMessages.DeleteSuccess) : Result.Success(SuccessMessages.RecoverSuccess);
    }

#endregion

    #endregion

    #region Client

    #region GetAllAsync

    public async Task<List<ClientOrderTypeViewModel>> GetAllAsync()
    {
        string[] includes =
        [
            nameof(OrderType.OrderTypeMapping)
        ];

        var model = await orderTypeRepository.GetAllAsync(x => x.MapToClientOrderTypeViewModel(), x => !x.IsDeleted, includes: includes);
        return model;
    }

    #endregion

    #region GetByIdAsync

    public async Task<Result<ClientOrderTypeDetailsViewModel>> GetByIdAsync(short id)
    {
        if (id < 1)
            return Result.Failure<ClientOrderTypeDetailsViewModel>(ErrorMessages.BadRequestError);

        string[] includes =
        [
            $"{nameof(Order.OrderTypeMapping)}.{nameof(OrderTypeMapping.Order)}"
        ];

        var orderType = await orderTypeRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, includes: includes);

        if (orderType is null)
            return Result.Failure<ClientOrderTypeDetailsViewModel>(ErrorMessages.NotFoundError);

        return orderType.MapToClientOrderDetailsViewModel();
    }

#endregion

    #endregion
}