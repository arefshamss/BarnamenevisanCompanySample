using BarnamenevisanCompany.Application.Services.Implementations;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.UserPosition;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.UserPositionManagement)]
public class UserPositionController(IUserPositionService userPositionService) : AdminBaseController
{
    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.UserPositionList)]
    public async Task<IActionResult> List(AdminFilterUserPositionViewModel filter)
    {
        var result = await userPositionService.FilterAsync(filter, null);
        return AjaxSubstitutionResult(filter, result);
    }

    [HttpGet]
    [InvokePermission(PermissionsName.UserPositionList)]
    public async Task<IActionResult> ListPartial(AdminFilterUserPositionViewModel filter, short projectId)
    {
        filter.TakeEntity = 5;
        var result = await userPositionService.FilterAsync(filter, projectId);
        result.FormId = "filter-users";
        return PartialView("_ListPartial", result);
    }

    #endregion

    #region Create

    [HttpGet]
    [InvokePermission(PermissionsName.CreateUserPosition)]
    public IActionResult Create(int userId)
    {
        return PartialView("_Create");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.CreateUserPosition)]
    public async Task<IActionResult> Create(AdminCreateUserPositionViewModel model)
    {
        #region Validation

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetModelErrorsAsString());
        }

        #endregion

        var result = await userPositionService.CreateAsync(model);
        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region Update

    [HttpGet]
    [InvokePermission(PermissionsName.UpdateUserPosition)]
    public async Task<IActionResult> Update(short id)
    {
        var result = await userPositionService.FillModelForUpdateAsync(id);
        if (result.IsFailure)
            ShowToasterErrorMessage(result.Message);

        return PartialView("_Update", result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.UpdateUserPosition)]
    public async Task<IActionResult> Update(AdminUpdateUserPositionViewModel model)
    {
        #region Validation

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetModelErrorsAsString());
        }

        #endregion

        var result = await userPositionService.UpdateAsync(model);
        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region Delete

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteOrRecoverUserPosition)]
    public async Task<IActionResult> DeleteOrRecover(short id)
    {
        var result = await userPositionService.DeleteAsync(id);
        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion
}