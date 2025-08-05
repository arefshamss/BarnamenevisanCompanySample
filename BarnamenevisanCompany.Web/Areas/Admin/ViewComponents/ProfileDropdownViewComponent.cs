using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.ViewComponents;

public class ProfileDropdownViewComponent(IUserService userService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = (await userService.GetByIdAsync(User.GetUserId())).Value;
        return View("~/Areas/Admin/Views/Shared/Components/ProfileDropdown/ProfileDropdown.cshtml", user);
    }
}