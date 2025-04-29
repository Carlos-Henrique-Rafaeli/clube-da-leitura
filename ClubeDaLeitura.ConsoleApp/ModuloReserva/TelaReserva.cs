using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ClubeDaLeitura.ConsoleApp.Util;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva;

public class TelaReserva : TelaBase<Reserva>, ITelaCrud
{
    public IRepositorioReserva repositorioReserva;
    public IRepositorioRevista repositorioRevista;
    public IRepositorioAmigo repositorioAmigo;

    public TelaReserva(IRepositorioReserva repositorioReserva,
        IRepositorioRevista repositorioRevista, IRepositorioAmigo repositorioAmigo) 
        : base("Reserva", repositorioReserva)
    {
        this.repositorioReserva = repositorioReserva;
        this.repositorioRevista = repositorioRevista;
        this.repositorioAmigo = repositorioAmigo;
    }

    public override void MostrarOpcoes()
    {
        Console.WriteLine("1 - Criar Reserva");
        Console.WriteLine("2 - Cancelar Reserva");
        Console.WriteLine("3 - Visualizar Reservas");
        Console.WriteLine("S - Voltar ao Menu");
    }


    public void CancelarReserva()
    {
        ExibirCabecalho();

        Console.WriteLine("Cancelando Reserva...");
        Console.WriteLine("---------------------------------");

        VisualizarRegistros(false);

        int idReserva;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID da reserva que deseja cancelar: ");
            idValido = int.TryParse(Console.ReadLine(), out idReserva);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        Reserva reserva = repositorioReserva.SelecionarRegistroPorId(idReserva);

        if (reserva == null)
        {
            Notificador.ExibirMensagem($"Não existe Reserva com o id {idReserva}!", ConsoleColor.Red);
            return;
        }

        if (reserva.Revista.Status == StatusRevista.Emprestada)
        {
            Notificador.ExibirMensagem($"A revista da reserva está emprestada no momento!", ConsoleColor.Red);
            return;
        }

        reserva.Revista.Devolver();
        reserva.Status = StatusReserva.Concluída;

        Notificador.ExibirMensagem("Revista devolvida com sucesso!", ConsoleColor.Green);
    }

    public override void VisualizarRegistros(bool exibirTitulo)
    {
        if (exibirTitulo)
        {
            ExibirCabecalho();

            Console.WriteLine("Visualizando Reservas...");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -19} | {4, -15}",
            "Id", "Amigo", "Revista", "Data da Reserva", "Status"
        );

        List<Reserva> reservas = repositorioReserva.SelecionarRegistros();

        foreach (Reserva r in reservas)
        {
            if (r == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -19} | {4, -15}",
            r.Id, r.Amigo.Nome, r.Revista.Titulo, r.DataReserva.ToShortDateString(), r.Status
            );
        }

        if (exibirTitulo) Console.ReadLine();
    }


    public void VisualizarRevistas()
    {
        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -13} | {3, -19} | {4, -15} | {5, -15}",
            "Id", "Título", "Num. Edição", "Ano de Publicação", "Status", "Caixa"
        );

        List<Revista> revistas = repositorioRevista.SelecionarRegistros();

        foreach (Revista r in revistas)
        {
            if (r == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -13} | {3, -19} | {4, -15} | {5, -15}",
            r.Id, r.Titulo, r.NumeroEdicao, r.DataPublicacao.ToShortDateString(), r.Status, r.Caixa.Etiqueta
            );
        }
    }


    public void VisualizarAmigos()
    {
        Console.WriteLine();
        Console.WriteLine("Visualizando Amigos...");
        Console.WriteLine("---------------------------------");

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -15} | {5, -15}",
            "Id", "Nome", "Responsável", "Telefone", "Empréstimo", "Multa"
        );

        List<Amigo> amigos = repositorioAmigo.SelecionarRegistros();

        foreach (Amigo a in amigos)
        {
            if (a == null) continue;

            if (a.TemMulta) Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -15} | {5, -15}",
            a.Id, a.Nome, a.Responsavel, a.Telefone, a.ObterEmprestimos(), a.ObterMultas()
            );
            Console.ResetColor();
        }
    }


    public override Reserva ObterDados(bool validacaoExtra)
    {
        VisualizarAmigos();

        int idAmigo;
        bool idAmigoValido;
        do
        {
            Console.Write("Selecione o ID do amigo: ");
            idAmigoValido = int.TryParse(Console.ReadLine(), out idAmigo);

            if (!idAmigoValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idAmigoValido);

        Amigo amigo = repositorioAmigo.SelecionarRegistroPorId(idAmigo);

        if (amigo == null)
        {
            Notificador.ExibirMensagem($"Não existe Amigo com o id {idAmigo}!", ConsoleColor.Red);
            return null;
        }

        if (amigo.TemMulta)
        {
            Notificador.ExibirMensagem("O Amigo contém multas pendentes!", ConsoleColor.Red);
            return null;
        }

        VisualizarRevistas();

        int idRevista;
        bool idRevistaValido;
        do
        {
            Console.Write("Selecione o ID da revista que deseja reservar: ");
            idRevistaValido = int.TryParse(Console.ReadLine(), out idRevista);

            if (!idRevistaValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idRevistaValido);

        Revista revista = repositorioRevista.SelecionarRegistroPorId(idRevista);

        if (revista == null)
        {
            Notificador.ExibirMensagem($"Não existe Revista com o id {idRevista}!", ConsoleColor.Red);
            return null;
        }

        if (revista.Status == StatusRevista.Emprestada || revista.Status == StatusRevista.Reservada)
        {
            Notificador.ExibirMensagem("Revista não disponível!", ConsoleColor.Red);
            return null;
        }

        Reserva novaReserva = new Reserva(amigo, revista);
        return novaReserva;
    }
}
