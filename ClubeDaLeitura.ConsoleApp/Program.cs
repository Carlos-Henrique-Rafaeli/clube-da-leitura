using ClubeDaLeitura.ConsoleApp.Compartilhado;
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
        TelaPrincipal telaPrincipal = new TelaPrincipal();

        while (true)
        {
            telaPrincipal.ApresentarMenu();

            ITelaCrud telaSelecionada = telaPrincipal.ObterTela();

            if (telaSelecionada == null)
            {
                if (telaPrincipal.OpcaoPrincipal == "S")
                    return;

                Notificador.ExibirMensagem("Opção inválida!", ConsoleColor.Red);
                continue;
            }

            bool deveRodar = true;
            while (deveRodar)
            {
                string opcaoEscolhida = telaSelecionada.ApresentarMenu();
                
                if (telaSelecionada is TelaEmprestimo)
                {
                    if (opcaoEscolhida == "5")
                    {
                        TelaEmprestimo telaEmprestimo = (TelaEmprestimo)telaSelecionada;

                        telaEmprestimo.RegistrarDevolucao();
                    }
                }
                else if (telaSelecionada is TelaReserva)
                {
                    TelaReserva telaReserva = (TelaReserva)telaSelecionada;

                    if (opcaoEscolhida == "1")
                        telaReserva.CadastrarRegistro();

                    else if (opcaoEscolhida == "2")
                        telaReserva.CancelarReserva();

                    else if (opcaoEscolhida == "3")
                        telaReserva.VisualizarRegistros(true);

                    else if (opcaoEscolhida == "S")
                        break;
                    
                    continue;
                }
                else if (telaSelecionada is TelaMulta)
                {
                    TelaMulta telaMulta = (TelaMulta)telaSelecionada;

                    if (opcaoEscolhida == "1")
                        telaMulta.VisualizarRegistros(true);

                    else if (opcaoEscolhida == "2")
                        telaMulta.QuitarMulta();

                    else if (opcaoEscolhida == "S")
                        break;

                    continue;
                }

                switch (opcaoEscolhida)
                {
                    case "1": telaSelecionada.CadastrarRegistro(); break;

                    case "2": telaSelecionada.EditarRegistro(); break;

                    case "3": telaSelecionada.ExcluirRegistro(); break;

                    case "4": telaSelecionada.VisualizarRegistros(true); break;

                    case "S": deveRodar = false; break;

                    default: Notificador.ExibirMensagem("Opção inválida!", ConsoleColor.Red); break;
                }
            }
        }
    }
}
