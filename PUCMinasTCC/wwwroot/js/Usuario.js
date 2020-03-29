

function Pesquisar(url, paginaAtual) {
    var id = $('#Filtro_IdUsuario').val();
    var descricao = $('#Filtro_Nome').val();
    var idPerfilSistema = $('#Filtro_PerfilUsuario option:selected').val();
    var status = $('#Filtro_Status option:selected').val();

    if (id == '') id = 0;

    $.get(url, { i: id, d: descricao, idps: idPerfilSistema, s: status, p: paginaAtual })
        .done(function (data) {
            $('#gridItems').html(data);
        }).fail(function (xhr, error, status) {
            var msg = JSON.parse(xhr.responseText);
            popupErro('SQG', msg.Message);
        });
}
