using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista;

public class RepositorioRevista
{
    Revista[] revistas = new Revista[100];
    int contadorRevistas = 0;

    public void Inserir(Revista novaRevista)
    {
        novaRevista.id = GeradorIds.GerarIdRevista();

        revistas[contadorRevistas++] = novaRevista;
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

    public bool VerificarTituloEdicao(string titulo, int edicao, int id = -1)
    {
        foreach (Revista r in revistas)
        {
            if (r == null) continue;

            if (r.id == id) continue;

            if (r.titulo == titulo && r.numeroEdicao == edicao) return true;
        }

        return false;
    }
}
