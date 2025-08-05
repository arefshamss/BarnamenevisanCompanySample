using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.SocialNetwork;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.SocialNetworkManagement)]
public class SocialNetworkController(ISocialNetworkService socialNetworkService) : AdminBaseController
{
    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.SocialNetworkList)]
    public async Task<IActionResult> List(AdminFilterSocialNetworkViewModel filter)
    {
        var result = await socialNetworkService.FilterAsync(filter);
        return AjaxSubstitutionResult(filter, result);
    }

    #endregion

    #region Create

    [HttpGet]
    [InvokePermission(PermissionsName.CreateSocialNetwork)]
    public IActionResult Create() =>
        PartialView("_Create");

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.CreateSocialNetwork)]
    public async Task<IActionResult> Create(AdminCreateSocialNetworkViewModel model)
    {
        #region Validation

        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetModelErrorsAsString());

        #endregion

        var result = await socialNetworkService.CreateAsync(model);
        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region Update

    [HttpGet]
    [InvokePermission(PermissionsName.UpdateSocialNetwork)]
    public async Task<IActionResult> Update(byte id)
    {
        var result = await socialNetworkService.FillModelForUpdateAsync(id);
        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(List));
        }

        return PartialView("_Update", result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.UpdateSocialNetwork)]
    public async Task<IActionResult> Update(AdminSocialNetworkUpdateViewModel model)
    {
        #region Validation

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetModelErrorsAsString());
        }

        #endregion

        var result = await socialNetworkService.UpdateAsync(model);
        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region Delete

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteOrRecoverSocialNetwork)]
    public async Task<IActionResult> Delete(byte id)
    {
        var result = await socialNetworkService.DeleteAsync(id);
        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion
}