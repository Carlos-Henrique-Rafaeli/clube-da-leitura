using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using System.Drawing;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista;

public class TelaRevista
{
    RepositorioRevista repositorioRevista;
    RepositorioCaixa repositorioCaixa;

    public TelaRevista(RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa)
    {
        this.repositorioRevista = repositorioRevista;
        this.repositorioCaixa = repositorioCaixa;
    }

    public string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine("1 - Inserir Nova Revista");
        Console.WriteLine("2 - Editar Revista");
        Console.WriteLine("3 - Excluir Revista");
        Console.WriteLine("4 - Visualizar Revistas");
        Console.WriteLine("5 - Reservar Revista");
        Console.WriteLine("S - Voltar ao Menu");
        Console.WriteLine();

        Console.Write("Digite uma opção válida: ");
        string opcaoEscolhida = Console.ReadLine()!.ToUpper();
        return opcaoEscolhida;
    }


    public void Inserir()
    {
        ExibirCabecalho();

        Console.WriteLine("Cadastrando Revista...");
        Console.WriteLine("---------------------------------");

        Revista novaRevista = ObterDadosCaixa();

        if (novaRevista == null) return;

        string erros = novaRevista.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            Inserir();
            return;
        }

        bool verificacao = repositorioRevista.VerificarTituloEdicao(novaRevista.titulo, novaRevista.numeroEdicao);

        if (verificacao)
        {
            Notificador.ExibirMensagem("Já existe uma revista com o mesmo título e número de edição!", ConsoleColor.Red);
            return;
        }

        repositorioRevista.Inserir(novaRevista);
        Notificador.ExibirMensagem("Revista adicionada com sucesso!", ConsoleColor.Green);
    }

    
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

        Revista revistaEditada = ObterDadosCaixa();

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
    

    public void Reservar()
    {
        ExibirCabecalho();

        Console.WriteLine("Reservando Revista...");
        Console.WriteLine("---------------------------------");

        VisualizarTodos(false);

        int idRevista;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID da revista que deseja reservar: ");
            idValido = int.TryParse(Console.ReadLine(), out idRevista);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        Revista revista = repositorioRevista.SelecionarPorId(idRevista);

        if (revista == null)
        {
            Notificador.ExibirMensagem($"Não existe Revista com o id {idRevista}!", ConsoleColor.Red);
            return;
        }

        if (revista.status == StatusRevista.Emprestada)
        {
            Notificador.ExibirMensagem("Revista não disponível!", ConsoleColor.Green);
            return;
        }

        else if (revista.status == StatusRevista.Reservada)
        {
            revista.Devolver();
            Notificador.ExibirMensagem("Revista devolvida com sucesso!", ConsoleColor.Green);
            return;
        }

        revista.Reservar();
        Notificador.ExibirMensagem("Revista reservada com sucesso!", ConsoleColor.Green);
    }


    public void VisualizarTodos(bool exibirTitulo)
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

        Revista[] revistas = repositorioRevista.SelecionarTodos();

        foreach (Revista r in revistas)
        {
            if (r == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -13} | {3, -19} | {4, -15} | {5, -15}",
            r.id, r.titulo, r.numeroEdicao, r.dataPublicacao.ToShortDateString(), r.status, r.caixa.etiqueta
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

        Caixa[] caixas = repositorioCaixa.SelecionarTodos();

        foreach (Caixa c in caixas)
        {
            if (c == null) continue;

            Console.ForegroundColor = GerenciadorDeCor.CorParaConsoleColor(c.cor);

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -10} | {3, -15}",
            c.id, c.etiqueta, GerenciadorDeCor.CorParaString(c.cor), c.diasEmprestimo
            );
            Console.ResetColor();
        }
    }

    public Revista ObterDadosCaixa()
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

        Caixa caixa = repositorioCaixa.SelecionarPorId(idCaixa);

        if (caixa == null)
        {
            Notificador.ExibirMensagem($"Não existe Caixa com o id {idCaixa}!", ConsoleColor.Red);
            return null;
        }
     
        Revista revista = new Revista(titulo, numeroEdicao, dataLancamento, caixa);

        return revista;
    }

    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("|      Controle de Revistas     |");
        Console.WriteLine("---------------------------------");
        Console.WriteLine();
    }


    public StatusRevista ExibirStatus()
    {
        Console.WriteLine();

        Console.WriteLine("1 - Disponível");
        Console.WriteLine("2 - Emprestada");
        Console.WriteLine("3 - Reservada");
        Console.WriteLine();

        int opcao;
        bool opcaoValida;
        do
        {
            Console.Write("Escolha o Status: ");
            opcaoValida = int.TryParse(Console.ReadLine(), out opcao);

            if (!opcaoValida || opcao < 1 || opcao > 3)
            {
                Notificador.ExibirMensagem("Opção Inválida!", ConsoleColor.Red);
                opcaoValida = false;
            }
        } while (!opcaoValida);


        return (StatusRevista)(opcao - 1);
    }
}
