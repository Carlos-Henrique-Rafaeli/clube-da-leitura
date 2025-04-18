﻿using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloMulta;
using ClubeDaLeitura.ConsoleApp.ModuloReserva;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

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
                            
                            case "3": telaRevista.Excluir(); break;

                            case "4": telaRevista.VisualizarTodos(true); break;

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
                            case "1": telaEmprestimo.Inserir(); break;
                            
                            case "2": telaEmprestimo.Editar(); break;

                            case "3": telaEmprestimo.Excluir(); break;
                            
                            case "4": telaEmprestimo.VisualizarTodos(true); break;
                            
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
