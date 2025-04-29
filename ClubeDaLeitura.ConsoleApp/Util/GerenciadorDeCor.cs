using System.Drawing;

namespace ClubeDaLeitura.ConsoleApp.Util;

public static class GerenciadorDeCor
{
    public static ConsoleColor CorParaConsoleColor(Color cor)
    {
        ConsoleColor consoleCor;

        int argb = cor.ToArgb();

        if (argb == Color.Red.ToArgb()) consoleCor = ConsoleColor.Red;
        else if (argb == Color.Green.ToArgb()) consoleCor = ConsoleColor.Green;
        else if (argb == Color.Blue.ToArgb()) consoleCor = ConsoleColor.Blue;
        else if (argb == Color.Yellow.ToArgb()) consoleCor = ConsoleColor.Yellow;
        else consoleCor = ConsoleColor.White;

        return consoleCor;
    }

    public static string CorParaString(Color cor)
    {
        string corString;

        int argb = cor.ToArgb();

        if (argb == Color.Red.ToArgb()) corString = "Vermelho";
        else if (argb == Color.Green.ToArgb()) corString = "Verde";
        else if (argb == Color.Blue.ToArgb()) corString = "Azul";
        else if (argb == Color.Yellow.ToArgb()) corString = "Amarelo";
        else corString = "Branco";

        return corString;
    }
}
