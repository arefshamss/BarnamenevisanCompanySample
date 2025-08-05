using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Tools;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Client.ContactUs;
using BarnamenevisanCompany.Web.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Controllers;

public class ContactUsController(
    IContactUsInformationService contactUsInformationService,
    IContactUsService contactUsService,
    ISiteSettingService siteSettingService,
    ICaptchaValidator captchaValidator) : SiteBaseController
{
    #region ContactUs

    [HttpGet(RoutingExtension.Site.ContactUs)]
    public async Task<IActionResult> ContactUs()
    {
        var result = await contactUsInformationService.GetSiteContactUsInformationAsync();
        ViewData["ContactUsHeaderText"] = (await siteSettingService.FillModelForUpdateAsync(1)).Value.ContactUsHeaderText;
        return View(result.Value);
    }

    #endregion

    #region Create

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClientCreateContactUsViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return RedirectToAction(nameof(ContactUs));
        }

        if (!await captchaValidator.IsCaptchaPassedAsync(model.Captcha))
        {
            ShowToasterErrorMessage(ErrorMessages.CaptchaError);
            return RedirectToAction(nameof(ContactUs));

        }

        var result = await contactUsService.CreateAsync(model);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(ContactUs));

        }

        ShowToasterSuccessMessage(result.Message);

        return RedirectToAction(nameof(ContactUs));
    }

    #endregion
}