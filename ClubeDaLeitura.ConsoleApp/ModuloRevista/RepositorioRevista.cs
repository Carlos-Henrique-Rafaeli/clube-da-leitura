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

    public bool Editar(int idRevista, Revista revistaEditada)
    {
        foreach (Revista r in revistas)
        {
            if (r == null) continue;

            if (r.id == idRevista)
            {
                r.titulo = revistaEditada.titulo;
                r.numeroEdicao = revistaEditada.numeroEdicao;
                r.dataPublicacao = revistaEditada.dataPublicacao;
                r.status = revistaEditada.status;

                return true;
            }
        }

        return false;
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
