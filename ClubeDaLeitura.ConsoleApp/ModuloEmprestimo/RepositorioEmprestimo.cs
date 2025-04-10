using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

public class RepositorioEmprestimo
{
    Emprestimo[] emprestimos = new Emprestimo[100];
    int contadorEmprestimos = 0;

    public void Inserir(Emprestimo novoEmprestimo)
    {
        novoEmprestimo.id = GeradorIds.GerarIdEmprestimo();

        emprestimos[contadorEmprestimos++] = novoEmprestimo;
    }

    public void Editar()
    {

    }

    public void Excluir()
    {

    }

    public Emprestimo[] SelecionarTodos()
    {
        return emprestimos;
    }

    public void SelecionarPorId()
    {

    }

    public bool VerificarEmprestimo(Emprestimo emprestimo, int idEmprestimo = -1)
    {
        foreach (Emprestimo e in emprestimos)
        {
            if (e == null) continue;

            if (e.id == idEmprestimo) continue;

            if (e.amigo != null) return true;
        }

        return false;
    }
}
