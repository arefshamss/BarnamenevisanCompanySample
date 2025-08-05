using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Honors;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.HonorManagement)]
public class HonorController(
    IHonorsService honorsService,
    ICompanyService companyService,
    IHonorsGalleryService honorsGalleryService) : AdminBaseController
{
    #region List

    [HttpGet]
    [InvokePermission(PermissionsName.HonorList)]
    public async Task<IActionResult> List(AdminFilterHonorsViewModel filter)
    {
        var model = await honorsService.FilterAsync(filter);
        return View(model);
    }

    #endregion

    #region Create

    [HttpGet]
    [InvokePermission(PermissionsName.CreateHonor)]
    public async Task<IActionResult> Create()
    {
        ViewBag.Company = await companyService.AdminGetSelectListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.CreateHonor)]
    public async Task<IActionResult> Create(AdminCreateHonorsViewModel model)
    {
        #region Validation

        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            ViewBag.Company = await companyService.AdminGetSelectListAsync();
            return View(model);
        }

        #endregion

        var result = await honorsService.CreateAsync(model);
        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            ViewBag.Company = await companyService.AdminGetSelectListAsync();
            return View(model);
        }

        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction(nameof(List), new { area = "Admin" });
    }

    #endregion

    #region Update

    [HttpGet]
    [InvokePermission(PermissionsName.UpdateHonor)]
    public async Task<IActionResult> Update(short id)
    {
        ViewBag.Company = await companyService.AdminGetSelectListAsync();
        var result = await honorsService.FillModelForUpdateAsync(id);
        return View(result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.UpdateHonor)]
    public async Task<IActionResult> Update(AdminUpdateHonorsViewModel model)
    {
        #region Validation

        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            ViewBag.Company = await companyService.AdminGetSelectListAsync();
            return View(model);
        }

        #endregion

        var result = await honorsService.UpdateAsync(model);
        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            ViewBag.Company = await companyService.AdminGetSelectListAsync();
            return View(model);
        }

        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction(nameof(List), new { area = "Admin" });
    }

    #endregion

    #region DeleteOrRecover

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteOrRecoverHonor)]
    public async Task<IActionResult> DeleteOrRecover(short id)
    {
        var result = await honorsService.DeleteOrRecoverAsync(id);

        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region HonorsGallery

    [HttpGet]
    [InvokePermission(PermissionsName.HonorGallery)]
    public async Task<IActionResult> HonorsGallery(short id)
    {
        var result = await honorsGalleryService.FillModelForGalleryAsync(id);
        ViewBag.HonorId = id;

        return View(result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [InvokePermission(PermissionsName.AddImageToHonorGallery)]
    public async Task<IActionResult> AddToGallery([FromForm] short honorId, IFormFile image)
    {
        var result = await honorsGalleryService.CreatGallery(honorId, image);
        return Json(result.Value);
    }

    #endregion

    #region DeleteImageFromGallery

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteImageFromHonorGallery)]
    public async Task<IActionResult> DeleteImageFromGallery(short honorId, string imageName)
    {
        var result = await honorsGalleryService.DeleteFromGallery(honorId, imageName);

        if (result.IsFailure)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Message);
    }

    #endregion

    #region DeleteTeaser

    [HttpGet]
    [InvokePermission(PermissionsName.DeleteHonorTeaser)]
    public async Task<IActionResult> DeleteTeaser(short id)
    {
        var result = await honorsService.DeleteHonorTeaserAsync(id);
        
        if (result.IsFailure)
            ShowToasterErrorMessage(result.Message);
        else
            ShowToasterSuccessMessage(result.Message);
        
        return RedirectToAction(nameof(Update), new { area = "Admin", id = id });
    }

    #endregion
}