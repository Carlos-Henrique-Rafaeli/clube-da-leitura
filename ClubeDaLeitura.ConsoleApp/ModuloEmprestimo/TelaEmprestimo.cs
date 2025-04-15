using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

public class TelaEmprestimo
{
    RepositorioEmprestimo repositorioEmprestimo;
    RepositorioAmigo repositorioAmigo;
    RepositorioRevista repositorioRevista;
    RepositorioMulta repositorioMulta;
    RepositorioReserva repositorioReserva;

    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista, RepositorioMulta repositorioMulta, RepositorioReserva repositorioReserva)
    {
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
        this.repositorioMulta = repositorioMulta;
        this.repositorioReserva = repositorioReserva;
    }
    public string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine("1 - Inserir Novo Empréstimo");
        Console.WriteLine("2 - Editar Empréstimo");
        Console.WriteLine("3 - Excluir Empréstimo");
        Console.WriteLine("4 - Visualizar Empréstimos");
        Console.WriteLine("5 - Devolução Empréstimo");
        Console.WriteLine("S - Voltar ao Menu");
        Console.WriteLine();

        Console.Write("Digite uma opção válida: ");
        string opcaoEscolhida = Console.ReadLine()!.ToUpper();
        return opcaoEscolhida;
    }

    public void Inserir()
    {
        ExibirCabecalho();

        Console.WriteLine("Cadastrando Empréstimo...");
        Console.WriteLine("---------------------------------");

        Emprestimo novoEmprestimo = ObterDadosEmprestimo(false);

        if (novoEmprestimo == null) return;

        string erros = novoEmprestimo.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            Inserir();
            return;
        }

        bool verificacao = repositorioEmprestimo.VerificarEmprestimo(novoEmprestimo);

        if (verificacao)
        {
            Notificador.ExibirMensagem("O amigo selecionado já contém um empréstimo!", ConsoleColor.Red);
            return;
        }

        repositorioEmprestimo.Inserir(novoEmprestimo);
        Notificador.ExibirMensagem("Empréstimo adicionado com sucesso!", ConsoleColor.Green);
    }

    
    public void Editar()
    {
        ExibirCabecalho();

        Console.WriteLine("Editando Empréstimo...");
        Console.WriteLine("---------------------------------");

        VisualizarTodos(false);

        int idEmprestimo;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID do Empréstimo que deseja editar: ");
            idValido = int.TryParse(Console.ReadLine(), out idEmprestimo);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        Emprestimo emprestimo = repositorioEmprestimo.SelecionarPorId(idEmprestimo);

        if (emprestimo == null)
        {
            Notificador.ExibirMensagem($"Não existe Empréstimo com o id {idEmprestimo}!", ConsoleColor.Red);
            return;
        }

        if (emprestimo.status == StatusEmprestimo.Fechado)
        {
            Notificador.ExibirMensagem("Edição de Empréstimo teve falha!", ConsoleColor.Red);
            return;
        }


        Emprestimo emprestimoEditado = ObterDadosEmprestimo(true);

        if (emprestimoEditado == null) return;

        string erros = emprestimoEditado.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            Editar();
            return;
        }

        bool verificacao = repositorioEmprestimo.VerificarEmprestimo(emprestimoEditado, idEmprestimo);

        if (verificacao)
        {
            Notificador.ExibirMensagem("O amigo selecionado já contém um empréstimo!", ConsoleColor.Red);
            return;
        }

        bool conseguiuEditar = repositorioEmprestimo.Editar(idEmprestimo, emprestimoEditado);

        if (!conseguiuEditar)
        {
            Notificador.ExibirMensagem("Erro ao editar Empréstimo!", ConsoleColor.Red);
            return;
        }

        Notificador.ExibirMensagem("Empréstimo editado com sucesso!", ConsoleColor.Green);

    }
    
    public void Excluir()
    {
        ExibirCabecalho();

        Console.WriteLine("Excluindo Empréstimo...");
        Console.WriteLine("---------------------------------");

        VisualizarTodos(false);

        int idEmprestimo;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID da Empréstimo que deseja excluir: ");
            idValido = int.TryParse(Console.ReadLine(), out idEmprestimo);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        if (repositorioEmprestimo.SelecionarPorId(idEmprestimo) == null)
        {
            Notificador.ExibirMensagem($"Não existe Empréstimo com o id {idEmprestimo}!", ConsoleColor.Red);
            return;
        }

        bool conseguiuExcluir = repositorioEmprestimo.Excluir(idEmprestimo);

        if (!conseguiuExcluir)
        {
            Notificador.ExibirMensagem("Erro ao excluir Empréstimo!", ConsoleColor.Red);
            return;
        }

        Notificador.ExibirMensagem("Empréstimo excluída com sucesso!", ConsoleColor.Green);
    }


    public void RegistrarDevolucao()
    {
        ExibirCabecalho();

        Console.WriteLine("Devolução Empréstimo...");
        Console.WriteLine("---------------------------------");

        VisualizarTodos(false);

        int idEmprestimo;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID da Empréstimo que deseja fazer a devolução: ");
            idValido = int.TryParse(Console.ReadLine(), out idEmprestimo);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        Emprestimo emprestimo = repositorioEmprestimo.SelecionarPorId(idEmprestimo);

        if (emprestimo == null)
        {
            Notificador.ExibirMensagem($"Não existe Empréstimo com o id {idEmprestimo}!", ConsoleColor.Red);
            return;
        }

        if (emprestimo.status == StatusEmprestimo.Fechado)
        {
            Notificador.ExibirMensagem("Devolução de Empréstimo teve falha!", ConsoleColor.Red);
            return;
        }

        if (emprestimo.status == StatusEmprestimo.Atrasado)
        {
            Multa novaMulta = new Multa(emprestimo);
            repositorioMulta.Inserir(novaMulta);
        }

        Reserva reserva = repositorioReserva.SelecionarPorRevistaAmigo(emprestimo.revista, emprestimo.amigo);

        if (reserva != null) reserva.Status = StatusReserva.Concluída;

        emprestimo.RegistrarDevolucao();

        Notificador.ExibirMensagem("Devolução de Empréstimo concluída com sucesso!", ConsoleColor.Green);
    }


    public void VisualizarTodos(bool exibirTitulo)
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

        Emprestimo[] emprestimos = repositorioEmprestimo.SelecionarTodos();

        foreach (Emprestimo e in emprestimos)
        {
            if (e == null) continue;

            e.VerificarAtraso();

            if (e.status == StatusEmprestimo.Atrasado) Console.ForegroundColor = ConsoleColor.Yellow;
            
            Console.WriteLine(
                "{0, -10} | {1, -15} | {2, -15} | {3, -17} | {4, -17} | {5, -15}",
                e.id, e.amigo.nome, e.revista.titulo, e.data.ToShortDateString(), e.dataDevolucao.ToShortDateString(), e.status
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
    
    public void VisualizarRevistas()
    {
        Console.WriteLine();
        Console.WriteLine("Visualizando Revistas...");
        Console.WriteLine("---------------------------------");

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

    public Emprestimo ObterDadosEmprestimo(bool editarData)
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

        Amigo amigo = repositorioAmigo.SelecionarPorId(idAmigo);

        if (amigo == null)
        {
            Notificador.ExibirMensagem($"Não existe Amigo com o id {idAmigo}!", ConsoleColor.Red);
            return null;
        }

        if (amigo.temEmprestimo && !editarData)
        {
            Notificador.ExibirMensagem("O Amigo já tem Empréstimo!", ConsoleColor.Red);
            return null;
        }

        if (amigo.temMulta)
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

        Revista revista = repositorioRevista.SelecionarPorId(idRevista);

        if (revista == null)
        {
            Notificador.ExibirMensagem($"Não existe Revista com o id {idRevista}!", ConsoleColor.Red);
            return null;
        }


        if (revista.status == StatusRevista.Emprestada)
        {
            Notificador.ExibirMensagem("A Revista não está disponível!", ConsoleColor.Red);
            return null;
        }

        else if (revista.status == StatusRevista.Reservada)
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
    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("|     Controle de Empréstimo    |");
        Console.WriteLine("---------------------------------");
        Console.WriteLine();
    }


}
