using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Client.Consult;
using BarnamenevisanCompany.Web.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Controllers;

public class ConsultController(
    IConsultService consultService,
    IUserService userService,
    ICaptchaValidator captchaValidator) : SiteBaseController
{
    #region Create

    [HttpGet(RoutingExtension.Site.Consult.Create)]
    public async Task<IActionResult> Create(string? title = null)
    {
        var information = await consultService.GetPageInformation(1);

        ViewData["PageInformation"] = information.Value;

        ClientCreateConsultViewModel model = new();
        if (!title.IsNullOrEmptyOrWhiteSpace())
            model.Title = title;

        if (User.Identity!.IsAuthenticated)
        {
            var user = (await userService.GetByIdForUserPanelAsync(User.GetUserId())).Value;
            model.FirstName = user.FirstName ?? string.Empty;
            model.LastName = user.LastName ?? string.Empty;
            model.Mobile = user.Mobile;
        }

        return View(model);
    }

    [HttpPost(RoutingExtension.Site.Consult.Create)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClientCreateConsultViewModel model)
    {
        if (!await captchaValidator.IsCaptchaPassedAsync(model.Captcha))
        {
            ShowToasterErrorMessage(ErrorMessages.CaptchaError);
            return View(model);
        }

        model.UserId = User.GetUserId();

        #region Validation

        if (!ModelState.IsValid)
        {
            ViewData["PageInformation"] = await consultService.GetPageInformation(1);
            return View(model);
        }

        #endregion

        var result = await consultService.CreateAsync(model);
        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return View(model);
        }

        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction(nameof(Create));
    }

    #endregion
}