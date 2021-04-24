using System;

namespace ColoringGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board(5, 3);

            board.PerformGame();
        }
    }
}
