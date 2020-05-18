var MicroondasModulo = function () {

    var configuracaoHub;

    var conexaoIniciada = function () {
        
    }

    var inicioRapido = function () {

        $("#input-tempo").val(30);
        $("#input-potencia").val(8);
        $("#input-potencia").trigger("input");

        ligarMicroondas();
    }

    var ligarMicroondas = function () {

        var alimento = $("#input-alimento").val();
        var tempo = $("#input-tempo").val();
        var potencia = $("#input-potencia").val();
        var caractere = $("#input-caractere").val();

        if (campoVazio(alimento)) {
            alert("Preencha o campo alimento");
            return;
        }

        if (campoVazio(tempo)) {
            alert("Escolha o tempo de aquecimento");
            return;
        }

        if (campoVazio(potencia)) {
            alert("Escolha a potência");
            return;
        }

        if (tempo < 1 || tempo > (60 * 2)) {
            alert("Escolha um tempo entre 1 segundo e 2 minutos");
            return;
        }

        if (potencia < 1 || potencia > 10) {
            alert("Escolha uma potência entre 1 e 10");
            return;
        }

        $("#botao-iniciar-microondas").prop("disabled", true);
        $("#botao-inicio-rapido").prop("disabled", true);
        $("#loading-aquecendo").fadeIn("fast");

        configuracaoHub.server.ligarMicroondas(alimento, tempo, potencia, caractere);
    }

    var utilizarPrograma = function (event) {

        var li = $(event.currentTarget).parent();

        $("#input-tempo").val(li.attr("data-tempo"));
        $("#input-potencia").val(li.attr("data-potencia")).trigger("input");
        $("#input-caractere").val(li.attr("data-caractere"));
    }

    var inicializarModulo = function () {

        //Inicialização do SignalR
        configuracaoHub = $.connection.microondasHub;
        configuracaoHub.client.exibirProgressoAquecimento = exibirProgressoAquecimento;
        $.connection.hub.logging = true;
        $.connection.hub.start().done(conexaoIniciada);

        //Atribuindo eventos
        $("#botao-iniciar-microondas").click(ligarMicroondas);
        $("#botao-inicio-rapido").click(inicioRapido);
        $("body").on("click", ".botao-exibir-instrucoes", exibirInstrucoes);
        $("body").on("click", ".botao-utilizar-programa", utilizarPrograma);
        $("body").on("click", "#botao-novo-programa", exibirCadastroPrograma);
        $("body").on("click", "#botao-gravar-programa", gravarPrograma);

        $("body").on("input", "#input-potencia, #input-potencia-novo", function () {

            if ($(this).attr("id") == "input-potencia")
                $("#span-valor-potencia").html($(this).val());
            else
                $("#span-valor-potencia-novo").html($(this).val());
        });

        //Busca os programas pré-configurados
        buscarProgramas();

    }

    var exibirProgressoAquecimento = function (texto, terminou) {

        var resultado = $("#resultado-aquecimento");

        if (!terminou) {
            
            resultado.html(texto);
        }
        else {
            resultado.append("aquecida");

            $("#botao-iniciar-microondas").prop("disabled", false);
            $("#botao-inicio-rapido").prop("disabled", false);
            $("#loading-aquecendo").fadeOut("fast");
            $("#input-caractere").val("");
        }
    }

    var exibirInstrucoes = function (event) {

        var instrucoes = $(event.currentTarget).parent().attr("data-instrucoes");

        $("#modal-instrucoes .modal-body").html(instrucoes);
        $("#modal-instrucoes").modal("show");
    }

    var campoVazio = function (valor) {
        return valor === undefined || valor === null || valor === "";
    }

    var buscarProgramas = function () {

        $.ajax({
            url: caminhoBase + "Microondas/BuscarProgramas",
            type: "GET"

        })
        .done(function (data) {

            $("#programas-disponiveis").html(data);
            $('[data-toggle="tooltip"]').tooltip()

        })
        .fail(function (data) {

        });
    }

    var exibirCadastroPrograma = function () {

        $("#modal-cadastro-programa").modal("show");

    }

    var gravarPrograma = function () {

        var nome = $("#input-nome-novo").val();
        var instrucoes = $("#input-instrucoes-novo").val();
        var tempo = $("#input-tempo-novo").val();
        var potencia = $("#input-potencia-novo").val();
        var caractere = $("#input-caractere-novo").val();

        if (campoVazio(nome)) {
            alert("Preencha o nome");
            return;
        }

        if (campoVazio(instrucoes)) {
            alert("Preencha as instrucoes");
            return;
        }

        if (campoVazio(tempo)) {
            alert("Escolha o tempo de aquecimento");
            return;
        }

        if (campoVazio(potencia)) {
            alert("Escolha a potência");
            return;
        }

        if (campoVazio(caractere)) {
            alert("Preencha o caractere de aquecimento");
            return;
        }

        if (tempo < 1 || tempo > (60 * 2)) {
            alert("Escolha um tempo entre 1 segundo e 2 minutos");
            return;
        }

        if (potencia < 1 || potencia > 10) {
            alert("Escolha uma potência entre 1 e 10");
            return;
        }

        $.ajax({
            url: caminhoBase + "Microondas/GravarPrograma",
            type: "POST",
            data: {
                TempoAquecimento: tempo,
                Nome: nome,
                Potencia: potencia,
                CaractereAquecimento: caractere,
                Instrucoes: instrucoes
            }
        })
        .done(function (data) {

            alert(data.mensagem);
            $("#modal-cadastro-programa").off("hidden.bs.modal").on("hidden.bs.modal", function () {
                buscarProgramas();
            });
            $("#modal-cadastro-programa").modal("hide");
            

        })
        .fail(function (data) {
            alert("Erro ao gravar programa");
        });

    }

    return {
        inicializarModulo: inicializarModulo
    };

};


$(function () {

    var microondas = new MicroondasModulo();
    microondas.inicializarModulo();
  
});
