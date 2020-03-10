$(document).ready(function () {
    $('#IdEmpresaFiltro').change(onChangeEmpresa);
    $('#IdEmpresaPerfilFiltro').change(onChangeEmpresa);
    $('#IdSistemaFiltro').change(onChangeSistema);
})

function onChangeEmpresa() {
    var id = $(this).find('option:selected').val();
    $('#IdSistemaFiltro').load('/SICCA/Sistema/_SelectSistemas?idEmpresa=' + id);
    $('#IdPerfilFiltro').html('');
}

function onChangeSistema() {
    var id = $(this).find('option:selected').val();
    $('#IdPerfilFiltro').load('/SICCA/Perfil/_SelectPerfis?idSistemaEmp=' + id);
}

function Pesquisar(url, paginaAtual) {
    var id = $('#Filtro_IdUsuario').val();
    var descricao = $('#Filtro_Nome').val();
    var idPerfilSistema = $('#IdPerfilFiltro option:selected').val();
    var status = $('#Filtro_Status option:selected').val();

    if (id == '') id = 0;

    $.get(url, { i: id, d: descricao, idps: idPerfilSistema, s: status, p: paginaAtual })
        .done(function (data) {
            $('#gridItems').html(data);
        }).fail(function (xhr, error, status) {
            var msg = JSON.parse(xhr.responseText);
            popupErro('SICCA', msg.Message);
        });
}

function Vincular(url) {
    var idUsuario = $('#Detalhe_IdUsuario').val();
    var idEmpresa = $('#IdEmpresaFiltro option:selected').val();

    if (idEmpresa == undefined || idEmpresa == 0) {
        popupErro('SICCA', 'Selecione uma empresa para vincular!');
        return;
    }

    var model = {
        Empresa: {
            IdEmpresa: idEmpresa
        },
        Usuario: {
            IdUsuario: idUsuario
        },
        Status: 'Ativo'
    };

    $.post(url, model)
        .done(function (data) {
            $('#gridEmpresas').html(data);
            $('#IdEmpresaFiltro').change(onChangeEmpresa);
            $.get('/usuario/ListaPerfis?idUsuario=' + idUsuario)
                .done(function (result) {
                    $('#gridPerfis').html(result);
                    $('#IdEmpresaPerfilFiltro').change(onChangeEmpresa);
                    $('#IdSistemaFiltro').change(onChangeSistema);
                })
                .fail(function (xhr, error, status) {
                    var msg = JSON.parse(xhr.responseText);
                    popupErro('SICCA', msg.Message);
                });
        }).fail(function (xhr, error, status) {
            var msg = JSON.parse(xhr.responseText);
            popupErro('SICCA', msg.Message);
        });
}

function Desvincular(url, isUsuarioEmp) {
    var idUsuario = $('#Detalhe_IdUsuario').val();

    var model = {
        IdUsuarioEmp: isUsuarioEmp,
        Usuario: {
            IdUsuario: idUsuario
        },
        Status: 'Inativo'
    };

    $.post(url, model)
        .done(function (data) {
            $('#gridEmpresas').html(data);
            $('#IdEmpresaFiltro').change(onChangeEmpresa);
            $.get('/usuario/ListaPerfis?idUsuario=' + idUsuario)
                .done(function (result) {
                    $('#gridPerfis').html(result);
                    $('#IdEmpresaPerfilFiltro').change(onChangeEmpresa);
                    $('#IdSistemaFiltro').change(onChangeSistema);
                })
                .fail(function (xhr, error, status) {
                    var msg = JSON.parse(xhr.responseText);
                    popupErro('SICCA', msg.Message);
                });

        }).fail(function (xhr, error, status) {
            var msg = JSON.parse(xhr.responseText);
            popupErro('SICCA', msg.Message);
        });
}

function VincularPerfil(url) {
    var idUsuario = $('#Detalhe_IdUsuario').val();
    var idEmpresa = $('#IdEmpresaPerfilFiltro option:selected').val();
    var idPerfilSistema = $('#IdPerfilFiltro option:selected').val();

    if (idPerfilSistema == undefined || idPerfilSistema == 0) {
        popupErro('SICCA', 'Selecione um Perfil para vincular!');
        return;
    }

    var model = {
        PerfilSistema: {
            IdPerfilSistema: idPerfilSistema
        },
        UsuarioEmpresa: {
            Usuario: {
                IdUsuario: idUsuario
            },
            Empresa: {
                IdEmpresa: idEmpresa
            }
        },
        Status: 'Ativo'
    };

    $.post(url, model)
        .done(function (data) {
            $('#gridPerfis').html(data);
            $('#IdEmpresaPerfilFiltro').change(onChangeEmpresa);
            $('#IdSistemaFiltro').change(onChangeSistema);
        }).fail(function (xhr, error, status) {
            var msg = JSON.parse(xhr.responseText);
            popupErro('SICCA', msg.Message);
        });
}

function DesvincularPerfil(url, idPerfilUsuario) {
    var idUsuario = $('#Detalhe_IdUsuario').val();

    var model = {
        IdPerfilUsuario: idPerfilUsuario,
        UsuarioEmpresa: {
            Usuario: {
                IdUsuario: idUsuario
            }
        },
        Status: 'Inativo'
    };

    $.post(url, model)
        .done(function (data) {
            $('#gridPerfis').html(data);
            $('#IdEmpresaPerfilFiltro').change(onChangeEmpresa);
            $('#IdSistemaFiltro').change(onChangeSistema);
        }).fail(function (xhr, error, status) {
            var msg = JSON.parse(xhr.responseText);
            popupErro('SICCA', msg.Message);
        });
}

