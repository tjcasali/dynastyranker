using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DynastyRanker.Models
{
    public class SleeperMatchups
    {
        public string Week { get; set; }

        [JsonProperty(PropertyName = "roster_id")]
        public string RosterId { get; set; }

        [JsonProperty(PropertyName = "matchup_id")]
        public string MatchupId { get; set; }    
    }
}
