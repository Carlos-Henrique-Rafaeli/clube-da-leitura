using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeDaLeitura.ConsoleApp.ModuloMulta;

public class Multa : EntidadeBase<Multa>
{
    public double ValorMulta { get; set; }
    public StatusMulta Status { get; set; }
    public Emprestimo Emprestimo { get; set; }

    public Multa(Emprestimo emprestimo)
    {
        this.Emprestimo = emprestimo;
        emprestimo.Amigo.TemMulta = true;
        Status = StatusMulta.Pendente;
        ObterValorMulta();
    }

    public void ObterValorMulta()
    {
        int diasAtraso = (DateTime.Now - Emprestimo.DataDevolucao).Days;

        ValorMulta = diasAtraso * 2.0;
    }

    public override void AtualizarRegistro(Multa registroEditado)
    {
        throw new NotImplementedException();
    }

    public override string Validar()
    {
        throw new NotImplementedException();
    }
}
