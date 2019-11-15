function send() {
    var login = document.getElementById("loginInput");
    var password = document.getElementById("passwordInput");

    if (login.value == "" || password.value == "") {
        alert("Неверно введены данные");
    }

    var button = document.getElementById("enter");
    button.value = login.value + "|" + password.value;
}

function exit() {
    window.close();
}
