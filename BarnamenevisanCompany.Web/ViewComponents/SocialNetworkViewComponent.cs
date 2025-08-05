using BarnamenevisanCompany.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.ViewComponents;

public class SocialNetworkViewComponent(ISocialNetworkService socialNetworkService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await socialNetworkService.GetAllAsync();
        return View("SocialNetwork", model.Value);
    }
}