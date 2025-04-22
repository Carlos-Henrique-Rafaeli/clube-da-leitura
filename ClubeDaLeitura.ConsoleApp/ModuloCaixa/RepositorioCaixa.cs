using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa;

public class RepositorioCaixa : RepositorioBase
{
    public bool VerificarEtiqueta(string etiqueta, int id = -1)
    {
        ArrayList registros = this.SelecionarRegistros();

        foreach (Caixa c in registros)
        {
            if (c == null) continue;

            if (c.Id == id) continue;

            if (c.Etiqueta == etiqueta) return true;
        }

        return false;
    }

}
