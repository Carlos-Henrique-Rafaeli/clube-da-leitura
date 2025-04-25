using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista;

public class Revista : EntidadeBase<Revista>
{
    public string Titulo { get; set; }
    public int NumeroEdicao { get; set; }
    public DateTime DataPublicacao { get; set; }
    public StatusRevista Status { get; set; }
    public Caixa Caixa { get; set; }

    public Revista(string titulo, int numeroEdicao, DateTime dataPublicacao, Caixa caixa, StatusRevista status = StatusRevista.Disponível)
    {
        Titulo = titulo;
        NumeroEdicao = numeroEdicao;
        DataPublicacao = dataPublicacao;
        Caixa = caixa;
        Status = status;
    }

    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Titulo)) erros += "O campo 'Título' é obrigatório\n";
        else if (Titulo.Length < 2 || Titulo.Length > 100) erros += "O campo 'Título' necessita entre 2 e 100 caracteres\n";
        
        if (NumeroEdicao < 0) erros += "O campo 'Número de Edição' não pode ser negativo\n";
        
        if (DataPublicacao > DateTime.Now) erros += "O campo 'Data de Publicação' não pode ser do futuro\n";
        
        if (Caixa == null) erros += "O campo 'Caixa' está inválido";

        return erros;
    }

    public override void AtualizarRegistro(Revista revista)
    {
        Titulo = revista.Titulo;
        NumeroEdicao = revista.NumeroEdicao;
        DataPublicacao = revista.DataPublicacao;
        Status = revista.Status;

    }

    public void Emprestar()
    {
        Status = StatusRevista.Emprestada;
    }

    public void Devolver()
    {
        Status = StatusRevista.Disponível;
    }

    public void Reservar()
    {
        Status = StatusRevista.Reservada;
    }
}