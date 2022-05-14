using System;

namespace Eragonia_Demo_Day_One
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (World game = new World())
            {
                game.Run();
            }
        }
    }
#endif
}

