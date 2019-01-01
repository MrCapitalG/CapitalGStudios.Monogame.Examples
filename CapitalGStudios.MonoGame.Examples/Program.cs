using System;

namespace CapitalGStudios.MonoGame.Examples
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Examples())
            {
                game.Run();
            }
        }
    }
#endif
}