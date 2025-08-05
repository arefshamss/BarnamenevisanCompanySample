using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.Consult;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Consult;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.ViewComponents;

public class InProgressConsultViewComponent(IConsultService consultService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await consultService.FilterAsync(new AdminFilterConsultViewModel()
        {
            ConsultStatus = ConsultStatus.InProcess,
            DeleteStatus = DeleteStatus.NotDeleted,
            TakeEntity = 10,
        });
        return View("InProgressConsult", model);
    }
}