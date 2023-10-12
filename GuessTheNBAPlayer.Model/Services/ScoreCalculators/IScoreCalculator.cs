using GuessTheNBAPlayer.Model.Entities.Enums;
using GuessTheNBAPlayer.Model.Entities.External;
using GuessTheNBAPlayer.Model.Entities.Internal;

namespace GuessTheNBAPlayer.Model.Services.ScoreCalculators
{
    public interface IScoreCalculator
    {
        Difficulty Difficulty { get; }
        bool CalculateScore(CalculateScoreArgs args, out int newScore);
    }
}