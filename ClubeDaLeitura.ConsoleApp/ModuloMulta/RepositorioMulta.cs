using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloMulta;

public class RepositorioMulta
{
    Multa[] multas = new Multa[100];
    int contadorMulta = 0;


    public void Inserir(Multa novaMulta)
    {
        novaMulta.Id = GeradorIds.GerarIdMulta();

        multas[contadorMulta++] = novaMulta;
    }

    public void Excluir()
    {

    }

    public Multa[] SelecionarTodos()
    {
        return multas;
    }

    public Multa SelecionarPorId(int idMulta)
    {
        foreach (Multa m in multas)
        {
            if (m == null) continue;

            if (m.Id == idMulta) return m;
        }

        return null;
    }
}
