using Newtonsoft.Json;
using System.Collections.Generic;

namespace ColLeaguesOpGg.Models
{
    public class UserDto
    {
        [JsonProperty("gameLogins")]
        public IList<GameLoginsDto> GameLogins { get; set; }
    }
}