using GuessTheNBAPlayer.Model.Entities.Enums;
using GuessTheNBAPlayer.Model.Entities.Internal;

namespace GuessTheNBAPlayer.Model.Services.ScoreCalculators
{
    public class EasyScoreCalculator : IScoreCalculator
    {
        public Difficulty Difficulty => Difficulty.Easy;

        public bool CalculateScore(CalculateScoreArgs args, out int newScore)
        {
            newScore = args.CurrentScore;

            if (args.ChosenTeamID != args.Player.Team.ID)
            {
                newScore--;
                return false;
            }

            newScore+=2;
            return true;
        }
    }
}