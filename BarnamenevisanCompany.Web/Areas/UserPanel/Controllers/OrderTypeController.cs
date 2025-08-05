using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Web.Areas.UserPanel.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.UserPanel.Controllers;

public class OrderTypeController(IOrderTypeService orderTypeService) : UserPanelBaseController
{
    #region Detail

    [HttpGet]
    public async Task<IActionResult> DetailsPartial(short id)
    {
        var result = await orderTypeService.GetByIdAsync(id);
        return PartialView("_DetailsPartial", result.Value);
    }

    #endregion
}