using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Client.Consult;
using BarnamenevisanCompany.Web.Areas.UserPanel.Controllers.Common;
using BarnamenevisanCompany.Web.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.UserPanel.Controllers;

public class ConsultController(
    IConsultService consultService,
    ICaptchaValidator captchaValidator) : UserPanelBaseController
{
    #region List

    [HttpGet(RoutingExtension.UserPanel.Consult.List)]
    public async Task<IActionResult> List(ClientConsultFilterViewModel filter)
    {
        var result = await consultService.FilterAsync(filter, User.GetUserId());
        return View(result.Value);
    }

    #endregion

    #region ConsultDetail

    [HttpGet(RoutingExtension.UserPanel.Consult.Detail)]
    public async Task<IActionResult> Detail(short consultId)
    {
        var result = await consultService.GetConsultDetailAsync(consultId);
        return result.IsFailure ? BadRequest(result.Message) : View(result.Value);
    }

    #endregion
}