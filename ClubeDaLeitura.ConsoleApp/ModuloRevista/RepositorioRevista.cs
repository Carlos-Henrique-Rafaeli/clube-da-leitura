using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using System.Collections;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista;

public class RepositorioRevista : RepositorioBase
{
    public override void ExtrasCadastro(EntidadeBase registro)
    {
        Revista novaRevista = (Revista)registro;

        novaRevista.Caixa.AdicionarRevista();
    }
    
    public bool VerificarTituloEdicao(string titulo, int edicao, int id = -1)
    {
        ArrayList registros = this.SelecionarRegistros();

        foreach (Revista r in registros)
        {
            if (r == null) continue;

            if (r.Id == id) continue;

            if (r.Titulo == titulo && r.NumeroEdicao == edicao) return true;
        }

        return false;
    }
}
