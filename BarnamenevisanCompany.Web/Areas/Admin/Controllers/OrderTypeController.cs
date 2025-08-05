using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.OrderType;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.OrderTypeManagement)]
public class OrderTypeController(
    IOrderTypeService orderTypeService
) : AdminBaseController
{
    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.OrderTypeList)]
    public async Task<IActionResult> List(AdminFilterOrderTypeViewModel filter)
    {
        var result = await orderTypeService.FilterAsync(filter);

        return View(result.Value);
    }

    #endregion

    #region Create

    [HttpGet]
    [InvokePermission(PermissionsName.CreateOrderType)]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.CreateOrderType)]
    public async Task<IActionResult> Create(AdminCreateOrderTypeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        var result = await orderTypeService.CreateAsync(model);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return View(model);
        }

        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction(nameof(List), new { area = "Admin" });
    }

    #endregion

    #region Update

    [HttpGet]
    [InvokePermission(PermissionsName.UpdateOrderType)]
    public async Task<IActionResult> Update(short id, int? page)
    {
        var result = await orderTypeService.FillModelForUpdateAsync(id);
        ViewBag.Page = page;

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(List), new { area = "Admin" });
        }

        return View(result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.UpdateOrderType)]
    public async Task<IActionResult> Update(AdminUpdateOrderTypeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        var result = await orderTypeService.UpdateAsync(model);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return View(model);
        }

        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction(nameof(List), new { area = "Admin" });
    }

    #endregion

    #region DeleteOrRecover

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteOrRecoverOrderType)]
    public async Task<IActionResult> DeleteOrRecover(short id)
    {
        var result = await orderTypeService.DeleteOrRecoverAsync(id);
        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion
}