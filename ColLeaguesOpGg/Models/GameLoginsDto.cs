using Newtonsoft.Json;

namespace ColLeaguesOpGg.Models
{
    public class GameLoginsDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("gamerId")]
        public string GamerId { get; set; }

        [JsonProperty("gameLoginTypeId")]
        public int GameLoginTypeId { get; set; }

        [JsonProperty("gameLoginTypeName")]
        public string GameLoginTypeName { get; set; }
    }
}