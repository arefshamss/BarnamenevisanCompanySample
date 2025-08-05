using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Web.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Controllers;

public class HonorController(IHonorsService honorsService) : SiteBaseController
{
    #region Honors

    [Route(RoutingExtension.Site.Honor.List)]
    public async Task<IActionResult> List() 
    {
        var model = await honorsService.GetAllAsync();
        return View(model.Value);
    }

    #endregion

    #region HonorsDetail

    [HttpGet(RoutingExtension.Site.Honor.Detail)]
    public async Task<IActionResult> Detail(string slug)    
    {
        var model = await honorsService.GetHonorDetailAsync(slug);

        if (model.IsFailure)
        {
            ShowToasterErrorMessage(model.Message);
            return RedirectToAction(nameof(List));
        }

        return View(model.Value);
    }

    #endregion
}