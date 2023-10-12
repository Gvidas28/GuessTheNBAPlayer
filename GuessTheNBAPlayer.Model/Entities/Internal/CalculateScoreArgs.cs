using GuessTheNBAPlayer.Model.Entities.External;

namespace GuessTheNBAPlayer.Model.Entities.Internal
{
    public class CalculateScoreArgs
    {
        public int CurrentScore { get; set; }
        public int ChosenTeamID { get; set; }
        public NBAPlayerResponse Player { get; set; }
    }
}