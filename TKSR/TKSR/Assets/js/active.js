document.addEventListener("DOMContentLoaded", function () {
    const $ = document.querySelector.bind(document);
    const $$ = document.querySelectorAll.bind(document);
    /* login and register */
    (function () {
        const icon__login = $('.navbar__login');
        const modal = $('.modal');
        const auth__form = $$('.auth-form');
        const icon__close = $$('.auth-form__icon');
        const modal__overlay = $('.modal__overlay');
        const go__register = $$('.form__go-register');
        const ac__login = $$('.active__login');
        for (var i = 0; i < ac__login.length; i++) {
            ac__login[i].onclick = function () {
                modal.classList.add('modal__active');
                auth__form[0].classList.add('auth-form__active');
                nav__mobile.classList.remove('nav__mobile-active');
                nav_modal__mobile.classList.remove('modal__active');
            }
        }
        icon__login.onclick = function () {
            modal.classList.add('modal__active');
            auth__form[0].classList.add('auth-form__active');
        }
        for (var i = 0; i < icon__close.length; i++) {
            icon__close[i].onclick = function () {
                modal.classList.remove('modal__active');
                const tab__close = $('.auth-form.auth-form__active');
                tab__close.classList.remove('auth-form__active');
            }
        }
        modal__overlay.onclick = function () {
            modal.classList.remove('modal__active');
            const tab__close = $('.auth-form.auth-form__active');
            tab__close.classList.remove('auth-form__active');
        }
        for (var i = 0; i < go__register.length; i++) {
            go__register[i].onclick = function () {
                for (var j = 0; j < auth__form.length - 1; j++) {
                    auth__form[j].classList.toggle('auth-form__active');
                }
            }
        }

        /* slide navbar mobile */
        const nav__mobile_slideTop = $$('.nav__m-item__top');
        const nav__mobile__slideUp = $$('.nav__m-item__menu');
        const nav__mobile__menu = $$(".m-item__menu-list");
        nav__mobile_slideTop.forEach((nav, index) => {
            const count = nav__mobile__slideUp[index];
            nav.onclick = function () {
                if (this.classList.contains('nav_mobile-active') == true) {
                    this.classList.remove('nav_mobile-active');
                    count.style.height = `0`;
                }
                else {
                    this.classList.add('nav_mobile-active');
                    count.style.height = nav__mobile__menu[index].offsetHeight + "px";
                }
            }
        })
        const login = $('.login');
        const register = $('.register');
        const nav__mobile = $('.nav__mobile-container');
        const nav_iconClose_mobile = $('.auth-form__icon-mobile');
        const nav_modal__mobile = $('.nav-modal__mobile');
        const navbar__bar = $('.navbar__bar');
        const modal__overlay_mobile = $('.modal__overlay-mobile');
        const auth_form__login = $('.auth-form__login');
        const auth_form__register = $('.auth-form__register');
        navbar__bar.onclick = function () {
            nav__mobile.classList.add('nav__mobile-active');
            nav_modal__mobile.classList.add('modal__active');
        }
        nav_iconClose_mobile.onclick = function () {
            nav__mobile.classList.remove('nav__mobile-active');
            nav_modal__mobile.classList.remove('modal__active');
        }
        modal__overlay_mobile.onclick = function () {
            nav__mobile.classList.remove('nav__mobile-active');
            nav_modal__mobile.classList.remove('modal__active');
        }
        login.onclick = function () {
            nav__mobile.classList.remove('nav__mobile-active');
            nav_modal__mobile.classList.remove('modal__active');
            modal.classList.add('modal__active');
            auth_form__login.classList.add('auth-form__active');
        }
        register.onclick = function () {
            nav__mobile.classList.remove('nav__mobile-active');
            nav_modal__mobile.classList.remove('modal__active');
            modal.classList.add('modal__active');
            auth_form__register.classList.add('auth-form__active');
        }
    })();
    /* scroll */
    (function () {
        let status = true;
        let height = 1800;
        const scroll = $('.scroll');
        window.addEventListener('scroll', function () {
            if (window.pageYOffset >= height && status == true) {
                scroll.classList.add('scroll__active');
                status = false;
            }
            else if (window.pageYOffset <= height && status == false) {
                status = true;
                scroll.classList.remove('scroll__active');
            }
        })
    })();
}, false)