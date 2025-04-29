using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.Compartilhado;

public abstract class RepositorioBase<T> where T : EntidadeBase<T>
{
    private List<T> registros = new List<T>();
    private int contadorIds = 0;

    protected ContextoDados contexto;

    protected RepositorioBase(ContextoDados contexto)
    {
        this.contexto = contexto;

        registros = ObterRegistros();

        int maiorId = 0;

        foreach (var registro in registros)
        {
            if (registro.Id > maiorId)
                maiorId = registro.Id;
        }

        contadorIds = maiorId;
    }

    protected abstract List<T> ObterRegistros();

    public void CadastrarRegistro(T novoRegistro)
    {
        novoRegistro.Id = ++contadorIds;

        ExtrasCadastro(novoRegistro);

        registros.Add(novoRegistro);

        contexto.Salvar();
    }

    public virtual void ExtrasCadastro(T registro)
    {

    }

    
    public bool EditarRegistro(int idRegistro, T registroEditado)
    {
        foreach (T item in registros)
        {
            if (item.Id == idRegistro)
            {
                item.AtualizarRegistro(registroEditado);

                contexto.Salvar();

                return true;
            }
        }

        return false;
    }

    public bool ExcluirRegistro(int idRegistro)
    {
        T registroExluir = SelecionarRegistroPorId(idRegistro);

        if (registroExluir != null)
        {
            registros.Remove(registroExluir);

            contexto.Salvar();

            return true;
        }

        return false;
    }

    public List<T> SelecionarRegistros()
    {
        return registros;
    }

    public T SelecionarRegistroPorId(int idRegistro)
    {
        foreach (T item in registros)
            if (item.Id == idRegistro) return item;
        
        return null;
    }

}
