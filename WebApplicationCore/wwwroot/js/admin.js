function add() {

    var text = document.getElementById("addIp");
    var value = text.value;

    var regex = new RegExp("^\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}$");

    if (!value.match(regex)) {
        alert("Неверный ввод IP");
        add.value = "error";
        return;
    }

    value = "add" + value;
    var add = document.getElementById("addbut");
    add.value = value;
}

function del() {

    var select = document.getElementById("deleteSelect");
    var option = select.options[select.selectedIndex].value;

    var add = document.getElementById("delbut");
    add.value = "delete|" + option;
}