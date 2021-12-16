document.addEventListener("DOMContentLoaded", function () {
    (function () {
        setInterval(function () {
            $.ajax({
                url: "/Home/GetTaiKhoan",
                method: "GET",
                data: "",
                contentType: "application/json",
                dataType: ""
            }).done(function (response) {
                if (response == "False" || response == "false") {
                    active__messageNotification('Hết hạn đăng nhập, Hãy đăng nhập lại', false);
                }
                else {
                    var monney = document.querySelectorAll('.auto-updateSodu');
                    monney[0].innerText = `${Number(response).toLocaleString("en")} Đ`;
                    monney[1].innerText = `${Number(response).toLocaleString("en")} Đ`;
                }
            }).fail(function () {
                active__messageNotification('Có lỗi xảy ra, thử lại sau', false);
            })
        }, 3000)
    })()
}, false)