using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using System.Drawing;
using System.Runtime.ConstrainedExecution;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa;

public class TelaCaixa
{
    RepositorioCaixa repositorioCaixa;

    public TelaCaixa(RepositorioCaixa repositorioCaixa)
    {
        this.repositorioCaixa = repositorioCaixa;
    }

    public string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine("1 - Inserir Nova Caixa");
        Console.WriteLine("2 - Editar Caixa");
        Console.WriteLine("3 - Excluir Caixa");
        Console.WriteLine("4 - Visualizar Caixas");
        Console.WriteLine("S - Voltar ao Menu");
        Console.WriteLine();

        Console.Write("Digite uma opção válida: ");
        string opcaoEscolhida = Console.ReadLine()!.ToUpper();
        return opcaoEscolhida;
    }


    public void Inserir()
    {
        ExibirCabecalho();

        Console.WriteLine("Cadastrando Caixa...");
        Console.WriteLine("---------------------------------");

        Caixa novaCaixa = ObterDadosCaixa();

        string erros = novaCaixa.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            Inserir();
            return;
        }

        repositorioCaixa.Inserir(novaCaixa);
        Notificador.ExibirMensagem("Caixa adicionada com sucesso!", ConsoleColor.Green);

    }

    /*
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

        Amigo amigoEditado = ObterDadosCaixa();

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

        int idAmigo;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID do amigo que deseja excluir: ");
            idValido = int.TryParse(Console.ReadLine(), out idAmigo);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

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
    */
    public Caixa ObterDadosCaixa()
    {
        Console.Write("Digite a etiqueta da Caixa: ");
        string etiqueta = Console.ReadLine()!;

        string opcao = ExibirCores();
        Color cor;

        if (opcao == "1") cor = Color.Red;
        else if (opcao == "2") cor = Color.Green;
        else if (opcao == "3") cor = Color.Blue;
        else if (opcao == "4") cor = Color.Yellow;
        else cor = Color.White;
        
        int diasEmprestimos;
        bool diasValido;

        do
        {
            Console.Write("Digite os dias de empréstimos: ");
            diasValido = int.TryParse(Console.ReadLine(), out diasEmprestimos);

            if (!diasValido) Notificador.ExibirMensagem("Número inválido!", ConsoleColor.Red);

        } while (!diasValido);

        Caixa novaCaixa = new Caixa(etiqueta, cor, diasEmprestimos);

        return novaCaixa;
    }

    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("|       Controle de Caixas      |");
        Console.WriteLine("---------------------------------");
        Console.WriteLine();
    }

    public string ExibirCores()
    {
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("1 - Vermelho");
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("2 - Verde");
        
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("3 - Azul");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("4 - Amarelo");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("5 - Branco");

        Console.ResetColor();
        Console.WriteLine();

        Console.Write("Selecione a cor: ");
        string opcao = Console.ReadLine()!;

        return opcao;
    }
}
