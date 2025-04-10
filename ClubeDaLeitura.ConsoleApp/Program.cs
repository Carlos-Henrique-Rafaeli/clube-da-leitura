using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
        RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
        RepositorioRevista repositorioRevista = new RepositorioRevista();

        TelaAmigo telaAmigo = new TelaAmigo(repositorioAmigo);
        TelaCaixa telaCaixa = new TelaCaixa(repositorioCaixa);
        TelaRevista telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa);

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
                            
                            case "2": telaAmigo.Editar(); break;
                            
                            case "3": telaAmigo.Excluir(); break;

                            case "4": telaAmigo.VisualizarTodos(true); break;
                            
                            case "S": deveRodar = false; break;

                            default: Console.WriteLine("Opção Inválida!"); Console.ReadLine(); break;
                        }
                    }
                    break;

                case "2":
                    while (deveRodar)
                    {
                        opcaoSelecionada = telaCaixa.ApresentarMenu();

                        switch (opcaoSelecionada)
                        {
                            case "1": telaCaixa.Inserir(); break;
                            
                            case "2": telaCaixa.Editar(); break;
                            
                            case "3": telaCaixa.Excluir(); break;
                            
                            case "4": telaCaixa.Visualizar(true); break;

                            case "S": deveRodar = false; break;

                            default: Console.WriteLine("Opção Inválida!"); Console.ReadLine(); break;
                        }
                    }
                    break;
                
                case "3":
                    while (deveRodar)
                    {
                        opcaoSelecionada = telaRevista.ApresentarMenu();

                        switch (opcaoSelecionada)
                        {
                            case "1": telaRevista.Inserir(); break;
                            
                            case "2": telaRevista.Editar(); break;

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
