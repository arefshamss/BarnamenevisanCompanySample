using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.ViewComponents;

public class TicketListViewComponent(ITicketService ticketService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int userId)
    {
        var result = await ticketService.FilterAsync(new AdminFilterTicketsViewModel()
        {
            UserId = userId,
            FormId = "filter-tickets",
        });
        
        return View("TicketList", result.Value);
    }
}