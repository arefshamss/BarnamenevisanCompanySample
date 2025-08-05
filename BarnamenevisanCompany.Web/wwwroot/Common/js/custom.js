$(() => {
    lightbox.option({
        'wrapAround': true,
        'albumLabel': "تصویر %1 از %2",
        'resizeDuration': 200,
    })

})

function setCkeditorThemeForSite(theme) {
    const darkThemeCssLink = $("<link rel=\"stylesheet\" href=\"/Common/lib/ckeditor5/ckeditor5-site-dark.css\" id=\"ckeditor-dark\">");

    switch (theme) {
        case "dark":
            $("#ckeditor").after(darkThemeCssLink);
            break;
        case "light":
            $("#ckeditor-dark").remove();
            break;
    }
}
function showSweetAlert(message, icon) {
    if (icon === null || icon === undefined || icon === '') {
        icon = 'info';
    }
    $(() => {
        Swal.fire({
            icon: icon,
            title: message,
            confirmButtonText: "تایید"
        });
    })
}
function showToaster(message, type, title = null) {

    switch (type) {
        case 'warning': {
            toastr.warning(message, `<strong>${title ?? "هشدار"}</strong>`);
            break;
        }
        case 'info': {
            toastr.info(message, `<strong>${title ?? "اطلاع رسانی"}</strong>`);
            break;
        }
        case 'success': {
            toastr.success(message, `<strong>${title ?? "موفقیت آمیز"}</strong>`);
            break;
        }
        case 'error': {
            toastr.error(message, `<strong>${title ?? "خطا"}</strong>`);
            break;
        }
    }
}


function FillPageId(id) {
    $("#Page").val(id);
    document.getElementById("filter-search").submit();
}
function FillPageIdByFromId(pageId, formId) {
    const form = $(`#${formId}`);
    form.find("[name='Page']").val(pageId);
    const elem = '<button class="d-none submit-hidden" type="submit"></button>';
    form.append(elem);
    form.find(".submit-hidden").click();
}


function toggleOnCheckBoxChanged(input, targetId) {
    if ($(input).is(':checked')) {
        $(targetId).show('slow');
    } else {
        $(targetId).hide('slow');
    }
}
function showOnSelectChange(select, targetId, targetIndex) {
    let value = $(select).val();
    if (value == targetIndex) {
        $(targetId).show('slow');
    } else {
        $(targetId).hide('slow');
    }
}

function countDown(element, onFinish = null, targetElementText = null) {
    let endDateAttr = element.attr("enddate");
    let endDate = new Date(endDateAttr).getTime();
    if (endDate) {
        var dateTimeNow = new Date().getTime();
        var remainingTime = endDate - dateTimeNow;

        if (remainingTime < 1) {
            if (onFinish)
                onFinish(element)

            return;
        }

        var minutes = Math.floor((remainingTime % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((remainingTime % (1000 * 60)) / 1000);

        // Add leading zeros
        var displayMinutes = String(minutes).padStart(2, '0');
        var displaySeconds = String(seconds).padStart(2, '0');

        if (targetElementText)
            element.html(`${targetElementText}${displayMinutes}:${displaySeconds}`);
        else
            element.html(`${displayMinutes}:${displaySeconds}`);

        setTimeout(() => {
            countDown(element, onFinish, targetElementText)
        }, 1000)
    } else {
        console.error("You must set endDate attribute value !")
    }
}
function redirect(url, timeout = null) {
    if (timeout) {
        setTimeout(function () {
            window.location.href = url;
        }, Number(timeout));
    } else {
        window.location.href = url;
    }
}

