using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.OurService;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.OurServiceManagement)]
public class OurServiceController(IOurServiceService ourServiceService) : AdminBaseController
{
    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.OurServiceList)]
    public async Task<IActionResult> List(AdminFilterOurServiceViewModel filter)
    {
        var model = await ourServiceService.FilterAsync(filter);
        return View(model);
    }

    #endregion

    #region Create

    [HttpGet]
    [InvokePermission(PermissionsName.CreateOurService)]
    public IActionResult Create() =>
        View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.CreateOurService)]
    public async Task<IActionResult> Create(AdminCreateOurServiceViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        var result = await ourServiceService.CreateAsync(model);
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
    [InvokePermission(PermissionsName.UpdateOurService)]
    public async Task<IActionResult> Update(short id)
    {
        var result = await ourServiceService.FillModelForUpdateAsync(id);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(List), new { area = "Admin" });
        }

        return View(result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.UpdateOurService)]
    public async Task<IActionResult> Update(AdminUpdateOurServiceViewModel model)
    {
        #region Validation

        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        #endregion

        var result = await ourServiceService.UpdateAsync(model);

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
    [InvokePermission(PermissionsName.DeleteOrRecoverOurService)]
    public async Task<IActionResult> DeleteOrRecover(short id)
    {
        var result = await ourServiceService.DeleteOrRecoverAsync(id);
        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion
}