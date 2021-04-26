using System;

namespace ColoringGame
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4)
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
            else
            {
                var board = new Board(size, streakLength, alpha, beta);

                board.PerformGame();
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("4 positional arguments are required: board size, streak length, AI alpha and AI beta");
        }
    }
}
