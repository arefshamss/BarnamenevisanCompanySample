using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Web.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Controllers;

public class AboutUsController(
    IAboutUsService aboutUsService,
    IAboutUsCommentService aboutUsCommentService)
    : SiteBaseController
{
    [HttpGet(RoutingExtension.Site.AboutUs)]
    public async Task<IActionResult> AboutUs()
    {
        var result = await aboutUsService.FillModelForShow(1);
        var comments = await aboutUsCommentService.GetAllCommentsAsync();
        ViewData["Comments"] = comments.Value;
        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction("Index", "Home");
        }


        return View(result.Value);
    }
}