using System;
using System.Collections.Generic;
using System.Linq;

namespace ColoringGame
{
    public class Board
    {
        public int Size { get; }
        public int StreakLength { get; }
        private BoardField[] Fields { get; }
        private Player Player1 { get; }
        private Player Player2 { get; }
        private PlayerNumber CurrentPlayer { get; set; }
        private List<int[]> LoosingSequences { get; }

        public Board(int size, int streakLength, double alpha, double beta)
        {
            Size = size;
            StreakLength = streakLength;
            Fields = Enumerable.Repeat(BoardField.Empty, size).ToArray();
            LoosingSequences = GenerateLoosingSequences();
            Player1 = new HumanPlayer(PlayerNumber.Player1);
            Player2 = new AiPlayer2(PlayerNumber.Player2, alpha, beta, StreakLength, LoosingSequences);
            CurrentPlayer = PlayerNumber.Player1;
        }

        private List<int[]> GenerateLoosingSequences()
        {
            var result = new List<int[]>();

            var sequenceStart = 0;
            var step = 0;
            var generate = true;

            while (generate)
            {
                sequenceStart = 0;
                step++;
                var listLength = result.Count;

                while (sequenceStart + step * (StreakLength - 1) < Size)
                {
                    result.Add(
                        Enumerable.Repeat(sequenceStart, StreakLength)
                            .Select((v, i) => v + i * step)
                            .ToArray());
                    sequenceStart++;
                }

                generate = listLength != result.Count;
            }

            return result;
        }

        public GameStatus PerformGame()
        {
            var gameStatus = GetGameStatus();
            while (gameStatus == GameStatus.InProgress)
            {
                if (CurrentPlayer == PlayerNumber.Player1)
                {
                    Play(Player1, PlayerNumber.Player2);
                }
                else
                {
                    Play(Player2, PlayerNumber.Player1);
                }
                gameStatus = GetGameStatus();

                PrintGame(gameStatus);
            }

            return gameStatus;
        }

        private void PrintGame(GameStatus gameStatus)
        {
            Console.WriteLine($"Current game status {gameStatus}");
            Console.WriteLine("Current board state: ");
            Console.ForegroundColor = ConsoleColor.White;
            for (var i = 0; i < Fields.Length; ++i)
            {
                Console.BackgroundColor = GetColor(Fields[i]);
                Console.Write($"{i + 1,4} ");
                Console.ResetColor();
                if ((i + 1) % StreakLength == 0)
                {
                    Console.WriteLine();
                }
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        private ConsoleColor GetColor(BoardField boardField)
        {
            return boardField switch
            {
                BoardField.Player1 => ConsoleColor.Blue,
                BoardField.Player2 => ConsoleColor.Red,
                _ => ConsoleColor.Black,
            };
        }

        private void Play(Player currentPlayer, PlayerNumber nextPlayer)
        {
            var fieldIndex = currentPlayer.SelectField(Fields.ToArray());
            if (Fields[fieldIndex] == BoardField.Empty)
            {
                Fields[fieldIndex] = (BoardField)currentPlayer.PlayerNumber;
            }
            else
            {
                throw new InvalidOperationException(
                    $"Player {currentPlayer.PlayerNumber} tried to play on field {fieldIndex} with value {Fields[fieldIndex]}");
            }

            CurrentPlayer = nextPlayer;
        }

        private GameStatus GetGameStatus()
        {
            if (LoosingSequences.Where(s => s.All(v => Fields[v] == BoardField.Player1)).Any())
            {
                return GameStatus.Player2Won;
            }
            else if (LoosingSequences.Where(s => s.All(v => Fields[v] == BoardField.Player2)).Any())
            {
                return GameStatus.Player1Won;
            }
            else if (Fields.All(v => v != BoardField.Empty))
            {
                return GameStatus.Draw;
            }
            else
            {
                return GameStatus.InProgress;
            }
        }
    }

    public enum BoardField
    {
        Empty = 0,
        Player1 = 1,
        Player2 = 2,
    }

    public enum GameStatus
    {
        InProgress = 0,
        Player1Won = 1,
        Player2Won = 2,
        Draw = 3,
    }
}
