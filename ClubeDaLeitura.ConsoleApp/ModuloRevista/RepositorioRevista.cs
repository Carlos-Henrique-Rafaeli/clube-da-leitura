using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

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

    public bool Excluir(int idRevista)
    {
        for (int i = 0; i < revistas.Length; i++)
        {
            if (revistas[i] == null) continue;

            if (revistas[i].id == idRevista && revistas[i].status == StatusRevista.Disponível)
            {
                revistas[i] = null;

                return true;
            }
        }
        return false;
    }

    public Revista[] SelecionarTodos()
    {
        return revistas;
    }

    public Revista SelecionarPorId(int idRevista)
    {
        foreach (Revista r in revistas)
        {
            if (r == null) continue;

            if (r.id == idRevista) return r;
        }
        return null;
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
