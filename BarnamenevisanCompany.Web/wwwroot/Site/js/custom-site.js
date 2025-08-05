function initPlyr() {
    $(".plyr-init").each(function () {
        const player = new Plyr($(this));
    })
}

function onSubmit(token) {
    document.getElementById("captcha-id").submit();
}

function initialMaps() {
    $("[map]").each(function () {
        let lat = $(this).attr("lat");
        let lng = $(this).attr("lng");
        let onClickIsDisabled = $(this).is("[clickable]");
        let element = $(this).get(0);
        let options = {
            scrollWheelZoom: false,
            zoom: 16
        }

        // Initialize the map
        if (lat == null || lng == null || lat == 0 || lng == 0) {
            var map = L.map(element, options).setView([35.700, 51.35]);
        } else {
            var map = L.map(element, options).setView([lat, lng]);
        }

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 18,
        }).addTo(map);

        var marker = null;

        if (lat == null || lng == null) {
            marker = null;
        } else {
            marker = L.marker([lat, lng]).addTo(map);
        }

        if (onClickIsDisabled) {
            map.on('click', function (e) {
                if (marker) {
                    map.removeLayer(marker);
                }

                marker = L.marker(e.latlng).addTo(map);

                document.getElementById('latitude').value = e.latlng.lat;
                document.getElementById('longitude').value = e.latlng.lng;

            });
        }

        map.getContainer().addEventListener('wheel', function (e) {
            if (e.ctrlKey) {
                e.preventDefault();
                map.scrollWheelZoom.enable();
            } else {
                map.scrollWheelZoom.disable();
            }
        }, {passive: false});

        // Resize the map to ensure it fits the container properly
        setTimeout(function () {
            map.invalidateSize();
        }, 10);
    })
}


//#region Change Password

function successChangePassword(element) {
    resetForm(element);
}

//#endregion

//#region Update Avatar

let inputAvatarFile = $("input[type='file']#Avatar");
inputAvatarFile.on("change", function (e) {
    $("#UpdateAvatarForm").submit();
})

function successUpdateAvatar(data) {
    let avatarName = data.value.avatarName;
    let message = data.value.message;

    $("img#AvatarImage").attr("src", avatarName);
    showToaster(message, "success");

    let deleteAvatarButton = `<a class="*:text-red-400 flex" 
                                        href="/UserPanel/User/DeleteAvatar"
                                        data-ajax="true"
                                        data-ajax-method="get"
                                        data-ajax-mode="replace"
                                        data-ajax-success="close_waiting();successDeleteAvatar(data);"
                                        data-ajax-failure="onAjaxFailure(xhr, status, error);"
                                        data-ajax-begin="open_waiting();"
                                        onclick="showConfirmableAlert(event, 'حذف تصویر پروفایل', 'آیا از حذف تصویر پروفایل خود مطمئن هستید ؟')"
                                        id="delete-avatar">
                    <iconify-icon icon="tabler:trash" width="24" height="24"></iconify-icon>
                </a>`;
    let avatarActions = $("#avatar-actions");
    if (avatarActions.find("#delete-avatar").length < 1) {
        avatarActions.append(deleteAvatarButton);
    }

    inputAvatarFile.val('');
}

function successDeleteAvatar(data) {
    let avatarName = data.value.avatarName;
    let message = data.value.message;

    $("img#AvatarImage").attr("src", avatarName);
    showToaster(message, "success");

    $("#delete-avatar").remove();
}

//#endregion


$(() => {
    // setCkeditorDarkTheme(localStorage.getItem('template-theme') ?? "light");
    initialMaps();
    initPlyr();
    $(".breadcrumb-area .bread-menu li + li").before("<iconify-icon icon=\"codicon:circle-filled\" width=\"14\" height=\"14\"></iconify-icon>");

    const gallerySwiper = document.querySelector("#gallery-swiper");
    const blogsSwiper = document.querySelector("#blogs-swiper");
    const projectsSwiper = document.querySelector("#projects-swiper");
    const commentsSwiper = document.querySelector("#comments-swiper");

    if (gallerySwiper) {
        Object.assign(gallerySwiper, {
            breakpoints: {
                320: {slidesPerView: 1},
                640: {slidesPerView: 2},
                1024: {slidesPerView: 3},
                1280: {slidesPerView: 4}
            }
        });
        gallerySwiper.initialize();
    }

    if (blogsSwiper) {
        Object.assign(blogsSwiper, {
            breakpoints: {
                320: {slidesPerView: 1},
                640: {slidesPerView: 2},
                1024: {slidesPerView: 3},
                1280: {slidesPerView: 4}
            }
        });
        blogsSwiper.initialize();
    }

    if (projectsSwiper) {
        Object.assign(projectsSwiper, {
            breakpoints: {
                320: {slidesPerView: 1},
                640: {slidesPerView: 2},
                1024: {slidesPerView: 3},
                1280: {slidesPerView: 4}
            }
        });
        projectsSwiper.initialize();    
    }

    if (commentsSwiper) {
        Object.assign(commentsSwiper, {
            breakpoints: {
                320: {slidesPerView: 1},
                640: {slidesPerView: 2},
                1024: {slidesPerView: 3},
            }
        });
        commentsSwiper.initialize();
    }
    
    $("#profile-dropdown-button").click(function () {
        $("#profile-dropdown-body").fadeToggle(250);
    })

    // $('#order-form').on("submit", function (event) {
    //     let checkedInputsCount = 0;
    //     $(this).find('.order-type').each(function () {
    //         if ($(this).is(":checked"))
    //             checkedInputsCount++;
    //     });
    //
    //     if (checkedInputsCount < 1) {
    //         showToaster('انتخاب کردن نوع سفارش الزامی است', 'error')
    //         event.preventDefault();
    //     } else {
    //         return
    //     }
    // });

})

function copyShortLink() {
    const text = document.getElementById('shortLink').innerText;
    navigator.clipboard.writeText(text).then(() => {
        showToaster("لینک با موفقیت کپی شد", "info", "");
    });
}

