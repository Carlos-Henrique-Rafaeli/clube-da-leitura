using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo;

public class Amigo : EntidadeBase<Amigo>
{
    public string Nome { get; set; }
    public string Responsavel { get; set; }
    public string Telefone { get; set; }
    public bool TemEmprestimo { get; set; }
    public bool TemMulta { get; set; }

    private int[] telefoneValido = [12, 13];

    public Amigo(string nome, string responsavel, string telefone)
    {
        Nome = nome;
        Responsavel = responsavel;
        Telefone = telefone;
        TemEmprestimo = false;
        TemMulta = false;
    }

    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Nome)) erros += "O campo 'Nome' é obrigatório\n";

        else if (Nome.Length < 3 || Nome.Length > 100) erros += "O campo 'Nome' necessita entre 3 e 100 caracteres\n";
        
        if (string.IsNullOrWhiteSpace(Responsavel)) erros += "O campo 'Responsável' é obrigatório\n";
        
        else if (Responsavel.Length < 3 || Responsavel.Length > 100) erros += "O campo 'Responsável' necessita entre 3 e 100 caracteres\n";

        if (string.IsNullOrWhiteSpace(Telefone)) erros += "O campo 'Telefone' é obrigatório\n";

        else if (!telefoneValido.Contains(Telefone.Length)) erros += "O campo 'Telefone' precisa ser no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX";

        return erros;
    }

    public string ObterEmprestimos()
    {
        string opcao;
        if (TemEmprestimo) opcao = "Sim";
        else opcao = "Não";
        return opcao;
    }

    public string ObterMultas()
    {
        string opcao;
        if (TemMulta) opcao = "Sim";
        else opcao = "Não";
        return opcao;
    }

    public override void AtualizarRegistro(Amigo novoAmigo)
    {
        Nome = novoAmigo.Nome;
        Responsavel = novoAmigo.Responsavel;
        Telefone = novoAmigo.Telefone;
    }
}