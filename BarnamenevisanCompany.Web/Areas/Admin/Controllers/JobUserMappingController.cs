using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Admin.JobUserMapping;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.JobUserMappingManagement)]
public class JobUserMappingController(IJobUserMappingService jobUserMappingService) : AdminBaseController
{
    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.JobUserMappingList)]
    public async Task<IActionResult> List(AdminFilterJobUserMappingViewModel filter)
    {
        var result = await jobUserMappingService.FilterAsync(filter);

        return View(result.Value);
    }

    #endregion

    #region Details

    [HttpGet]
    [InvokePermission(PermissionsName.JobUserMappingDetails)]
    public async Task<IActionResult> Details(int id, int? page)
    {
        var result = await jobUserMappingService.GetByIdAsync(id);

        ViewBag.Page = page;

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(List), new { area = "Admin" });
        }

        return View(result.Value);
    }

    #endregion

    #region DeleteOrRecover

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteOrRecoverJobUserMapping)]
    public async Task<IActionResult> DeleteOrRecover(int id)
    {
        var result = await jobUserMappingService.DeleteOrRecoverAsync(id);

        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion
}