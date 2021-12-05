document.addEventListener("DOMContentLoaded",function(){
    const $ = document.querySelector.bind(document);
    const $$ = document.querySelectorAll.bind(document);
    (function(){
        const list__help = $$('.list__help-item');
        const list__card = $$('.help__content-item');
        list__help.forEach((help__item,index) => {
            help__item.onclick = function(){
                if(this.classList.contains('help__item-active') == false){
                    const help__close = $('.list__help-item.help__item-active');
                    help__close.classList.remove('help__item-active');
                    this.classList.add('help__item-active');
                    const card__close = $('.help__content-item.help__content-item__active');
                    card__close.classList.remove('help__content-item__active');
                    list__card[index].classList.add('help__content-item__active');
                }
            }
        })
    })();
    /* kích thước */
    (function(){
        const help__content = $$('.card__content');
        const width__ani = $$('.card__bug');
        const help__header = $$('.card__header');
        const card = $$('.card');
        help__content[0].style.height = width__ani[0].offsetHeight + 'px';
        const time = 300;
        setTimeout(function(){
            help__content[0].style.height = `fit-content`;
        },time)
        let status = true; // xử lý ng dùng click nhiều lần phá 
        help__header.forEach((header__click,index) => {
            header__click.onclick = function(){
                if(card[index].classList.contains('card__active') == false){
                    if(status == false){
                        return false;
                    }
                    status = false;
                    card[index].addEventListener('webkitTransitionEnd',function(){
                        status = true;
                    })
                    const card__close = $$('.help__content-item__active .card__active');
                    if(card__close.length > 0){
                        const slide__close = $('.help__content-item__active .card__active .card__content');
                        const ani = $('.help__content-item__active .card__active .card__content .card__bug');
                        slide__close.style.height = ani.offsetHeight + 'px';
                        setTimeout(function(){
                            slide__close.style.height = `0px`;
                            card__close[0].classList.remove('card__active');
                        },1)
                    }
                    setTimeout(function(){
                        card[index].classList.add('card__active');
                        let px = width__ani[index].offsetHeight;
                        help__content[index].style.height = px + 'px';
                    },1)
                    setTimeout(function(){
                        help__content[index].style.height = `fit-content`;
                    },time + 1)
                }
                else{   
                    if(status == false){
                        return false;
                    }
                    status = false;
                    card[index].addEventListener('webkitTransitionEnd',function(){
                        status = true;
                    })
                    const ani = $('.help__content-item__active .card__active .card__content .card__bug');
                    help__content[index].style.height = ani.offsetHeight + 'px';
                    setTimeout(function(){
                        card[index].classList.remove('card__active');
                        help__content[index].style.height = `0px`;
                    },1)
                }
            }
        })
    })();
    document.getElementById("files").onchange = function () {
        var reader = new FileReader();
    
        reader.onload = function (e) {
            // get loaded data and render thumbnail.
            document.getElementById("image").src = e.target.result;
        };
    
        // read the image file as a data URL.
        reader.readAsDataURL(this.files[0]);
    };
},false)