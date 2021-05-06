using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace DynastyRanker.Models
{
    public class Draft
    {
        [JsonProperty(PropertyName = "rounds")]
        public int Rounds { get; set; }

        [JsonProperty(PropertyName = "draft_order")]
        public Dictionary<string, int> DraftOrder { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

    }
}