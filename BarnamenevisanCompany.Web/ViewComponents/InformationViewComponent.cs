using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Client.Index;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.ViewComponents;

public class InformationViewComponent(
    IUserPositionService userPositionService,
    IProjectService projectService,
    ISiteSettingService siteSettingService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var projectCount = (await siteSettingService.FillModelForUpdateAsync(1)).Value.CompletedProject;
        
        var userPositionCount = await userPositionService.GetUserPositionsCountAsync();
        
        var count = new ClientIndexInformation()
        {
            CompeletedProject = projectCount,
            UserPositionCount = userPositionCount.Value
        };
        return View("Information",count);
    } 
}