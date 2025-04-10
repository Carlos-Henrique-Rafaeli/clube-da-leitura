using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista;

public class Revista
{
    int id;
    string titulo;
    int numeroEdicao;
    DateTime dataPublicacao;
    bool estaEmprestada;
    Caixa caixa;

    public Revista(string titulo, int numeroEdicao, DateTime dataPublicacao, bool estaEmprestada, Caixa caixa)
    {
        this.titulo = titulo;
        this.numeroEdicao = numeroEdicao;
        this.dataPublicacao = dataPublicacao;
        this.estaEmprestada = estaEmprestada;
        this.caixa = caixa;
    }

    public void Validar()
    {

    }

    public void Emprestar()
    {

    }

    public void Devolver()
    {

    }

    public void Reservar()
    {

    }
}