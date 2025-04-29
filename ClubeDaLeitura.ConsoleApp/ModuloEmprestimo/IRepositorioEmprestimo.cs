using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

public interface IRepositorioEmprestimo : IRepositorio<Emprestimo>
{
    public bool VerificarEmprestimo(Emprestimo emprestimo, int idEmprestimo = -1);
}