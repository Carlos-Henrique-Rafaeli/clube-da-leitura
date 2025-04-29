using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeDaLeitura.ConsoleApp.ModuloMulta;

public class RepositorioMulta : RepositorioBase<Multa>, IRepositorioMulta
{
    public RepositorioMulta(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<Multa> ObterRegistros()
    {
        return contexto.Multas;
    }
}
