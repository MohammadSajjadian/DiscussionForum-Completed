function Counter(num) {
    $(document).ready(function () {
        var maxLength = num;
        $('textarea').on('input', function () {
            var length = $(this).val().length;
            var remaining = maxLength - length;
            $('#char-counter').text(remaining);
        });
    });
}
