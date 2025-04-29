using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.Util;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo;

public class TelaAmigo : TelaBase<Amigo>, ITelaCrud
{
    IRepositorioAmigo repositorioAmigo;

    public TelaAmigo(IRepositorioAmigo repositorioAmigo) : base("Amigo", repositorioAmigo)
    {
        this.repositorioAmigo = repositorioAmigo;
    }

    public override bool ValidarInserirEditar(Amigo amigoEditado, int idRegistro)
    {
        bool verificacao = repositorioAmigo.VerificarNomeTelefone(amigoEditado.Nome, amigoEditado.Telefone, idRegistro);

        if (verificacao)
        {
            Notificador.ExibirMensagem("Ja existe um Amigo com o mesmo nome e telefone!", ConsoleColor.Red);
            return false;
        }

        return true;
    }

    public override bool ValidarExlcuir(Amigo amigo, int idRegistro)
    {
        if (amigo.TemMulta || amigo.TemEmprestimo)
        {
            Notificador.ExibirMensagem($"Não foi possivel excluir amigo pois ele contém multas/empréstimos!", ConsoleColor.Red);
            return false;
        }
        
        return true;
    }

    public override void VisualizarRegistros(bool exibirTitulo)
    {
        if (exibirTitulo)
        {
            ExibirCabecalho();

            Console.WriteLine("Visualizando Amigos...");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -15} | {5, -15}",
            "Id", "Nome", "Responsável", "Telefone", "Empréstimo", "Multa"
        );

        List<Amigo> registros = repositorioAmigo.SelecionarRegistros();


        foreach (Amigo a in registros)
        {
            if (a == null) continue;

            if (a.TemMulta) Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -15} | {5, -15}",
            a.Id, a.Nome, a.Responsavel, a.Telefone, a.ObterEmprestimos(), a.ObterMultas()
            );
            Console.ResetColor();
        }

        if (exibirTitulo) Console.ReadLine();
    }


    public override Amigo ObterDados(bool validacaoExtra)
    {
        Console.Write("Digite o nome do Amigo: ");
        string nome = Console.ReadLine()!;

        Console.Write($"Digite o nome do Responsável de {nome}: ");
        string responsavel = Console.ReadLine()!;

        Console.Write($"Digite o telefone de {nome}: ");
        string telefone = Console.ReadLine()!;

        Amigo novoAmigo = new Amigo(nome, responsavel, telefone);

        return novoAmigo;
    }

}
