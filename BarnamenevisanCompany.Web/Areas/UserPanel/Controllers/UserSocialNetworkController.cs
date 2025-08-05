using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Client.UserSocialNetwork;
using BarnamenevisanCompany.Domain.ViewModels.Client.UserSocialNetworkMapper;
using BarnamenevisanCompany.Web.Areas.UserPanel.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.UserPanel.Controllers;

public class UserSocialNetworkController(
    IUserSocialNetworkService userSocialNetworkService,
    IUserSocialNetworkMapperService userSocialNetworkMapperService,
    IUserPositionService userPositionService) : UserPanelBaseController
{
    #region List

    [HttpGet(RoutingExtension.UserPanel.UserSocialNetwork.List)]
    public async Task<IActionResult> List(ClientFilterUserSocialNetworkListViewModel filter)
    {
        if (!await userPositionService.IsUserProgrammer(User.GetUserId()))
            return RedirectToAction("Index", "Home", new { area = "UserPanel" });

        filter.UserId = User.GetUserId();
        var model = await userSocialNetworkMapperService.FilterAsync(filter);
        return AjaxSubstitutionResult(filter, model.Value);
    }

    #endregion

    #region Create

    [HttpGet(RoutingExtension.UserPanel.UserSocialNetwork.Create)]
    public async Task<IActionResult> Create()
    {
        ViewBag.Socials = await userSocialNetworkService.GetAllUserSocialNetworksAsync();
        return PartialView("_Create");
    }

    [HttpPost(RoutingExtension.UserPanel.UserSocialNetwork.Create)]
    public async Task<IActionResult> Create(ClientCreateSocialNetworkMapperViewModel model)
    {
        model.UserId = User.GetUserId();

        #region Validation

        if (!ModelState.IsValid)
        {
            ViewBag.Socials = await userSocialNetworkService.GetAllUserSocialNetworksAsync();
            return BadRequest(ModelState.GetModelErrorsAsString());
        }

        #endregion

        var result = await userSocialNetworkMapperService.CreateAsync(model);

        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region Update

    [HttpGet]
    public async Task<IActionResult> Update(byte id)
    {
        var result = await userSocialNetworkMapperService.FillModelForUpdateAsync(id, User.GetUserId());

        return result.IsFailure ? BadRequest(result.Message) : PartialView("_Update", result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Update(ClientUpdateUserSocialNetworkViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ErrorMessages.BadRequestError);
        }

        var result = await userSocialNetworkMapperService.UpdateAsync(model, User.GetUserId());

        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion

    #region Delete

    [HttpGet]
    public async Task<IActionResult> Delete(byte id)
    {
        var result = await userSocialNetworkMapperService.DeleteAsync(id, User.GetUserId());

        return result.IsFailure ? BadRequest(result.Message) : Ok(result.Message);
    }

    #endregion
}