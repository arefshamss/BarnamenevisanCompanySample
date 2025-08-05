using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.Ticket;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.ViewComponents;

public class InProgressTicketViewComponent(ITicketService ticketService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await ticketService.FilterAsync(new AdminFilterTicketsViewModel
        {
            TicketStatus = FilterByTicketStatus.InProgress,
            DeleteStatus = DeleteStatus.NotDeleted,
            TakeEntity = 10,
        });
        return View("InProgressTicket", result.Value);
    }
}