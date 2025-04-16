using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

public class RepositorioEmprestimo : RepositorioBase
{
    public override void ExtrasCadastro(EntidadeBase registro)
    {
        Emprestimo novoEmprestimo = (Emprestimo)registro;

        novoEmprestimo.Revista.Emprestar();
        novoEmprestimo.Amigo.TemEmprestimo = true;
    }
    public bool VerificarEmprestimo(Emprestimo emprestimo, int idEmprestimo = -1)
    {
        EntidadeBase[] registros = this.SelecionarRegistros();
        Emprestimo[] emprestimos = new Emprestimo[registros.Length];

        for (int i = 0; i < registros.Length; i++)
            emprestimos[i] = (Emprestimo)registros[i];

        foreach (Emprestimo e in emprestimos)
        {
            if (e == null || e.Status == StatusEmprestimo.Fechado) continue;

            if (e.Id == idEmprestimo) continue;

            if (emprestimo.Amigo.TemEmprestimo) return true;
        }

        return false;
    }
}
