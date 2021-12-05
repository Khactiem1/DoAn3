document.addEventListener("DOMContentLoaded",function(){
    const $ = document.querySelector.bind(document);
    const $$ = document.querySelectorAll.bind(document);
        const view__pass = $$('.PasswordField_iconButton__1LFBr');
        const element__parent = view__pass[0].parentElement.querySelector('.auth-form__input');;
        view__pass[0].onclick = function(){
            view__pass[0].classList.remove('view__active');
            view__pass[1].classList.add('view__active');
            element__parent.type = "text";
        }
        view__pass[1].onclick = function(){
            view__pass[1].classList.remove('view__active');
            view__pass[0].classList.add('view__active');
            element__parent.type = "password";
        }
},false)