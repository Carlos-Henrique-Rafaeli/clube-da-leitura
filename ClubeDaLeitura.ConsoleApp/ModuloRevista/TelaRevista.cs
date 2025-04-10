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

    /*
    public void Editar()
    {
        ExibirCabecalho();

        Console.WriteLine("Editando Amigo...");
        Console.WriteLine("---------------------------------");

        Visualizar(false);

        int idCaixa;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID da caixa que deseja editar: ");
            idValido = int.TryParse(Console.ReadLine(), out idCaixa);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        if (repositorioCaixa.SelecionarPorId(idCaixa) == null)
        {
            Notificador.ExibirMensagem($"Não existe Caixa com o id {idCaixa}!", ConsoleColor.Red);
            return;
        }

        Caixa caixaEditada = ObterDadosCaixa();

        string erros = caixaEditada.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            Editar();
            return;
        }

        bool verificacao = repositorioCaixa.VerificarEtiqueta(caixaEditada.etiqueta, idCaixa);

        if (verificacao)
        {
            Notificador.ExibirMensagem("Já existe uma caixa com a mesma etiqueta!", ConsoleColor.Red);
            return;
        }

        bool conseguiuEditar = repositorioCaixa.Editar(idCaixa, caixaEditada);

        if (!conseguiuEditar)
        {
            Notificador.ExibirMensagem("Erro ao editar Caixa!", ConsoleColor.Red);
            return;
        }

        Notificador.ExibirMensagem("Caixa editada com sucesso!", ConsoleColor.Green);

    }

    public void Excluir()
    {
        ExibirCabecalho();

        Console.WriteLine("Excluindo Caixa...");
        Console.WriteLine("---------------------------------");

        Visualizar(false);

        int idCaixa;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID da caixa que deseja excluir: ");
            idValido = int.TryParse(Console.ReadLine(), out idCaixa);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        if (repositorioCaixa.SelecionarPorId(idCaixa) == null)
        {
            Notificador.ExibirMensagem($"Não existe Caixa com o id {idCaixa}!", ConsoleColor.Red);
            return;
        }

        bool conseguiuExcluir = repositorioCaixa.Excluir(idCaixa);

        if (!conseguiuExcluir)
        {
            Notificador.ExibirMensagem("Erro ao excluir Caixa!", ConsoleColor.Red);
            return;
        }

        Notificador.ExibirMensagem("Caixa excluída com sucesso!", ConsoleColor.Green);
    }

    public void Visualizar(bool exibirTitulo)
    {
        if (exibirTitulo)
        {
            ExibirCabecalho();

            Console.WriteLine("Visualizando Caixas...");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -10} | {3, -15}",
            "Id", "Etiqueta", "Cor", "Dias de Empréstimo"
        );

        Caixa[] caixas = repositorioCaixa.SelecionarTodos();

        foreach (Caixa c in caixas)
        {
            if (c == null) continue;

            Console.ForegroundColor = CorParaConsoleColor(c.cor);

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -10} | {3, -15}",
            c.id, c.etiqueta, CorParaString(c.cor), c.diasEmprestimo
            );
            Console.ResetColor();
        }

        if (exibirTitulo) Console.ReadLine();
    }
    */

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

        if (repositorioCaixa.SelecionarPorId(idCaixa) == null)
        {
            Notificador.ExibirMensagem($"Não existe Caixa com o id {idCaixa}!", ConsoleColor.Red);
            return null;
        }

        Caixa caixa = repositorioCaixa.SelecionarPorId(idCaixa);

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
}
