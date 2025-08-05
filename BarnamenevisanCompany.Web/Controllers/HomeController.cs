using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Implementations;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Web.ActionFilters;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Controllers;

public class HomeController(
    ISiteSettingService siteSettingService,
    IOrderStepService orderStepService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewData["IndexSetting"] = (await siteSettingService.FillModelForUpdateAsync(1)).Value;
        var orderSteps = await orderStepService.GetClientOrderStepAsync();
        
        ViewData["OrderSteps"] = orderSteps.Value;

        return View();
    }

    #region CkEditor

    [HttpPost("UploadCkeditorImage")]
    public async Task<JsonResult> UploadCkeditorImage(IFormFile upload)
    {
        if (upload.Length <= 0 || !upload.IsImage())
        {
            return default!;
        }

        string fileName = Guid.NewGuid() +
                          Path.GetExtension(upload.FileName).ToLower();

        string path = Path.Combine(Directory.GetCurrentDirectory(), FilePaths.CkeditorImageSavePath);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        path = Path.Combine(path, fileName);

        await using (FileStream stream = new(path, FileMode.Create))
            await upload.CopyToAsync(stream);


        string url = $"{FilePaths.CkeditorImageGetPath}{fileName}";

        return Json(new { uploaded = true, url });
    }

    #endregion

    #region Errors

    [HttpGet(RoutingExtension.Site.NotFound)]
    public IActionResult NotFound() =>
        View();


    [HttpGet(RoutingExtension.Site.ServerError)]
    public IActionResult ServerError() =>
        View();

    #endregion
}