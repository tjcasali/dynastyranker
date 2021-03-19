using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynastyRanker.Models
{
    public class SleeperUsers
    {
        [JsonProperty(PropertyName = "user_id")]
        public string UserID { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        public string RosterID { get; set; }
    }
}
