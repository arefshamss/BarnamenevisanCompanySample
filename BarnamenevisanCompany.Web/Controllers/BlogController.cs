using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Client.Blog;
using BarnamenevisanCompany.Web.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Controllers;

public class BlogController(
    IBlogService blogService,
    IBlogUserMappingService blogUserMappingService
) : SiteBaseController
{
    #region List

    [HttpGet(RoutingExtension.Site.Blog.List)]
    public async Task<IActionResult> List(ClientFilterBlogViewModel filter)
    {
        filter.TakeEntity = 4;

        var result = await blogService.FilterAsync(filter);

        return View(result.Value);
    }

    #endregion

    #region Details

    [HttpGet(RoutingExtension.Site.Blog.Detail)]
    public async Task<IActionResult> Details(string slug)
    {
        var result = await blogService.GetBySlugAsync(slug);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToRefererUrl();
        }

        await blogUserMappingService.RegisterVisitAsync(result.Value.Id, User.GetUserId(), HttpContext.GetUserIpAddress());

        return View(result.Value);
    }


    [HttpGet(RoutingExtension.Site.Blog.ShortLink)]
    public async Task<IActionResult> ShortLink(int id)
    {
        var result = await blogService.GetSlugByIdAsync(id);
                                                                                                                                    
        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToRefererUrl();
        }

        return RedirectToAction(nameof(Details), new { slug = result.Value });
    }

    #endregion
}