using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeDaLeitura.ConsoleApp.ModuloMulta;

public class Multa
{
    public int id;
    public double valorMulta;
    public StatusMulta status = StatusMulta.Pendente;
    public Emprestimo emprestimo;

    public Multa(Emprestimo emprestimo)
    {
        this.emprestimo = emprestimo;
        emprestimo.amigo.TemMulta = true;
        ObterValorMulta();
    }

    public void ObterValorMulta()
    {
        int diasAtraso = (DateTime.Now - emprestimo.dataDevolucao).Days;

        valorMulta = diasAtraso * 2.0;
    }

}
