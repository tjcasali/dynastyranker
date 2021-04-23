using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynastyRanker.Models
{
    public class MFLDraftPicks
    {
        public string Round { get; set; }
        public string Pick { get; set; }
        public string PickOwnedBy { get; set; }

        public string FullPickText { get; set; }

    }
}