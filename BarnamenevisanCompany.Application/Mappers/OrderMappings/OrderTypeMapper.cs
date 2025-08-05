using BarnamenevisanCompany.Domain.Models.Order;
using BarnamenevisanCompany.Domain.ViewModels.Admin.OrderType;
using BarnamenevisanCompany.Domain.ViewModels.Client.OrderType;

namespace BarnamenevisanCompany.Application.Mappers.OrderMappings;

public static class OrderTypeMapper
{
    #region Admin

    public static AdminOrderTypeViewModel MapToAdminOrderTypeViewModel(this OrderType model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            ImageUrl = model.ImageUrl,
            CreatedDate = model.CreatedDate,
            IsDeleted = model.IsDeleted,
        };

    public static OrderType MapToOrderType(this AdminCreateOrderTypeViewModel model) =>
        new()
        {
            Title = model.Title,
            Description = model.Description,
        };

    public static AdminUpdateOrderTypeViewModel MapToAdminUpdateOrderTypeViewModel(this OrderType model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            ImageUrl = model.ImageUrl,
            Description = model.Description,
        };

    public static void UpdateOrderType(this OrderType model, AdminUpdateOrderTypeViewModel viewModel)
    {
        model.Title = viewModel.Title;
        model.Description = viewModel.Description;
        model.ImageUrl = viewModel.ImageUrl;
    }

    #endregion

    #region Client

    public static ClientOrderTypeViewModel MapToClientOrderTypeViewModel(this OrderType model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            ImageUrl = model.ImageUrl
        };

    public static ClientOrderTypeDetailsViewModel MapToClientOrderDetailsViewModel(this OrderType model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            ImageUrl = model.ImageUrl,
            Description = model.Description,
        };

    #endregion
}