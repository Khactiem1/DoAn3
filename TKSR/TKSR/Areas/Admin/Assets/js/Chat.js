document.addEventListener("DOMContentLoaded",function(){
    const $ = document.querySelector.bind(document);
    const $$ = document.querySelectorAll.bind(document);
    /* Chat */
    (function(){
        /* Chat fb */
        var today = new Date();
        var date = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds() + ' ' + today.getDate()+'-'+(today.getMonth()+1)+'-'+today.getFullYear();
        const chat__time = $('.chat__time');
        chat__time.innerHTML = date;
        const chat__send = $('.chat__send');
        const chat__input = $('.chat__input-content');
        chat__input.onkeydown = function(evt){
            if(evt.keyCode == 13){
                chatWindow = $('.chat__user');
                const chat__value = chat__input.value;
                if(chat__value != ''){
                chatWindow.innerHTML = chatWindow.innerHTML + `<div class="chat__history user__chat-history">
                                        <div class="send__logo">
                                        <img src="../assets/img/logo/logo.jpg" alt="">
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
        chat__send.onclick = function(){
            chatWindow = $('.chat__user');
            const chat__value = chat__input.value;
            if(chat__value != ''){
                chatWindow.innerHTML = chatWindow.innerHTML + `<div class="user__chat-history chat__history">
                                        <div class="send__logo">
                                        <img src="../assets/img/logo/logo.jpg" alt="">
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
    const user = $$('.user__content-items');
    const name__chat = $('.info__chat-items.name__chat span');
    user.forEach((items,index) => {
        items.onclick = function(){
            if(this.classList.contains('user__active') == false){
                const close__items = $('.user__content-items.user__active');
                close__items.classList.remove('user__active');
                this.classList.add('user__active');
                const name = this.querySelector('span')
                name__chat.innerHTML = name.textContent;
            }
        }
    })
    const open = $('.open__items');
    const close = $('.close__items');
    const items = $('.user.full__chat');
    open.onclick = function(){
        items.classList.add('user__bar-active');
        open.classList.add('hidden__icon');
        close.classList.remove('hidden__icon');
    }
    close.onclick = function(){
        items.classList.remove('user__bar-active');
        open.classList.remove('hidden__icon');
        close.classList.add('hidden__icon');
    }
},false)