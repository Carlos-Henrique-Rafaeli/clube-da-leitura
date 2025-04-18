﻿using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa;

public class RepositorioCaixa
{
    Caixa[] caixas = new Caixa[100];
    int contadorCaixas = 0;

    public void Inserir(Caixa novaCaixa)
    {
        novaCaixa.id = GeradorIds.GerarIdCaixa();

        caixas[contadorCaixas++] = novaCaixa;
    }

    public bool Editar(int idCaixa, Caixa caixaEditada)
    {
        if (caixaEditada == null) return false;

        foreach (Caixa c in caixas)
        {
            if (c == null) continue;

            if (c.id == idCaixa)
            {
                c.etiqueta = caixaEditada.etiqueta;
                c.cor = caixaEditada.cor;
                c.diasEmprestimo = caixaEditada.diasEmprestimo;

                return true;
            }
        }
        return false;
    }

    public bool Excluir(int idCaixa)
    {
        for (int i = 0; i < caixas.Length; i++)
        {
            if (caixas[i] == null) continue;

            if (caixas[i].id == idCaixa)
            {
                caixas[i] = null;

                return true;
            }
        }
        return false;
    }

    public Caixa[] SelecionarTodos()
    {
        return caixas;
    }

    public Caixa SelecionarPorId(int idCaixa)
    {
        foreach (Caixa c in caixas)
        {
            if (c == null) continue;

            if (c.id == idCaixa) return c;
        }
        return null;
    }

    public bool VerificarEtiqueta(string etiqueta, int id = -1)
    {
        foreach (Caixa c in caixas)
        {
            if (c == null) continue;

            if (c.id == id) continue;

            if (c.etiqueta == etiqueta) return true;
        }

        return false;
    }

}
