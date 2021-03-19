using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;


namespace DynastyRanker.Models
{
    public class TradedPick
    {
        [JsonProperty(PropertyName = "season")]
        public string Season { get; set; }

        [JsonProperty(PropertyName = "round")]
        public int Round { get; set; }

        [JsonProperty(PropertyName = "roster_id")]
        public int RosterIDOriginalOwnerForDraftPosition { get; set; }

        [JsonProperty(PropertyName = "previous_owner_id")]
        public int RosterIDOfPreviousOwner { get; set; }

        [JsonProperty(PropertyName = "owner_id")]
        public int RosterIDOfCurrentOwner { get; set; }

        public string Position {get; set;}
    }
}