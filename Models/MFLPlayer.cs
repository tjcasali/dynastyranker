using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynastyRanker.Models
{
    [Serializable()]
    public class MFLPlayer
    {
        [System.Xml.Serialization.XmlElement("id")]
        public string PlayerID { get; set; }

        [System.Xml.Serialization.XmlElement("name")]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlElement("position")]
        public string Position { get; set; }

        [System.Xml.Serialization.XmlElement("team")]
        public string Team { get; set; }

        public string Status { get; set; }

        public string KeepTradeCutValue { get; set; }

        public bool OnRoster { get; set; }
    }

    [System.Xml.Serialization.XmlRoot("players")]
    public class MFLPlayers
    {
        public List<MFLPlayer> Player { get; set; }
    }
}