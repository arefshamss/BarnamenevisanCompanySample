using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Models.Consult;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Consult;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.ConsultManagement)]
public class ConsultController(IConsultService consultService) : AdminBaseController
{
    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.ConsultList)]
    public async Task<IActionResult> List(AdminFilterConsultViewModel filter)
    {
        var model = await consultService.FilterAsync(filter);
        return View(model);
    }


    [HttpGet]
    [InvokePermission(PermissionsName.ConsultList)]
    public async Task<IActionResult> ListPartial(AdminFilterConsultViewModel filter)
    {
        var result = await consultService.FilterAsync(filter);
        return PartialView("_ListPartial", result);
    }

    #endregion

    #region Details

    [HttpGet]
    [InvokePermission(PermissionsName.ConsultDetails)]
    public async Task<IActionResult> Details(short id, int? page)
    {
        var result = await consultService.FillModelDetailForShow(id);
        ViewBag.Page = page;
        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(List));
        }

        return View(result.Value);
    }

    #endregion

    #region Answer

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.AnswerConsult)]
    public async Task<IActionResult> Answer(AdminAnswerConsultViewModel model)
    {
        var result = await consultService.AnswerAsync(model);

        if (!ModelState.IsValid)
        {
           ShowToasterErrorMessage("وارد کردن پاسخ الزالمی میباشد");
            return RedirectToAction(nameof(Details), new { id = model.ConsultId });
        }

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction("Details", "Consult", new { id = model.ConsultId });
        }

        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction(nameof(List), new { area = "Admin" });
    }

    #endregion

    #region DeleteOrRecoverAsync

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteOrRecoverConsult)]
    public async Task<IActionResult> DeleteOrRecoverAsync(short id)
    {
        var result = await consultService.DeleteOrRecoverAsync(id);

        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion
}