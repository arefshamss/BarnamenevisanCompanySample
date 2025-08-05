using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Consult;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.ViewComponents;

public class ConsultListViewComponent(IConsultService consultService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int userId)
    {
        var result = await consultService.FilterAsync(new AdminFilterConsultViewModel()
        {
            UserId = userId,
            FormId = "filter-consults",
        });
        
        return View("ConsultList", result);
    }
}