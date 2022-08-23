namespace QuantumDriver
{
    public class Position
    {
        public Player Player { get; set; }
        
        public Position(Player player)
        {
            Player = player;
        }

        public override string? ToString()
        {
            return Player switch
            {
                Player.One => "X",
                Player.Two => "O",
                _ => " ",
            };
        }
    }

    public enum Player
    {
        One,
        Two
    }
}