using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Client.Job;
using BarnamenevisanCompany.Web.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Controllers;

public class JobController(
    IJobService jobService,
    ISiteSettingService siteSettingService) : SiteBaseController
{
    #region List

    [HttpGet(RoutingExtension.Site.Job.List)]
    public async Task<IActionResult> List(ClientFilterJobViewModel filter)
    {
        var result = await jobService.FilterAsync(filter);

        ViewData["SiteSettings"] = (await siteSettingService.FillModelForUpdateAsync(1)).Value;

        return View(result.Value);
    }

    #endregion

    #region Detail

    [HttpGet(RoutingExtension.Site.Job.Detail)]
    public async Task<IActionResult> Details(string slug)
    {
        var result = await jobService.GetBySlugAsync(slug);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToRefererUrl();
        }

        return View(result.Value);
    }


    [HttpGet(RoutingExtension.Site.Job.ShortLink)]
    public async Task<IActionResult> ShortLink(int id)
    {
        var result = await jobService.GetSlugByIdAsync(id);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToRefererUrl();
        }

        return RedirectToAction(nameof(Details), new { slug = result.Value });
    }

    #endregion
}