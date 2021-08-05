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

        public string DisplayName { get; set; }

        public List<string> PlayerNames { get; set; }

        public List<string> PlayerTradeValues { get; set; }

        public double TeamRankingTotal { get; set; }
        public double QBRankingTotal { get; set; }
        public double RBRankingTotal { get; set; }
        public double WRRankingTotal{ get; set; }
        public double TERankingTotal{ get; set; }

        public double TeamStartingTotal { get; set; }
        public double QBStartingTotal { get; set; }
        public double RBStartingTotal { get; set; }
        public double WRStartingTotal { get; set; }
        public double TEStartingTotal { get; set; }
        public double FLEXStartingTotal { get; set; }

        public int StartingTeamRank { get; set; }
        public int TeamTotalRank { get; set; }


        public Dictionary<string, POR> PlayersOnRoster { get; set; }

        public int QBRank { get; set; }
        public int RBRank { get; set; }
        public int WRRank { get; set; }
        public int TERank { get; set; }

        //TODO: Add total and rank values for FantasyPros projections
        public double ProjTeamStartingTotal { get; set; }
        public double ProjQBStartingTotal { get; set; }
        public double ProjRBStartingTotal { get; set; }
        public double ProjWRStartingTotal { get; set; }
        public double ProjTEStartingTotal { get; set; }
        public double ProjFLEXStartingTotal { get; set; }
        public int ProjQBRank { get; set; }
        public int ProjRBRank { get; set; }
        public int ProjWRRank { get; set; }
        public int ProjTERank { get; set; }

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
        public double PORProjection { get; set; }
    }
}
