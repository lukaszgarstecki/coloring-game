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
            else if (!int.TryParse(args[3], out int numberOfGames))
            {
                PrintUsage();
            }
            else
            {
                var player1 = new RandomPlayer(PlayerNumber.Player1);
                var player1Wins = 0;
                var player2Wins = 0;
                for (var i = 0; i < numberOfGames; ++i)
                {
                    var board = new Board(size, streakLength, alpha, player1);

                    var result = board.PerformGame();

                    if (result == GameStatus.Player1Won)
                    {
                        player1Wins++;
                    }
                    else if (result == GameStatus.Player2Won)
                    {
                        player2Wins++;
                    }
                }

                Console.WriteLine($"Player 1 won {player1Wins} times, player 2 won {player2Wins} times, games played {numberOfGames}");
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("4 positional arguments are required: board size, streak length, AI alpha and number of games");
        }
    }
}
