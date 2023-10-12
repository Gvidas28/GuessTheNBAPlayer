using Newtonsoft.Json;

namespace GuessTheNBAPlayer.Model.Entities.External
{
    public class NBAPlayerResponse
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("position")]
        public string Position { get; set; }
        [JsonProperty("height_feet")]
        public int? HeightFeet { get; set; }
        [JsonProperty("height_inches")]
        public int? HeightInches { get; set; }
        [JsonProperty("weight_pounds")]
        public int? WeightPounds { get; set; }
        [JsonProperty("team")]
        public NBATeam Team { get; set; }
    }
}