using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista;

public interface IRepositorioRevista : IRepositorio<Revista>
{
    public bool VerificarTituloEdicao(string titulo, int edicao, int id = -1);
}