function active__messageNotification(message, type) {
    var content__notification = document.querySelector('.content__notification');
    const toast = document.createElement('div');
    if (type == true) {
        toast.classList.add('new__notification');
        toast.classList.add('new__notification-success');
        toast.innerHTML = `<i class="fas fa-check-circle new__notification-icon new__notification-iconTrue"></i>
                                        <span>${message}</span>
                    <i class="fas fa-times new__notification-close"></i>`
        content__notification.appendChild(toast);
    }
    else if (type == false) {
        toast.classList.add('new__notification');
        toast.classList.add('new__notification-error');
        toast.innerHTML = `<i class="fas fa-exclamation-circle new__notification-icon new__notification-iconFalse"></i>
                                        <span>${message}</span>
                    <i class="fas fa-times new__notification-close"></i>`
        content__notification.appendChild(toast);
    }
    setTimeout(function () {
        var item = content__notification.querySelectorAll('.new__notification');
        item[item.length - 1].classList.add('new__notification-active');
    }, 50)
    const autoHide = setTimeout(function () {
        var item = content__notification.querySelector('.new__notification');
        item.classList.add('new__notification-hidden');
    }, 4000)

    const autoDelete = setTimeout(function () {
        var item = content__notification.querySelector('.new__notification');
        content__notification.removeChild(item);
    }, 4300)
    toast.onclick = function (e) {
        if (e.target.closest('.new__notification-close')) {
            content__notification.removeChild(toast);
            clearTimeout(autoHide);
            clearTimeout(autoDelete);
        }
    }
}
function active__Loading(check) {
    var load = document.querySelector('.loader');
    if (check == true) {
        load.classList.add('active');
    }
    else if (check == false) {
        load.classList.remove('active');
    }
}