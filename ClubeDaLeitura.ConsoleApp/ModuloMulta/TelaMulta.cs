
using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloMulta;

public class TelaMulta
{
    
    public RepositorioMulta repositorioMulta;

    public TelaMulta(RepositorioMulta repositorioMulta)
    {
        this.repositorioMulta = repositorioMulta;
    }

    public string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine("1 - Visualizar Multas");
        Console.WriteLine("2 - Quitar Multa");
        Console.WriteLine("S - Voltar ao Menu");
        Console.WriteLine();

        Console.Write("Digite uma opção válida: ");
        string opcaoEscolhida = Console.ReadLine()!.ToUpper();
        return opcaoEscolhida;
    }


    public void Visualizar(bool exibirTitulo)
    {
        if (exibirTitulo)
        {
            ExibirCabecalho();

            Console.WriteLine("Visualizando Multas...");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15}",
            "Id", "Amigo", "Valor", "Status"
        );

        Multa[] multas = repositorioMulta.SelecionarTodos();

        foreach (Multa m in multas)
        {
            if (m == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15}",
            m.id, m.emprestimo.amigo.nome, m.valorMulta.ToString("C2"), m.status
            );
        }

        if (exibirTitulo) Console.ReadLine();
    }


    public void QuitarMulta()
    {
        ExibirCabecalho();

        Console.WriteLine("Quitando Multa...");
        Console.WriteLine("---------------------------------");

        Visualizar(false);

        int idMulta;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID da multa que deseja quitar: ");
            idValido = int.TryParse(Console.ReadLine(), out idMulta);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        Multa multa = repositorioMulta.SelecionarPorId(idMulta);

        if (multa == null)
        {
            Notificador.ExibirMensagem($"Não existe Multa com o id {idMulta}!", ConsoleColor.Red);
            return;
        }

        if (multa.status == StatusMulta.Quitada)
        {
            Notificador.ExibirMensagem($"A multa já foi quitada!", ConsoleColor.Green);
            return;
        }


        multa.emprestimo.amigo.temMulta = false;
        multa.status = StatusMulta.Quitada;

        Notificador.ExibirMensagem("Multa quitada com sucesso!", ConsoleColor.Green);
    }



    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("|       Controle de Multas      |");
        Console.WriteLine("---------------------------------");
        Console.WriteLine();
    }
}
