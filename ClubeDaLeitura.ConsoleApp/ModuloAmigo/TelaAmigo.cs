
using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

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
        ExibirCabecalho();

        Console.WriteLine("Editando Amigo...");
        Console.WriteLine("---------------------------------");

        VisualizarTodos(false);

        int idAmigo;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID do amigo que deseja editar: ");
            idValido = int.TryParse(Console.ReadLine(), out idAmigo);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        if (repositorioAmigo.SelecionarPorId(idAmigo) == null)
        {
            Notificador.ExibirMensagem($"Não existe Amigo com o id {idAmigo}!", ConsoleColor.Red);
            return;
        }

        Amigo amigoEditado = ObterDadosAmigo();

        string erros = amigoEditado.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);

            Editar();

            return;
        }

        bool verificacao = repositorioAmigo.VerificarNomeTelefone(amigoEditado.nome, amigoEditado.telefone, idAmigo);

        if (verificacao)
        {
            Notificador.ExibirMensagem("Ja existe um Amigo com o mesmo nome e telefone!", ConsoleColor.Red);
            return;
        }

        bool conseguiuEditar = repositorioAmigo.Editar(idAmigo, amigoEditado);

        if (!conseguiuEditar)
        {
            Notificador.ExibirMensagem("Erro ao editar Amigo!", ConsoleColor.Red);
            return;
        }
        
        Notificador.ExibirMensagem("Amigo editado com sucesso!", ConsoleColor.Green);

    }

    public void Excluir()
    {
        ExibirCabecalho();

        Console.WriteLine("Excluindo Amigo...");
        Console.WriteLine("---------------------------------");

        VisualizarTodos(false);

        int idAmigo;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID do amigo que deseja excluir: ");
            idValido = int.TryParse(Console.ReadLine(), out idAmigo);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        if (repositorioAmigo.SelecionarPorId(idAmigo) == null)
        {
            Notificador.ExibirMensagem($"Não existe Amigo com o id {idAmigo}!", ConsoleColor.Red);
            return;
        }

        bool conseguiuExcluir = repositorioAmigo.Excluir(idAmigo);

        if (!conseguiuExcluir)
        {
            Notificador.ExibirMensagem("Erro ao excluir Amigo!", ConsoleColor.Red);
            return;
        }

        Notificador.ExibirMensagem("Amigo excluído com sucesso!", ConsoleColor.Green);
    }

    public void VisualizarTodos(bool exibirTitulo)
    {
        if (exibirTitulo)
        {
            ExibirCabecalho();

            Console.WriteLine("Visualizando Amigos...");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -15}",
            "Id", "Nome", "Responsável", "Telefone", "Empréstimo"
        );

        Amigo[] amigos = repositorioAmigo.SelecionarTodos();

        foreach (Amigo a in amigos)
        {
            if (a == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -15}",
            a.id, a.nome, a.responsavel, a.telefone, "Empréstimo"
            );
        }

        if (exibirTitulo) Console.ReadLine();
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
