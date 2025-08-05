using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Admin.OrderStep;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.OrderStepManagement)]
public class OrderStepController(IOrderStepService orderStepService) : AdminBaseController
{
    #region List

    [InvokePermission(PermissionsName.OrderStepList)]
    [HttpGet]
    public async Task<IActionResult> List(AdminFilterOrderStepViewModel model)
    {
        var result = await orderStepService.FilterAsync(model);
        return AjaxSubstitutionResult(model, result);
    } 

    #endregion

    #region Create

    [InvokePermission(PermissionsName.CreateOrderStep)]
    [HttpGet]
    public IActionResult Create()
    {
        return PartialView("_Create");
    }

    [InvokePermission(PermissionsName.CreateOrderStep)]
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdminCreateOrderStepViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetModelErrorsAsString());
        }

        var result = await orderStepService.CrateAsync(model);
        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region Update

    [InvokePermission(PermissionsName.UpdateOrderStep)]
    [HttpGet]
    public async Task<IActionResult> Update(short id)
    {
        var result = await orderStepService.FillModelForUpdateAsync(id);
        if (result.IsFailure)
            return BadRequest(result.Message);


        return PartialView("_Update", result.Value);
    }

    [InvokePermission(PermissionsName.UpdateOrderStep)]
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(AdminUpdateOrderStepViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetModelErrorsAsString());
        }

        var result = await orderStepService.UpdateAsync(model);


        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region DeleteOrRecoverAsync

    [InvokePermission(PermissionsName.DeleteOrRecoverOrderStep)]
    public async Task<IActionResult> DeleteOrRecoverAsync(short id)
    {
        var result = await orderStepService.DeleteOrRecoverAsync(id);

        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion
}