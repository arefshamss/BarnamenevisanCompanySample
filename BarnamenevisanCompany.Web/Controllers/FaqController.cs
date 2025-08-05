using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Client.Faq;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Controllers;

public class FaqController(
    IFaqService faqService
) : Controller
{
    #region List

    [HttpGet(RoutingExtension.Site.Faq)]
    public async Task<IActionResult> List(ClientFilterFaqViewModel filter)  
    {
        var result = await faqService.FilterAsync(filter);
        return View(result.Value);
    }

    #endregion
}