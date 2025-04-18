﻿using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva;

public class TelaReserva
{
    public RepositorioReserva repositorioReserva;
    public RepositorioRevista repositorioRevista;
    public RepositorioAmigo repositorioAmigo;

    public TelaReserva(RepositorioReserva repositorioReserva, RepositorioRevista repositorioRevista, RepositorioAmigo repositorioAmigo)
    {
        this.repositorioReserva = repositorioReserva;
        this.repositorioRevista = repositorioRevista;
        this.repositorioAmigo = repositorioAmigo;
    }

    public string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine("1 - Criar Reserva");
        Console.WriteLine("2 - Cancelar Reserva");
        Console.WriteLine("3 - Visualizar Reservas");
        Console.WriteLine("S - Voltar ao Menu");
        Console.WriteLine();

        Console.Write("Digite uma opção válida: ");
        string opcaoEscolhida = Console.ReadLine()!.ToUpper();
        return opcaoEscolhida;
    }


    public void CriarReserva()
    {
        ExibirCabecalho();

        Console.WriteLine("Criando Reserva...");
        Console.WriteLine("---------------------------------");

        VisualizarAmigos();

        int idAmigo;
        bool idAmigoValido;
        do
        {
            Console.Write("Selecione o ID do amigo: ");
            idAmigoValido = int.TryParse(Console.ReadLine(), out idAmigo);

            if (!idAmigoValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idAmigoValido);

        Amigo amigo = repositorioAmigo.SelecionarPorId(idAmigo);

        if (amigo == null)
        {
            Notificador.ExibirMensagem($"Não existe Amigo com o id {idAmigo}!", ConsoleColor.Red);
            return;
        }

        if (amigo.temMulta)
        {
            Notificador.ExibirMensagem("O Amigo contém multas pendentes!", ConsoleColor.Red);
            return;
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

        Revista revista = repositorioRevista.SelecionarPorId(idRevista);

        if (revista == null)
        {
            Notificador.ExibirMensagem($"Não existe Revista com o id {idRevista}!", ConsoleColor.Red);
            return;
        }

        if (revista.status == StatusRevista.Emprestada || revista.status == StatusRevista.Reservada)
        {
            Notificador.ExibirMensagem("Revista não disponível!", ConsoleColor.Red);
            return;
        }


        Reserva novaReserva = new Reserva(amigo, revista);

        repositorioReserva.Inserir(novaReserva);

        Notificador.ExibirMensagem("Revista reservada com sucesso!", ConsoleColor.Green);

    }

    public void CancelarReserva()
    {
        ExibirCabecalho();

        Console.WriteLine("Cancelando Reserva...");
        Console.WriteLine("---------------------------------");

        Visualizar(false);

        int idReserva;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID da reserva que deseja cancelar: ");
            idValido = int.TryParse(Console.ReadLine(), out idReserva);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        Reserva reserva = repositorioReserva.SelecionarPorId(idReserva);

        if (reserva == null)
        {
            Notificador.ExibirMensagem($"Não existe Reserva com o id {idReserva}!", ConsoleColor.Red);
            return;
        }

        if (reserva.Revista.status == StatusRevista.Emprestada)
        {
            Notificador.ExibirMensagem($"A revista da reserva está emprestada no momento!", ConsoleColor.Red);
            return;
        }

        reserva.Revista.Devolver();
        reserva.Status = StatusReserva.Concluída;

        Notificador.ExibirMensagem("Revista devolvida com sucesso!", ConsoleColor.Green);
    }

    public void Visualizar(bool exibirTitulo)
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

        Reserva[] reservas = repositorioReserva.SelecionarTodos();

        foreach (Reserva r in reservas)
        {
            if (r == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -19} | {4, -15}",
            r.Id, r.Amigo.nome, r.Revista.titulo, r.DataReserva.ToShortDateString(), r.Status
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

        Revista[] revistas = repositorioRevista.SelecionarTodos();

        foreach (Revista r in revistas)
        {
            if (r == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -13} | {3, -19} | {4, -15} | {5, -15}",
            r.id, r.titulo, r.numeroEdicao, r.dataPublicacao.ToShortDateString(), r.status, r.caixa.etiqueta
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

        Amigo[] amigos = repositorioAmigo.SelecionarTodos();

        foreach (Amigo a in amigos)
        {
            if (a == null) continue;

            if (a.temMulta) Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -15} | {4, -15} | {5, -15}",
            a.id, a.nome, a.responsavel, a.telefone, a.ObterEmprestimos(), a.ObterMultas()
            );
            Console.ResetColor();
        }
    }


    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("|      Controle de Reservas     |");
        Console.WriteLine("---------------------------------");
        Console.WriteLine();
    }

}
