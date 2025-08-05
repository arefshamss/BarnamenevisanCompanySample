using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.ContactUsInformation;
using BarnamenevisanCompany.Infra.Data.Statics;
using BarnamenevisanCompany.Web.Areas.Admin.Controllers.Common;
using BarnamenevisanCompany.Web.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.Admin.Controllers;

[InvokePermission(PermissionsName.UpdateContactUsInformation)]
public class ContactUsInformationController(IContactUsInformationService contactUsInformationService) : AdminBaseController
{
    #region Update

    [HttpGet]
    public async Task<IActionResult> Update()
    {
        var result = await contactUsInformationService.GetContactUsInformationAsync();
        return View(result.Value);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(AdminUpdateContactUsInformationViewModel model)
    {
        #region Valication

        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ErrorMessages.NotValid);
            return View(model);
        }

        #endregion

        var result = await contactUsInformationService.UpdateAsync(model);
        ShowToasterSuccessMessage(result.Message);

        return View(model);
    }

    #endregion
}