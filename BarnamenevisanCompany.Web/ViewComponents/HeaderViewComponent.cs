using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Client.User;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.ViewComponents;

public class HeaderViewComponent(
    ISiteSettingService siteSettingService,
    IUserService userService,
    IJobService jobService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        ClientUserViewModel user = new();

        var siteLogoImageName = await siteSettingService.SiteLogoAsync();

        if (User.Identity.IsAuthenticated)
            user = (await userService.GetByIdForUserPanelAsync(User.GetUserId())).Value;

        ViewData["SiteLogo"] = siteLogoImageName.Value;
        ViewData["IsActivejobExists"] = await jobService.IsActiveJobExistsAsync();

        return View("Header", user);
    }
}