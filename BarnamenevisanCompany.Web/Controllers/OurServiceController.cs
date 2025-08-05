using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Models.OurServices;
using BarnamenevisanCompany.Domain.ViewModels.Client.OurService;
using BarnamenevisanCompany.Web.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Controllers;

public class OurServiceController(
    IOurServiceService ourServiceService,
    ISiteSettingService siteSettingService) : SiteBaseController
{
    #region List

    [HttpGet(RoutingExtension.Site.OurServices.List)]
    public async Task<IActionResult> List(ClientFilterServiceViewModel model)
    {
        model.TakeEntity = 9;
        var result = await ourServiceService.GetAllServiceAsync(model);

        ViewData["SiteSettings"] = (await siteSettingService.FillModelForUpdateAsync(1)).Value;

        return View(result.Value);
    }

    #endregion

    #region Detail

    [HttpGet(RoutingExtension.Site.OurServices.Detail)]
    public async Task<IActionResult> Detail(string slug)
    {
        var model = await ourServiceService.FillModelForShow(slug);
        return View(model.Value);
    }

    #endregion
}