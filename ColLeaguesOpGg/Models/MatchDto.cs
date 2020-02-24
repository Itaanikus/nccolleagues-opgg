using ColLeaguesOpGg.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ColLeaguesOpGg
{
    public class MatchDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("MatchTeams")]
        public IList<MatchTeamsDto> MatchTeams { get; set; }
    }
}
