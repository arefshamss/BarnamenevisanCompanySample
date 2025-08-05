using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Client.OurService;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.ViewComponents;

public class ServiceViewComponent(IOurServiceService ourServiceService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await ourServiceService.GetAllServiceAsync(new ClientFilterServiceViewModel());
        return View("Service", result.Value);
    }
}