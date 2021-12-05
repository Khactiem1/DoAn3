document.addEventListener("DOMContentLoaded",function(){
    /* Validate */
    (function(){
        const input = document.querySelectorAll('.select__input');
        function remove_oninput(element){
            var message = element.querySelector('.form-message');
            message.innerText = '';
            element.classList.remove('invalid');
        }
        function add__blur(element,messages){
            var message = element.querySelector('.form-message');
            message.innerText = messages;
            element.classList.add('invalid');
        }
        var isFormValidNumber = true;
        input.forEach((blurs,index) =>{
            blurs.onblur = function(){
                if(index == 1){
                    function tests(){
                        return isNumber(input[1].value) && input[1].value.length >= 11 ? undefined :  `Nhập số và nhập tối thiểu 11 ký tự`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[1].parentElement,mess);
                    }
                }
                else if(index == 2){
                    function tests(){
                        return input[2].value.length >= 6 ? undefined :  `nhập tối thiểu 6 ký tự`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[2].parentElement,mess);
                    }
                }
                else if(index == 3){
                    function tests(){
                        return input[3].value != 'Mệnh giá'? undefined :  `Vui lòng chọn mệnh giá`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[3].parentElement,mess);
                    }
                }
            }
            blurs.oninput = function(){
                if(index == 0){
                    remove_oninput(input[0].parentElement);
                    isFormValidNumber = true;
                }
                if(index == 1){
                    remove_oninput(input[1].parentElement);
                    isFormValidNumber = true;
                }
                if(index == 2){
                    remove_oninput(input[2].parentElement);
                    isFormValidNumber = true;
                }
                if(index == 3){
                    remove_oninput(input[3].parentElement);
                    isFormValidNumber = true;
                }
            }
        })
        const number = document.querySelector('.btn__card-buy');
        number.onclick = function(){
            for(var i = 0; i < input.length;i++){
                if(i == 1){
                    function tests(){
                        return isNumber(input[1].value) && input[1].value.length >= 11 ? undefined :  `Nhập số và nhập tối thiểu 11 ký tự`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[1].parentElement,mess);
                        isFormValidNumber = false;
                    }
                }
                else if(i == 2){
                    function tests(){
                        return input[2].value.length >= 6 ? undefined :  `nhập tối thiểu 6 ký tự`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[2].parentElement,mess);
                        isFormValidNumber = false;
                    }
                }
                else if(i == 3){
                    function tests(){
                        return input[3].value != 'Mệnh giá'? undefined :  `Vui lòng chọn mệnh giá`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[3].parentElement,mess);
                        isFormValidNumber = false;
                    }
                }
            }
            if(isFormValidCard == true){
            
            }
        }
        function isNumber(numbers) 
        {
            var regex = new RegExp('^[0-9]+$');
            return regex.test(numbers);
        }
    })();
},false)