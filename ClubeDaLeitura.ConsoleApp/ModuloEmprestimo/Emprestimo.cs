using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

public class Emprestimo
{
    public int id;
    public Amigo amigo;
    public Revista revista;
    public DateTime data;
    public DateTime dataDevolucao;
    public StatusEmprestimo status;

    public Emprestimo(Amigo amigo, Revista revista, DateTime data)
    {
        this.amigo = amigo;
        this.revista = revista;
        this.data = data;
        dataDevolucao = ObterDataDevolucao();
        status = StatusEmprestimo.Aberto;
    }

    public string Validar()
    {
        string erros = "";

        if (amigo == null) erros += "O campo 'Amigo' está inválido\n";
        
        if (revista == null) erros += "O campo 'Revista' está inválido\n";
        

        return erros;
    }

    public DateTime ObterDataDevolucao()
    {
        DateTime dataDevolucao = data.AddDays(revista.caixa.diasEmprestimo);
        this.dataDevolucao = dataDevolucao;
        
        return dataDevolucao;
    }

    public void VerificarAtraso()
    {
        if (status == StatusEmprestimo.Fechado) return;

        if (dataDevolucao < DateTime.Now) status = StatusEmprestimo.Atrasado;
    }

    public void RegistrarDevolucao()
    {
        status = StatusEmprestimo.Fechado;
        amigo.temEmprestimo = false;

        revista.Devolver();
    }
}