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

    public Reserva()
    {
    }
    public Reserva(Amigo amigo, Revista revista) : this()
    {
        Amigo = amigo;
        Revista = revista;
        DataReserva = DateTime.Now;
        Status = StatusReserva.Ativa;
        Revista.Reservar();
    }


    public override string Validar()
    {
        return "";
    }

    public override void AtualizarRegistro(Reserva registroEditado)
    {
        
    }
}
