using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DynastyRanker.Models;
namespace DynastyRanker.ViewModels
{
    public class SelectLeagueViewModel
    {
        public List<League> LeagueList { get; set; }

        public string LeagueID { get; set; }
    }
}