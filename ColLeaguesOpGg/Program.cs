using ColLeaguesOpGg.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web;

namespace ColLeaguesOpGg
{
    public class Program
    {
        private static readonly long netcompanyOrganisationId = 661;
        private static readonly int lolGameTypeId = 2;
        private static HttpClient _client;

        public static void Main(string[] args)
        {
            _client = new HttpClient();
            var matchId = ValidateArgument(args);

            var response = RequestMatchDetails(matchId);

            var summonerNamesToCheck = GetSummonernames(response);
            var totalSummonerNameString = string.Join(",", summonerNamesToCheck);
            var opGgUrl = $"https://euw.op.gg/multi/query={HttpUtility.UrlEncode(totalSummonerNameString)}";
            OpenUrl(opGgUrl);
        }

        private static long ValidateArgument(string[] args)
        {
            string input;

            if (args == null || args.Length != 1)
            {
                Console.WriteLine("You did not provide a matchid.. Please enter the match id");
                input = Console.ReadLine();
            }
            else
            {
                input = args[0];
            }

            var parseSucceeded = long.TryParse(input, out var matchId);

            if (!parseSucceeded)
            {
                throw new ArgumentException("You failed to provide an integer, noob.. You can try again by rerunning the program.");
            }

            return matchId;
        }

        private static MatchDto RequestMatchDetails(long matchId)
        {
            var requestUrl = $"https://app.esportligaen.dk/api/match/details/{matchId}";
            return JsonConvert.DeserializeObject<MatchDto>(HttpGet(requestUrl));
        }

        private static List<string> GetSummonernames(MatchDto response)
        {
            var summonerNames = new List<string>();

            var enemyTeam = response.MatchTeams.Single(t => t.Team.Organisation.Id != netcompanyOrganisationId);
            foreach (var player in enemyTeam.Team.TeamMembers)
            {
                var gamerIdRequestUrl = $"https://app.esportligaen.dk/api/user/{player.Id}?includeGameTeamInfo=true";
                var user = JsonConvert.DeserializeObject<UserDto>(HttpGet(gamerIdRequestUrl));
                var gamerIds = user.GameLogins.Where(x => x.GameLoginTypeId == lolGameTypeId).Select(x => x.GamerId);
                summonerNames.AddRange(gamerIds);
            }

            return summonerNames;
        }

        private static string HttpGet(string requestUrl)
        {
            Console.WriteLine($"Requesting url: {requestUrl}");

            try
            {
                var response = _client.GetAsync(requestUrl).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }
                else
                {
                    throw new Exception($"The statuscode was not successful. Statuscode: {response.StatusCode}, reason: {response.ReasonPhrase}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("What the hell.. Something went very wrong :P Contact iro!");
                throw e;
            }
        }

        private static void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
