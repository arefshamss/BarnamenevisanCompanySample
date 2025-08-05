using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.TicketPriority;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.TicketPriorityManagement)]
public class TicketPriorityController(ITicketPriorityService ticketPriorityService) : AdminBaseController
{
    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.TicketPriorityList)]
    public async Task<IActionResult> List(AdminFilterTicketPrioritiesViewModel filter)
    {
        var result = await ticketPriorityService.FilterAsync(filter);
        return AjaxSubstitutionResult(result.Value, result);
    }

    #endregion

    #region Create

    [HttpGet]
    [InvokePermission(PermissionsName.CreateTicketPriority)]
    public IActionResult Create() =>
        PartialView("_CreatePartial", new AdminCreateTicketPriorityViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.CreateTicketPriority)]
    public async Task<IActionResult> Create(AdminCreateTicketPriorityViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetModelErrorsAsString());

        var result = await ticketPriorityService.CreateAsync(model);

        return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
    }

    #endregion

    #region Update

    [HttpGet]
    [InvokePermission(PermissionsName.UpdateTicketPriority)]
    public async Task<IActionResult> Update(short id)
    {
        var result = await ticketPriorityService.FillModelForUpdateAsync(id);

        if (result.IsFailure)
            return BadRequest(result.Message);

        return PartialView("_UpdatePartial", result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.UpdateTicketPriority)]
    public async Task<IActionResult> Update(AdminUpdateTicketPriorityViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetModelErrorsAsString());

        var result = await ticketPriorityService.UpdateAsync(model);

        return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
    }

    #endregion

    #region DeleteOrRecover

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteOrRecoverTicketPriority)]
    public async Task<IActionResult> DeleteOrRecover(short id)
    {
        var result = await ticketPriorityService.DeleteOrRecoverAsync(id);
        return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
    }

    #endregion
}