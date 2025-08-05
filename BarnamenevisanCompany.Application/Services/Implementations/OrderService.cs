using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.OrderMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.Order;
using BarnamenevisanCompany.Domain.Models.Order;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Order;
using BarnamenevisanCompany.Domain.ViewModels.Client.Order;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class OrderService(
    IOrderRepository orderRepository,
    IUserRepository userRepository,
    ISmsService smsService,
    IOrderTypeMappingRepository orderTypeMappingRepository
) : IOrderService
{
    #region Admin

    #region FilterAsync

    public async Task<Result<AdminFilterOrderViewModel>> FilterAsync(AdminFilterOrderViewModel filter)
    {
        filter ??= new();

        var conditions = Filter.GenerateConditions<Order>();
        var orders = Filter.GenerateOrders<Order>();

        #region Filter

        if (!string.IsNullOrEmpty(filter.Title))
            conditions.Add(x => EF.Functions.Like(x.Title, $"%{filter.Title.Trim()}%"));

        if (!string.IsNullOrEmpty(filter.Mobile))
            conditions.Add(x => EF.Functions.Like(x.User.Mobile, $"%{filter.Mobile.Trim()}%"));

        if (filter.UserId > 0)
            conditions.Add(x => x.UserId == filter.UserId);

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
            case DeleteStatus.NotDeleted:
                conditions.Add(x => !x.IsDeleted);
                break;
            case DeleteStatus.All:
                break;
            case DeleteStatus.Deleted:
                conditions.Add(x => x.IsDeleted);
                break;
        }

        switch (filter.OrderStatus)
        {
            case FilterOrderStatus.InProgress:
                conditions.Add(x => x.OrderStatus == OrderStatus.InProgress);
                break;
            case FilterOrderStatus.All:
                break;
            case FilterOrderStatus.Rejected:
                conditions.Add(x => x.OrderStatus == OrderStatus.Rejected);
                break;
            case FilterOrderStatus.Done:
                conditions.Add(x => x.OrderStatus == OrderStatus.Done);
                break;
        }

        #endregion

        string[] includes =
        [
            nameof(Order.User),
            $"{nameof(Order.OrderTypeMapping)}.{nameof(OrderTypeMapping.OrderType)}"
        ];


        await orderRepository.FilterAsync(filter, conditions, x => x.MapToAdminOrderViewModel(), orders, includes: includes);
        return filter;
    }

    #endregion

    #region GetByIdAsync

    public async Task<Result<AdminOrderDetailsViewModel>> GetByIdAsync(int id)
    {
        if (id < 1)
            return Result.Failure<AdminOrderDetailsViewModel>(ErrorMessages.BadRequestError);

        string[] includes =
        [
            nameof(Order.User),
            $"{nameof(Order.OrderTypeMapping)}.{nameof(OrderTypeMapping.OrderType)}"
        ];

        var order = await orderRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, includes: includes);

        if (order is null)
            return Result.Failure<AdminOrderDetailsViewModel>(ErrorMessages.NotFoundError);

        return order.MapToAdminOrderDetailsViewModel();
    }

    #endregion

    #region DeleteOrRecoverAsync

    public async Task<Result> DeleteOrRecoverAsync(int id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.SomethingWentWrong);

        var order = await orderRepository.GetByIdAsync(id);

        if (order is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        var result = orderRepository.SoftDeleteOrRecover(order);
        await orderRepository.SaveChangesAsync();

        return result ? Result.Success(SuccessMessages.DeleteSuccess) : Result.Success(SuccessMessages.RecoverSuccess);
    }

    #endregion

    #region AnswerToOrderAsync

    public async Task<Result> AnswerToOrderAsync(AdminAnswerToOrderViewModel model)
    {
        string[] includes =
        [
            nameof(Order.User),
        ];

        var order = await orderRepository.FirstOrDefaultAsync(x => x.Id == model.OrderId && !x.IsDeleted, includes: includes);

        if (order is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        
        if (model.Answer.IsNullOrEmptyOrWhiteSpace())
            return Result.Failure(ErrorMessages.RequiredOrderAnswer);


        if (order.Answer.IsNullOrEmptyOrWhiteSpace() && !model.Answer.IsNullOrEmptyOrWhiteSpace())
        {
            var result = await sendSmsForAnswerAsync(order.User.Mobile, string.Format(SmsMessages.OrderAnswer, order.User.FirstName, order.User.LastName, order.OrderNumber, order.Title));
            if (result.IsFailure)
                return Result.Failure(result.Message);
        }

        order.MapToOrder(model);

        orderRepository.Update(order);
        await orderRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.MessageSentSuccessfully);
    }

    #endregion

    #region UpdateOrderStatusAsync

    public async Task<Result> UpdateOrderStatusAsync(int orderId, OrderStatus status)
    {
        var order = await orderRepository.GetByIdAsync(orderId);

        if (order is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        order.OrderStatus = status;

        orderRepository.Update(order);
        await orderRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.SuccessfullyDone);
    }

    #endregion

    #region SendSms - PRIVATE

    private async Task<Result> sendSmsForAnswerAsync(string mobile, string message)
    {
        Result smsSendResult;
        byte retryCount = 0;
        do
        {
            smsSendResult = await smsService.SendSmsAsync(mobile, message);
            retryCount++;
        } while (smsSendResult.IsFailure && retryCount <= 5);

        return smsSendResult.IsSuccess
            ? Result.Success(message: SuccessMessages.SmsSentToUserSuccessfully)
            : Result.Failure(message: ErrorMessages.SmsDidNotSendError);
    }

    #endregion

    #endregion

    #region Client

    #region FilterAsync

    public async Task<Result<ClientFilterOrderViewModel>> FilterAsync(ClientFilterOrderViewModel filter)
    {
        filter ??= new();

        var conditions = Filter.GenerateConditions<Order>();
        var orders = Filter.GenerateOrders<Order>();

        orders.Add(x => x.CreatedDate, FilterOrderBy.Descending);

        conditions.Add(x => !x.IsDeleted);

        conditions.Add(x => x.UserId == filter.UserId);

        string[] includes =
        [
            nameof(Order.User),
            $"{nameof(Order.OrderTypeMapping)}.{nameof(OrderTypeMapping.OrderType)}"
        ];


        await orderRepository.FilterAsync(filter, conditions, x => x.MapToClientOrderViewModel(), orders, includes: includes);
        return filter;
    }

    #endregion

    #region CreateAsync

    public async Task<Result> CreateAsync(ClientCreateOrderViewModel model)
    {
        var order = model.MapToOrder();

        #region Add File

        if (model.Attachment is not null)
        {
            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(model.Attachment.FileName);

            var result = await model.Attachment.AddArchiveFilesToServer(fileName, FilePaths.OrderAttachment);

            if (result.IsFailure)
                return Result.Failure(result.Message);

            order.Attachment = result.Value;
        }

        #endregion

        var user = await userRepository.GetByIdAsync(model.UserId);

        if (user == null || user.Id < 1)
            return Result.Failure<ClientOrderDetailsViewModel>(ErrorMessages.BadRequestError);

        if (user.FirstName.IsNullOrEmptyOrWhiteSpace() || user.LastName.IsNullOrEmptyOrWhiteSpace())
            return Result.Failure(string.Format(ErrorMessages.RequiredUserFullName, "ثبت سفارش"));

        order.OrderNumber = await GenerateUniqueOrderNumberAsync();
        
        await orderRepository.InsertAsync(order);
        await orderRepository.SaveChangesAsync();

        var type = new List<OrderTypeMapping>();
        foreach (var typeId in model.OrderTypeIds)
        {
            type.Add(new()
            {
                OrderTypeId = typeId,
                OrderId = order.Id,
            });
        }

        await orderTypeMappingRepository.InsertRangeAsync(type);
        await orderTypeMappingRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.SubmitOrderSuccessfullyDone);
    }

    #endregion

    #region GetByIdAsync

    public async Task<Result<ClientOrderDetailsViewModel>> GetByIdAsync(int id, int userId)
    {
        if (id < 1 || userId < 1)
            return Result.Failure<ClientOrderDetailsViewModel>(ErrorMessages.BadRequestError);

        string[] includes =
        [
            nameof(Order.User),
            $"{nameof(Order.OrderTypeMapping)}.{nameof(OrderTypeMapping.OrderType)}"
        ];

        var order = await orderRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted && x.UserId == userId, includes: includes);

        if (order is null)
            return Result.Failure<ClientOrderDetailsViewModel>(ErrorMessages.NotFoundError);

        return order.MapToClientOrderDetailsViewModel();
    }

    #endregion

    #region GenerateUniqueOrderNumberAsync - PRIVATE

    private async Task<string> GenerateUniqueOrderNumberAsync()
    {
        string code;
        bool exists;
        do
        {
            var random = new Random();
            code = "BN-" + random.Next(10000, 99999);

            exists = await orderRepository.AnyAsync(x => x.OrderNumber == code);
        } while (exists);
        return code;
    }
    
    #endregion

    #endregion
}