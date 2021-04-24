namespace ColoringGame
{
    public abstract class Player
    {
        public PlayerNumber PlayerNumber { get; }

        public Player(PlayerNumber playerNumber)
        {
            PlayerNumber = playerNumber;
        }

        public abstract int SelectField(BoardField[] currentFields);
    }

    // Keep in sync with BoardField
    public enum PlayerNumber
    {
        Player1 = 1,
        Player2 = 2,
    }
}
