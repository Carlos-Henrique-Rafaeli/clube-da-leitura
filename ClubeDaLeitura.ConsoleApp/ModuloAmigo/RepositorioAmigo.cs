using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo;

public class RepositorioAmigo : RepositorioBase<Amigo>, IRepositorioAmigo
{
    public RepositorioAmigo(ContextoDados contexto) : base(contexto)
    {
        
    }

    protected override List<Amigo> ObterRegistros()
    {
        return contexto.Amigos;
    }

    public bool VerificarNomeTelefone(string nome, string telefone, int id = -1)
    {
        List<Amigo> registros = this.SelecionarRegistros();

        foreach (Amigo a in registros)
        {
            if (a == null) continue;

            if (a.Id == id) continue;

            if (a.Nome == nome && a.Telefone == telefone) return true;
        }

        return false;
    }
}
