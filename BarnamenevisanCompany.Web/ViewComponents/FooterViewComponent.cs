using BarnamenevisanCompany.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.ViewComponents;

public class FooterViewComponent(ISiteSettingService siteSettingService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await siteSettingService.FillModelForUpdateAsync(1);
        return View("Footer", model.Value);
    }
}