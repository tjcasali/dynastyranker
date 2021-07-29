using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynastyRanker.Models
{
    public class UserInfo
    {
        public string LeagueID { get; set; }

        [JsonProperty(PropertyName = "roster_positions")]
        public string[] LeagueRosterPositions { get; set; }

        [JsonProperty(PropertyName = "total_rosters")]
        public int TotalRosters { get; set; }

        public int QBCount { get; set; }

        public int RBCount { get; set; }

        public int WRCount { get; set; }

        public int TECount { get; set; }

        public int FLEXCount { get; set; }

        public int SUPERFLEXCount { get; set; }

        public int RECFLEXCount { get; set; }

        public bool SuperFlex { get; set; }
        //public string UserID { get; set; }
        
        [JsonProperty(PropertyName = "previous_league_id")]
        public string PreviousLeagueID { get; set; }

        [JsonProperty(PropertyName = "draft_id")]
        public string DraftID { get; set; }

        public string UserName { get; set; }

        [JsonProperty(PropertyName = "playoff_week_start")]
        public string PlayoffWeekStart { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string LeagueName { get; set; }
    }
}
