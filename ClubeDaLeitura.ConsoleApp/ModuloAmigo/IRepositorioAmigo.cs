using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigo;

public interface IRepositorioAmigo : IRepositorio<Amigo>
{
    public bool VerificarNomeTelefone(string nome, string telefone, int id = -1);
}