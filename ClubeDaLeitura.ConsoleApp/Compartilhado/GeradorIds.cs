namespace ClubeDaLeitura.ConsoleApp.Compartilhado;

public static class GeradorIds
{

    public static int idMulta = 0;
    public static int idReserva = 0;

    public static int GerarIdMulta()
    {
        idMulta++;
        return idMulta;
    }

    public static int GerarIdReserva()
    {
        idReserva++;
        return idReserva;
    }
}
