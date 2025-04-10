using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

public class Emprestimo
{
    int id;
    Amigo amigo;
    Revista revista;
    DateTime data;
    bool situacao;

    public Emprestimo(Amigo amigo, Revista revista, DateTime data, bool situacao)
    {
        this.amigo = amigo;
        this.revista = revista;
        this.data = data;
        this.situacao = situacao;
    }

    public void Validar()
    {

    }

    public void ObterDataDevolucao()
    {

    }

    public void RegistrarDevolucao()
    {

    }
}