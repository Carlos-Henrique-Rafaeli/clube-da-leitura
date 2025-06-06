﻿using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.Util;
using System.Collections;
using System.Drawing;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista;

public class TelaRevista : TelaBase<Revista>, ITelaCrud
{
    IRepositorioRevista repositorioRevista;
    IRepositorioCaixa repositorioCaixa;

    public TelaRevista(IRepositorioRevista repositorioRevista,
        IRepositorioCaixa repositorioCaixa) 
        : base("Revista", repositorioRevista)
    {
        this.repositorioRevista = repositorioRevista;
        this.repositorioCaixa = repositorioCaixa;
    }

    public override bool ValidarInserirEditar(Revista novaRevista, int idRegistro = -1)
    {
        bool verificacao = repositorioRevista.VerificarTituloEdicao(novaRevista.Titulo, novaRevista.NumeroEdicao);

        if (verificacao)
        {
            Notificador.ExibirMensagem("Já existe uma revista com o mesmo título e número de edição!", ConsoleColor.Red);
            return false;
        }
        return true;
    }

    public override bool ValidarExlcuir(Revista novaRevista, int idRegistro)
    {
        bool verificacao = novaRevista.Status == StatusRevista.Disponível;

        if (verificacao)
        {
            Notificador.ExibirMensagem("A revista não está disponível!", ConsoleColor.Red);
            return false;
        }
        return true;
    }

    public override void ExcluirExtras(Revista revista)
    {
        revista.Caixa.RemoverRevista();
    }

    public override void VisualizarRegistros(bool exibirTitulo)
    {
        if (exibirTitulo)
        {
            ExibirCabecalho();

            Console.WriteLine("Visualizando Revistas...");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -13} | {3, -19} | {4, -15} | {5, -15}",
            "Id", "Título", "Num. Edição", "Ano de Publicação", "Status", "Caixa"
        );

        List<Revista> registros = repositorioRevista.SelecionarRegistros();

        foreach (Revista r in registros)
        {
            if (r == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -13} | {3, -19} | {4, -15} | {5, -15}",
            r.Id, r.Titulo, r.NumeroEdicao, r.DataPublicacao.ToShortDateString(), r.Status, r.Caixa.Etiqueta
            );
        }

        if (exibirTitulo) Console.ReadLine();
    }

    public void VisualizarCaixas()
    {
        Console.WriteLine();
        Console.WriteLine("Visualizando Caixas...");
        Console.WriteLine("---------------------------------");

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -10} | {3, -15}",
            "Id", "Etiqueta", "Cor", "Dias de Empréstimo"
        );

        List<Caixa> registros = repositorioCaixa.SelecionarRegistros();

        foreach (Caixa c in registros)
        {
            if (c == null) continue;

            Console.ForegroundColor = GerenciadorDeCor.CorParaConsoleColor(c.Cor);

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -10} | {3, -15}",
            c.Id, c.Etiqueta, GerenciadorDeCor.CorParaString(c.Cor), c.DiasEmprestimo
            );
            Console.ResetColor();
        }
    }

    public override Revista ObterDados(bool validacaoExtra)
    {
        Console.Write("Digite o título da Revista: ");
        string titulo = Console.ReadLine()!;

        int numeroEdicao;
        bool edicaoValida;

        do
        {
            Console.Write($"Digite o número da edicão da Revista {titulo}: ");
            edicaoValida = int.TryParse(Console.ReadLine(), out numeroEdicao);

            if (!edicaoValida) Notificador.ExibirMensagem("Número inválido!", ConsoleColor.Red);

        } while (!edicaoValida);

        DateTime dataLancamento;
        bool dataValida;
        do
        {
            Console.Write($"Digite a data de lançamento da Revista {titulo}: ");
            dataValida = DateTime.TryParse(Console.ReadLine(), out dataLancamento);

            if (!dataValida) Notificador.ExibirMensagem("Data fora do padrão dd/mm/aaaa!", ConsoleColor.Red);

        } while (!dataValida);

        VisualizarCaixas();

        int idCaixa;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID da Caixa que deseja selecionar: ");
            idValido = int.TryParse(Console.ReadLine(), out idCaixa);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        Caixa caixa = (Caixa)repositorioCaixa.SelecionarRegistroPorId(idCaixa);

        if (caixa == null)
        {
            Notificador.ExibirMensagem($"Não existe Caixa com o id {idCaixa}!", ConsoleColor.Red);
            return null;
        }

        Revista revista = new Revista(titulo, numeroEdicao, dataLancamento, caixa);

        return revista;
    }
}
