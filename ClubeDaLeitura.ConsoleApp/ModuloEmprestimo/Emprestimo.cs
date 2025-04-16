using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

public class Emprestimo : EntidadeBase
{
    public Amigo Amigo { get; set; }
    public Revista Revista { get; set; }
    public DateTime Data { get; set; }
    public DateTime DataDevolucao { get; set; }
    public StatusEmprestimo Status { get; set; }

    public Emprestimo(Amigo amigo, Revista revista, DateTime data)
    {
        this.Amigo = amigo;
        this.Revista = revista;
        this.Data = data;
        DataDevolucao = ObterDataDevolucao();
        Status = StatusEmprestimo.Aberto;
    }

    public override string Validar()
    {
        string erros = "";

        if (Amigo == null) erros += "O campo 'Amigo' está inválido\n";
        
        if (Revista == null) erros += "O campo 'Revista' está inválido\n";

        return erros;
    }

    public override void AtualizarRegistro(EntidadeBase registroEditado)
    {
        Emprestimo emprestimo = (Emprestimo)registroEditado;

        Amigo = emprestimo.Amigo;
        Revista = emprestimo.Revista;
        Data = emprestimo.Data;
        DataDevolucao = emprestimo.DataDevolucao;
    }

    public DateTime ObterDataDevolucao()
    {
        DateTime dataDevolucao = Data.AddDays(Revista.Caixa.DiasEmprestimo);
        this.DataDevolucao = dataDevolucao;
        
        return dataDevolucao;
    }

    public void VerificarAtraso()
    {
        if (Status == StatusEmprestimo.Fechado) return;

        if (DataDevolucao < DateTime.Now) Status = StatusEmprestimo.Atrasado;
    }

    public void RegistrarDevolucao()
    {
        Status = StatusEmprestimo.Fechado;
        Amigo.TemEmprestimo = false;

        Revista.Devolver();
    }

    
}