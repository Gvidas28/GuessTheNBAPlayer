using GuessTheNBAPlayer.Model.Entities.Internal;
using GuessTheNBAPlayer.Model.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessTheNBAPlayer.Server.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class ScoreController : Controller
    {
        private readonly IScoreService _scoreService;

        public ScoreController(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        [HttpPost]
        public ServerResult<ScoreResponse> UpdateScore([FromBody]ScoreRequest request) => _scoreService.UpdateScore(request);
    }
}