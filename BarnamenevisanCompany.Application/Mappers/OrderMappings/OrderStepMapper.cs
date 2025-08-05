using BarnamenevisanCompany.Domain.Models.Order;
using BarnamenevisanCompany.Domain.ViewModels.Admin.OrderStep;
using BarnamenevisanCompany.Domain.ViewModels.Client.OrderStep;

namespace BarnamenevisanCompany.Application.Mappers.OrderMappings;

public static class OrderStepMapper
{
    public static OrderStep MapToOrderStep(this AdminCreateOrderStepViewModel model) =>
        new()
        {
            Title = model.Title,
            PriorityId = model.PriorityId,
            CreatedDate = DateTime.Now,
        };

    public static AdminUpdateOrderStepViewModel MapToAdminUpdateOrderStepViewModel(this OrderStep model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            PriorityId = model.PriorityId,
        };

    public static void MapToOrderStep(this OrderStep model, AdminUpdateOrderStepViewModel viewModel)
    {
        model.Title = viewModel.Title;
        model.PriorityId = viewModel.PriorityId;
        model.Id = viewModel.Id;
    }

    public static AdminOrderStepViewModel MapToAdminOrderStepViewModel(this OrderStep model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            PriorityId = model.PriorityId,
            IsDeleted = model.IsDeleted,
            CreateDate = model.CreatedDate,
        };

    public static ClientOrderStepViewModel MapToClientOrderStepViewModel(this OrderStep model) =>
        new()
        {
            Title = model.Title,
            PriorityId = model.PriorityId,
        };
}