using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Web.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Controllers;

public class DynamicPageController(
    IDynamicPageService dynamicPageService
) : SiteBaseController
{
    [HttpGet(RoutingExtension.Site.DynamicPage)]
    public async Task<IActionResult> Page(string slug)  
    {
        var result = await dynamicPageService.GetBySlugAsync(slug);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToRefererUrl();
        }

        return View(result.Value);
    }
}