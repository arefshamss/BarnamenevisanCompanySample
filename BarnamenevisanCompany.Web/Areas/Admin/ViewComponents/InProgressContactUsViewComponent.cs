using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.ContactUs;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.ViewComponents;

public class InProgressContactUsViewComponent(IContactUsService contactUsService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await contactUsService.FilterAsync(new()
        {
            TakeEntity = 10,
            DeleteStatus = DeleteStatus.NotDeleted,
            ContactUsStatus = ContactUsStatus.InProcess
        });
        return View("InProgressContactUs", result);
    }
}