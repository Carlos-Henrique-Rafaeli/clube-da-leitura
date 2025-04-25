using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva;

public class Reserva : EntidadeBase<Reserva>
{
    public Amigo Amigo { get; set; }
    public Revista Revista { get; set; }
    public DateTime DataReserva { get; set; }
    public StatusReserva Status { get; set; }

    public Reserva(Amigo amigo, Revista revista)
    {
        Amigo = amigo;
        Revista = revista;
        DataReserva = DateTime.Now;
        Status = StatusReserva.Ativa;
        Revista.Reservar();
    }


    public override string Validar()
    {
        throw new NotImplementedException();
    }

    public override void AtualizarRegistro(Reserva registroEditado)
    {
        throw new NotImplementedException();
    }
}
