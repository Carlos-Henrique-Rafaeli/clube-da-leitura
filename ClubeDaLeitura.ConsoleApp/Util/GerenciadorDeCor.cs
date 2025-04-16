using System.Drawing;

namespace ClubeDaLeitura.ConsoleApp.Util;

public static class GerenciadorDeCor
{
    public static ConsoleColor CorParaConsoleColor(Color cor)
    {
        ConsoleColor consoleCor;

        if (cor == Color.Red) consoleCor = ConsoleColor.Red;
        else if (cor == Color.Green) consoleCor = ConsoleColor.Green;
        else if (cor == Color.Blue) consoleCor = ConsoleColor.Blue;
        else if (cor == Color.Yellow) consoleCor = ConsoleColor.Yellow;
        else consoleCor = ConsoleColor.White;

        return consoleCor;
    }

    public static string CorParaString(Color cor)
    {
        string corString;

        if (cor == Color.Red) corString = "Vermelho";
        else if (cor == Color.Green) corString = "Verde";
        else if (cor == Color.Blue) corString = "Azul";
        else if (cor == Color.Yellow) corString = "Amarelo";
        else corString = "Branco";

        return corString;
    }
}
