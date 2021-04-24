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
            return currentFields
                .Select((v, i) => new { v, i })
                .Where(x => x.v == BoardField.Empty)
                .Select(x => x.i)
                .First();
        }
    }
}
