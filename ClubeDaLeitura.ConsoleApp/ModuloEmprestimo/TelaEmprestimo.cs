using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ClubeDaLeitura.ConsoleApp.Util;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

public class TelaEmprestimo : TelaBase
{
    RepositorioEmprestimo repositorioEmprestimo;
    RepositorioAmigo repositorioAmigo;
    RepositorioRevista repositorioRevista;
    RepositorioMulta repositorioMulta;
    RepositorioReserva repositorioReserva;

    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, 
        RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista, 
        RepositorioMulta repositorioMulta, RepositorioReserva repositorioReserva) 
        : base("Empréstimo", repositorioEmprestimo)
    {
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
        this.repositorioMulta = repositorioMulta;
        this.repositorioReserva = repositorioReserva;
    }
    public override void MostrarOpcoes()
    {
        Console.WriteLine("1 - Inserir Novo Empréstimo");
        Console.WriteLine("2 - Editar Empréstimo");
        Console.WriteLine("3 - Excluir Empréstimo");
        Console.WriteLine("4 - Visualizar Empréstimos");
        Console.WriteLine("5 - Devolução Empréstimo");
        Console.WriteLine("S - Voltar ao Menu");
    }

    public override bool ValidarInserirEditar(EntidadeBase registroEditado, int idRegistro = -1)
    {
        Emprestimo novoEmprestimo = (Emprestimo)registroEditado;

        if (novoEmprestimo.Status == StatusEmprestimo.Fechado)
        {
            Notificador.ExibirMensagem("Edição de Empréstimo teve falha!", ConsoleColor.Red);
            return false;
        }


        bool verificacao = repositorioEmprestimo.VerificarEmprestimo(novoEmprestimo, idRegistro);

        if (verificacao)
        {
            Notificador.ExibirMensagem("O amigo selecionado já contém um empréstimo!", ConsoleColor.Red);
            return false;
        }

        return true;
    }

    public void RegistrarDevolucao()
    {
        ExibirCabecalho();

        Console.WriteLine("Devolução Empréstimo...");
        Console.WriteLine("---------------------------------");

        VisualizarRegistros(false);

        int idEmprestimo;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID da Empréstimo que deseja fazer a devolução: ");
            idValido = int.TryParse(Console.ReadLine(), out idEmprestimo);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        Emprestimo emprestimo = (Emprestimo)repositorioEmprestimo.SelecionarRegistroPorId(idEmprestimo);

        if (emprestimo == null)
        {
            Notificador.ExibirMensagem($"Não existe Empréstimo com o id {idEmprestimo}!", ConsoleColor.Red);
            return;
        }

        if (emprestimo.Status == StatusEmprestimo.Fechado)
        {
            Notificador.ExibirMensagem("Devolução de Empréstimo teve falha!", ConsoleColor.Red);
            return;
        }

        if (emprestimo.Status == StatusEmprestimo.Atrasado)
        {
            Multa novaMulta = new Multa(emprestimo);
            repositorioMulta.Inserir(novaMulta);
        }

        Reserva reserva = repositorioReserva.SelecionarPorRevistaAmigo(emprestimo.Revista, emprestimo.Amigo);

        if (reserva != null) reserva.Status = StatusReserva.Concluída;

        emprestimo.RegistrarDevolucao();

        Notificador.ExibirMensagem("Devolução de Empréstimo concluída com sucesso!", ConsoleColor.Green);
    }


    public override void VisualizarRegistros(bool exibirTitulo)
    {
        if (exibirTitulo)
        {
            ExibirCabecalho();

            Console.WriteLine("Visualizando Empréstimos...");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -15} | {3, -17} | {4, -17} | {5, -15}",
            "Id", "Amigo", "Revista", "Data de Abertura", "Data de Devolução", "Status"
        );

        EntidadeBase[] registros = repositorioEmprestimo.SelecionarRegistros();
        Emprestimo[] emprestimos = new Emprestimo[registros.Length];

        for (int i = 0; i < registros.Length; i++)
            emprestimos[i] = (Emprestimo)registros[i];

        foreach (Emprestimo e in emprestimos)
        {
            if (e == null) continue;

            e.VerificarAtraso();

            if (e.Status == StatusEmprestimo.Atrasado) Console.ForegroundColor = ConsoleColor.Yellow;
            
            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -15} | {3, -17} | {4, -17} | {5, -15}",
                e.Id, e.Amigo.Nome, e.Revista.Titulo, e.Data.ToShortDateString(), e.DataDevolucao.ToShortDateString(), e.Status
            );
            
            Console.ResetColor();
        }

        if (exibirTitulo) Console.ReadLine();
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

        EntidadeBase[] registros = repositorioAmigo.SelecionarRegistros();
        Amigo[] amigos = new Amigo[registros.Length];

        for (int i = 0; i < registros.Length; i++)
            amigos[i] = (Amigo)registros[i];

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
    
    public void VisualizarRevistas()
    {
        Console.WriteLine();
        Console.WriteLine("Visualizando Revistas...");
        Console.WriteLine("---------------------------------");

        Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -13} | {3, -19} | {4, -15} | {5, -15}",
            "Id", "Título", "Num. Edição", "Ano de Publicação", "Status", "Caixa"
        );

        EntidadeBase[] registros = repositorioRevista.SelecionarRegistros();
        Revista[] revistas = new Revista[registros.Length];

        for (int i = 0; i < registros.Length; i++)
            revistas[i] = (Revista)registros[i];

        foreach (Revista r in revistas)
        {
            if (r == null) continue;

            Console.WriteLine(
            "{0, -10} | {1, -15} | {2, -13} | {3, -19} | {4, -15} | {5, -15}",
            r.Id, r.Titulo, r.NumeroEdicao, r.DataPublicacao.ToShortDateString(), r.Status, r.Caixa.Etiqueta
            );
        }
    }


    public override Emprestimo ObterDados(bool editarData)
    {
        VisualizarAmigos();

        int idAmigo;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID do amigo: ");
            idValido = int.TryParse(Console.ReadLine(), out idAmigo);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        Amigo amigo = (Amigo)repositorioAmigo.SelecionarRegistroPorId(idAmigo);

        if (amigo == null)
        {
            Notificador.ExibirMensagem($"Não existe Amigo com o id {idAmigo}!", ConsoleColor.Red);
            return null;
        }

        if (amigo.TemEmprestimo && !editarData)
        {
            Notificador.ExibirMensagem("O Amigo já tem Empréstimo!", ConsoleColor.Red);
            return null;
        }

        if (amigo.TemMulta)
        {
            Notificador.ExibirMensagem("O Amigo contém multas pendentes!", ConsoleColor.Red);
            return null;
        }

        VisualizarRevistas();

        int idRevista;
        do
        {
            Console.Write("Selecione o ID da revista: ");
            idValido = int.TryParse(Console.ReadLine(), out idRevista);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        Revista revista = (Revista)repositorioRevista.SelecionarRegistroPorId(idRevista);

        if (revista == null)
        {
            Notificador.ExibirMensagem($"Não existe Revista com o id {idRevista}!", ConsoleColor.Red);
            return null;
        }


        if (revista.Status == StatusRevista.Emprestada && !editarData)
        {
            Notificador.ExibirMensagem("A Revista não está disponível!", ConsoleColor.Red);
            return null;
        }

        else if (revista.Status == StatusRevista.Reservada)
        {
            bool verificacao = repositorioReserva.VerificarRevistaAmigo(revista, amigo);

            if (!verificacao)
            {
                Notificador.ExibirMensagem("A Revista não está disponível!", ConsoleColor.Red);
                return null;
            }
        }

        if (editarData)
        {
            DateTime dataEditada;
            bool dataValida;
            do
            {
                Console.Write("Digite a data do empréstimo: ");
                dataValida = DateTime.TryParse(Console.ReadLine(), out dataEditada);

                if (!dataValida) Notificador.ExibirMensagem("Data Inválida!", ConsoleColor.Red);
            } while (!dataValida);

            Emprestimo novoEmprestimo = new Emprestimo(amigo, revista, dataEditada);

            return novoEmprestimo;
        }
        else
        {
            Emprestimo novoEmprestimo = new Emprestimo(amigo, revista, DateTime.Now);

            return novoEmprestimo;
        }
    }
}
