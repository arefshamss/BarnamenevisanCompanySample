using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Client.Order;
using BarnamenevisanCompany.Web.Areas.UserPanel.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.UserPanel.Controllers;

public class OrderController(
    IOrderService orderService,
    IOrderTypeService orderTypeService,
    ICaptchaValidator captchaValidator
) : UserPanelBaseController
{

    #region List

    [HttpGet(RoutingExtension.UserPanel.Order.List)]
    public async Task<IActionResult> List(ClientFilterOrderViewModel filter)
    {
        filter.UserId = User.GetUserId();
        var result = await orderService.FilterAsync(filter);
        return View(result.Value);
    }

    #endregion

    #region Create

    [HttpGet(RoutingExtension.UserPanel.Order.Create)]
    public async Task<IActionResult> Create()
    {
        ViewData["Types"] = await orderTypeService.GetAllAsync();
        return View();
    }

    [HttpPost(RoutingExtension.UserPanel.Order.Create), ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClientCreateOrderViewModel model)
    {
        model.UserId = User.GetUserId();
        if (!await captchaValidator.IsCaptchaPassedAsync(model.Captcha))
        {
            ShowToasterErrorMessage(ErrorMessages.CaptchaError);
            return View(model);
        }

        if (!ModelState.IsValid)
        {
            ViewData["Types"] = await orderTypeService.GetAllAsync();
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        var result = await orderService.CreateAsync(model);

        if (result.IsFailure)
        {
            ViewData["Types"] = await orderTypeService.GetAllAsync();
            ShowToasterErrorMessage(result.Message);
            return View(model);
        }

        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction(nameof(List), new { area = "UserPanel" });
    }

    #endregion

    #region Details

    [HttpGet(RoutingExtension.UserPanel.Order.Detail)]
    public async Task<IActionResult> Details(int id, int? page)
    {
        var userId = User.GetUserId();

        var result = await orderService.GetByIdAsync(id, userId);

        ViewBag.Page = page;

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(List), new { area = "UserPanel" });
        }

        return View(result.Value);
    }

    #endregion
}