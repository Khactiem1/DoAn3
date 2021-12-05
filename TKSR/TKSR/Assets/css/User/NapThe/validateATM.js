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
        console.log(input[6])
        var isFormValidNumber = true;
        var isFormValidCard = true;
        var isFormValidNumber2 = true;
        var isFormValidCard2 = true;
        input.forEach((blurs,index) =>{
            blurs.onblur = function(){
                if(index == 0){
                    function tests(){
                        return isNumber(input[0].value) && input[0].value >= 10000 ? undefined :  `Nhập số và nạp tối thiểu 10000đ`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[0].parentElement,mess);
                    }
                }
                else if(index == 2){
                    function tests(){
                        return isNumber(input[2].value) && input[2].value.length >= 11 ? undefined :  `Nhập số và nhập tối thiểu 11 ký tự`;
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
                        return input[3].value.length >= 5 ? undefined :  `Vui lòng nhập tối thiểu 5 kí tự`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[3].parentElement,mess);
                    }
                }
                else if(index == 5){
                    function tests(){
                        return input[5].value.length >= 4  ? undefined :  `Nhập tối thiểu 4 ký tự`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[5].parentElement,mess);
                    }
                }
                else if(index == 6){
                    function tests(){
                        return isNumber(input[6].value) && input[6].value >= 10000 ? undefined :  `Nhập số và nạp tối thiểu 10000đ`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[6].parentElement,mess);
                    }
                }
                else if(index == 9){
                    function tests(){
                        return input[9].value.length >= 4  ? undefined :  `Nhập tối thiểu 4 ký tự`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[9].parentElement,mess);
                    }
                }
            }
            blurs.oninput = function(){
                if(index == 0){
                    remove_oninput(input[0].parentElement);
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
                if(index == 5){
                    remove_oninput(input[5].parentElement);
                    isFormValidCard = true;
                }
                if(index == 6){
                    remove_oninput(input[6].parentElement);
                    isFormValidNumber2 = true;
                }
                if(index == 9){
                    remove_oninput(input[9].parentElement);
                    isFormValidCard2 = true;
                }
            }
        })
        const number = document.querySelector('.call__api-code');
        const card = document.querySelector('.call__api-done');
        const number2 = document.querySelector('.call__api-code2');
        const card2 = document.querySelector('.call__api-done2');
        number2.onclick = function(){
            for(var i = 0; i < input.length;i++){
                if(i == 6){
                    function tests(){
                        return isNumber(input[6].value) && input[6].value >= 10000 ? undefined :  `Nhập số và nạp tối thiểu 10000đ`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[6].parentElement,mess);
                        isFormValidNumber2 = false;
                    }
                }
            }
            if(isFormValidNumber2 == true){
            
            }
        }
        card2.onclick = function(){
            for(var i = 0; i < input.length;i++){
                if(i == 6){
                    function tests(){
                        return isNumber(input[6].value) && input[6].value >= 10000 ? undefined :  `Nhập số và nạp tối thiểu 10000đ`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[6].parentElement,mess);
                        isFormValidCard2 = false;
                    }
                }
                else if(i == 9){
                    function tests(){
                        return input[9].value.length >= 4  ? undefined :  `Nhập tối thiểu 4 ký tự`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[9].parentElement,mess);
                        isFormValidCard2 = false;
                    }
                }
            }
            if(isFormValidCard2 == true){
            
            }
        }
        number.onclick = function(){
            for(var i = 0; i < input.length;i++){
                if(i == 0){
                    function tests(){
                        return isNumber(input[0].value) && input[0].value >= 10000 ? undefined :  `Nhập số và nạp tối thiểu 10000đ`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[0].parentElement,mess);
                        isFormValidNumber = false;
                    }
                }
                else if(i == 2){
                    function tests(){
                        return isNumber(input[2].value) && input[2].value.length >= 11 ? undefined :  `Nhập số và nhập tối thiểu 11 ký tự`;
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
                        return input[3].value.length >= 5 ? undefined :  `Vui lòng nhập tối thiểu 5 kí tự`;
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
            if(isFormValidNumber == true){
            
            }
        }
        card.onclick = function(){
            for(var i = 0; i < input.length;i++){
                if(i == 0){
                    function tests(){
                        return isNumber(input[0].value) && input[0].value >= 10000 ? undefined :  `Nhập số và nạp tối thiểu 10000đ`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[0].parentElement,mess);
                        isFormValidCard = false;
                    }
                }
                else if(i == 2){
                    function tests(){
                        return isNumber(input[2].value) && input[2].value.length >= 11 ? undefined :  `Nhập số và nhập tối thiểu 11 ký tự`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[2].parentElement,mess);
                        isFormValidCard = false;
                    }
                }
                else if(i == 3){
                    function tests(){
                        return input[3].value.length >= 5 ? undefined :  `Vui lòng nhập tối thiểu 5 kí tự`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[3].parentElement,mess);
                        isFormValidCard = false;
                    }
                }
                else if(i == 5){
                    function tests(){
                        return input[5].value.length >= 4  ? undefined :  `Nhập tối thiểu 4 ký tự`;
                    }
                    if(tests() == null){
                    }
                    else{
                        var mess = tests();
                        add__blur(input[5].parentElement,mess);
                        isFormValidCard = false;
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