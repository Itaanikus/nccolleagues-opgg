using Newtonsoft.Json;
using System.Collections.Generic;

namespace ColLeaguesOpGg.Models
{
    public class TeamDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public OrganisationDto Organisation { get; set; }

        public IList<TeamMemberDto> TeamMembers { get; set; }
    }
}