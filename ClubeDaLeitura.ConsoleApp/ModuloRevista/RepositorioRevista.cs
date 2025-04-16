using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

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
        EntidadeBase[] registros = this.SelecionarRegistros();
        Revista[] revistas = new Revista[registros.Length];

        for (int i = 0; i < registros.Length; i++)
            revistas[i] = (Revista)registros[i];

        foreach (Revista r in revistas)
        {
            if (r == null) continue;

            if (r.Id == id) continue;

            if (r.Titulo == titulo && r.NumeroEdicao == edicao) return true;
        }

        return false;
    }
}
