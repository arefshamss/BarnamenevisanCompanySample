using BarnamenevisanCompany.Domain.Enums.Order;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Order;
using BarnamenevisanCompany.Domain.ViewModels.Client.Order;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IOrderService
{
    #region Admin

    Task<Result<AdminFilterOrderViewModel>> FilterAsync(AdminFilterOrderViewModel filter);

    Task<Result> DeleteOrRecoverAsync(int id);

    Task<Result<AdminOrderDetailsViewModel>> GetByIdAsync(int id);

    Task<Result> AnswerToOrderAsync(AdminAnswerToOrderViewModel model);

    Task<Result> UpdateOrderStatusAsync(int orderId, OrderStatus status);

    #endregion

    #region Client

    Task<Result<ClientFilterOrderViewModel>> FilterAsync(ClientFilterOrderViewModel filter);

    Task<Result> CreateAsync(ClientCreateOrderViewModel model);

    Task<Result<ClientOrderDetailsViewModel>> GetByIdAsync(int id, int userId);

    #endregion
}