
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


$(function () {
    $("#Detalhe_Ano").mask("9999");
    $("#cnpj").mask("99.999.999/9999-99");
    $("#Filtro_Ano").mask("9999");
})

function RemoveMaskAndSubmit() {
    $('#cnpj').val($('#cnpj').val().replace('.', '').replace('.', '').replace('/', '').replace('-', ''));
    $('#formId').submit();
}
