using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva;

public class RepositorioReserva : RepositorioBase<Reserva>, IRepositorioReserva
{
    public RepositorioReserva(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<Reserva> ObterRegistros()
    {
        return contexto.Reservas;
    }

    public bool VerificarRevistaAmigo(Revista revista, Amigo amigo)
    {
        List<Reserva> reservas = this.SelecionarRegistros();

        foreach (Reserva r in reservas)
        {
            if (r == null) continue;

            if (r.Revista.Id == revista.Id && r.Amigo.Id == amigo.Id) return true;
        }
        return false;
    }

    public Reserva SelecionarPorRevistaAmigo(Revista revista, Amigo amigo)
    {
        List<Reserva> reservas = this.SelecionarRegistros();

        foreach (Reserva r in reservas)
        {
            if (r == null) continue;

            if (r.Revista == revista && r.Amigo == amigo) return r;
        }

        return null;
    }
}
