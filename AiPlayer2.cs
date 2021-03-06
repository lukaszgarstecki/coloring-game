using System;
using System.Collections.Generic;
using System.Linq;

namespace ColoringGame
{
    public class AiPlayer2 : Player
    {
        public double Alpha { get; }
        public double Beta { get; }
        public double Gamma { get; }
        public int StreakLength { get; }
        public List<int[]> LoosingSequences { get; }

        public AiPlayer2(PlayerNumber playerNumber, double alpha, int boardSize, int streakLength, List<int[]> loosingSequences)
            : base(playerNumber)
        {
            if (playerNumber == PlayerNumber.Player1)
            {
                throw new InvalidOperationException("This AI player can be player 2 only.");
            }

            if (alpha > 1 || alpha < 0)
            {
                throw new ArgumentException("alpha must be double from [0, 1]", nameof(alpha));
            }

            Alpha = alpha;
            Beta = 1 - alpha;
            Gamma = Math.Pow(boardSize, 3);
            StreakLength = streakLength;
            LoosingSequences = loosingSequences.Select(s => s.ToArray()).ToList();
        }

        public override int SelectField(BoardField[] currentFields)
        {
            if (currentFields.Length % 2 == 0)
            {
                return NonLoosingStrategy(currentFields);
            }
            else
            {
                return OptimizingStrategy(currentFields);
            }
        }

        private int NonLoosingStrategy(BoardField[] currentFields)
        {
            for (var i = 0; i < currentFields.Length; ++i)
            {
                if (currentFields[i] == BoardField.Player1 && currentFields[^(i + 1)] == BoardField.Empty)
                {
                    return currentFields.Length - i - 1;
                }
                else if (currentFields[i] == BoardField.Empty && currentFields[^(i + 1)] == BoardField.Player1)
                {
                    return i;
                }
            }

            // Unreachable
            throw new NotImplementedException();
        }

        private int OptimizingStrategy(BoardField[] currentFields)
        {
            var fieldValues = new double[currentFields.Length];
            for (var i = 0; i < currentFields.Length; ++i)
            {
                var currentProlong = new int[StreakLength];
                var opponentProlong = new int[StreakLength];
                var sequences = 0;

                if (currentFields[i] != BoardField.Empty)
                {
                    fieldValues[i] = double.PositiveInfinity;
                    continue;
                }
                foreach (var s in LoosingSequences.Where(s => s.Contains(i)))
                {
                    // foreach (var j in s)
                    // {
                    // Console.Write($"{j + 1,4} ");
                    // }
                    // Console.WriteLine();
                    sequences++;
                    var emptyFieldsInSequence = s.Where(j => currentFields[j] == BoardField.Empty).Count();
                    var currentPlayerFieldsInSequence = s.Where(j => currentFields[j] == (BoardField)PlayerNumber).Count();
                    var opponentPlayerFieldsInSequence = s.Length - emptyFieldsInSequence - currentPlayerFieldsInSequence;
                    // Console.WriteLine($"{emptyFieldsInSequence} {currentPlayerFieldsInSequence} {opponentPlayerFieldsInSequence}");

                    if (currentPlayerFieldsInSequence == 0 && opponentPlayerFieldsInSequence > 0)
                    {
                        opponentProlong[opponentPlayerFieldsInSequence - 1]++;
                    }
                    else if (currentPlayerFieldsInSequence > 0 && opponentPlayerFieldsInSequence == 0)
                    {
                        currentProlong[currentPlayerFieldsInSequence - 1]++;
                    }
                }

                // Console.WriteLine($"Current field: {i + 1}");
                // Console.WriteLine("Opponent sequences:");
                // for (var j = 0; j < opponentProlong.Length; ++j)
                // {
                //     Console.WriteLine($"{j + 1}:{opponentProlong[j]}");
                // }
                // Console.WriteLine("Current sequences:");
                // for (var j = 0; j < currentProlong.Length; ++j)
                // {
                //     Console.WriteLine($"{j + 1}:{currentProlong[j]}");
                // }
                // Console.WriteLine($"Sequences count: {sequences}");

                fieldValues[i] = sequences
                    + Alpha * (
                        opponentProlong.Take(opponentProlong.Length - 2).Select((v, j) => v * (j + 1)).Sum())
                    + Gamma * Math.Min(opponentProlong[StreakLength - 2], 1)
                    + Beta * (
                        currentProlong.Take(currentProlong.Length - 2).Select((v, j) => v * (j + 1)).Sum())
                    + 2 * Gamma * Math.Min(currentProlong[StreakLength - 2], 1);

            }
            return fieldValues.Select((v, i) => new { Value = v, Index = i })
                .OrderBy(x => x.Value)
                .Select(x => x.Index)
                .First();
        }
    }
}
