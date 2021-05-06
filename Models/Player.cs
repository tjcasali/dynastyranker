using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynastyRanker.Models
{
    public class Player
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Team { get; set; }
        public string Value { get; set; }

        public string IsRookie { get; set; }
    }
}