using Newtonsoft.Json;

namespace ColLeaguesOpGg.Models
{
    public class TeamMemberDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nickName")]
        public string NickName { get; set; }
    }
}