using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynastyRanker.Models
{
    public class League
    {
        [JsonProperty(PropertyName = "league_id")]
        public string LeagueID { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string LeagueName { get; set; }
    }
}