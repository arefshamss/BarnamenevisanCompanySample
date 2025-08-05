function toggleChangeAuthenticationContainer() {
    let password = $("#authentication-container").find("#Password-input-group");
    let link = $('[onclick="toggleChangeAuthenticationContainer()"]');
    let submitBtn = $("#submit-btn");
    let isLoginByPassword = $("#IsLoginByPassword");
    if (isLoginByPassword.val().toLowerCase() === "true") {
        isLoginByPassword.val("False");
        password.addClass("hidden");
        link.html("ورود با رمز عبور")
        submitBtn.html("ورود / ثبت نام");
    } else if (isLoginByPassword.val().toLowerCase() === "false") {
        isLoginByPassword.val("True");
        password.removeClass("hidden");
        link.html("ورود با کد یکبار مصرف")
        submitBtn.html("ورود")
    }
}

$(".confirm-opt-form").on("submit", function (e) {
    let values = [];
    $(".confirm-opt-input").each(function (e) {
        let value = $(this).val();
        if (!value) {
            e.preventDefault();
            showToaster("لطفا کد تایید را وارد کنید", "error")
        }
        values.push(value);
    })
    
    let finalCode = values.toString().trim().replace(/,/g, "");
    if (finalCode) {
        $("#Code").val(finalCode);
    } else {
        e.preventDefault();
    }
})

$(".confirm-opt-input").on("input", function (e) {
    let value = $(this).val();
    if (value) {
        if (!/^\d*$/.test(value)) {
            $(this).val('');
        } else {
            let inputs = $(".confirm-opt-input");
            let currentIndex = inputs.index($(this));
            let nextIndex = currentIndex + 1;
            if (nextIndex > -1 && nextIndex < inputs.length) {
                $(inputs).eq(nextIndex).focus();
            } else {
                $(".confirm-opt-form").submit();
            }
        }   
    }
}).keydown(function (e) {
    if (e.keyCode === 8) {
        $(this).val('');
        let inputs = $(".confirm-opt-input");
        let currentIndex = inputs.index($(this));
        let prevIndex = currentIndex - 1;
        if (prevIndex > -1 && prevIndex < inputs.length) {
            $(this).val('');
            $(inputs).eq(prevIndex).focus();
        } else {
            $(this).val('');
        }
        e.preventDefault();
    }    
});

let otpElement = $("#otp-countdown");
let resendOtpElement = $("#resend-otp");

initialOtp();

function initialOtp() {
    countDown(otpElement, (element) => {
        element.html("مدت زمان اعتبار کد به پایان رسید");
        resendOtpElement.removeClass("hidden");
        resendOtpElement.addClass("inline-block");
    }, "مانده تا دریافت مجدد کد : ");
}
function successResendOtp(data, status, xhr){
    close_waiting();
    showToaster(data.message, 'success');
    
    otpElement.attr("enddate", data.value.otpExpire)
    
    resendOtpElement.removeClass("inline-block");
    resendOtpElement.addClass("hidden");
    
    initialOtp();
    
}