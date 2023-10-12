using GuessTheNBAPlayer.Model.Entities;
using GuessTheNBAPlayer.Model.Entities.External;
using GuessTheNBAPlayer.Model.Entities.Internal;
using GuessTheNBAPlayer.Model.Services.ScoreCalculators;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GuessTheNBAPlayer.Model.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IClientService _clientService;
        private readonly List<IScoreCalculator> _scoreCalculators;
        private readonly ILogger<ScoreService> _logger;

        public ScoreService(
            IClientService clientService,
            IEnumerable<IScoreCalculator> scoreCalculators,
            ILogger<ScoreService> logger)
        {
            _clientService = clientService;
            _scoreCalculators = scoreCalculators.ToList();
            _logger = logger;
        }

        public ServerResult<ScoreResponse> UpdateScore(ScoreRequest request)
        {
            try
            {
                var url = new UriBuilder(Constants.API_URL);
                url.Path += $"players/{request.PlayerID}";

                var player = _clientService.SendRequest<NBAPlayerResponse>(url.ToString());

                var calculator = _scoreCalculators.SingleOrDefault(x => x.Difficulty == request.Difficulty);
                if (calculator is null)
                    return new() { Success = false, ErrorMessage = "Could not find a score calculator according to difficulty!" };

                var calculationArgs = new CalculateScoreArgs
                {
                    CurrentScore = request.CurrentScore,
                    ChosenTeamID = request.TeamID,
                    Player = player
                };

                var answerCorrect = calculator.CalculateScore(calculationArgs, out int newScore);

                return new()
                {
                    Success = true,
                    Data = new()
                    {
                        Correct = answerCorrect,
                        UpdatedScore = newScore,
                        ActualTeam = answerCorrect ? null : player.Team.FullName
                    }
                };

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while updating score: {ex}");
                return new() { Success = false, ErrorMessage = $"Error while updating score: {ex.Message}" };
            }
        }
    }
}