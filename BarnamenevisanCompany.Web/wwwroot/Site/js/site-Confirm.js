

function ConfirmHardDelete(element, requestUrl, formId = null) {
    let isDark=localStorage.getItem('color-theme');
    Swal.fire({
        title: "حذف",
        text: "آیا از حذف مطمئن هستید ؟",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "تایید",
        background: isDark  === 'dark' ? '#1e1e1e' : '#ffffff',
        color: isDark === 'dark' ? '#fff' : '#111827',
        confirmButtonColor: isDark === 'dark' ? '#3b82f6' : '#2563eb',
        cancelButtonText: "لغو",
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: requestUrl,
                type: "Get",
                beforeSend: function () {
                    open_waiting();
                },
                success: function (response) {
                    close_waiting();
                    $(element).remove();
                    if (formId) {
                        ajaxSubstitutionFormId(formId);
                    }
                    showToaster(response, "success");
                },
                error: function (xhr) {
                    close_waiting();
                    showToaster(response.message, "error");
                }
            });
        }
    });
}