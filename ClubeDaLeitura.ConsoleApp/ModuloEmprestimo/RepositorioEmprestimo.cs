using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

public class RepositorioEmprestimo
{
    Emprestimo[] emprestimos = new Emprestimo[100];
    int contadorEmprestimos = 0;

    public void Inserir(Emprestimo novoEmprestimo)
    {
        novoEmprestimo.id = GeradorIds.GerarIdEmprestimo();
        novoEmprestimo.revista.Emprestar();
        novoEmprestimo.amigo.TemEmprestimo = true;

        emprestimos[contadorEmprestimos++] = novoEmprestimo;
    }

    public bool Editar(int idEmprestimo, Emprestimo emprestimoEditado)
    {
        if (emprestimoEditado == null) return false;

        foreach (Emprestimo e in emprestimos)
        {
            if (e == null) continue;

            if (e.id == idEmprestimo)
            {
                e.amigo = emprestimoEditado.amigo;
                e.revista = emprestimoEditado.revista;
                e.data = emprestimoEditado.data;
                e.ObterDataDevolucao();

                return true;
            }
        }
        return false;
    }

    public bool Excluir(int idEmprestimo)
    {
        for (int i = 0; i < emprestimos.Length; i++)
        {
            if (emprestimos[i] == null) continue;

            if (emprestimos[i].id == idEmprestimo)
            {
                emprestimos[i] = null;
                return true;
            }
        }
        return false;
    }

    public Emprestimo[] SelecionarTodos()
    {
        return emprestimos;
    }

    public Emprestimo SelecionarPorId(int idEmprestimo)
    {
        foreach (Emprestimo e in emprestimos)
        {
            if (e == null) continue;

            if (e.id == idEmprestimo) return e;
        }
        return null;
    }

    public bool VerificarEmprestimo(Emprestimo emprestimo, int idEmprestimo = -1)
    {
        foreach (Emprestimo e in emprestimos)
        {
            if (e == null || e.status == StatusEmprestimo.Fechado) continue;

            if (e.id == idEmprestimo) continue;

            if (emprestimo.amigo.TemEmprestimo) return true;
        }

        return false;
    }
}
