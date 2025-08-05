using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Ticket;
using BarnamenevisanCompany.Domain.ViewModels.Admin.TicketMessage;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.TicketManagement)]
public class TicketController(
    ITicketService ticketService,
    ITicketMessageService ticketMessageService,
    ITicketPriorityService ticketPriorityService) : AdminBaseController
{
    #region Ticket

    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.TicketList)]
    public async Task<IActionResult> List(AdminFilterTicketsViewModel filter)
    {
        var result = await ticketService.FilterAsync(filter);
        ViewData["Priorities"] = (await ticketPriorityService.GetSelectListAsync()).Value;

        return AjaxSubstitutionResult(filter, result);
    }
    
    [HttpGet]
    [InvokePermission(PermissionsName.TicketList)]   
    public async Task<IActionResult> ListPartial(AdminFilterTicketsViewModel filter)
    {
        var result = await ticketService.FilterAsync(filter);
        return PartialView("_ListPartial", result.Value);
    }

    #endregion

    #region Detail

    [HttpGet]
    [InvokePermission(PermissionsName.TicketDetails)]
    public async Task<IActionResult> Detail(int id)
    {
        var result = await ticketService.GetByIdAsync(id);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToRefererUrl(nameof(List));
        }

        return View(result.Value);
    }

    #endregion

    #region Create

    [HttpGet]
    [InvokePermission(PermissionsName.CreateTicket)]
    public async Task<IActionResult> Create()
    {
        ViewData["Priorities"] = (await ticketPriorityService.GetSelectListAsync()).Value;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.CreateTicket)]
    public async Task<IActionResult> Create(AdminCreateTicketViewModel model)
    {
        model.TicketMessage.SenderId = User.GetUserId();

        if (!ModelState.IsValid)
        {
            ViewData["Priorities"] = (await ticketPriorityService.GetSelectListAsync()).Value;
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        var result = await ticketService.CreateAsync(model);

        if (result.IsFailure)
        {
            ViewData["Priorities"] = (await ticketPriorityService.GetSelectListAsync()).Value;
            ShowToasterErrorMessage(result.Message);
            return View(model);
        }

        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction(nameof(Detail), new { id = result.Value });
    }

    #endregion

    #region Update

    [HttpGet]
    [InvokePermission(PermissionsName.UpdateTicket)]
    public async Task<IActionResult> Update(int id)
    {
        var result = await ticketService.FillModelForUpdateAsync(id);

        if (result.IsFailure)
            return BadRequest(result.Message);

        ViewData["Priorities"] = (await ticketPriorityService.GetSelectListAsync()).Value;
        return PartialView("_UpdatePartial", result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.UpdateTicket)]
    public async Task<IActionResult> Update(AdminUpdateTicketViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetModelErrorsAsString());

        var result = await ticketService.UpdateAsync(model);

        if (result.IsFailure)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }

    #endregion

    #region DeleteOrRecover

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteOrRecoverTicket)]
    public async Task<IActionResult> DeleteOrRecover(int id)
    {
        var result = await ticketService.DeleteOrRecoverAsync(id);
        return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
    }

    #endregion

    #region Toggle Close

    [HttpGet]
    [InvokePermission(PermissionsName.ToggleCloseTicket)]
    public async Task<IActionResult> ToggleClose(int id)
    {
        var result = await ticketService.ToggleCloseTicketAsync(id, User.GetUserId());

        if (result.IsFailure)
            ShowToasterErrorMessage(result.Message);
        else
            ShowToasterSuccessMessage(result.Message);

        return RedirectToAction(nameof(Detail), new { id = id });
    }

    #endregion

    #endregion

    #region TicketMessage

    #region Create

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.CreateTicketMessage)]
    public async Task<IActionResult> CreateTicketMessage(AdminCreateTicketMessageViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return RedirectToAction(nameof(Detail), new { id = model.TicketId });
        }


        var result = await ticketMessageService.CreateAsync(model);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return RedirectToAction(nameof(Detail), new { id = model.TicketId });
        }

        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction(nameof(Detail), new { id = model.TicketId });
    }

    #endregion

    #endregion
}