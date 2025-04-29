using ClubeDaLeitura.ConsoleApp.Util;

namespace ClubeDaLeitura.ConsoleApp.Compartilhado;

public abstract class TelaBase<T> where T : EntidadeBase<T>
{
    protected string nomeEntidade;
    private IRepositorio<T> repositorio;

    protected TelaBase(string nomeEntidade, IRepositorio<T> repositorio)
    {
        this.nomeEntidade = nomeEntidade;
        this.repositorio = repositorio;
    }

    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine($"Controle de {nomeEntidade}s");
        Console.WriteLine("--------------------------------------------");
    }

    public string ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine();

        MostrarOpcoes();

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        string operacaoEscolhida = Console.ReadLine()!.ToUpper();

        return operacaoEscolhida;
    }


    public virtual void MostrarOpcoes()
    {
        Console.WriteLine($"1 - Cadastrar {nomeEntidade}");
        Console.WriteLine($"2 - Editar {nomeEntidade}");
        Console.WriteLine($"3 - Excluir {nomeEntidade}");
        Console.WriteLine($"4 - Visualizar {nomeEntidade}s");
        Console.WriteLine("S - Voltar");
    }



    public void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"Cadastrando {nomeEntidade}...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();

        T novoRegistro = ObterDados(false);

        if (novoRegistro == null) return;

        string erros = novoRegistro.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);

            CadastrarRegistro();

            return;
        }

        bool validacao = ValidarInserirEditar(novoRegistro);

        if (!validacao) return;

        repositorio.CadastrarRegistro(novoRegistro);

        Notificador.ExibirMensagem("O registro foi concluído com sucesso!", ConsoleColor.Green);
    }


    public void EditarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Editando {nomeEntidade}...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);

        int idRegistro;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID do registro que deseja editar: ");
            idValido = int.TryParse(Console.ReadLine(), out idRegistro);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);
        Console.WriteLine();

        T registroEditado = ObterDados(true);

        if (registroEditado == null) return;

        string erros = registroEditado.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);

            EditarRegistro();

            return;
        }

        bool validacao = ValidarInserirEditar(registroEditado, idRegistro);

        if (!validacao) return;

        bool conseguiuEditar = repositorio.EditarRegistro(idRegistro, registroEditado);

        if (!conseguiuEditar)
        {
            Notificador.ExibirMensagem("Houve um erro durante a edição do registro...", ConsoleColor.Red);

            return;
        }

        Notificador.ExibirMensagem("O registro foi editado com sucesso!", ConsoleColor.Green);
    }

    public virtual bool ValidarInserirEditar(T registroEditado, int idRegistro = -1)
    {
        return true;
    }

    public void ExcluirRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Excluindo {nomeEntidade}...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);

        int idRegistro;
        bool idValido;
        do
        {
            Console.Write("Selecione o ID do registro que deseja exlcuir: ");
            idValido = int.TryParse(Console.ReadLine(), out idRegistro);

            if (!idValido) Notificador.ExibirMensagem("Id Inválido!", ConsoleColor.Red);
        } while (!idValido);

        Console.WriteLine();

        T registroParaExcluir = repositorio.SelecionarRegistroPorId(idRegistro);

        bool validacao = ValidarExlcuir(registroParaExcluir, idRegistro);

        if (!validacao) return;

        ExcluirExtras(registroParaExcluir);

        bool conseguiuExcluir = repositorio.ExcluirRegistro(idRegistro);

        if (!conseguiuExcluir)
        {
            Notificador.ExibirMensagem("Houve um erro durante a exclusão do registro...", ConsoleColor.Red);

            return;
        }

        Notificador.ExibirMensagem("O registro foi excluído com sucesso!", ConsoleColor.Green);
    }

    public virtual void ExcluirExtras(T registro)
    {

    }

    public virtual bool ValidarExlcuir(T registro, int idRegistro)
    {
        return true;
    }


    public abstract void VisualizarRegistros(bool exibirTitulo);

    public abstract T ObterDados(bool validacaoExtra);

}
