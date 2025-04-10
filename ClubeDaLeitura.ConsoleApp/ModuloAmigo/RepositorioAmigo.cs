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

    public void Editar()
    {

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

    public bool VerificarNomeTelefone(string nome, string telefone)
    {
        foreach (Amigo a in amigos)
        {
            if (a == null) continue;

            if (a.nome == nome && a.telefone == telefone) return true;
        }

        return false;
    }
}
