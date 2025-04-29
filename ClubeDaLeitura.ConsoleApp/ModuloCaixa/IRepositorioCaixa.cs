using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa;

public interface IRepositorioCaixa : IRepositorio<Caixa>
{
    public bool VerificarEtiqueta(string etiqueta, int id = -1);
}