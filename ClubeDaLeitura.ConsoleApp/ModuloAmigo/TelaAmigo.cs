using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.Util;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo;

public class TelaAmigo : TelaBase
{
    RepositorioAmigo repositorioAmigo;

    public TelaAmigo(RepositorioAmigo repositorioAmigo) : base("Amigo", repositorioAmigo)
    {
        this.repositorioAmigo = repositorioAmigo;
    }

    public override bool ValidarInserirEditar(EntidadeBase registroEditado, int idRegistro)
    {
        Amigo amigoEditado = (Amigo)registroEditado;

        bool verificacao = repositorioAmigo.VerificarNomeTelefone(amigoEditado.Nome, amigoEditado.Telefone, idRegistro);

        if (verificacao)
        {
            Notificador.ExibirMensagem("Ja existe um Amigo com o mesmo nome e telefone!", ConsoleColor.Red);
            return false;
        }

        return true;
    }

    public override bool ValidarExlcuir(EntidadeBase registro, int idRegistro)
    {
        Amigo amigo = (Amigo)registro;

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

        EntidadeBase[] registros = repositorioAmigo.SelecionarRegistros();
        Amigo[] amigos = new Amigo[registros.Length];

        for (int i = 0; i < registros.Length; i++)
            amigos[i] = (Amigo)registros[i];

        foreach (Amigo a in amigos)
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
