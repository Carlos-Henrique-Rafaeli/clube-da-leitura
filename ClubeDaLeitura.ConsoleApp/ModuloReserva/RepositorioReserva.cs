using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva;

public class RepositorioReserva
{
    Reserva[] reservas = new Reserva[100];
    int contadorReserva = 0;

    public void Inserir(Reserva novaReserva)
    {
        novaReserva.Id = GeradorIds.GerarIdReserva();

        reservas[contadorReserva++] = novaReserva;
    }


    public bool VerificarRevistaAmigo(Revista revista, Amigo amigo)
    {
        foreach (Reserva r in reservas)
        {
            if (r == null) continue;

            if (r.Revista.id == revista.id && r.Amigo.Id == amigo.Id) return true;
        }
        return false;
    }


    public Reserva[] SelecionarTodos()
    {
        return reservas;
    }

    public Reserva SelecionarPorId(int idReserva)
    {
        foreach (Reserva r in reservas)
        {
            if (r == null) continue;

            if (r.Id == idReserva) return r;
        }

        return null;
    }

    public Reserva SelecionarPorRevistaAmigo(Revista revista, Amigo amigo)
    {
        foreach (Reserva r in reservas)
        {
            if (r == null) continue;

            if (r.Revista == revista && r.Amigo == amigo) return r;
        }

        return null;
    }
}
