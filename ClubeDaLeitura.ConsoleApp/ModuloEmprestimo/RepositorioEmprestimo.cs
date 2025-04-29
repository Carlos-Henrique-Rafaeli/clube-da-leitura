using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

public class RepositorioEmprestimo : RepositorioBase<Emprestimo>, IRepositorioEmprestimo
{
    public RepositorioEmprestimo(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<Emprestimo> ObterRegistros()
    {
        return contexto.Emprestimos;
    }


    public override void ExtrasCadastro(Emprestimo novoEmprestimo)
    {
        novoEmprestimo.Revista.Emprestar();
        novoEmprestimo.Amigo.TemEmprestimo = true;
    }
    
    public bool VerificarEmprestimo(Emprestimo emprestimo, int idEmprestimo = -1)
    {
        List<Emprestimo> registros = this.SelecionarRegistros();

        foreach (Emprestimo e in registros)
        {
            if (e == null || e.Status == StatusEmprestimo.Fechado) continue;

            if (e.Id == idEmprestimo) continue;

            if (emprestimo.Amigo.TemEmprestimo) return true;
        }

        return false;
    }
}
