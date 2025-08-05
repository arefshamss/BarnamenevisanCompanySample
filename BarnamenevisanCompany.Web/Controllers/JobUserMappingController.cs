using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Client.JobUserMapping;
using BarnamenevisanCompany.Web.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Controllers;

public class JobUserMappingController(IJobUserMappingService jobUserMappingService) : SiteBaseController
{
    #region Create

    [HttpGet]
    public IActionResult Create(int jobId)
    {
        return PartialView("_CreatePartial", new ClientCreateJobUserMappingViewModel{JobId = jobId});
    }
    
    
    [HttpPost, ValidateAntiForgeryToken, Authorize]
    public async Task<IActionResult> Create(ClientCreateJobUserMappingViewModel model)
    {
        model.UserId = User.GetUserId();
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetModelErrorsAsString());
        }
        
        var result = await jobUserMappingService.CreateAsync(model);

        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion
}