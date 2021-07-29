using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynastyRanker.Models
{
    public class RankingLists
    {
        public List<string> StrengthOfScheduleRankingList { get; set; }
        public List<string> TeamTotalRankingList { get; set; }
        public List<string> StaringLineupRankingList { get; set; }
        public List<string> QBRankingList { get; set; }
        public List<string> RBRankingList { get; set; }
        public List<string> WRRankingList { get; set; }
        public List<string> TERankingList { get; set; }
        public List<string> DraftCapitalRankingList { get; set; }
    }
}
