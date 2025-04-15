using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva;

public class Reserva
{
    public int Id;
    public Amigo Amigo;
    public Revista Revista;
    public DateTime DataReserva;
    public StatusReserva Status;

    public Reserva(Amigo amigo, Revista revista)
    {
        Amigo = amigo;
        Revista = revista;
        DataReserva = DateTime.Now;
        Status = StatusReserva.Ativa;
        Revista.Reservar();
    }
}
