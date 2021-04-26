using System;
using System.Linq;

namespace ColoringGame
{
    public class HumanPlayer : Player
    {

        public HumanPlayer(PlayerNumber playerNumber)
            : base(playerNumber)
        { }

        public override int SelectField(BoardField[] currentFields)
        {
            var availableFields = currentFields
                .Select((v, i) => new { Value = v, Index = i })
                .Where(x => x.Value == BoardField.Empty)
                .ToArray();

            Console.Write($"Available fields for player {PlayerNumber}: ");
            for (var i = 0; i < availableFields.Length; ++i)
            {
                Console.Write($"{availableFields[i].Index + 1} ");
            }
            Console.WriteLine();

            while (true)
            {
                Console.Write($"Please write available field number: ");
                var read = Console.ReadLine();
                if (int.TryParse(read, out int v) && availableFields.Any(f => f.Index == v - 1))
                {
                    return v - 1;
                }
            }
        }
    }
}
