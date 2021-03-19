using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynastyRanker.Models
{
    public class Username
    {
        public string User_Name { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public string UserID { get; set; }
    }
}