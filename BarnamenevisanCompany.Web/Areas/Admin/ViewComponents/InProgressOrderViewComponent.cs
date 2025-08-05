using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.Order;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Order;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.ViewComponents;

public class InProgressOrderViewComponent(IOrderService orderService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await orderService.FilterAsync(new AdminFilterOrderViewModel()
        {
            OrderStatus = FilterOrderStatus.InProgress,
            DeleteStatus = DeleteStatus.NotDeleted,
            TakeEntity = 10
        });
        return View("InProgressOrder", result.Value);
    }
}