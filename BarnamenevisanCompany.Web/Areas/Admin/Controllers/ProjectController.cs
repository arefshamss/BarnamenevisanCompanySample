using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Project;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.ProjectManagement)]
public class ProjectController(
    IProjectService projectService,
    IProjectGalleryService projectGalleryService) : AdminBaseController
{
    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.ProjectList)]
    public async Task<IActionResult> List(AdminFilterProjectViewModel filter)
    {
        var model = await projectService.FilterAsync(filter);
        return View(model);
    }

    #endregion

    #region Create

    [HttpGet]
    [InvokePermission(PermissionsName.CreateProject)]
    public IActionResult Create() =>
        View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.CreateProject)]
    public async Task<IActionResult> Create(AdminCreateProjectViewModel model)
    {
        #region Validation

        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        #endregion

        var result = await projectService.CreateAsync(model);
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
    [InvokePermission(PermissionsName.UpdateProject)]
    public async Task<IActionResult> Update(short id)
    {
        var result = await projectService.FillModelForUpdateAsync(id);
        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(List), new { area = "Admin" });
        }


        return View(result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.UpdateProject)]
    public async Task<IActionResult> Update(AdminUpdateProjectViewModel model)
    {
        #region Validation

        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        #endregion

        var result = await projectService.UpdateAsync(model);
        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return View(model);
        }

        ShowToasterSuccessMessage(result.Message);

        return RedirectToAction(nameof(List), new { area = "Admin" });
    }

    #endregion

    #region DeleteOrRecoverAsync

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteOrRecoverProject)]
    public async Task<IActionResult> DeleteOrRecover(short id)
    {
        var result = await projectService.DeleteOrRecoverAsync(id);

        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region ProjectGallery

    [HttpGet]
    [InvokePermission(PermissionsName.ProjectGallery)]
    public async Task<IActionResult> ProjectGallery(short id)
    {
        var result = await projectGalleryService.FillModelForProjectGalleryAsync(id);
        ViewBag.ProjectId = id;

        return result.IsFailure ? BadRequest(result.Message) : View(result.Value);
    }

    #endregion

    #region AddToGallery

    [HttpPost]
    [InvokePermission(PermissionsName.AddImageToProjectGallery)]
    public async Task<IActionResult> AddToGallery([FromForm] short projectId, IFormFile image)
    {
        var result = await projectGalleryService.CreateAsync(projectId, image);
        return Json(result.Value);
    }

    #endregion

    #region DeleteGalleryImage

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteImageFromProjectGallery)]
    public async Task<IActionResult> DeleteFromGalleryImage(short projectId, string imageName)
    {
        var result = await projectGalleryService.DeleteAsync(projectId, imageName);

        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region DeleteTeaser

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteProjectTeaser)]
    public async Task<IActionResult> DeleteTeaser(short projectId)
    {
        var result = await projectService.DeleteTeaserAsync(projectId);

        if (result.IsFailure)
            ShowToasterErrorMessage(result.Message);
        else
            ShowToasterSuccessMessage(result.Message);

        return RedirectToAction(nameof(Update), new { area = "Admin", id = projectId });
    }

    #endregion
}