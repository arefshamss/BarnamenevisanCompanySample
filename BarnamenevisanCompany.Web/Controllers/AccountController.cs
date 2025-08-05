using System.Security.Claims;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Client.User;
using BarnamenevisanCompany.Web.Controllers.Common;
using BarnamenevisanCompany.Web.Extensions;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BarnamenevisanCompany.Web.Controllers;

public class AccountController(
    IUserService userService,
    ICaptchaValidator captchaValidator) : SiteBaseController
{
    #region Login Or Register

    [HttpGet(RoutingExtension.Site.Account.Login)]
    public IActionResult LoginOrRegister(string? mobile = null, bool isLoginByPassword = false, string returnUrl = RoutingExtension.UserPanel.Home.Index)
    {
        if (UserIsAuthenticated())
            return RedirectToAction("Index", "Home");
            
        if (!string.IsNullOrEmpty(returnUrl) && returnUrl.Contains(RoutingExtension.UserPanel.Order.Create, StringComparison.OrdinalIgnoreCase))
        {
            TempData["AuthMessage"] = "برای ثبت سفارش، ابتدا باید وارد حساب کاربری خود شوید یا ثبت‌نام کنید";
        }
        
        return View(new LoginOrRegisterViewModel
        {
            IsLoginByPassword = isLoginByPassword,
            ReturnUrl = returnUrl
        });
    }


    [HttpPost(RoutingExtension.Site.Account.Login)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginOrRegister(LoginOrRegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        if (!await captchaValidator.IsCaptchaPassedAsync(model.Captcha))
        {
            ShowToasterErrorMessage(ErrorMessages.CaptchaError);
            return View(model);
        }

        var result = await userService.ConfirmLoginOrRegisterAsync(model);

        if (result.IsFailure)
        {
            ShowToasterErrorMessage(result.Message);
            return View(model);
        }

        if (result.Value.IsLoginByPassword)
        {
            await LoginUserAsync(new()
            {
                FullName = result.Value.FullName,
                Mobile = result.Value.Mobile,
                UserId = result.Value.UserId
            });

            ShowToasterSuccessMessage(result.Message);
            if (Url.IsLocalUrl(model.ReturnUrl))
                return LocalRedirect(model.ReturnUrl);

            return RedirectToAction("Index", "Home", new { area = "UserPanel" });
        }

        var sendOtpResult = await userService.SendOtpCodeSmsAsync($"کد تایید شما جهت ورود به برنامه نویسان {result.Value.ActiveCode!}", result.Value.Mobile);
        if (sendOtpResult.IsFailure)
        {
            ShowToasterErrorMessage(sendOtpResult.Message);
            return View(model);
        }

        TempData["Mobile"] = model.Mobile;
        TempData["UserId"] = result.Value.UserId;
        ShowToasterSuccessMessage(SuccessMessages.OtpSentSuccessfullyDone);

        return RedirectToAction("ConfirmOtpCode", new { returnUrl = model.ReturnUrl });
    }

    #endregion

    #region Confirm Otp

    [HttpGet(RoutingExtension.Site.Account.ConfirmOtp)]
    public async Task<IActionResult> ConfirmOtpCode(string returnUrl = RoutingExtension.UserPanel.Home.Index)
    {
        if (UserIsAuthenticated())
            return RedirectToAction("Index", "Home", new { area = "UserPanel" });

        var userId = PeekTempData("UserId") as int? ?? 0;
        var mobile = PeekTempData("Mobile") as string;
        ViewData["ActiveCodeExpireTime"] = (await userService.GetActiveCodeExpireTimeCodeAsync(mobile!)).Value;

        return View(new OtpCodeViewModel
        {
            ReturnUrl = returnUrl,
            UserId = userId,
            Mobile = mobile
        });
    }

    [HttpPost(RoutingExtension.Site.Account.ConfirmOtp)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmOtpCode(OtpCodeViewModel model)
    {
        if (UserIsAuthenticated())
            return RedirectToAction("Index", "Home", new { area = "UserPanel" });

        if (!await captchaValidator.IsCaptchaPassedAsync(model.Captcha))
        {
            ShowToasterErrorMessage(ErrorMessages.CaptchaError);
            return View(model);
        }
        
        model.ReturnUrl ??= RoutingExtension.UserPanel.Home.Index;

        if (!ModelState.IsValid)
        {
            ViewData["ActiveCodeExpireTime"] = (await userService.GetActiveCodeExpireTimeCodeAsync(model.Mobile!)).Value;
            ShowToasterErrorMessage(ModelState.GetModelErrorsAsString());
            return View(model);
        }

        var result = await userService.ConfirmOtpCodeAsync(model);

        if (result.IsSuccess)
        {
            RemoveTempData("Mobile");
            RemoveTempData("UserId");
            await LoginUserAsync(result.Value);
            ShowToasterSuccessMessage(SuccessMessages.LoginSuccessfullyDone);
            if (Url.IsLocalUrl(model.ReturnUrl))
                return LocalRedirect(model.ReturnUrl);

            return RedirectToAction("Index", "Home", new { area = "UserPanel" });
        }

        ViewData["ActiveCodeExpireTime"] = (await userService.GetActiveCodeExpireTimeCodeAsync(model.Mobile!)).Value;
        ShowToasterErrorMessage(result.Message);

        return View(model);
    }

    #endregion

    #region Resend Otp Code

    [HttpGet(RoutingExtension.Site.Account.Resend.Otp)]
    public async Task<IActionResult> ResendOtpCode(int userId)
    {
        var result = await userService.ResendOtpCodeSmsAsync(userId);
        TempData.Keep("UserId");
        return result.IsFailure
            ? BadRequest(result.Message)
            : Ok(new JsonResult(new
            {
                message = result.Message,
                otpExpire = result.Value.ActiveCodeExpireDateTime
            }));
    }

    #endregion

    #region Logout

    [HttpGet(RoutingExtension.Site.Account.Logout)]
    public async Task<IActionResult> Logout()
    {
        ShowToasterSuccessMessage(SuccessMessages.LogoutSuccessfullyDone);
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    #endregion

    #region Helpers

    private bool UserIsAuthenticated()
        => User.Identity?.IsAuthenticated ?? false;

    private async Task LoginUserAsync(AuthenticateUserViewModel model)
    {
        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier, model.UserId.ToString()),
            new(ClaimTypes.Name, model.FullName ?? ""),
            new(ClaimTypes.MobilePhone, model.Mobile ?? ""),
        ];

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);
        var properties = new AuthenticationProperties { IsPersistent = true };
        await HttpContext.SignInAsync(principal, properties);
    }

    #endregion
}