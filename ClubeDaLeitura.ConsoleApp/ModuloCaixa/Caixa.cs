using System.Drawing;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa;

public class Caixa
{
    public int id;
    public string etiqueta;
    public Color cor;
    public int diasEmprestimo;

    public Caixa(string etiqueta, Color cor, int diasEmprestimo)
    {
        this.etiqueta = etiqueta;
        this.cor = cor;
        this.diasEmprestimo = diasEmprestimo;
    }

    public string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(etiqueta)) erros += "O campo 'Etiqueta' é obrigatório\n";
        
        else if (etiqueta.Length < 3 || etiqueta.Length > 50) erros += "O campo 'Etiqueta' necessita entre 3 e 50 caracteres\n";
        
        else if (etiqueta.Contains(' ')) erros += "O campo 'Etiqueta' deve ser um nome único sem espaços\n";

        if (diasEmprestimo < 0) erros += "O campo 'Dias de Empréstimo' não pode ser negativo";

        return erros;
    }

    public void AdicionarRevista()
    {

    }

    public void RemoverRevista()
    {

    }
}