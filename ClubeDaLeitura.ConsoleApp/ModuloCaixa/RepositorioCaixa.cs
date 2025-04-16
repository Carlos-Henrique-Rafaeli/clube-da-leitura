using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa;

public class RepositorioCaixa : RepositorioBase
{
    public bool VerificarEtiqueta(string etiqueta, int id = -1)
    {
        EntidadeBase[] registros = this.SelecionarRegistros();
        Caixa[] caixas = new Caixa[registros.Length];

        for (int i = 0; i < registros.Length; i++)
            caixas[i] = (Caixa)registros[i];

        foreach (Caixa c in caixas)
        {
            if (c == null) continue;

            if (c.Id == id) continue;

            if (c.Etiqueta == etiqueta) return true;
        }

        return false;
    }

}
