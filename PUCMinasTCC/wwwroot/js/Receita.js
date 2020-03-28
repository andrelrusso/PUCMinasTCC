
function Pesquisar(url, paginaAtual) {
    var id = $('#Filtro_IdReceita').val();
    var ano = $('#Filtro_Ano').val();
    var cnpj = $('#cnpj').val();

    if (id == '') id = 0;

    $.get(url, { i: id, m: ano, c: cnpj, p: paginaAtual })
        .done(function (data) {
            $('#gridItems').html(data);
        }).fail(function (xhr, error, status) {
            var msg = JSON.parse(xhr.responseText);
            popupErro('SQG', msg.Message);
        });
}

//$(document).ready(function () {
//        $("input[data-tipo=''cnpj'']").mask("00.000.000/0000-00");
//    $("input[data-tipo=''moeda'']").mask("000.000.000,00", {reverse: true });
//});

/*$(document).ready(function ($) {
    $('#cnpj').inputmask("+7(999)999-99-99");
    //$('#phonemaskb').inputmask("+7(999)999-99-99");
    $('#cnpj').mask("+7(999)999-99-99");
    //$('#phonemaskd').mask("+7(999)999-99-99");
});*/

$(function () {
    //$("#Detalhe_CNPJ").mask("99.999.999/9999-99");
    $("#Detalhe_Ano").mask("9999");
    $("#cnpj").mask("99.999.999/9999-99");
    $("#Filtro_Ano").mask("9999");
})

function RemoveMaskAndSubmit() {
    $('#cnpj').val($('#cnpj').val().replace('.', '').replace('.', '').replace('/', '').replace('-', ''));
    $('#formId').submit();
}
