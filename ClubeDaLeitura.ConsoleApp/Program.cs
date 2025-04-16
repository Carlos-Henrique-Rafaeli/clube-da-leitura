using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;
using ClubeDaLeitura.ConsoleApp.Util;

namespace ClubeDaLeitura.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
        RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
        RepositorioRevista repositorioRevista = new RepositorioRevista();
        RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();
        RepositorioMulta repositorioMulta = new RepositorioMulta();
        RepositorioReserva repositorioReserva = new RepositorioReserva();

        TelaAmigo telaAmigo = new TelaAmigo(repositorioAmigo);
        TelaCaixa telaCaixa = new TelaCaixa(repositorioCaixa);
        TelaRevista telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa);
        TelaEmprestimo telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista, repositorioMulta, repositorioReserva);
        TelaMulta telaMulta = new TelaMulta(repositorioMulta);
        TelaReserva telaReserva = new TelaReserva(repositorioReserva, repositorioRevista, repositorioAmigo);

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
                            case "1": telaAmigo.CadastrarRegistro(); break;
                            
                            case "2": telaAmigo.EditarRegistro(); break;
                            
                            case "3": telaAmigo.ExcluirRegistro(); break;

                            case "4": telaAmigo.VisualizarRegistros(true); break;
                            
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
                            case "1": telaCaixa.CadastrarRegistro(); break;
                            
                            case "2": telaCaixa.EditarRegistro(); break;
                            
                            case "3": telaCaixa.ExcluirRegistro(); break;
                            
                            case "4": telaCaixa.VisualizarRegistros(true); break;

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
                            case "1": telaRevista.CadastrarRegistro(); break;
                            
                            case "2": telaRevista.EditarRegistro(); break;
                            
                            case "3": telaRevista.ExcluirRegistro(); break;

                            case "4": telaRevista.VisualizarRegistros(true); break;

                            case "S": deveRodar = false; break;

                            default: Console.WriteLine("Opção Inválida!"); Console.ReadLine(); break;
                        }
                    }
                    break;

                case "4":
                    while (deveRodar)
                    {
                        opcaoSelecionada = telaEmprestimo.ApresentarMenu();

                        switch (opcaoSelecionada)
                        {
                            case "1": telaEmprestimo.CadastrarRegistro(); break;
                            
                            case "2": telaEmprestimo.EditarRegistro(); break;

                            case "3": telaEmprestimo.ExcluirRegistro(); break;
                            
                            case "4": telaEmprestimo.VisualizarRegistros(true); break;
                            
                            case "5": telaEmprestimo.RegistrarDevolucao(); break;
                                
                            case "S": deveRodar = false; break;

                            default: Console.WriteLine("Opção Inválida!"); Console.ReadLine(); break;
                        }
                    }
                    break;

                case "5":
                    while (deveRodar)
                    {
                        opcaoSelecionada = telaReserva.ApresentarMenu();

                        switch (opcaoSelecionada)
                        {
                            case "1": telaReserva.CriarReserva(); break;

                            case "2": telaReserva.CancelarReserva(); break;

                            case "3": telaReserva.Visualizar(true); break;

                            case "S": deveRodar = false; break;

                            default: Console.WriteLine("Opção Inválida!"); Console.ReadLine(); break;
                        }
                    }
                    break;

                case "6":
                    while (deveRodar)
                    {
                        opcaoSelecionada = telaMulta.ApresentarMenu();

                        switch (opcaoSelecionada)
                        {
                            case "1": telaMulta.Visualizar(true); break;

                            case "2": telaMulta.QuitarMulta(); break;

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
