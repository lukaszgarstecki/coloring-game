using System;

namespace ColoringGame
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
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
            else if (!int.TryParse(args[2], out int version))
            {
                PrintUsage();
            }
            else
            {
                var alpha = -1.0;
                if (version == 1)
                {
                    alpha = 0.75;
                }
                else if (version == 2)
                {
                    alpha = 0.5;
                }
                else if (version == 3)
                {
                    alpha = 0.25;
                }
                else
                {
                    PrintUsage();
                    return;
                }
                var board = new Board(size, streakLength, alpha);

                board.PerformGame();
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("3 positional arguments are required: board size, streak length, AI type");
            Console.WriteLine("1 => AI offensive");
            Console.WriteLine("2 => AI balanced");
            Console.WriteLine("3 => AI defensive");
        }
    }
}
