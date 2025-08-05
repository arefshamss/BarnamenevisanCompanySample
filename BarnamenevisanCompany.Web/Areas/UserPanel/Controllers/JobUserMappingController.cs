using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Client.JobUserMapping;
using BarnamenevisanCompany.Web.Areas.UserPanel.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.UserPanel.Controllers;

public class JobUserMappingController(IJobUserMappingService jobUserMappingService) : UserPanelBaseController
{
    #region List

    [HttpGet(RoutingExtension.UserPanel.JobUserMapping.List)]
    public async Task<IActionResult> List(ClientFilterJobUserMappingViewModel filter)
    {
        filter.UserId = User.GetUserId();
        var result = await jobUserMappingService.FilterAsync(filter);
        return View(result.Value);
    }

    #endregion
    
    #region Details

    [HttpGet(RoutingExtension.UserPanel.JobUserMapping.Detail)]
    public async Task<IActionResult> Details(int id, int? page)
    {
        var userId = User.GetUserId();

        var result = await jobUserMappingService.GetByIdAsync(id, userId);

        ViewBag.Page = page;

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(List), new { area = "UserPanel" });
        }

        return View(result.Value);
    }

    #endregion
}