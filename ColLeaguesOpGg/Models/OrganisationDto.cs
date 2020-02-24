using Newtonsoft.Json;

namespace ColLeaguesOpGg.Models
{
    public class OrganisationDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}