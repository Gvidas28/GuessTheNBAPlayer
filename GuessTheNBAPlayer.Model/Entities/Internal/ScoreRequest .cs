using GuessTheNBAPlayer.Model.Entities.Enums;

namespace GuessTheNBAPlayer.Model.Entities.Internal
{
    public class ScoreRequest
    {
        public int CurrentScore { get; set; }
        public int PlayerID { get; set; }
        public int TeamID { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}