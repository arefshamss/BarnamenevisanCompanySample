using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Enums.Order;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Order;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.OrderManagement)]
public class OrderController(IOrderService orderService) : AdminBaseController
{
    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.OrderList)]
    public async Task<IActionResult> List(AdminFilterOrderViewModel filter)
    {
        var result = await orderService.FilterAsync(filter);

        return View(result.Value);
    }

    [HttpGet]
    [InvokePermission(PermissionsName.OrderList)]   
    public async Task<IActionResult> ListPartial(AdminFilterOrderViewModel filter)
    {
        var result = await orderService.FilterAsync(filter);
        return PartialView("_ListPartial", result.Value);
    }
    
    #endregion

    #region Details

    [HttpGet]
    [InvokePermission(PermissionsName.AnswerToOrder)]
    public async Task<IActionResult> Details(int id, int? page)
    {
        var result = await orderService.GetByIdAsync(id);

        ViewBag.Page = page;

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(List), new { area = "Admin" });
        }

        return View(result.Value);
    }

    #endregion

    #region DeleteOrRecover

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteOrRecoverOrder)]
    public async Task<IActionResult> DeleteOrRecover(int id)
    {
        var result = await orderService.DeleteOrRecoverAsync(id);
        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region UpdateStatus

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.ChangeOrderStatus)]
    public async Task<IActionResult> UpdateStatus(int orderId, OrderStatus status)
    {
        var result = await orderService.UpdateOrderStatusAsync(orderId, status);

        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region AnswerToOrder

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.AnswerToOrder)]
    public async Task<IActionResult> AnswerToOrder(AdminAnswerToOrderViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return RedirectToAction(nameof(Details), new { area = "Admin", id = model.OrderId });
        }

        var result = await orderService.AnswerToOrderAsync(model);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(Details), new { area = "Admin", id = model.OrderId });
        }

        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction(nameof(List), new { area = "Admin", id = model.OrderId });
    }

    #endregion
}