using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.ContactUs;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.ContactUsManagement)]
public class ContactUsController(IContactUsService contactUsService) : AdminBaseController
{
    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.ContactUsList)]
    public async Task<IActionResult> List(AdminFilterContactUsViewModel filter)
    {
        var model = await contactUsService.FilterAsync(filter);
        return View(model);
    }

    #endregion

    #region Answer

    [HttpGet]
    [InvokePermission(PermissionsName.AnswerContactUs)]
    public async Task<IActionResult> Answer(short id, int? page)
    {
        var result = await contactUsService.FillModelForAnswerAsync(id);
        ViewBag.Page = page;
        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(List));
        }

        return View(result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.AnswerContactUs)]
    public async Task<IActionResult> Answer(AdminAnswerContactUsMessageViewModel model)
    {
        #region Validaiotn

        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        #endregion

        var result = await contactUsService.AnswerAsync(model);
        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return View(model);
        }

        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction(nameof(List));
    }

    #endregion

    #region DeleteOrRecover

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteOrRecoverContactUs)]
    public async Task<IActionResult> DeleteOrRecover(short id)
    {
        var result = await contactUsService.DeleteOrRecoverAsync(id);
        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion
}