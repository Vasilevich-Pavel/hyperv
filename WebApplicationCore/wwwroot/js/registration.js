function adminPage() {

    let pass1 = document.getElementById("pass1").value;
    let pass2 = document.getElementById("pass2").value;
    let login = document.getElementById("login").value;

    if (pass1 == "" || pass2 == "" || login == "") {
        alert("Введите данные");
        return;
    }

    if (pass1 == pass2) {

        var count = document.getElementById("exit").value;
        var stringCheckbox = "checkbox/" + login + "/" + pass1;

        for (var i = 0; i < count.toString(); i++) {

            var name = "checkbox" + i;
            var checkbox = document.getElementById(name);

            if (checkbox.checked) {
                stringCheckbox += checkbox.value;
            }
        }

        var button = document.getElementById('save');
        button.value = stringCheckbox;

        alert("Пользователь зарегистрирован");
    }

    else {
        alert("Пароли не совпадают");
    }
}

function exit() {

    var exit = document.getElementById("exit");
    exit.value = "exit";
}