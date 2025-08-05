using BarnamenevisanCompany.Domain.Enums.Order;
using BarnamenevisanCompany.Domain.Models.Order;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Order;
using BarnamenevisanCompany.Domain.ViewModels.Admin.OrderType;
using BarnamenevisanCompany.Domain.ViewModels.Client.Order;
using BarnamenevisanCompany.Domain.ViewModels.Client.OrderType;

namespace BarnamenevisanCompany.Application.Mappers.OrderMappings;

public static class OrderMapper
{
    #region Admin

    public static AdminOrderViewModel MapToAdminOrderViewModel(this Order model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            CustomerName = model.User.FirstName + " " + model.User.LastName,
            OrderStatus = model.OrderStatus,
            CustomerMobile = model.User.Mobile,
            OrderDate = model.CreatedDate,
            IsDeleted = model.IsDeleted,
            OrderTypes = model.OrderTypeMapping.Select(x => x.MapToAdminOrderTypeDetailsViewModel()).ToList(),
        };
    
    public static AdminOrderTypeDetailsViewModel MapToAdminOrderTypeDetailsViewModel(this OrderTypeMapping model) =>
        new()
        {
            Title = model.OrderType.Title,
            ImageUrl = model.OrderType.ImageUrl,
        };
    
    public static AdminOrderDetailsViewModel MapToAdminOrderDetailsViewModel(this Order model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            CustomerName = model.User.FirstName + " " + model.User.LastName,
            OrderStatus = model.OrderStatus,
            CustomerMobile = model.User.Mobile,
            OrderDate = model.CreatedDate,
            Description = model.Description,
            Attachment = model.Attachment,
            Answer = model.Answer,
            OrderTypes = model.OrderTypeMapping.Select(x => x.MapToAdminOrderTypeDetailsViewModel()).ToList(),
            OrderNumber = model.OrderNumber,
        };
    
    public static void MapToOrder(this Order model, AdminAnswerToOrderViewModel viewModel)
    {
        model.Answer = viewModel.Answer;
        model.OrderStatus = OrderStatus.Done;
    }

    #endregion

    #region Client

    public static Order MapToOrder(this ClientCreateOrderViewModel model) =>
        new()
        {
            Title = model.Title,
            Description = model.Description,
            UserId = model.UserId,
            OrderStatus = OrderStatus.InProgress,
        };
    
    public static ClientOrderViewModel MapToClientOrderViewModel(this Order model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            OrderStatus = model.OrderStatus,
            OrderDate = model.CreatedDate,
            OrderTypes = model.OrderTypeMapping.Select(x => x.MapToClientOrderTypeDetailsViewModel()).ToList(),
            UserId = model.User.Id,
            OrderNumber = model.OrderNumber
        };
    
    public static ClientOrderTypeDetailsViewModel MapToClientOrderTypeDetailsViewModel(this OrderTypeMapping model) =>
        new()
        {
            Id = model.OrderType.Id,
            Title = model.OrderType.Title,
            ImageUrl = model.OrderType.ImageUrl,
            Description = model.OrderType.Description,
        };
    
    public static ClientOrderDetailsViewModel MapToClientOrderDetailsViewModel(this Order model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            OrderStatus = model.OrderStatus,
            OrderDate = model.CreatedDate,
            Description = model.Description,
            Attachment = model.Attachment,
            Answer = model.Answer,
            OrderTypes = model.OrderTypeMapping.Select(x => x.MapToClientOrderTypeDetailsViewModel()).ToList(),
            OrderNumber = model.OrderNumber
        };

    #endregion
}