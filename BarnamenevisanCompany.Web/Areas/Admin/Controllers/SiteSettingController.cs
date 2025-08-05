using BarnamenevisanCompany.Application.Services.Implementations;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.SiteSetting;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.UpdateSiteSettings)]
public class SiteSettingController(ISiteSettingService siteSettingService) : AdminBaseController
{
    [HttpGet]
    public async Task<IActionResult> UpdateSetting()
    {
        var model = await siteSettingService.FillModelForUpdateAsync(1);
        return View(model.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateSetting(AdminSiteSettingUpdateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }
        var result = await siteSettingService.UpdateAsync(model);
        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(UpdateSetting));
        }

        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction(nameof(UpdateSetting));
    }
}