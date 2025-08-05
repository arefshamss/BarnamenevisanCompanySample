using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Client.Project;
using BarnamenevisanCompany.Web.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Controllers;

public class ProjectController(
    IProjectService projectService,
    IProjectVisitsMappingService projectVisitsMappingService) : SiteBaseController
{
    [HttpGet(RoutingExtension.Site.Project.List)]
    public async Task<ActionResult> List(ClientFilterProjectViewModel filter)   
    {
        filter.TakeEntity = 9;

        var result = await projectService.GetAllAsync(filter);

        return View(result);
    }

    [HttpGet(RoutingExtension.Site.Project.Detail)]
    public async Task<ActionResult> Detail(short id, string slug)   
    {
        var userid = User.GetUserId();
        await projectVisitsMappingService.RegisterProjectVisitAsync(id, userIp: HttpContext.GetUserIpAddress(), userId: User.GetUserId());

        var result = await projectService.FillModelDetailForClient(id);
        return View(result.Value);
    }
}