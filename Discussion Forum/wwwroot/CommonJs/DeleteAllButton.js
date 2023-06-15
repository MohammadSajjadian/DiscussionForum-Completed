$(function () {
    $(selectAllCheckBox).click(function () {
        let checkboxes = $('.isDelete');
        let deleteAll = $('#deleteAll');

        let checked = $(selectAllCheckBox).is(":checked");
        deleteAll.prop('disabled', !checked);

        if ($(selectAllCheckBox).is(":checked")) {

            checkboxes.prop('checked', true);
        } else {
            checkboxes.prop('checked', false);
        }

    })
})

$(function () {
    var checkboxes = $('.isDelete');
    var deleteAll = $('#deleteAll');

    checkboxes.on('change', function () {
        let checked = checkboxes.is(":checked");
        deleteAll.prop("disabled", !checked);
    });
})

$(function () {
    $(deleteAllForm).submit(function (event) {
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
                form.submit();
            }
        })
    })
})
