
function Pesquisar(url, paginaAtual) {
    var id = $('#Filtro_IdNaoConformidade').val();
    var descricao = $('#Filtro_Descricao').val();
    var origem = $('#Filtro_OrigemNc option:selected').val();
    var status = $('#Filtro_Status option:selected').val();

    if (id == '') id = 0;

    $.get(url, { i: id, d: descricao, o: origem, s: status, p: paginaAtual })
        .done(function (data) {
            $('#gridItems').html(data);
        }).fail(function (xhr, error, status) {
            var msg = JSON.parse(xhr.responseText);
            popupErro('SQG', msg.Message);
        });
}