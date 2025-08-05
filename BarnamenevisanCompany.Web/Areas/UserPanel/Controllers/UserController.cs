using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Client.User;
using BarnamenevisanCompany.Web.Areas.UserPanel.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Areas.UserPanel.Controllers;

public class UserController(IUserService userService) : UserPanelBaseController
{
    #region Update

    [HttpGet(RoutingExtension.UserPanel.User.Update)]
    public async Task<IActionResult> Update()
    {
        var result = await userService.FillModelForUserPanelUpdateAsync(User.GetUserId());

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return RedirectToAction("Index", "Home", new { area = "UserPanel" });
        }


        return View(result.Value);
    }

    [HttpPost(RoutingExtension.UserPanel.User.Update)]
    public async Task<IActionResult> Update(ClientUpdateUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        var result = await userService.UpdateAsync(model);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return View(model);
        }
        
        ShowToasterSuccessMessage(result.Message);
        return RedirectToAction("Index", "Home", new { area = "UserPanel" });
    }
    
    #endregion

    #region Send Otp Code

    [HttpGet(RoutingExtension.UserPanel.User.ConfirmationCode.SendCode)]
    public async Task<IActionResult> SendOtpCode()
    {
        var userId = User.GetUserId();
        var result = await userService.SendConfirmationCodeAsync(userId);

        TempData.Keep("UserId");
        if (result.IsFailure) return BadRequest(result.Message);

        ViewData["ActiveCodeExpireTime"] = result.Value.ActiveCodeExpireDateTime;
        return Ok(new JsonResult(new
        {
            message = result.Message,
            otpExpire = result.Value.ActiveCodeExpireDateTime
        }));
    }

    #endregion
    
    #region ChangePassword

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ClientChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetModelErrorsAsString());

        model.UserId = User.GetUserId();

        //TODO: Check Service and ViewModel
        var confirmOtpResult = await userService.ConfirmOtpCodeAsync(new OtpCodeViewModel
        {
            UserId = model.UserId,
            Code = model.Code,
            Mobile = model.Mobile
        });

        if (confirmOtpResult.IsFailure)
            return BadRequest(confirmOtpResult.Message);

        var changePasswordResult = await userService.ChangePasswordAsync(model);

        return changePasswordResult.IsSuccess
            ? Ok(changePasswordResult.Message)
            : BadRequest(changePasswordResult.Message);
    }

    #endregion

    #region Avatar

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateAvatar(ClientUpdateUserAvatarViewModel model)
    {
        model.UserId = User.GetUserId();

        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetModelErrorsAsString());

        var result = await userService.UpdateAvatar(model);

        return result.IsSuccess
            ? Ok(new JsonResult(new
            {
                Message = result.Message,
                AvatarName = result.Value
            }))
            : BadRequest(result.Message);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteAvatar()
    {
        var result = await userService.DeleteAvatar(User.GetUserId());
        return result.IsSuccess
            ? Ok(new JsonResult(new
            {
                Message = result.Message,
                AvatarName = result.Value
            }))
            : BadRequest(result.Message);
    }
    
    #endregion
}