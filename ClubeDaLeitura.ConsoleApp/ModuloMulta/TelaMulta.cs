using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ClubeDaLeitura.ConsoleApp.Util;

namespace ClubeDaLeitura.ConsoleApp.ModuloMulta;

public class TelaMulta : TelaBase<Multa>, ITelaCrud
{
    public IRepositorioMulta repositorioMulta;

    public TelaMulta(IRepositorioMulta repositorioMulta) : base("Multa", repositorioMulta)
    {
        this.repositorioMulta = repositorioMulta;
    }

    public override void MostrarOpcoes()
    {
        Console.WriteLine("1 - Visualizar Multas");
        Console.WriteLine("2 - Quitar Multa");
        Console.WriteLine("S - Voltar ao Menu");
    }


    public override void VisualizarRegistros(bool exibirTitulo)
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

        List<Multa> multas = repositorioMulta.SelecionarRegistros();

        foreach (Multa m in multas)
        {
            if (m == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15}",
            m.Id, m.Emprestimo.Amigo.Nome, m.ValorMulta.ToString("C2"), m.Status
            );
        }

        if (exibirTitulo) Console.ReadLine();
    }


    public void QuitarMulta()
    {
        ExibirCabecalho();

        Console.WriteLine("Quitando Multa...");
        Console.WriteLine("---------------------------------");

        VisualizarRegistros(false);

        int idMulta;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID da multa que deseja quitar: ");
            idValido = int.TryParse(Console.ReadLine(), out idMulta);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        Multa multa = repositorioMulta.SelecionarRegistroPorId(idMulta);

        if (multa == null)
        {
            Notificador.ExibirMensagem($"Não existe Multa com o id {idMulta}!", ConsoleColor.Red);
            return;
        }

        if (multa.Status == StatusMulta.Quitada)
        {
            Notificador.ExibirMensagem($"A multa já foi quitada!", ConsoleColor.Green);
            return;
        }


        multa.Emprestimo.Amigo.TemMulta = false;
        multa.Status = StatusMulta.Quitada;

        Notificador.ExibirMensagem("Multa quitada com sucesso!", ConsoleColor.Green);
    }

    public override Multa ObterDados(bool validacaoExtra)
    {
        throw new NotImplementedException();
    }
}
