using System;

namespace NTSSP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (NTSSPGame game = new NTSSPGame())
            {
                game.Run();
            }
        }
    }
}

