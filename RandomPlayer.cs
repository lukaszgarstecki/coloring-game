using System;
using System.Linq;

namespace ColoringGame
{
    public class RandomPlayer : Player
    {
        public Random random;

        public RandomPlayer(PlayerNumber playerNumber)
            : base(playerNumber)
        {
            random = new Random();
        }

        public override int SelectField(BoardField[] currentFields)
        {
            var availableFields = currentFields
                .Select((v, i) => new { Value = v, Index = i })
                .Where(x => x.Value == BoardField.Empty)
                .ToArray();

            return availableFields[random.Next(availableFields.Length)].Index;
        }
    }
}
