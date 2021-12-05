document.addEventListener("DOMContentLoaded",function(){
    const $ = document.querySelector.bind(document);
    const $$ = document.querySelectorAll.bind(document);
    const pay = $$('.pay');
    const container__pay = $('.container__pay');
    let width__container = 100 * pay.length;
    let width__pay = 100 / pay.length;
    container__pay.style.width = `${width__container}%`;
    for(let i = 0; i < pay.length; i++){
        pay[i].style.width = `${width__pay}%`;
    }
},false)