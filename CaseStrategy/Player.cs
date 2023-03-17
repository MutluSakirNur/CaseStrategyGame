namespace CaseStrategy
{
    public class Player
    {
        public int TotalHealth { get; set; } = 100;
        public List<PlayerMoves> CharacterMoves { get; set; } = new List<PlayerMoves>();
    }
}
