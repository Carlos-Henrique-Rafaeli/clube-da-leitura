using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo;

public class RepositorioAmigo : RepositorioBase
{
    public bool VerificarNomeTelefone(string nome, string telefone, int id = -1)
    {
        EntidadeBase[] registros = this.SelecionarRegistros();
        Amigo[] amigos = new Amigo[registros.Length];

        for (int i = 0; i < registros.Length; i++)
            amigos[i] = (Amigo)registros[i];

        
        foreach (Amigo a in amigos)
        {
            if (a == null) continue;

            if (a.Id == id) continue;

            if (a.Nome == nome && a.Telefone == telefone) return true;
        }

        return false;
    }
}
