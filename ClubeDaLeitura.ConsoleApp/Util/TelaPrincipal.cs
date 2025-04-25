using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.Util;

public class TelaPrincipal
{
    private RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
    private RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
    private RepositorioRevista repositorioRevista = new RepositorioRevista();
    private RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();
    private RepositorioMulta repositorioMulta = new RepositorioMulta();
    private RepositorioReserva repositorioReserva = new RepositorioReserva();
    private string opcaoPrincipal;

    public void ApresentarMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("|        Clube da Leitura       |");
        Console.WriteLine("---------------------------------");
        Console.WriteLine();

        Console.WriteLine("1 - Controle de Amigos");
        Console.WriteLine("2 - Controle de Caixas");
        Console.WriteLine("3 - Controle de Revistas");
        Console.WriteLine("4 - Controle de Empréstimos");
        Console.WriteLine("5 - Controle de Reservas");
        Console.WriteLine("6 - Controle de Multas");
        Console.WriteLine("S - Sair do Programa");
        Console.WriteLine();

        Console.Write("Digite uma opção válida: ");
        opcaoPrincipal = Console.ReadLine()!.ToUpper();
    }

    public ITelaCrud ObterTela()
    {
        if (opcaoPrincipal == "1")
            return new TelaAmigo(repositorioAmigo);
        else if (opcaoPrincipal == "2")
            return new TelaCaixa(repositorioCaixa);
        else if (opcaoPrincipal == "3")
            return new TelaRevista(repositorioRevista, repositorioCaixa);
        else if (opcaoPrincipal == "4")
            return new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista, repositorioMulta, repositorioReserva);
        else if (opcaoPrincipal == "5")
            return new TelaReserva(repositorioReserva, repositorioRevista, repositorioAmigo);
        else if (opcaoPrincipal == "6")
            return new TelaMulta(repositorioMulta);

        return null;
    }
}
