using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

public class TelaEmprestimo
{
    RepositorioEmprestimo repositorioEmprestimo;
    RepositorioAmigo repositorioAmigo;
    RepositorioRevista repositorioRevista;

    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista)
    {
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
    }
    public string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine("1 - Inserir Novo Empréstimo");
        Console.WriteLine("2 - Editar Empréstimo");
        Console.WriteLine("3 - Excluir Empréstimo");
        Console.WriteLine("4 - Visualizar Empréstimos");
        Console.WriteLine("S - Voltar ao Menu");
        Console.WriteLine();

        Console.Write("Digite uma opção válida: ");
        string opcaoEscolhida = Console.ReadLine()!.ToUpper();
        return opcaoEscolhida;
    }

    public void Inserir()
    {
        ExibirCabecalho();

        Console.WriteLine("Cadastrando Empréstimo...");
        Console.WriteLine("---------------------------------");

        Emprestimo novoEmprestimo = ObterDadosEmprestimo();

        if (novoEmprestimo == null) return;

        string erros = novoEmprestimo.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            Inserir();
            return;
        }

        bool verificacao = repositorioEmprestimo.VerificarEmprestimo(novoEmprestimo);

        if (verificacao)
        {
            Notificador.ExibirMensagem("O amigo selecionado já contém um empréstimo!", ConsoleColor.Red);
            return;
        }

        repositorioEmprestimo.Inserir(novoEmprestimo);
        Notificador.ExibirMensagem("Empréstimo adicionado com sucesso!", ConsoleColor.Green);
    }

    /*
    public void Editar()
    {
        ExibirCabecalho();

        Console.WriteLine("Editando Revista...");
        Console.WriteLine("---------------------------------");

        VisualizarTodos(false);

        int idRevista;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID da revista que deseja editar: ");
            idValido = int.TryParse(Console.ReadLine(), out idRevista);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        if (repositorioRevista.SelecionarPorId(idRevista) == null)
        {
            Notificador.ExibirMensagem($"Não existe Revista com o id {idRevista}!", ConsoleColor.Red);
            return;
        }

        Revista revistaEditada = ObterDadosCaixa(true);

        if (revistaEditada == null) return;

        string erros = revistaEditada.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            Inserir();
            return;
        }

        bool verificacao = repositorioRevista.VerificarTituloEdicao(revistaEditada.titulo, revistaEditada.numeroEdicao, idRevista);

        if (verificacao)
        {
            Notificador.ExibirMensagem("Já existe uma revista com o mesmo título e número de edição!", ConsoleColor.Red);
            return;
        }

        bool conseguiuEditar = repositorioRevista.Editar(idRevista, revistaEditada);

        if (!conseguiuEditar)
        {
            Notificador.ExibirMensagem("Erro ao editar Revista!", ConsoleColor.Red);
            return;
        }

        Notificador.ExibirMensagem("Revista editada com sucesso!", ConsoleColor.Green);

    }

    public void Excluir()
    {
        ExibirCabecalho();

        Console.WriteLine("Excluindo Revista...");
        Console.WriteLine("---------------------------------");

        VisualizarTodos(false);

        int idRevista;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID da revista que deseja excluir: ");
            idValido = int.TryParse(Console.ReadLine(), out idRevista);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        if (repositorioRevista.SelecionarPorId(idRevista) == null)
        {
            Notificador.ExibirMensagem($"Não existe Revista com o id {idRevista}!", ConsoleColor.Red);
            return;
        }

        bool conseguiuExcluir = repositorioRevista.Excluir(idRevista);

        if (!conseguiuExcluir)
        {
            Notificador.ExibirMensagem("Erro ao excluir Revista!", ConsoleColor.Red);
            return;
        }

        Notificador.ExibirMensagem("Revista excluída com sucesso!", ConsoleColor.Green);
    }

    */

    public void VisualizarTodos(bool exibirTitulo)
    {
        if (exibirTitulo)
        {
            ExibirCabecalho();

            Console.WriteLine("Visualizando Empréstimos...");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -15} | {5, -15}",
            "Id", "Amigo", "Revista", "Data de Abertura", "Data de Devolução", "Status"
        );

        Emprestimo[] emprestimos = repositorioEmprestimo.SelecionarTodos();

        foreach (Emprestimo e in emprestimos)
        {
            if (e == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -15} | {5, -15}",
            e.id, e.amigo.nome, e.revista.titulo, e.data.ToShortDateString(), e.ObterDataDevolucao(), e.status
            );
        }

        if (exibirTitulo) Console.ReadLine();
    }
    

    public void VisualizarAmigos()
    {
        Console.WriteLine();
        Console.WriteLine("Visualizando Amigos...");
        Console.WriteLine("---------------------------------");

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
    }
    
    public void VisualizarRevistas()
    {
        Console.WriteLine();
        Console.WriteLine("Visualizando Revistas...");
        Console.WriteLine("---------------------------------");

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -10} | {3, -15} | {4, -15} | {5, -15}",
            "Id", "Título", "Num. Edição", "Ano de Publicação", "Status", "Caixa"
        );

        Revista[] revistas = repositorioRevista.SelecionarTodos();

        foreach (Revista r in revistas)
        {
            if (r == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -10} | {3, -15} | {4, -15} | {5, -15}",
            r.id, r.titulo, r.numeroEdicao, r.dataPublicacao, r.status, r.caixa.etiqueta
            );
        }
    }

    public Emprestimo ObterDadosEmprestimo()
    {
        VisualizarAmigos();

        int idAmigo;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID do amigo: ");
            idValido = int.TryParse(Console.ReadLine(), out idAmigo);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        Amigo amigo = repositorioAmigo.SelecionarPorId(idAmigo);

        if (amigo == null)
        {
            Notificador.ExibirMensagem($"Não existe Amigo com o id {idAmigo}!", ConsoleColor.Red);
            return null;
        }

        VisualizarRevistas();

        int idRevista;
        do
        {
            Console.Write("Selecione o ID da revista: ");
            idValido = int.TryParse(Console.ReadLine(), out idRevista);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        Revista revista = repositorioRevista.SelecionarPorId(idRevista);

        if (revista == null)
        {
            Notificador.ExibirMensagem($"Não existe Revista com o id {idRevista}!", ConsoleColor.Red);
            return null;
        }

        Emprestimo novoEmprestimo = new Emprestimo(amigo, revista);

        return novoEmprestimo;
    }
    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("|     Controle de Empréstimo    |");
        Console.WriteLine("---------------------------------");
        Console.WriteLine();
    }


}
