namespace GuessTheNBAPlayer.Model.Entities.Internal
{
    public class ScoreResponse
    {
        public bool Correct { get; set; }
        public string ActualTeam { get; set; }
        public int UpdatedScore { get; set; }
    }
}