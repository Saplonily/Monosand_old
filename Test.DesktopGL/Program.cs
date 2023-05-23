using Monosand;
using System.Runtime.InteropServices;

namespace Test.DesktopGL;

public class Program
{
    [DllImport("kernel32")]
    public static extern bool AllocConsole();

    public static void Main(string[] args)
    {
        try
        {
            AllocConsole();
            using var game = new MyGame();
            game.Run();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            Console.ReadLine();
        }
    }
}