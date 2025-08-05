using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Role;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.RoleManagement)]
public class RoleController(IRoleService roleService, IPermissionService permissionService) : AdminBaseController
{
    #region Role

    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.RoleList)]
    public async Task<IActionResult> List(AdminFilterRolesViewModel filter)
    {
        var result = await roleService.FilterAsync(filter);
        return AjaxSubstitutionResult(filter, result);
    }

    #endregion

    #region Create

    [HttpGet]
    [InvokePermission(PermissionsName.CreateRole)]
    public IActionResult Create() =>
        PartialView("_CreatePartial", new AdminCreateRoleViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.CreateRole)]
    public async Task<IActionResult> Create(AdminCreateRoleViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetModelErrorsAsString());

        var result = await roleService.CreateAsync(viewModel);

        return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
    }

    #endregion

    #region Update

    [HttpGet]
    [InvokePermission(PermissionsName.UpdateRole)]
    public async Task<IActionResult> Update(short id)
    {
        var result = await roleService.FillModelForUpdateAsync(id);
        return result.IsSuccess ? PartialView("_UpdatePartial", result.Value) : BadRequest(result.Message);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.UpdateRole)]
    public async Task<IActionResult> Update(AdminUpdateRoleViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetModelErrorsAsString());

        var result = await roleService.UpdateAsync(viewModel);

        return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
    }

    #endregion

    #region DeleteOrRecover

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteOrRecoverRole)]
    public async Task<IActionResult> DeleteOrRecover(short id)
    {
        var result = await roleService.DeleteOrRecoverAsync(id);
        return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
    }

    #endregion

    #endregion

    #region RolePermission

    #region SetPermissionToRole

    [HttpGet]
    [InvokePermission(PermissionsName.SetPermissionToRole)]
    public async Task<IActionResult> SetPermissionToRole(short id)  
    {
        var permissions = await permissionService.GetPermissionsAsync();
        var selectedPermission = await roleService.GetRoleSelectedPermissionAsync(id);
        return View(new AdminSetPermissionToRoleViewModel{ RoleId = id, Permissions = permissions, SelectedPermissionIds = selectedPermission });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.SetPermissionToRole)]
    public async Task<IActionResult> SetPermissionToRole(AdminSetPermissionToRoleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }
        
        var result = await roleService.SetPermissionToRoleAsync(model);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return View(model);
        }

        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction(nameof(List));
    }
    
    #endregion

    #endregion

    #region UserRole

    #region SetUserToRole
    
    [HttpGet]
    [InvokePermission(PermissionsName.SetUserToRole)]
    public async Task<IActionResult> SetRoleToUser(AdminFilterRolesViewModel filter, int userId)
    {
        var result = await roleService.FilterAsync(filter);
        
        ViewBag.UserId = userId;
        ViewBag.RoleIds = await roleService.GetRoleIdsJoinByCommaAsync(userId);
        
        return PartialView(result);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.SetUserToRole)]
    public async Task<IActionResult> SetRoleToUser(AdminSetRoleToUserViewModel model)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState.GetModelErrorsAsString());

        var result = await roleService.SetRoleToUserAsync(model);
        
        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    } 

    #endregion

    #endregion
}