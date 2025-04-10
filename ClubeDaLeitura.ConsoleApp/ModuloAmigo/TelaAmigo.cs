
using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo;

public class TelaAmigo
{
    RepositorioAmigo repositorioAmigo;

    public TelaAmigo(RepositorioAmigo repositorioAmigo)
    {
        this.repositorioAmigo = repositorioAmigo;
    }

    public string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine("1 - Inserir Novo Amigo");
        Console.WriteLine("2 - Editar Amigo");
        Console.WriteLine("3 - Excluir Amigo");
        Console.WriteLine("4 - Visualizar Amigos");
        Console.WriteLine("S - Voltar ao Menu");
        Console.WriteLine();

        Console.Write("Digite uma opção válida: ");
        string opcaoEscolhida = Console.ReadLine()!.ToUpper();
        return opcaoEscolhida;
    }


    public void Inserir()
    {
        ExibirCabecalho();

        Console.WriteLine("Cadastrando Amigo...");
        Console.WriteLine("---------------------------------");

        Amigo novoAmigo = ObterDadosAmigo();

        string erros = novoAmigo.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);

            Inserir();

            return;
        }

        bool verificacao = repositorioAmigo.VerificarNomeTelefone(novoAmigo.nome, novoAmigo.telefone);

        if (verificacao)
        {
            Notificador.ExibirMensagem("Ja existe um Amigo com o mesmo nome e telefone!", ConsoleColor.Red);
            return;
        }

        repositorioAmigo.Inserir(novoAmigo);
        Notificador.ExibirMensagem("Amigo adicionado com sucesso!", ConsoleColor.Green);

    }

    public void Editar()
    {

    }

    public void Excluir()
    {

    }

    public void VisualizarTodos()
    {

    }

    public void VisualizarEmprestimos()
    {

    }

    public Amigo ObterDadosAmigo()
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

    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("|       Controle de Amigos      |");
        Console.WriteLine("---------------------------------");
        Console.WriteLine();
    }
}
