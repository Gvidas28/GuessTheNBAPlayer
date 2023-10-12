using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuessTheNBAPlayer.Model.Entities.External
{
    public class NBATeamListResponse
    {
        [JsonProperty("data")]
        public List<NBATeam> Data { get; set; }
        [JsonProperty("meta")]
        public NBAPaginationInfo Meta { get; set; }
    }
}