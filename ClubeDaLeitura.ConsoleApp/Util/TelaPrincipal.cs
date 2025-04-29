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
    private RepositorioAmigo repositorioAmigo;
    private RepositorioCaixa repositorioCaixa;
    private RepositorioRevista repositorioRevista;
    private RepositorioEmprestimo repositorioEmprestimo;
    private RepositorioMulta repositorioMulta;
    private RepositorioReserva repositorioReserva;

    private ContextoDados contexto;
    
    public string OpcaoPrincipal { get; private set; }

    public TelaPrincipal()
    {
        this.contexto = new ContextoDados(true);
        this.repositorioAmigo = new RepositorioAmigo(contexto);
        this.repositorioCaixa = new RepositorioCaixa(contexto);
        this.repositorioRevista = new RepositorioRevista(contexto);
        this.repositorioEmprestimo = new RepositorioEmprestimo(contexto);
        this.repositorioMulta = new RepositorioMulta(contexto);
        this.repositorioReserva = new RepositorioReserva(contexto);
    }


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
        OpcaoPrincipal = Console.ReadLine()!.ToUpper();
    }

    public ITelaCrud ObterTela()
    {
        if (OpcaoPrincipal == "1")
            return new TelaAmigo(repositorioAmigo);
        else if (OpcaoPrincipal == "2")
            return new TelaCaixa(repositorioCaixa);
        else if (OpcaoPrincipal == "3")
            return new TelaRevista(repositorioRevista, repositorioCaixa);
        else if (OpcaoPrincipal == "4")
            return new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista, repositorioMulta, repositorioReserva);
        else if (OpcaoPrincipal == "5")
            return new TelaReserva(repositorioReserva, repositorioRevista, repositorioAmigo);
        else if (OpcaoPrincipal == "6")
            return new TelaMulta(repositorioMulta);

        return null;
    }
}
