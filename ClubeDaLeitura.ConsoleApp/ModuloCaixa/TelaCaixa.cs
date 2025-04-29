using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.Util;
using System.Collections;
using System.Drawing;
using System.Runtime.ConstrainedExecution;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa;

public class TelaCaixa : TelaBase<Caixa>, ITelaCrud
{
    IRepositorioCaixa repositorioCaixa;

    public TelaCaixa(IRepositorioCaixa repositorioCaixa) : base("Caixa", repositorioCaixa)
    {
        this.repositorioCaixa = repositorioCaixa;
    }

    public override bool ValidarInserirEditar(Caixa novaCaixa, int idRegistro = -1)
    {
        bool verificacao = repositorioCaixa.VerificarEtiqueta(novaCaixa.Etiqueta, idRegistro);

        if (verificacao)
        {
            Notificador.ExibirMensagem("Já existe uma caixa com a mesma etiqueta!", ConsoleColor.Red);
            return false;
        }

        return true;
    }

    public override bool ValidarExlcuir(Caixa caixa, int idRegistro)
    {
        if (caixa.Revistas > 0)
        {
            Notificador.ExibirMensagem($"Não foi possível excluir pois existem {caixa.Revistas} revista(s)!", ConsoleColor.Red);
            return false;
        }
        return true;
    }

    public override void VisualizarRegistros(bool exibirTitulo)
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

        if (exibirTitulo) Console.ReadLine();
    }
    
    public override Caixa ObterDados(bool validacaoExtra)
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
