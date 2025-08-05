using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.AboutUs;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.UpdateAboutUs)]
public class AboutUsController(IAboutUsService aboutUsService) : AdminBaseController
{
    #region AboutUsInformation

    [HttpGet]
    public async Task<IActionResult> AboutUsInformation()
    {
        var model = await aboutUsService.FillModelForUpdateAsync(1);

        if (model.IsFailure)
        {
            ShowToasterErrorMessage(model.Message);
            return View();
        }

        return View(model.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AboutUsInformation(AdminUpdateAboutUsViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        var result = await aboutUsService.UpdateAsync(model);
        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return View(model);
        }

        ShowToasterSuccessMessage(result.Message);
        return View(model);
    }

    #endregion
}