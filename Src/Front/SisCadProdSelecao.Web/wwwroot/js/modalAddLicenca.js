// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function FillPlataformaSelect() {

    $.get("/Plataforma/List/", function (data) {
        if (data != null && data.length > 0) {

            $('#plataforma').empty();
            $('#planoVendas').empty();
            $('#vencimento').val('');
            $('#contaCorretora').val('');

            $('#plataforma').append('<option value="-1">Selecione</option>');

            data.forEach(function (plataforma, i) {
                $('#plataforma').append('<option value="' + plataforma.id + '"> ' + plataforma.nome + ' </option>');
            });
        }
    });

    $.get("/Corretora/List/", function (data) {
        if (data != null && data.length > 0) {

            $('#corretora').empty();

            $('#corretora').append('<option value="-1">Selecione</option>');

            data.forEach(function (corretora, i) {
                $('#corretora').append('<option value="' + corretora.id + '"> ' + corretora.nome + ' </option>');
            });
        }
    });

};


function ChengePlataforma() {

    var plataforma = $('#plataforma').val();
    $('#planoVendas').empty();
    $('#vencimento').val('');

    $.get("/Plataforma/GetPlanos/" + plataforma, function (data) {
        if (data != null && data.length > 0) {

            $('#planoVendas').empty();
            $('#planoVendas').append('<option value="-1">Selecione</option>');

            data.forEach(function (plano, i) {
                $('#planoVendas').append('<option value="' + plano.id + '"> ' + plano.nomeComValor + ' </option>');
            });
        }
    });

};

function ChengePlano() {

    var plano = $('#planoVendas').val();
    $('#vencimento').val('');

    $.get("/PlanoVenda/Get/" + plano, function (data) {
        if (data != null) {

            $('#vencimento').val(data.vencimento);

        }
    });

};

function AddLicenca() {
    
    var idPessoa = $('#id').val();
    var plataforma = $('#plataforma').val();
    var plano = $('#planoVendas').val();
    var contaCorretora = $('#contaCorretora').val();
    var corretora = $('#corretora').val();

    var divErroModal = $('#divErroModal');
    var divModal = $('#divModal');
    if (!this.ValidateModal(idPessoa, plataforma, plano, contaCorretora)) {

        divErroModal.css("display", "block");

    } else {
        divErroModal.css("display", "none");
    }

    var licenca = {
        'pessoa': idPessoa,
        'plataforma': plataforma,
        'planoVenda': plano,
        'contaCorretora': contaCorretora,
        'corretora': corretora
    }

    var redireciona = false;

    $.post("/Licenca/AddToCliente", licenca, function () {
    })
        .done(function (data) {
            //alert(data.mensagem);
            if (data.status == "ok") {
                window.location.replace("/Cliente/Edit/" + idPessoa);
            } else {
                alert(data.erro);
            }
            
        })
        .fail(function (data) {
            alert("Erro não mapeado");
        });
     
};

function ValidateModal(idPessoa, plataforma, plano, contaCorretora) {
    var idPessoa = $('#id').val();
    var plataforma = $('#plataforma').val();
    var plano = $('#planoVendas').val();
    var contaCorretora = $('#contaCorretora').val();
    var corretora = $('#contaCorretora').val();

    if (plataforma == null || plataforma === "-1") return false;
    if (plano == null || plano === "-1") return false;
    if (corretora == null || corretora === "-1") return false;
    if (contaCorretora == null || contaCorretora === "") return false;

    return true;

}

function pageRedirect(idPessoa) {
    window.location.replace("/Cliente/Edit/" + idPessoa);
}