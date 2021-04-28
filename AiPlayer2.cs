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

        public AiPlayer2(PlayerNumber playerNumber, double alpha, double beta, double gamma, int streakLength, List<int[]> loosingSequences)
            : base(playerNumber)
        {
            if (playerNumber == PlayerNumber.Player1)
            {
                throw new InvalidOperationException("This AI player can be player 2 only.");
            }
            Alpha = alpha;
            Beta = beta;
            Gamma = gamma;
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

                Console.WriteLine($"Current field: {i + 1}");

                if (currentFields[i] != BoardField.Empty)
                {

                    Console.WriteLine("Field not empty, ignoring");
                    fieldValues[i] = double.PositiveInfinity;
                    continue;
                }
                foreach (var s in LoosingSequences.Where(s => s.Contains(i)))
                {
                    Console.WriteLine("Processing sequence:");
                    foreach (var j in s)
                    {
                        Console.Write($"{j + 1,4} ");
                    }
                    Console.WriteLine();
                    sequences++;
                    var emptyFieldsInSequence = s.Where(j => currentFields[j] == BoardField.Empty).Count();
                    var currentPlayerFieldsInSequence = s.Where(j => currentFields[j] == (BoardField)PlayerNumber).Count();
                    var opponentPlayerFieldsInSequence = s.Length - emptyFieldsInSequence - currentPlayerFieldsInSequence;
                    Console.WriteLine($"Empty fields in sequence count:{emptyFieldsInSequence}");
                    Console.WriteLine($"Current player fields in sequence count:{currentPlayerFieldsInSequence}");
                    Console.WriteLine($"Opponent player fields in sequence count:{opponentPlayerFieldsInSequence}");

                    if (currentPlayerFieldsInSequence == 0 && opponentPlayerFieldsInSequence > 0)
                    {
                        opponentProlong[opponentPlayerFieldsInSequence - 1]++;
                    }
                    else if (currentPlayerFieldsInSequence > 0 && opponentPlayerFieldsInSequence == 0)
                    {
                        currentProlong[currentPlayerFieldsInSequence - 1]++;
                    }
                }

                Console.WriteLine("Opponent sequences:");
                for (var j = 0; j < opponentProlong.Length; ++j)
                {
                    Console.WriteLine($"{j + 1}:{opponentProlong[j]}");
                }
                Console.WriteLine("Current sequences:");
                for (var j = 0; j < currentProlong.Length; ++j)
                {
                    Console.WriteLine($"{j + 1}:{currentProlong[j]}");
                }
                Console.WriteLine($"Sequences count: {sequences}");

                fieldValues[i] = sequences
                    + 1 / Alpha * (opponentProlong.Select((v, j) => v * (j + 1)).Sum() + Gamma * opponentProlong[StreakLength - 2])
                    + 1 / Beta * (currentProlong.Select((v, j) => v * (j + 1)).Sum() + 2 * Beta / Alpha * Gamma * currentProlong[StreakLength - 2]);
                Console.WriteLine($"Calculated field value: {fieldValues[i]}");

            }

            Console.WriteLine("Calculated field values:");
            for (var i = 0; i < fieldValues.Length; ++i)
            {
                Console.WriteLine($"{i + 1}: {fieldValues[i]}");
            }
            Console.WriteLine();


            return fieldValues.Select((v, i) => new { Value = v, Index = i })
                .OrderBy(x => x.Value)
                .Select(x => x.Index)
                .First();
        }
    }
}
