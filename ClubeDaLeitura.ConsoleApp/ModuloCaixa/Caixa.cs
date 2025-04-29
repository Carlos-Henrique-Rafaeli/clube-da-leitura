using ClubeDaLeitura.ConsoleApp.Compartilhado;
using System.Drawing;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa;

public class Caixa : EntidadeBase<Caixa>
{
    public string Etiqueta { get; set; }
    public Color Cor { get; set; }
    public int DiasEmprestimo { get; set; }
    public int Revistas { get; set; }

    public Caixa()
    {
    }

    public Caixa(string etiqueta, Color cor, int diasEmprestimo) : this()
    {
        Etiqueta = etiqueta;
        Cor = cor;
        DiasEmprestimo = diasEmprestimo;
        Revistas = 0;
    }

    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Etiqueta)) erros += "O campo 'Etiqueta' é obrigatório\n";
        
        else if (Etiqueta.Length < 3 || Etiqueta.Length > 50) erros += "O campo 'Etiqueta' necessita entre 3 e 50 caracteres\n";
        
        else if (Etiqueta.Contains(' ')) erros += "O campo 'Etiqueta' deve ser um nome único sem espaços\n";

        if (DiasEmprestimo < 0) erros += "O campo 'Dias de Empréstimo' não pode ser negativo";

        return erros;
    }

    public override void AtualizarRegistro(Caixa caixa)
    {
        Etiqueta = caixa.Etiqueta;
        Cor = caixa.Cor;
        DiasEmprestimo = caixa.DiasEmprestimo;

    }

    public void AdicionarRevista()
    {
        Revistas++;
    }

    public void RemoverRevista()
    {
        Revistas--;
    }
}