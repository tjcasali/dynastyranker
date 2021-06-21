using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynastyRanker.Models
{
    public class Rosters
    {
        [JsonProperty(PropertyName = "roster_id")]
        public string RosterID { get; set; }

        [JsonProperty(PropertyName = "starters")]
        public string[] Starters { get; set; }

        [JsonProperty(PropertyName = "players")]
        public string[] Bench { get; set; }

        [JsonProperty(PropertyName = "taxi")]
        public string[] TaxiSquad { get; set; }

        [JsonProperty(PropertyName = "owner_id")]
        public string OwnerID { get; set; }

        //[JsonProperty(PropertyName = "wins")]
        public string Wins { get; set; }

        //[JsonProperty(PropertyName = "losses")]
        public string Losses { get; set; }

        public string DisplayName { get; set; }

        public List<string> PlayerNames { get; set; }

        public List<string> PlayerTradeValues { get; set; }

        public double TeamRankingAverage { get; set; }
        public double QBRankingAverage { get; set; }
        public double RBRankingAverage { get; set; }
        public double WRRankingAverage { get; set; }
        public double TERankingAverage { get; set; }

        public double TeamStartingTotal { get; set; }
        public double QBStartingTotal { get; set; }
        public double RBStartingTotal { get; set; }
        public double WRStartingTotal { get; set; }
        public double TEStartingTotal { get; set; }
        public double FLEXStartingTotal { get; set; }

        public double WeekAgoTeamStarting { get; set; }
        public double WeekAgoTeamTotal { get; set; }

        public int StartingTeamRank { get; set; }
        public int TeamTotalRank { get; set; }


        public int SelectedRoster { get; set; }

        public Dictionary<string, POR> PlayersOnRoster { get; set; }

        public int QBRank { get; set; }
        public int RBRank { get; set; }
        public int WRRank { get; set; }
        public int TERank { get; set; }


        public List<string> TradeCandidates { get; set; }
        public int TotalCandidateAdvantage { get; set; }
        public int TotalCandidateDisadvantage { get; set; }
        public int TotalDisparity { get; set; }

        public List<string> StartingPlayerList { get; set; }

        public List<string> StartingFlexList { get; set; }

        public double DraftPosition { get; set; }
        public List<string> DraftPicks { get; set; }

        public int TotalDraftCapital { get; set; }
        public string FranchiseID { get; set; }
        public string FranchiseName { get; set; }

        public List<string> MatchupList { get; set; }

        public double StrengthOfScheduleTotal { get; set; }

        public int StrengthOfScheduleRank { get; set; }

        public List<string> StrengthOfScheduleRankingList { get; set; }
    }

    public class POR
    {
        public string PORName { get; set; }
        public string PORPosition { get; set; }
        public int PORValue { get; set; }
    }
}
