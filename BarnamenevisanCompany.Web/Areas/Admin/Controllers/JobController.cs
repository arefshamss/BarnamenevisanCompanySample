using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Job;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.JobManagement)]
public class JobController
    (
        IJobService jobService
        ): AdminBaseController
{
    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.JobList)]
    public async Task<IActionResult> List(AdminFilterJobViewModel filter)
    {
        var result = await jobService.FilterAsync(filter);

        return View(result.Value);
    }

    #endregion

    #region Create

    [HttpGet]
    [InvokePermission(PermissionsName.CreateJob)]
    public IActionResult Create() =>
        View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.CreateJob)]
    public async Task<IActionResult> Create(AdminCreateJobViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        var result = await jobService.CreateAsync(model);

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
    [InvokePermission(PermissionsName.UpdateJob)]
    public async Task<IActionResult> Update(int id, int? page)
    {
        var result = await jobService.FillModelForUpdateAsync(id);
        ViewBag.Page = page;

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(List), new { area = "Admin" });
        }

        return View(result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.UpdateJob)]
    public async Task<IActionResult> Update(AdminUpdateJobViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        var result = await jobService.UpdateAsync(model);

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
    [InvokePermission(PermissionsName.DeleteOrRecoverJob)]
    public async Task<IActionResult> DeleteOrRecover(int id)
    {
        var result = await jobService.DeleteOrRecoverAsync(id);
        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion
}