using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva;

public interface IRepositorioReserva : IRepositorio<Reserva>
{
    public bool VerificarRevistaAmigo(Revista revista, Amigo amigo);

    public Reserva SelecionarPorRevistaAmigo(Revista revista, Amigo amigo);
}