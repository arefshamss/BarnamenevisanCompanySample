let otpElementChanegPassword = $("#otp-countdown");
let resendOtpElement = $("#resend-otp-code");
function initialOtpForChangePassword() {
    countDown(otpElementChanegPassword, (element) => {
        element.addClass("hidden");
        resendOtpElement.removeClass("hidden");
        resendOtpElement.html("ارسال مجدد");
    }, "");
}
function successResendOtpForChangePassword(data, status, xhr) {
    close_waiting();
    showToaster(data.message, 'success');

    otpElementChanegPassword.attr("enddate", data.value.otpExpire)
    otpElementChanegPassword.removeClass("hidden");
    resendOtpElement.addClass("hidden");
    initialOtpForChangePassword();

}