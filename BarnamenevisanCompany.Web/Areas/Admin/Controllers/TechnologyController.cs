using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Technology;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.TechnologyManagement)]
public class TechnologyController(ITechnologyService technologyService) : AdminBaseController
{
    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.TechnologyList)]
    public async Task<IActionResult> List(AdminFilterTechnologyViewModel filter)
    {
        var result = await technologyService.FilterAsync(filter);
        return AjaxSubstitutionResult(filter, result);
    }

    [HttpGet]
    [InvokePermission(PermissionsName.TechnologyList)]
    public async Task<IActionResult> ListPartial(AdminFilterTechnologyViewModel filter)
    {
        filter.TakeEntity = 5;
        var result = await technologyService.FilterAsync(filter);
        result.FormId = "filter-technology";
        return PartialView("_ListPartial", result);
    }

    #endregion

    #region Create

    [HttpGet]
    [InvokePermission(PermissionsName.CreateTechnology)]
    public IActionResult Create() =>
        PartialView("_Create");


    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.CreateTechnology)]
    public async Task<IActionResult> Create(AdminCreateTechnologyViewModel model)
    {
        #region Validation

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.GetModelErrorsAsString());
        }

        #endregion

        var result = await technologyService.CreateAsync(model);
        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region Update

    [HttpGet]
    [InvokePermission(PermissionsName.UpdateTechnology)]
    public async Task<IActionResult> Update(short id)
    {
        var result = await technologyService.FillModelForUpdateAsync(id);

        return result.IsFailure ? BadRequest(result.Message) : PartialView("_Update", result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.UpdateTechnology)]
    public async Task<IActionResult> Update(AdminUpdateTechnologyViewModel model)
    {
        #region Validation

        if (!ModelState.IsValid)
        {
            BadRequest(ModelState.GetModelErrorsAsString());
        }

        #endregion

        var result = await technologyService.UpdateAsync(model);

        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region DeleteOrRecover

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteOrRecoverTechnology)]
    public async Task<IActionResult> DeleteOrRecover(short id)
    {
        var result = await technologyService.DeleteOrRecoverAsync(id);

        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion
}