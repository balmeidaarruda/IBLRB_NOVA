function MascaraTelefone(objeto) {
    if (document.getElementById(objeto.id).value.length == 0)
        document.getElementById(objeto.id).value = '(' + document.getElementById(objeto.id).value;

    if (document.getElementById(objeto.id).value.length == 3)
        document.getElementById(objeto.id).value = document.getElementById(objeto.id).value + ')';

    if (document.getElementById(objeto.id).value.length == 9)
        document.getElementById(objeto.id).value = document.getElementById(objeto.id).value + '-';
}
//"onkeypress", "return validaSomenteInteiros(this, event,6);"
function validaSomenteInteiros(objeto, event, tammax) {
    if (document.all) // Internet Explorer
        var tecla = event.keyCode;
    else //Outros Browsers
        var tecla = e.which;

    if (event.keyCode == 13) {
        event.keyCode = 9;
        return true;
    }

    if (tecla >= 48 && tecla <= 57 || tecla == 8 || tecla == 0) {
        var tam;
        var vr = document.getElementById(objeto.id).value;
        vr = vr.toString().replace(".", "");
        vr = vr.toString().replace("/", "");
        vr = vr.toString().replace(",", "");

        if (document.selection.createRange().text != '') {
            document.getElementById(objeto.id).value = '';
            return true;
        }

        if (vr.length + 1 <= tammax || tecla == 8 || tecla == 0) {
            return true;
        }
        return false;
    } else return false;
}

function MascaraCEP(objeto) {
    if (document.getElementById(objeto.id).value.length == 2)
        document.getElementById(objeto.id).value = document.getElementById(objeto.id).value + '.';

    if (document.getElementById(objeto.id).value.length == 6)
        document.getElementById(objeto.id).value = document.getElementById(objeto.id).value + '-';
}

function MascaraData(objeto, e) {
    if (document.all) // Internet Explorer
        var tecla = event.keyCode;
    else //Outros Browsers
        var tecla = e.which;

    if (tecla >= 47 && tecla < 58) { // numeros de 0 a 9 e "/"
        var data = document.getElementById(objeto.id).value;
        if (data.length == 10)
            return false;
        if (data.length == 2 || data.length == 5) {
            data += '/';
            document.getElementById(objeto.id).value = data;
        }
    } else if (tecla == 8 || tecla == 0) // Backspace, Delete e setas direcionais(para mover o cursor, apenas para FF)
        return true;
    else
        return false;
}

//function SomenteNumero(e){
// var tecla=(window.event)?event.keyCode:e.which;
// if((tecla>47 && tecla<58)) return true;
// else{
// if (tecla==8 || tecla==0) return true;
// else  return false;
// }
//}

function HabilitarSomenteNumeros() {
    if (window.event.keyCode >= 48 && window.event.keyCode <= 57)
        return true
    else
        window.event.keyCode = 0;
}

$(function datapic(obj) {
    $obj.datepicker({
        showOn: 'button',
        dateFormat: 'dd/mm/yy',
        buttonImageOnly: true,
        buttonImage: '../Images/calendar_24x24.png',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado', 'Domingo'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez']

    });
});