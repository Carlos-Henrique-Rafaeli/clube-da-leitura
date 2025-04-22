using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo;

public class RepositorioAmigo : RepositorioBase
{
    public bool VerificarNomeTelefone(string nome, string telefone, int id = -1)
    {
        ArrayList registros = this.SelecionarRegistros();

        foreach (Amigo a in registros)
        {
            if (a == null) continue;

            if (a.Id == id) continue;

            if (a.Nome == nome && a.Telefone == telefone) return true;
        }

        return false;
    }
}
