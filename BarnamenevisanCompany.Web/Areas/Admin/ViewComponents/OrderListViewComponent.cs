using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Order;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.ViewComponents;

public class OrderListViewComponent(IOrderService orderService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int userId)
    {
        var result = await orderService.FilterAsync(new AdminFilterOrderViewModel()
        {
            UserId = userId,
            FormId = "filter-orders",
        });

        return View("OrderList", result.Value);
    }
}