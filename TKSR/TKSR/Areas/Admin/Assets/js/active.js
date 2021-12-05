document.addEventListener("DOMContentLoaded",function(){
    const phanhoi__img = document.querySelectorAll('.img__phanhoi');
    const phanhoi__content = document.querySelectorAll('.content__chitiet')
    const close__img = document.querySelectorAll('.close__img');
    phanhoi__img.forEach((items,index) => {
        items.onclick = function(){
            if(phanhoi__content[index].classList.contains('content__chitiet-active') == false){
                phanhoi__content[index].classList.add('content__chitiet-active');
            }
        }
        close__img[index].onclick = function(){
            if(phanhoi__content[index].classList.contains('content__chitiet-active') == true){
                phanhoi__content[index].classList.remove('content__chitiet-active');
            }
        }
    })
},false)