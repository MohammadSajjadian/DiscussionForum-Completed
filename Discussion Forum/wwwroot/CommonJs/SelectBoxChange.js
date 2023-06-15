function onSelectBoxChange(discussionSelect, forumSelect, deleteForum, toggleSelectBox) {
    $(function () {
        $(discussionSelect).change(function () {
            $.post("/Topic/SelectDiscussionForums", { discussionId: $(this).val() }, function (value) {
                $(value).each(function () {
                    $(forumSelect).append(`<option value=${this.id}>${this.name}</option>`);
                })
            })
            deleteForum();
        })
    })
    toggleSelectBox();
}
