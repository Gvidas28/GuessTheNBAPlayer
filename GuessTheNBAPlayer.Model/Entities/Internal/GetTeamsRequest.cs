using GuessTheNBAPlayer.Model.Entities.Enums;

namespace GuessTheNBAPlayer.Model.Entities.Internal
{
    public class GetTeamsRequest
    {
        public int CurrentScore { get; set; }
        public Difficulty Difficulty { get; set; }
        public int PlayerID { get; set; }
    }
}