namespace ClubeDaLeitura.ConsoleApp.Compartilhado;

public static class GeradorIds
{
    public static int idAmigo = 0;
    public static int idCaixa = 0;
    public static int idRevista = 0;
    public static int idEmprestimo = 0;

    public static int GerarIdAmigo()
    {
        idAmigo++;
        return idAmigo;
    }

    public static int GerarIdCaixa()
    {
        idCaixa++;
        return idCaixa;
    }

    public static int GerarIdRevista()
    {
        idRevista++;
        return idRevista;
    }
}
