using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Client.Project;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.ViewComponents;

public class ProjectSliderViewComponent(IProjectService projectService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await projectService.GetAllAsync(new ClientFilterProjectViewModel()
        {
            FilterOrderBy = FilterOrderBy.Descending,
            TakeEntity = 5,
            
        });
        return View("ProjectSlider", result);
    }
}