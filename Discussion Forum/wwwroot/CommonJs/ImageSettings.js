$(function () {
    $(avatar).change(function () {
        var reader = new FileReader;

        reader.readAsDataURL(avatar.files[0]);
        reader.onloadend = (x) => {
            avatarImg.src = x.target.result;
        }
    })
})

$(function () {
    $(avatar).change(function () {
        var maxFileSize = 2 * 1024 * 1024; // 2 MB
        var imageError = $("#imageError");

        if (this.files && this.files[0]) {
            if (this.files[0].size > maxFileSize) {
                imageError.text("Maximum allowed file size is 2 MB");
                this.value = "";
                $(avatarImg).value = "";
            } else {
                imageError.text("");
            }
        }
    });
})
