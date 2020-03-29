
function Pesquisar(url, paginaAtual) {
    var id = $('#Filtro_IdDespesa').val();
    var mesAno = $('#Filtro_MesAno').val();
    var cnpj = $('#cnpj').val();

    if (id == '') id = 0;

    $.get(url, { i: id, m: mesAno, c: cnpj, p: paginaAtual })
        .done(function (data) {
            $('#gridItems').html(data);
        }).fail(function (xhr, error, status) {
            var msg = JSON.parse(xhr.responseText);
            popupErro('SQG', msg.Message);
        });
}

$(function () {
    $("#Detalhe_MesAno").mask("99/9999");
    $("#cnpj").mask("99.999.999/9999-99");
    $("#Filtro_MesAno").mask("99/9999");
})

function RemoveMaskAndSubmit() {
    $('#cnpj').val($('#cnpj').val().replace('.', '').replace('.', '').replace('/', '').replace('-', ''));
    $('#formId').submit();
}
