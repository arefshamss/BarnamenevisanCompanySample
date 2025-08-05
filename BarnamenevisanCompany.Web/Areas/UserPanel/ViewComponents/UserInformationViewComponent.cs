using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.UserPanel.ViewComponents;

public class UserInformationViewComponent(IUserService userService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await userService.GetByIdForUserPanelAsync(User.GetUserId());
        return View("UserInformation", result.Value);
    }
}