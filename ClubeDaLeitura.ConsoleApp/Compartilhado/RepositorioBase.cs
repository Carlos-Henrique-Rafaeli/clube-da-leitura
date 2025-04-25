using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.Compartilhado;

public abstract class RepositorioBase<T> where T : EntidadeBase<T>
{
    private List<T> registros = new List<T>();
    private int contadorIds = 0;

    public void CadastrarRegistro(T novoRegistro)
    {
        novoRegistro.Id = ++contadorIds;

        ExtrasCadastro(novoRegistro);

        registros.Add(novoRegistro);
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
