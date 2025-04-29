using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using System.Text.Json.Serialization;
using System.Text.Json;
using ClubeDaLeitura.ConsoleApp.Util;

namespace ClubeDaLeitura.ConsoleApp.Compartilhado;

public class ContextoDados
{
    public List<Amigo> Amigos { get; set; }
    public List<Caixa> Caixas { get; set; }
    public List<Revista> Revistas { get; set; }
    public List<Emprestimo> Emprestimos { get; set; }
    public List<Multa> Multas { get; set; }
    public List<Reserva> Reservas { get; set; }

    private string pastaRaiz = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "AcademiaProgramador2025");
    private string arquivoArmazenamento = "dados.json";

    public ContextoDados()
    {
        Amigos = new List<Amigo>();
        Caixas = new List<Caixa>();
        Revistas = new List<Revista>();
        Emprestimos = new List<Emprestimo>();
        Multas = new List<Multa>();
        Reservas = new List<Reserva>();
    }

    public ContextoDados(bool carregarDados) : this()
    {
        if (carregarDados)
            Carregar();
    }

    public void Salvar()
    {
        if (!Directory.Exists(pastaRaiz))
            Directory.CreateDirectory(pastaRaiz);

        string pastaProjeto = Path.Combine(pastaRaiz, "ClubeDaLeitura");

        if (!Directory.Exists(pastaProjeto))
            Directory.CreateDirectory(pastaProjeto);

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        string caminhoCompleto = Path.Combine(pastaProjeto, arquivoArmazenamento);

        string json = JsonSerializer.Serialize(this, jsonOptions);

        File.WriteAllText(caminhoCompleto, json);
    }

    public void Carregar()
    {
        string pastaProjeto = Path.Combine(pastaRaiz, "ClubeDaLeitura");

        string caminhoCompleto = Path.Combine(pastaProjeto, arquivoArmazenamento);

        if (!File.Exists(caminhoCompleto)) return;

        string json = File.ReadAllText(caminhoCompleto);

        if (string.IsNullOrWhiteSpace(json)) return;

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;
        jsonOptions.Converters.Add(new ColorJsonConverter());

        ContextoDados contextoArmazenado = JsonSerializer.Deserialize<ContextoDados>(json, jsonOptions)!;

        if (contextoArmazenado == null) return;

        this.Amigos = contextoArmazenado.Amigos;
        this.Caixas = contextoArmazenado.Caixas;
        this.Revistas = contextoArmazenado.Revistas;
        this.Emprestimos = contextoArmazenado.Emprestimos;
        this.Multas = contextoArmazenado.Multas;
        this.Reservas = contextoArmazenado.Reservas;
    }
}
