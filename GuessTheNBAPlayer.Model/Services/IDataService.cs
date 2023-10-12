using GuessTheNBAPlayer.Model.Entities.Internal;
using System.Collections.Generic;

namespace GuessTheNBAPlayer.Model.Services
{
    public interface IDataService
    {
        ServerResult<List<Team>> GetTeams(GetTeamsRequest request);
        ServerResult<Player> GetRandomPlayer();
    }
}