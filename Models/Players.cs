using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynastyRanker.Models
{
    public class PlayerData
    {
        [JsonProperty(PropertyName = "player_id")]
        public string PlayerID { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "position")]
        public string Position { get; set; }

        [JsonProperty(PropertyName = "team")]
        public string Team { get; set; }

        [JsonProperty(PropertyName = "age")]
        public string Age { get; set; }

        public string TradeValueChart { get; set; }

        public string KeepTradeCutValue { get; set; }

        public string WeekAgoValue { get; set; }
    }

    public class Players
    {
        public List<PlayerData> Player { get; set; }
    }

    //IEnumerator IEnumerable.GetEnumerator()
    //{
    //    return this.GetEnumerator();
    //}
}
