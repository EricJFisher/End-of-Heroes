using EndOfHeroes.Services;
using System;

namespace EndOfHeroes
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            CrashHandler.Bind();

            try
            {
                using (var game = new Desktop())
                    game.Run();
            }
            catch (Exception e)
            {
                CrashHandler.Report(e);
            }
        }
    }
}
