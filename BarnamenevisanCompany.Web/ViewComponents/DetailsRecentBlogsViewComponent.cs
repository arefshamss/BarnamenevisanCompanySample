using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Client.Blog;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.ViewComponents;

public class DetailsRecentBlogsViewComponent(
    IBlogService blogService
) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await blogService.FilterAsync(new ClientFilterBlogViewModel()
        {
            TakeEntity = 4
        });

        return View("DetailsRecentBlogs", result.Value);
    }
}