$(function () {
    $(submitForm).submit(function (event) {
        event.preventDefault();

        var form = this;
        Swal.fire({
            icon: 'question',
            text: 'Do you want to continue?',
            confirmButtonText: 'Yes',
            showDenyButton: true,
            denyButtonText: 'No'

        }).then((result) => {
            if (result.isConfirmed) {
                if ($(form).valid()) {
                    form.submit();
                }
            }
        })
    })
})
