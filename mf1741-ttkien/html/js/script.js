$(document).ready(function () {
    $("body").click(function (e) {
        // ẩn các danh sách 
        let dropLists = $(".droplist")
        for (let dropList of dropLists) {
            $(dropList).hide()
        }

        $(".record-in-page").css("border-color", "var(--input-border-color)");
        $(this).children().css({ "transform": "rotate(0deg)" })

        $(".box-icon-arrow-down_blue").css("border", "none")
    })

    // hiển thị form
    $(".btn-add-emp").click(function (e) {
        e.stopPropagation()
        $(".form-view").show()
        $($(".emp-code").children()[1]).addClass("box-input-focus")
        $($($(".emp-code").children()[1]).children()[0]).focus()
    })

    // ẩn form
    $(".hide-form-view").click(function () {
        $(".form-view").hide()
    })

    // hiển thị danh sách chức năng sửa dữ liệu cho bảng
    $(".box-icon-arrow-down_blue").click(function (e) {
        e.stopPropagation()
        if ($($(this).children()[1]).css("display") == "none") {
            let boxIconTrue = $(".box-icon-true")
            $(boxIconTrue.children()[1]).hide()
            boxIconTrue.css("border", "none")
            boxIconTrue.removeClass("box-icon-true")

            $($(this).children()[1]).show()
            $(this).css("border", "1px solid var(--primary-btn-background-color)")
            $(this).addClass("box-icon-true")

            // ẩn danh sách chọn số bản ghi trên 1 trang
            $(".record-in-page").css("border-color", "var(--input-border-color)");
            $(".droplist-record").hide()
            $(this).children().css({ "transform": "rotate(0deg)" })
        }
        else {
            $($(this).children()[1]).hide()
            $(this).css("border", "none")
            $(this).removeClass("box-icon-true")
        }
    })

    // hiển thị lựa chọn số bản ghi trên một trang
    $(".box-icon-arrow-down").click(function (e) {
        e.stopPropagation()
        if ($(".record-in-page").siblings().css("display") == "none") {
            $(".record-in-page").css("border-color", "var(--primary-btn-background-color)");
            $(".droplist-record").show()
            $(this).children().css({ "transform": "rotate(180deg)" })

            // ẩn danh sách chức năng ở bảng
            let boxIconTrue = $(".box-icon-true")
            $(boxIconTrue.children()[1]).hide()
            boxIconTrue.css({ "border": "none" })
            boxIconTrue.removeClass("box-icon-true")
        }
        else {
            $(".record-in-page").css("border-color", "var(--input-border-color)");
            $(".droplist-record").hide()
            $(this).children().css({ "transform": "rotate(0deg)" })
        }
    })


    // lựa chọn giới tính
    $(".box-input-radio").click(function () {
        if ($(($(this).children()[1])).css("display") == "none") {
            let radioChecked = $(".input-radio-checked-true")
            radioChecked.removeClass("input-radio-checked-true")
            $(radioChecked.siblings()).hide()
            $(($(this).children()[0])).addClass("input-radio-checked-true")
            $(($(this).children()[1])).show()
        }
    })

    // hiển thị thông báo dữ liệu đã tồn tại
    $(".btn-store").click(function (e) {
        e.preventDefault()
        $(".notif-view").show()
    })

    // ẩn thông báo dữ liệu đã tồn tại
    $(".btn-agree").click(function (e) {
        e.stopPropagation()
        $(".notif-view").hide()
        $($(".emp-code").children()[1]).children().focus()
    })
    $(".close-notif").click(function (e) {
        e.stopPropagation()
        $(".notif-view").hide()
        $($(".emp-code").children()[1]).children().focus()
    })

    showInputError()
});

// hiện thông báo trường không được bỏ trống
function showInputError() {
    let labels = $(".label-required")
    for (let label of labels) {
        $($(label).siblings()[0]).children().hover(
            function () {
                let children = $($(label).siblings()[0]).children()
                for (let child of children) {
                    $(child).addClass("input-error")
                }
                console.log($($(label).siblings()[1]))
                $($(label).siblings()[1]).addClass("tag-visible")
            },
            function () {
                let children = $(label).siblings().children()
                for (let child of children) {
                    $(child).removeClass("input-error")
                }
                $($(label).siblings()[1]).removeClass("tag-visible")
            })
    }
}