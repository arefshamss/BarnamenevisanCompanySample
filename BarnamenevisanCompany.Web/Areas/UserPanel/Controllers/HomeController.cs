using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Company;
using BarnamenevisanCompany.Web.ActionFilters;
using BarnamenevisanCompany.Web.Areas.UserPanel.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.UserPanel.Controllers;

public class HomeController : UserPanelBaseController
{
    [HttpGet(RoutingExtension.UserPanel.Home.Index)]
    public IActionResult Index()    
    {
        return View();
    }
}