using GuessTheNBAPlayer.Model.Entities;
using GuessTheNBAPlayer.Model.Entities.External;
using GuessTheNBAPlayer.Model.Entities.Internal;
using GuessTheNBAPlayer.Model.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace GuessTheNBAPlayer.Model.Services
{
    public class DataService : IDataService
    {
        private readonly IClientService _clientService;
        private readonly IPictureService _pictureService;
        private readonly ILogger<DataService> _logger;

        public DataService(
            IClientService clientService,
            IPictureService pictureService,
            ILogger<DataService> logger)
        {
            _clientService = clientService;
            _pictureService = pictureService;
            _logger = logger;
        }

        public ServerResult<List<Team>> GetTeams(GetTeamsRequest request)
        {
            try
            {
                var teamsUrl = new UriBuilder(Constants.API_URL);
                teamsUrl.Path += "teams";
                var playerUrl = new UriBuilder(Constants.API_URL);
                playerUrl.Path += $"players/{request.PlayerID}";

                var teamsResponse = _clientService.SendRequest<NBATeamListResponse>(teamsUrl.ToString());
                var playerResponse = _clientService.SendRequest<NBAPlayerResponse>(playerUrl.ToString());

                var teams = teamsResponse.Data.Select(x => new Team { ID = x.ID, Name = x.FullName }).ToList();
                var teamsToRemove = teams
                    .Where(x => x.ID != playerResponse.Team.ID)
                    .OrderBy(_ => HelperService.GetRandomNumber(1, 100))
                    .Take(GetExclusionCount(request.Difficulty, request.CurrentScore))
                    .ToList();

                teamsToRemove.ForEach(x => teams.Remove(x));

                return new() { Success = true, Data = teams };
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while loading teams: {ex}");
                return new() { Success = false, ErrorMessage = $"Error while loading teams: {ex.Message}" };
            }
        }

        public ServerResult<Player> GetRandomPlayer()
        {
            try
            {
                var randomPlayerId = HelperService.GetRandomNumber(1, 1000);

                var url = new UriBuilder(Constants.API_URL);
                url.Path += $"players/{randomPlayerId}";

                var player = _clientService.SendRequest<NBAPlayerResponse>(url.ToString());

                var position = Enum.GetName(typeof(PlayerPosition), HelperService.DeterminePlayerPosition(player.Position));
                var weight = HelperService.DeterminePlayerWeight(player.WeightPounds);
                var height = HelperService.DeterminePlayerHeight(player.HeightFeet, player.HeightInches);

                var pictureUrl = _pictureService.GetFirstGoogleImage($"{player.FirstName} {player.LastName} NBA");

                return new()
                {
                    Success = true,
                    Data = new()
                    {
                        ID = player.ID,
                        Name = $"{player.FirstName} {player.LastName}",
                        Position = position,
                        Weight = weight,
                        Height = height,
                        PictureURL = pictureUrl
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Error while getting player: {ex}");
                return new() { Success = false, ErrorMessage = $"Error while getting player: {ex.Message}" };
            }
        }

        private int GetExclusionCount(Difficulty difficulty, int score) => difficulty switch
        {
            Difficulty.Easy => 25,
            Difficulty.Normal => 20,
            Difficulty.Hard when score < 30 => 15,
            Difficulty.Hard => 5,
            _ => 0
        };

        ~DataService()
        {
            _logger.Log(LogLevel.Trace, "Cleaning up DataService!");
        }
    }
}