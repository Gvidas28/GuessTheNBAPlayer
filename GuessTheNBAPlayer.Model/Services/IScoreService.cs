using GuessTheNBAPlayer.Model.Entities.Internal;

namespace GuessTheNBAPlayer.Model.Services
{
    public interface IScoreService
    {
        ServerResult<ScoreResponse> UpdateScore(ScoreRequest request);
    }
}