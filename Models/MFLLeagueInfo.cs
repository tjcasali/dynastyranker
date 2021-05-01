using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynastyRanker.Models
{
    public class MFLLeagueInfo
    {
        public string LeagueID { get; set; }

        public string StartersCount { get; set; }

        public string MinQBCount { get; set; }

        public string MaxQBCount { get; set; }

        public string MinRBCount { get; set; }

        public string MaxRBCount { get; set; }

        public string MinWRCount { get; set; }

        public string MaxWRCount { get; set; }

        public string MinTECount { get; set; }

        public string MaxTECount { get; set; }

        public string MinFLEXCount { get; set; }

        public string MaxFLEXCount { get; set; }

        public int SUPERFLEXCount { get; set; }

        public string MinRECFLEXCount { get; set; }

        public string MaxRECFLEXCount { get; set; }

        public List<string> FranchiseIDs { get; set; }

        public List<string> FranchiseNames { get; set; }

        public List<KeyValuePair<string, string>> Franchises { get; set; }

        public string FranchiseCount { get; set; }

        public bool PrivateLeague { get; set; }

    }
}