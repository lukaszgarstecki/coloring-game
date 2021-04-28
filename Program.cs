using System;

namespace ColoringGame
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 5)
            {
                PrintUsage();
            }
            else if (!int.TryParse(args[0], out int size))
            {
                PrintUsage();
            }
            else if (!int.TryParse(args[1], out int streakLength))
            {
                PrintUsage();
            }
            else if (!double.TryParse(args[2], out double alpha))
            {
                PrintUsage();
            }
            else if (!double.TryParse(args[3], out double beta))
            {
                PrintUsage();
            }
            else if (!double.TryParse(args[4], out double gamma))
            {
                PrintUsage();
            }
            else
            {
                var board = new Board(size, streakLength, alpha, beta, gamma);

                board.PerformGame();
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("5 positional arguments are required: board size, streak length, AI alpha, AI beta and AI gamma");
        }
    }
}
