using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Client.Ticket;
using BarnamenevisanCompany.Domain.ViewModels.Client.TicketMessage;
using BarnamenevisanCompany.Web.Areas.UserPanel.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.UserPanel.Controllers;

public class TicketController(
    ITicketService ticketService,
    ITicketMessageService ticketMessageService,
    ITicketPriorityService ticketPriorityService) : UserPanelBaseController
{
    #region Ticket

    #region List

    [HttpGet(RoutingExtension.UserPanel.Ticket.List)]
    public async Task<IActionResult> List(ClientFilterTicketsViewModel filter)
    {
        filter.UserId = User.GetUserId();
        var result = (await ticketService.FilterAsync(filter)).Value;
        return View(result);
    }

    #endregion

    #region Detail

    [HttpGet(RoutingExtension.UserPanel.Ticket.Detail)]
    public async Task<IActionResult> Detail(int id)
    {
        var result = await ticketService.GetByIdForUserPanelAsync(id);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
        }

        return View(result.Value);
    }

    #endregion

    #region Create

    [HttpGet(RoutingExtension.UserPanel.Ticket.Create)]
    public async Task<IActionResult> Create()
    {
        ViewData["Priorities"] = (await ticketPriorityService.GetSelectListAsync()).Value;

        return View();
    }

    [HttpPost(RoutingExtension.UserPanel.Ticket.Create)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClientCreateTicketViewModel model)
    {
        model.Message.SenderId = User.GetUserId();
        model.UserId = User.GetUserId();
        
        if (!ModelState.IsValid)
        {
            ViewData["Priorities"] = (await ticketPriorityService.GetSelectListAsync()).Value;
            return View(model);
        }

        var result = await ticketService.CreateAsync(model);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return View(model);
        }
        
        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction(nameof(List));
    }

    #endregion

    #endregion

    #region TicketMessage

    #region Create

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateMessage(ClientCreateTicketMessageViewModel model)
    {
        model.SenderId = User.GetUserId();
        
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(model.Message);
            return BadRequest(ModelState.GetModelErrorsAsString());
        }

        var result = await ticketMessageService.CreateAsync(model);

        if (result.IsFailure)
            return BadRequest(result.Message);
        
        ShowToasterSuccessMessage(result.Message);
        return Ok();
    }

    #endregion

    #endregion
}