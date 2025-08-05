using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.PartnerCompany;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.PartnerCompanyManagement)]
public class PartnerCompanyController(
    IPartnerCompanyService partnerCompanyService
) : AdminBaseController
{
    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.PartnerCompanyList)]
    public async Task<IActionResult> List(AdminFilterPartnerCompanyViewModel filter)
    {
        var result = await partnerCompanyService.FilterAsync(filter);
        return View(result.Value);
    }

    #endregion

    #region Create

    [HttpGet]
    [InvokePermission(PermissionsName.CreatePartnerCompany)]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.CreatePartnerCompany)]
    public async Task<IActionResult> Create(AdminCreatePartnerCompanyViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        var result = await partnerCompanyService.CreateAsync(model);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return View(model);
        }

        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction(nameof(List), new { area = "Admin" });
    }

    #endregion

    #region Update

    [HttpGet]
    [InvokePermission(PermissionsName.UpdatePartnerCompany)]
    public async Task<IActionResult> Update(short id, int? page)
    {
        var result = await partnerCompanyService.FillModelForUpdateAsync(id);
        ViewBag.Page = page;

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction(nameof(List), new { area = "Admin" });
        }

        return View(result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.UpdatePartnerCompany)]
    public async Task<IActionResult> Update(AdminUpdatePartnerCompanyViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        var result = await partnerCompanyService.UpdateAsync(model);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return View(model);
        }

        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction(nameof(List), new { area = "Admin" });
    }

    #endregion

    #region DeleteOrRecover

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteOrRecoverPartnerCompany)]
    public async Task<IActionResult> DeleteOrRecover(short id)
    {
        var result = await partnerCompanyService.DeleteOrRecoverAsync(id);
        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion
}