using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.Util;
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

        bool verificacao = repositorioCaixa.VerificarEtiqueta(novaCaixa.etiqueta);

        if (verificacao)
        {
            Notificador.ExibirMensagem("Já existe uma caixa com a mesma etiqueta!", ConsoleColor.Red);
            return;
        }

        repositorioCaixa.Inserir(novaCaixa);
        Notificador.ExibirMensagem("Caixa adicionada com sucesso!", ConsoleColor.Green);

    }

    
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

        Caixa caixa = repositorioCaixa.SelecionarPorId(idCaixa);

        if (caixa == null)
        {
            Notificador.ExibirMensagem($"Não existe Caixa com o id {idCaixa}!", ConsoleColor.Red);
            return;
        }


        if (caixa.revistas > 0)
        {
            Notificador.ExibirMensagem($"Não foi possível excluir pois existem {caixa.revistas} revista(s)!", ConsoleColor.Red);
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

            Console.ForegroundColor = GerenciadorDeCor.CorParaConsoleColor(c.cor);

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -10} | {3, -15}",
            c.id, c.etiqueta, GerenciadorDeCor.CorParaString(c.cor), c.diasEmprestimo
            );
            Console.ResetColor();
        }

        if (exibirTitulo) Console.ReadLine();
    }
    
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
