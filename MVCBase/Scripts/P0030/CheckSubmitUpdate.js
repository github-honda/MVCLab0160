// submit前檢核 for jQuery Validation Unobtrusive 
$(function () {
    var formValidator,
        $form = $("#form1"); // 注意form id要符合!


    // add handler to the forms submit action
    $form.submit(function () {
        if (!formValidator) {
            formValidator = $form.validate({}); // Get existing jquery validate object
        }

        var errorList = [];

        // get existing summary errors from jQuery validate
        $.each(formValidator.errorList, function (index, errorListItem) {
            errorList.push(errorListItem.message);
        });

        // 在這裡增加你所需要的檢核.
        //if (!confirm("客戶端jQuery Validation Unobtrusive已檢核無誤, 是否繼續 ?")) {
        //    errorList.push("是否繼續 = 不繼續!");
        //}
        var ms1 = $('#ms1').val();
        var ms2 = $('#ms2').val();
        var mi1 = parseInt($('#mi1').val());
        var mi2 = parseInt($('#mi2').val());
        if ((mi1 < 30) && (mi2 < 30)) {
            errorList.push("國文與英文分數不可以都低於30分.")
        }


        // No errors, do nothing
        if (0 === errorList.length) {
            return confirm("確認送出嗎?"); // allow submit
        }

        // find summary div
        var $summary = $form.find("[data-valmsg-summary=true]");

        // find the unordered list
        var $ul = $summary.find("ul");

        // Clear existing errors from DOM by removing all element from the list
        $ul.empty();

        // Add all errors to the list
        $.each(errorList, function (index, message) {
            $("<li />").html(message).appendTo($ul);
        });

        // Add the appropriate class to the summary div
        $summary.removeClass("validation-summary-valid")
            .addClass("validation-summary-errors");

        return false; // Block the submit
    });
});
