using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

public class Emprestimo
{
    public int id;
    public Amigo amigo;
    public Revista revista;
    public DateTime data;
    public StatusEmprestimo status;

    public Emprestimo(Amigo amigo, Revista revista)
    {
        this.amigo = amigo;
        this.revista = revista;
        this.data = DateTime.Now;
        this.status = StatusEmprestimo.Aberto;
    }

    public string Validar()
    {
        string erros = "";

        if (amigo == null) erros += "O campo 'Amigo' está inválido\n";
        
        if (revista == null) erros += "O campo 'Revista' está inválido\n";
        
        else if (revista.status != StatusRevista.Disponível) erros += "O campo 'Revista' não está disponível\n";

        return erros;
    }

    public string ObterDataDevolucao()
    {
        DateTime dataDevolucao = data.AddDays(revista.caixa.diasEmprestimo);
        return dataDevolucao.ToShortDateString();
    }

    public void RegistrarDevolucao()
    {

    }
}