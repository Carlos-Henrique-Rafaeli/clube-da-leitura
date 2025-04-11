using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista;

public class Revista
{
    public int id;
    public string titulo;
    public int numeroEdicao;
    public DateTime dataPublicacao;
    public StatusRevista status;
    public Caixa caixa;

    public Revista(string titulo, int numeroEdicao, DateTime dataPublicacao, Caixa caixa, StatusRevista status = StatusRevista.Disponível)
    {
        this.titulo = titulo;
        this.numeroEdicao = numeroEdicao;
        this.dataPublicacao = dataPublicacao;
        this.caixa = caixa;
        this.status = status;
    }

    public string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(titulo)) erros += "O campo 'Título' é obrigatório\n";
        else if (titulo.Length < 2 || titulo.Length > 100) erros += "O campo 'Título' necessita entre 2 e 100 caracteres\n";
        
        if (numeroEdicao < 0) erros += "O campo 'Número de Edição' não pode ser negativo\n";
        
        if (dataPublicacao > DateTime.Now) erros += "O campo 'Data de Publicação' não pode ser do futuro\n";
        
        if (caixa == null) erros += "O campo 'Caixa' está inválido";

        return erros;
    }

    public void Emprestar()
    {
        status = StatusRevista.Emprestada;
    }

    public void Devolver()
    {
        status = StatusRevista.Disponível;
    }

    public void Reservar()
    {
        status = StatusRevista.Reservada;
    }
}