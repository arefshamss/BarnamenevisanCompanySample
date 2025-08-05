using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.User;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.UserManagement)]
public class UserController(IUserService userService) : AdminBaseController
{
    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.UserList)]
    public async Task<IActionResult> List(AdminFilterUsersViewModel filter)
    {
        var result = await userService.FilterAsync(filter);
        return View(result.Value);
    }

    [HttpGet]
    [InvokePermission(PermissionsName.UserList)]
    public async Task<IActionResult> ListPartial(AdminFilterUsersViewModel filter)
    {
        filter.TakeEntity = 5;
        var result = await userService.FilterAsync(filter);
        result.Value.FormId = "filter-users";
        return PartialView("_ListPartial", result.Value);
    }

    #endregion

    #region Detail

    [HttpGet]
    [InvokePermission(PermissionsName.UserDetails)]
    public async Task<IActionResult> Detail(int id)
    {
        var result = await userService.GetByIdAsync(id);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(List));
        }

        return View(result.Value);
    }

    #endregion

    #region Create

    [HttpGet]
    [InvokePermission(PermissionsName.CreateUser)]
    public IActionResult Create() =>
        View();


    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.CreateUser)]
    public async Task<IActionResult> Create(AdminCreateUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        var result = await userService.CreateAsync(model);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return View(model);
        }

        ShowToasterSuccessMessage(result.Message);

        return RedirectToAction(nameof(List));
    }

    #endregion

    #region Update

    [HttpGet]
    [InvokePermission(PermissionsName.UpdateUser)]
    public async Task<IActionResult> Update(int id)
    {
        var result = await userService.FillModelForUpdateAsync(id);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(List));
        }

        return View(result.Value);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.UpdateUser)]
    public async Task<IActionResult> Update(AdminUpdateUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        var result = await userService.UpdateAsync(model);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return View(model);
        }

        ShowToasterSuccessMessage(result.Message);

        return RedirectToAction(nameof(List));
    }

    #endregion

    #region DeleteOrRecover

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteOrRecoverUser)]
    public async Task<IActionResult> DeleteOrRecover(int id)
    {
        var result = await userService.DeleteOrRecoverAsync(id);
        return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
    }

    #endregion
}