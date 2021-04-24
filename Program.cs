using System;

namespace ColoringGame
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
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
            else
            {
                var board = new Board(size, streakLength);

                board.PerformGame();
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("2 positional arguments are required: board size and streak length");
        }
    }
}
