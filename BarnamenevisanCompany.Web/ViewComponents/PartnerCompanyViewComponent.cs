using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Client.PartnerCompany;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.ViewComponents;

public class PartnerCompanyViewComponent(IPartnerCompanyService partnerCompanyService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(bool showButton = true)
    {
        var result = await partnerCompanyService.FilterAsync(new ClientFilterPartnerCompanyViewModel());
        ViewBag.ShowButton = showButton;
        return View("PartnerCompany", result.Value);
    }
}