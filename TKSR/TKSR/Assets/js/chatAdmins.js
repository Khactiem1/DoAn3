document.addEventListener("DOMContentLoaded", function () {
    const $ = document.querySelector.bind(document);
    const $$ = document.querySelectorAll.bind(document);
    /* Chat */
    (function () {
        /* Chat fb */
        const messenger = $('.icon__chat');
        const chat = $('.chat__container');
        const close = $('.chat__close');
        var today = new Date();
        var date = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds() + ' ' + today.getDate() + '-' + (today.getMonth() + 1) + '-' + today.getFullYear();
        const chat__time = $('.chat__time');
        chat__time.innerHTML = date;
        messenger.onclick = function () {
            chat.classList.toggle('chat__container-active');
        }
        close.onclick = function () {
            chat.classList.remove('chat__container-active');
        }
        const chat__send = $('.chat__send');
        const chat__input = $('.chat__input-content');
        chat__input.onkeydown = function (evt) {
            if (evt.keyCode == 13) {
                chatWindow = $('.chat__user');
                const chat__value = chat__input.value;
                if (chat__value != '') {
                    chatWindow.innerHTML = chatWindow.innerHTML + `<div class="user chat__history">
                                        <div class="send__logo">
                                            <i class="fas fa-user mess__icon"></i>
                                        </div>
                                        <span class="messenger__send">
                                            ${chat__value}
                                        </span>
                                    </div>`;
                    chat__input.value = '';
                    let xH = chatWindow.scrollHeight;
                    chatWindow.scrollTo(0, xH);
                }
            }
        }
        chat__send.onclick = function () {
            chatWindow = $('.chat__user');
            const chat__value = chat__input.value;
            if (chat__value != '') {
                chatWindow.innerHTML = chatWindow.innerHTML + `<div class="user chat__history">
                                        <div class="send__logo">
                                            <i class="fas fa-user mess__icon"></i>
                                        </div>
                                        <span class="messenger__send">
                                            ${chat__value}
                                        </span>
                                    </div>`;
                chat__input.value = '';
                let xH = chatWindow.scrollHeight;
                chatWindow.scrollTo(0, xH);
            }
        }
        chatWindow = $('.chat__user');
        let xH = chatWindow.scrollHeight;
        chatWindow.scrollTo(0, xH);
    })()
}, false)