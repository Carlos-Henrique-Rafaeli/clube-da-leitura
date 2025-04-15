namespace ClubeDaLeitura.ConsoleApp.Compartilhado;

public static class TelaPrincipal
{
    public static string ApresentarMenu()
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
        string opcaoEscolhida = Console.ReadLine()!.ToUpper();
        return opcaoEscolhida;
    }
}
