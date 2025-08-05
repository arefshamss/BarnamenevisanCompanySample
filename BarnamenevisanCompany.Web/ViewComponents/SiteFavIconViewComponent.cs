using BarnamenevisanCompany.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.ViewComponents;

public class SiteFavIconViewComponent(ISiteSettingService siteSettingService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var siteFavIconNameViewModel = await siteSettingService.SiteFavIconAsync();
        ViewData["SiteFavIConName"] = siteFavIconNameViewModel.Value;
        return View("SiteFavIcon");
    }
}