using System;
using System.Linq;

namespace ColoringGame
{
    public class AiPlayer2 : Player
    {

        public AiPlayer2(PlayerNumber playerNumber)
            : base(playerNumber)
        {
            if (playerNumber == PlayerNumber.Player1)
            {
                throw new InvalidOperationException("This AI player can be player 2 only.");
            }
        }

        public override int SelectField(BoardField[] currentFields)
        {
            if (currentFields.Length % 2 == 0)
            {
                return NonLoosingStrategy(currentFields);
            }
            else
            {
                throw new NotImplementedException();
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
    }
}
