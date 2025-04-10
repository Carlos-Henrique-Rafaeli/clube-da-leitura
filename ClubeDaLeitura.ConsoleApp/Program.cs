using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

namespace ClubeDaLeitura.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        RepositorioAmigo repositorioAmigo = new RepositorioAmigo();

        TelaAmigo telaAmigo = new TelaAmigo(repositorioAmigo);


        while (true)
        {
            string opcaoSelecionada = TelaPrincipal.ApresentarMenu();

            bool deveRodar = true;

            switch (opcaoSelecionada)
            {
                case "1":
                    while (deveRodar)
                    {
                        opcaoSelecionada = telaAmigo.ApresentarMenu();

                        switch (opcaoSelecionada)
                        {
                            case "1": telaAmigo.Inserir(); break;
                            
                            case "S": deveRodar = false; break;

                            default: Console.WriteLine("Opção Inválida!"); Console.ReadLine(); break;
                        }
                    }
                    break;

                case "S": return;

                default: Console.WriteLine("Opção Inválida!"); Console.ReadLine(); break;
            }
        }
    }
}
