
function Pesquisar(url, paginaAtual) {
    var id = $('#Filtro_IdIncidente').val();
    var descricao = $('#Filtro_DescricaoIncidente').val();
    var idNaoConformidade = $('#IdNaoConformidadeFiltro option:selected').val();
    var estado = $('#Filtro_Estado option:selected').val();

    if (id == '') id = 0;

    $.get(url, { i: id, d: descricao, idps: idNaoConformidade, s: estado, p: paginaAtual })
        .done(function (data) {
            $('#gridItems').html(data);
        }).fail(function (xhr, error, status) {
            var msg = JSON.parse(xhr.responseText);
            popupErro('SQG', msg.Message);
        });
}