using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Client.Project;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.ViewComponents;

public class ProjectViewComponent(IProjectService projectService):ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await projectService.GetAllAsync(new ClientFilterProjectViewModel()
        {
            TakeEntity = 6,
        });
        return View("Project",result.Entities);
    }
}