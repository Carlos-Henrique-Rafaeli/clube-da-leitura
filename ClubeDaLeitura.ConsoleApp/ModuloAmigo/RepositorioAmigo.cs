using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo;

public class RepositorioAmigo
{
    Amigo[] amigos = new Amigo[100];
    int contadorAmigos = 0;

    public void Inserir(Amigo novoAmigo)
    {
        novoAmigo.id = GeradorIds.GerarIdAmigo();

        amigos[contadorAmigos++] = novoAmigo;
    }

    public bool Editar(int idAmigo, Amigo amigoEditado)
    {
        if (amigoEditado == null) return false;

        foreach (Amigo a in amigos)
        {
            if (a == null) continue;

            if (a.id == idAmigo)
            {
                a.nome = amigoEditado.nome;
                a.responsavel = amigoEditado.responsavel;
                a.telefone = amigoEditado.telefone;

                return true;
            }
        }
        return true;
    }

    public void Excluir()
    {

    }

    public void SelecionarTodos()
    {

    }

    public void SelecionarPorId()
    {

    }

    public bool VerificarNomeTelefone(string nome, string telefone, int id = -1)
    {
        foreach (Amigo a in amigos)
        {
            if (a == null) continue;

            if (a.id == id) continue;

            if (a.nome == nome && a.telefone == telefone) return true;
        }

        return false;
    }
}
