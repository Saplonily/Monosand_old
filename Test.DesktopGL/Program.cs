using Monosand;
using System.Runtime.InteropServices;

namespace Test.DesktopGL;

public class Program
{
    [DllImport("kernel32")]
    public static extern bool AllocConsole();

    public static void Main(string[] args)
    {
        AllocConsole();
        using var game = new MyGame();
        game.Run();
    }
}