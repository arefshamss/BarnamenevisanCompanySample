using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Client.UserPosition;
using BarnamenevisanCompany.Web.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Controllers;

public class UsersPositionController(IUserPositionService userPositionService) : SiteBaseController
{
    #region List

    [HttpGet(RoutingExtension.Site.UserPosition)]
    public async Task<IActionResult> List(ClientFilterUserPositionViewModel filter)
    {
        filter.TakeEntity = 13;
        var model = await userPositionService.GetAllClientUserPositionsAsync(filter);

        return View(model);
    }

    #endregion
}