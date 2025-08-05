using BarnamenevisanCompany.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.ViewComponents;

public class AdminSiteLogoViewComponent(ISiteSettingService siteSettingService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(bool isMobile = false)
    {
        var siteLogo = await siteSettingService.SiteLogoAsync();
        ViewData["SiteLogo"] = siteLogo.Value;
        ViewBag.IsMobile = isMobile;
        return View("AdminSiteLogo");
    }
}