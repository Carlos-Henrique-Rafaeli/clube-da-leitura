using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista;

public class RepositorioRevista : RepositorioBase<Revista>
{
    public override void ExtrasCadastro(Revista novaRevista)
    {
        novaRevista.Caixa.AdicionarRevista();
    }
    
    public bool VerificarTituloEdicao(string titulo, int edicao, int id = -1)
    {
        List<Revista> registros = this.SelecionarRegistros();

        foreach (Revista r in registros)
        {
            if (r == null) continue;

            if (r.Id == id) continue;

            if (r.Titulo == titulo && r.NumeroEdicao == edicao) return true;
        }

        return false;
    }
}
