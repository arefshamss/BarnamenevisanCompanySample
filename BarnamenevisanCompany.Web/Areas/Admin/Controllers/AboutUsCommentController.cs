using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Admin.AboutUsComment;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;
[InvokePermission(PermissionsName.AboutUsCommentManagement)]
public class AboutUsCommentController(IAboutUsCommentService aboutUsCommentService) : AdminBaseController
{
    #region List
    [InvokePermission(PermissionsName.AboutUsCommentList)]
    public async Task<IActionResult> List(AdminFilterAboutUsCommentViewModel filter)
    {
        var result = await aboutUsCommentService.FilterAsync(filter);
        result.FormId = "about-us-comment";
        return AjaxSubstitutionResult(filter, result);
    }

    #endregion

    #region Create

    [HttpGet]
    [InvokePermission(PermissionsName.CreateAboutUsComment)]
    public IActionResult Create(int userId)
    {
        ViewBag.UserId = userId;
        return PartialView("_Create");
    }

    [HttpPost, ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.CreateAboutUsComment)]
    public async Task<IActionResult> Create(AdminCreateAboutUsCommentViewModel model)
    {
        #region Validation

        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetModelErrorsAsString());

        #endregion

        var result = await aboutUsCommentService.CreateAsync(model);

        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region Update

    [HttpGet]
    [InvokePermission(PermissionsName.UpdateAboutUsComment)]
    public async Task<IActionResult> UpdateAsync(short id)
    {
        var result = await aboutUsCommentService.FillModelForUpdateAsync(id);
        return result.IsFailure ? BadRequest(result.Message) : PartialView("_Update", result.Value);
    }

    [HttpPost, ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.UpdateAboutUsComment)]
    public async Task<IActionResult> UpdateAsync(AdminUpdateAboutUsCommentViewModel model)
    {
        #region Validation

        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetModelErrorsAsString());

        #endregion

        var result = await aboutUsCommentService.UpdateAsync(model);

        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region DeleteOrRecover
    [InvokePermission(PermissionsName.DeleteOrRecoverAboutUsComment)]
    public async Task<IActionResult> DeleteOrRecover(short id)
    {
        var result = await aboutUsCommentService.DeleteOrRecover(id);
        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion
}