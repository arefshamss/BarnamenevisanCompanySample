using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Client.Blog;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.ViewComponents;

public class RecentBlogsViewComponent(
    IBlogService blogService
) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await blogService.FilterAsync(new ClientFilterBlogViewModel()
        {
            TakeEntity = 3
        });

        return View("RecentBlogs", result.Value);
    }
}