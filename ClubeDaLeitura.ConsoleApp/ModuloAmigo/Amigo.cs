using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo;

public class Amigo
{
    public int id;
    public string nome;
    public string responsavel;
    public string telefone;
    
    private int[] telefoneValido = [12, 13];

    public Amigo(string nome, string responsavel, string telefone)
    {
        this.nome = nome;
        this.responsavel = responsavel;
        this.telefone = telefone;
    }

    public string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(nome)) erros += "O campo 'Nome' é obrigatório\n";

        else if (nome.Length < 3 || nome.Length > 100) erros += "O campo 'Nome' necessita entre 3 e 100 caracteres\n";
        
        if (string.IsNullOrWhiteSpace(responsavel)) erros += "O campo 'Responsável' é obrigatório\n";
        
        else if (responsavel.Length < 3 || responsavel.Length > 100) erros += "O campo 'Responsável' necessita entre 3 e 100 caracteres\n";

        if (string.IsNullOrWhiteSpace(telefone)) erros += "O campo 'Telefone' é obrigatório\n";

        else if (!telefoneValido.Contains(telefone.Length)) erros += "O campo 'Telefone' precisa ser no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX";

        return erros;
    }

    public void ObterImprestimos()
    {

    }
}