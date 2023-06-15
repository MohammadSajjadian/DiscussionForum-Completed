function ConfirmAndRedirect(submit, url, objectName) {
    $(submit).click(function () {

        var objectValue = $(this).attr(objectName);

        Swal.fire({
            icon: 'question',
            text: 'Do you want to continue?',
            confirmButtonText: 'Yes',
            showDenyButton: true,
            denyButtonText: 'No'

        }).then((result) => {
            if (result.isConfirmed) {
                window.location.href = `${url}?${objectName}=${objectValue}`;
            }
        })
    })
}
