using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.Compartilhado;

public abstract class RepositorioBase
{
    private ArrayList registros = new ArrayList();
    private int contadorIds = 0;

    public void CadastrarRegistro(EntidadeBase novoRegistro)
    {
        novoRegistro.Id = ++contadorIds;

        ExtrasCadastro(novoRegistro);

        registros.Add(novoRegistro);
    }

    public virtual void ExtrasCadastro(EntidadeBase registro)
    {

    }

    
    public bool EditarRegistro(int idRegistro, EntidadeBase registroEditado)
    {
        foreach (EntidadeBase item in registros)
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
        EntidadeBase registroExluir = SelecionarRegistroPorId(idRegistro);

        if (registroExluir != null)
        {
            registros.Remove(registroExluir);
            return true;
        }

        return false;
    }

    public ArrayList SelecionarRegistros()
    {
        return registros;
    }

    public EntidadeBase SelecionarRegistroPorId(int idRegistro)
    {
        foreach (EntidadeBase item in registros)
            if (item.Id == idRegistro) return item;
        
        return null;
    }

}
