using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Client.OrderType;
using BarnamenevisanCompany.Web.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Controllers;

public class OrderTypeController(IOrderTypeService orderTypeService) : SiteBaseController
{
    [HttpGet]
    public async Task<IActionResult> Details(short typeId)
    {
        var result = await orderTypeService.GetByIdAsync(typeId);

        if (result.IsFailure)
        {
            return BadRequest(result.Message);
        }
        
        return PartialView("_DetailsPartial", result.Value);
    }
}