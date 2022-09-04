using SadConsole;
using SadRogue.Primitives;
using System;
using Console = SadConsole.Console;

namespace LiteChat
{
    internal class Program
    {
       /*   Version definition:
        *   b       = beta
        *   #.00    = main release
        *   0.##    = release
        */

        const string VERSION = "0.01b";

        const int SCREEN_WIDTH = 120;
        const int SCREEN_HEIGHT = 30;

        static readonly ScreenObject MAIN_CONTAINER = new ScreenObject();

        private static void Main(string[] args)
        {

            Settings.WindowTitle = $"LiteChat - {VERSION}";
            Settings.UseDefaultExtendedFont = true;

            Game.Create(SCREEN_WIDTH, SCREEN_HEIGHT);

            Game.Instance.OnStart = Init;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Init()
        {
            Game.Instance.Screen = MAIN_CONTAINER;
            Game.Instance.DestroyDefaultStartingConsole();
        }
    }
}
