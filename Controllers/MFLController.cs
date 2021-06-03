using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DynastyRanker.Models;
using DynastyRanker.ViewModels;
using System.Net;
using DynastyRanker.Controllers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;

namespace DynastyRanker.Controllers
{
    public class MFLController : Controller
    {
        private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

        public UserInfo leagueInformation = new UserInfo();
        public Dictionary<string, string> draftPickRankings = new Dictionary<string, string>();
        MFLLeagueInfo leagueInfo = new MFLLeagueInfo();
        List<Rosters> rosters = new List<Rosters>();
        //List<MFLPlayer> playerList = new List<MFLPlayer>();
        Dictionary<string, MFLPlayer> playerList = new Dictionary<string, MFLPlayer>();
        List<MFLDraftPicks> draftPicks = new List<MFLDraftPicks>();
        public string lastScrapeDate;
        bool includeDraftCapital;
        public List<POR> topWaiverPlayers = new List<POR>();

        // GET: MFL
        public ActionResult Index()
        {
            return View();
        }

        public MFLController(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            _env = env;
        }

        /// MFLDisplayLeague(League league)
        /// The psuedo main function of the Sleeper functionality. Called by the submit button click on the homepage
        /// and calls all of our functions and returns the DisplayLeague View.
        public ActionResult MFLDisplayLeague(MFLLeagueInfo league)
        {
            if (league.LeagueID != null)
            {
                try
                {
                    //LoadMFLPlayers(league.LeagueID);
                    playerList = GetPlayers(league.LeagueID);
                    leagueInfo = GetLeagueInfo(league.LeagueID);

                    if(leagueInfo.PrivateLeague == true)
                        return View("MFLPrivateLeague");

                    rosters = GetRosters(league.LeagueID);
                    lastScrapeDate = GetPreviousScrapeDate(lastScrapeDate);

                    //ScrapeRankings(lastScrapeDate);
                    //ScrapeSFRankings(lastScrapeDate);

                    LinkUsersAndRosters(rosters, leagueInfo);
                    playerList = LoadRankings(playerList, leagueInfo);

                    try
                    {
                        includeDraftCapital = true;
                        leagueInfo = Include2021Capital(playerList, leagueInfo, rosters);

                        if (leagueInfo.IncludeDraftCapital == false)
                        {
                            includeDraftCapital = false;
                        }
                        else
                        {
                            draftPicks = GetDraftPicks(leagueInfo);
                            rosters = AddDraftPicksToRoster(rosters, draftPicks);
                            rosters = GetTotalDraftCapital(rosters, draftPickRankings);
                        }
                    }
                    catch
                    {
                        includeDraftCapital = false;
                    }

                    try
                    {
                        rosters = AverageTeamRanking(rosters, playerList);
                    }
                    catch
                    {
                        return RedirectToAction("MFLBadLeague");
                    }

                    AddPlayerNamesToRosters(rosters, playerList);
                    rosters = RankPositionGroups(rosters);
                    rosters = SortRostersByRanking(rosters);
                    rosters = RankStartingLineups(rosters, leagueInfo);
                    OrderStartingLineupRanking(rosters);

                    topWaiverPlayers = GetHighestValuesWaivers(playerList, draftPickRankings, rosters);
                }
                catch
                {
                    return View("MFLInvalidLeagueID");
                }
            }
            else
            {
                return View("MFLInvalidLeagueID");
            }

            var viewModel = new DisplayLeagueViewModel
            {
                Rosters = rosters,
                UserInfo = leagueInformation,
                LastScrapeDate = lastScrapeDate,
                DraftPickRankings = draftPickRankings,
                IncludeDraftCapital = includeDraftCapital,
                TopWaiverPlayers = topWaiverPlayers
            };

            return View(viewModel);
        }

        public ActionResult MFLInvalidLeagueID()
        {
            return View();
        }

        public ActionResult MFLBadLeague()
        {
            return View();
        }

        #region Get MFL Data
        public MFLLeagueInfo GetLeagueInfo(string leagueID)
        {
            MFLLeagueInfo leagueInfo = new MFLLeagueInfo();
            string temp;
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("https://www61.myfantasyleague.com/2021/export?TYPE=league&L=" + leagueID);

            if(xdoc.InnerText.Contains("requires logged in user"))
            {
                leagueInfo.PrivateLeague = true;
                return leagueInfo;
            }

            leagueInfo.FranchiseCount = xdoc.SelectSingleNode("league/franchises/@count").Value;
            leagueInfo.StartersCount = xdoc.SelectSingleNode("league/starters/@count").Value;

            if(leagueInfo.StartersCount.Contains("-"))
            {
                temp = leagueInfo.StartersCount;
                temp = temp.Substring(temp.IndexOf('-') + 1);
                leagueInfo.StartersCount = temp;
            }

            string startingPositionsRemoved = "";
            int tempRemoved;
            string posLimit = "";
            var positionList = xdoc.SelectNodes("league/starters/position");
            foreach (XmlNode node in positionList)
            {
                var positionName = node.Attributes["name"];
                if (positionName != null)
                {
                    if (positionName.Value == "QB")
                    {
                        if (node.Attributes["limit"].Value.Contains("-"))
                        {
                            posLimit = node.Attributes["limit"].Value;
                            leagueInfo.MinQBCount = posLimit.Substring(0, 1);
                            posLimit = posLimit.Remove(0, 1);
                            posLimit = posLimit.Replace("-", String.Empty);
                            leagueInfo.MaxQBCount = posLimit;
                            posLimit = "";
                            continue;
                        }
                        else
                        {
                            leagueInfo.MinQBCount = node.Attributes["limit"].Value;
                            leagueInfo.MaxQBCount = node.Attributes["limit"].Value;
                            continue;
                        }
                    }
                    if (positionName.Value == "RB")
                    {
                        if (node.Attributes["limit"].Value.Contains("-"))
                        {
                            posLimit = node.Attributes["limit"].Value;
                            leagueInfo.MinRBCount = posLimit.Substring(0, 1);
                            posLimit = posLimit.Remove(0, 1);
                            posLimit = posLimit.Replace("-", String.Empty);
                            leagueInfo.MaxRBCount = posLimit;
                            posLimit = "";
                            continue;
                        }
                        else
                        {
                            leagueInfo.MinRBCount = node.Attributes["limit"].Value;
                            leagueInfo.MaxRBCount = node.Attributes["limit"].Value;
                            continue;
                        }
                    }
                    if (positionName.Value == "WR")
                    {
                        if (node.Attributes["limit"].Value.Contains("-"))
                        {
                            posLimit = node.Attributes["limit"].Value;
                            leagueInfo.MinWRCount = posLimit.Substring(0, 1);
                            posLimit = posLimit.Remove(0, 1);
                            posLimit = posLimit.Replace("-", String.Empty);
                            leagueInfo.MaxWRCount = posLimit;
                            posLimit = "";
                            continue;
                        }
                        else
                        {
                            leagueInfo.MinWRCount = node.Attributes["limit"].Value;
                            leagueInfo.MaxWRCount = node.Attributes["limit"].Value;
                            continue;
                        }
                    }
                    if (positionName.Value == "TE")
                    {
                        if (node.Attributes["limit"].Value.Contains("-"))
                        {
                            posLimit = node.Attributes["limit"].Value;
                            leagueInfo.MinTECount = posLimit.Substring(0, 1);
                            posLimit = posLimit.Remove(0, 1);
                            posLimit = posLimit.Replace("-", String.Empty);
                            leagueInfo.MaxTECount = posLimit;
                            posLimit = "";
                            continue;
                        }
                        else
                        {
                            leagueInfo.MinTECount = node.Attributes["limit"].Value;
                            leagueInfo.MaxTECount = node.Attributes["limit"].Value;
                            continue;
                        }
                    }
                    if (positionName.Value == "WR+TE")
                    {
                        if (node.Attributes["limit"].Value.Contains("-"))
                        {
                            posLimit = node.Attributes["limit"].Value;
                            leagueInfo.MinRECFLEXCount = posLimit.Substring(0, 1);
                            posLimit = posLimit.Remove(0, 1);
                            posLimit = posLimit.Replace("-", String.Empty);
                            leagueInfo.MaxRECFLEXCount = posLimit;
                            posLimit = "";
                            continue;
                        }
                        else
                        {
                            leagueInfo.MinRECFLEXCount = node.Attributes["limit"].Value;
                            leagueInfo.MaxRECFLEXCount = node.Attributes["limit"].Value;
                            continue;
                        }
                    }
                    if (positionName.Value != "QB" && positionName.Value != "RB" && positionName.Value.Contains("WR") && positionName.Value.Contains("TE"))
                    {
                        if (node.Attributes["limit"].Value.Contains("-"))
                        {
                            posLimit = node.Attributes["limit"].Value;
                            startingPositionsRemoved = posLimit.Substring(0, 1);
                            tempRemoved = Convert.ToInt32(leagueInfo.StartersCount) - Convert.ToInt32(startingPositionsRemoved);
                            leagueInfo.StartersCount = tempRemoved.ToString();
                            posLimit = "";
                            startingPositionsRemoved = "";
                            continue;
                        }
                        else
                        {
                            startingPositionsRemoved = node.Attributes["limit"].Value;
                            tempRemoved = Convert.ToInt32(leagueInfo.StartersCount) - Convert.ToInt32(startingPositionsRemoved);
                            leagueInfo.StartersCount = tempRemoved.ToString();
                            continue;
                        }
                    }
                }
            }

            List<KeyValuePair<string, string>> tempFranchises = new List<KeyValuePair<string, string>>();
            var franchiseList = xdoc.SelectNodes("league/franchises/franchise");
            foreach (XmlNode node in franchiseList)
            {
                var tempFranchiseID = node.Attributes["id"].Value;
                var tempFranchiseName = node.Attributes["name"].Value;
                tempFranchiseName = Regex.Replace(tempFranchiseName, "<.*?>", String.Empty);
                tempFranchises.Add(new KeyValuePair<string, string>(tempFranchiseID, tempFranchiseName));
            }

            leagueInfo.Franchises = tempFranchises;
            leagueInfo.LeagueID = leagueID;

            return leagueInfo;
        }

        public MFLLeagueInfo Include2021Capital(Dictionary<string, MFLPlayer> players, MFLLeagueInfo leagueInfo, List<Rosters> rosters)
        {
            leagueInfo.IncludeDraftCapital = true;
            foreach(var r in rosters)
            {
                foreach(string p in r.Bench)
                {
                    try
                    {
                        if (players[p].Status == "R")
                        {
                            leagueInfo.IncludeDraftCapital = false;
                            break;
                        }
                    }
                    catch
                    {
                        continue;
                    }

                }
            }

            return leagueInfo;
        }

        public List<Rosters> GetRosters(string leagueID)
        {
            List<Rosters> rosters = new List<Rosters>();
            Rosters ros = new Rosters();
            List<string> playerList = new List<string>();

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("https://www61.myfantasyleague.com/2021/export?TYPE=rosters&L=" + leagueID);

            var result = xdoc.SelectNodes("rosters/franchise");

            foreach (XmlNode node in result)
            {
                ros.FranchiseID = node.Attributes["id"].Value;
                foreach (XmlNode play in node.SelectNodes("player/@id"))
                {
                    playerList.Add(play.Value);
                }
                ros.Bench = playerList.ToArray();
                playerList = new List<string>();
                rosters.Add(ros);
                ros = new Rosters();
            }

            return rosters;
        }

        public string GetPreviousScrapeDate(string date)
        {
            var webRoot = _env.WebRootPath;

            string path = System.IO.Path.Combine(webRoot, "LastScrapeDate.txt");
            date = System.IO.File.ReadAllText(path);

            return date;
        }

        public void LoadMFLPlayers(string leagueID)
        {
            var webRoot = _env.WebRootPath;
            List<MFLPlayer> players = new List<MFLPlayer>();
            MFLPlayer player = new MFLPlayer();
            //List<string> playerList = new List<string>();
            Dictionary<string, MFLPlayer> playerDict = new Dictionary<string, MFLPlayer>();

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("https://www61.myfantasyleague.com/2021/export?TYPE=players&L=" + leagueID + "&APIKEY=&DETAILS=1&SINCE=&PLAYERS=&JSON=0");
            //xdoc.Save("E:\\projects\\DynastyRanker-Main\\DynastyRanker-main\\DynastyRanker\\Data\\MFLPlayerData.xml");
            xdoc.Save(System.IO.Path.Combine(webRoot, "MFLPlayerData.xml"));
            //"~/Data/MFLPlayerData.xml"

        }

        public Dictionary<string, MFLPlayer> GetPlayers(string leagueID)
        {
            var webRoot = _env.WebRootPath;
            List<MFLPlayer> players = new List<MFLPlayer>();
            MFLPlayer player = new MFLPlayer();
            //List<string> playerList = new List<string>();
            Dictionary<string, MFLPlayer> playerDict = new Dictionary<string, MFLPlayer>();

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(System.IO.Path.Combine(webRoot, "MFLPlayerData.xml"));

            var result = xdoc.SelectNodes("players/player");

            foreach (XmlNode node in result)
            {
                if (node.Attributes["position"].Value == "QB" || node.Attributes["position"].Value == "RB" || node.Attributes["position"].Value == "WR" || node.Attributes["position"].Value == "TE")
                {
                    player.PlayerID = node.Attributes["id"].Value;
                    player.Name = node.Attributes["name"].Value;
                    player.Position = node.Attributes["position"].Value;
                    player.Team = node.Attributes["team"].Value;

                    XmlElement testForStatus = node as XmlElement;
                    if (testForStatus.HasAttribute("status"))
                    {
                        player.Status = node.Attributes["status"].Value;
                    }

                    //players.Add(player);
                    playerDict.Add(player.PlayerID, player);
                    player = new MFLPlayer();
                }
            }

            return playerDict;
        }

        public List<MFLDraftPicks> GetDraftPicks(MFLLeagueInfo leagueInfo)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("https://www69.myfantasyleague.com/2021/export?TYPE=draftResults&L=" + leagueInfo.LeagueID + "&APIKEY=&JSON=0");

            List<MFLDraftPicks> draftPicks = new List<MFLDraftPicks>();
            MFLDraftPicks tempDraftPick = new MFLDraftPicks();

            string leagueSize = leagueInfo.FranchiseCount; //12
            double eml = 0.00D;
            string tempPick = "";

            var draftPicksList = xdoc.SelectNodes("draftResults/draftUnit/draftPick");
            foreach (XmlNode node in draftPicksList)
            {
                tempDraftPick.Round = node.Attributes["round"].Value;
                tempDraftPick.Pick = node.Attributes["pick"].Value;
                tempDraftPick.PickOwnedBy = node.Attributes["franchise"].Value;

                if(tempDraftPick.Round == "05")
                    continue;
                

                eml = Convert.ToDouble(tempDraftPick.Pick) / Convert.ToDouble(leagueSize);
                tempPick = "2021";
                if (eml <= 0.34)
                    tempPick = tempPick.Insert(4, " Early");
                if (eml >= 0.34 && eml <= 0.67)
                    tempPick = tempPick.Insert(4, " Mid");
                if (eml >= 0.67)
                    tempPick = tempPick.Insert(4, " Late");

                if (tempDraftPick.Round == "01")
                    tempPick = tempPick + " 1st";
                if (tempDraftPick.Round == "02")
                    tempPick = tempPick + " 2nd";
                if (tempDraftPick.Round == "03")
                    tempPick = tempPick + " 3rd";
                if (tempDraftPick.Round == "04")
                    tempPick = tempPick + " 4th";

                tempDraftPick.FullPickText = tempPick;
                draftPicks.Add(tempDraftPick);

                tempDraftPick = new MFLDraftPicks();
                tempPick = "";
            }
            return draftPicks;
        }
        #endregion

        public void LinkUsersAndRosters(List<Rosters> rosters, MFLLeagueInfo users)
        {
            foreach (Rosters ros in rosters)
            {
                foreach (var fran in users.Franchises)
                {
                    if (fran.Key == ros.FranchiseID)
                    {
                        ros.DisplayName = fran.Value;
                    }
                }
            }
        }

        public Dictionary<string, MFLPlayer> LoadRankings(Dictionary<string, MFLPlayer> players, MFLLeagueInfo leagueInfo)
        {
            string sr = "";
            var webRoot = _env.WebRootPath;

            if (leagueInfo.MaxQBCount != "1")
                sr = System.IO.Path.Combine(webRoot, "KTCScrapeSF.csv");
            else
                sr = System.IO.Path.Combine(webRoot, "KTCScrape.csv");

            using (var reader = new StreamReader(sr))
            {
                List<string> playerNameList = new List<string>();
                List<string> playerPositionList = new List<string>();
                List<string> playerTeamList = new List<string>();
                List<string> playerKeepTradeCutList = new List<string>();
                string tempName = "";

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    tempName = values[0].Replace(".", String.Empty);
                    playerNameList.Add(tempName);
                    playerPositionList.Add(values[1]);
                    playerTeamList.Add(values[2]);
                    playerKeepTradeCutList.Add(values[3]);

                    if (values[1] == "PI")
                    {
                        draftPickRankings.Add(values[0], values[3]);
                    }
                }

                foreach (var p in players)
                {
                    string temp = "";
                    int tempIndex = 0;
                    int playerNameLength = p.Value.Name.Length;
                    int commaIndex = p.Value.Name.IndexOf(",");
                    int indexAfterCommaSpace = commaIndex + 2;

                    string lastNameTemp = p.Value.Name.Substring(0, commaIndex);
                    string firstNameTemp = p.Value.Name.Substring(indexAfterCommaSpace);

                    temp = '"' + firstNameTemp + " " + lastNameTemp + '"';
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);
                    temp = temp.Replace(".", string.Empty);
                    temp = temp.Replace(" Jr", string.Empty);

                    if (temp.Contains("Irv Smith") && !playerNameList.Contains(temp))
                    {
                        temp = "Irv Smith Jr";
                    }

                    if (temp.Contains("Chris Herndon") && !playerNameList.Contains(temp))
                    {
                        temp = "Christopher Herndon";
                    }

                    if (playerNameList.Contains(temp))
                    {
                        tempIndex = playerNameList.IndexOf(temp);
                        p.Value.KeepTradeCutValue = playerKeepTradeCutList[tempIndex];
                    }

                }

                return players;
            }
        }
        public void AddPlayerNamesToRosters(List<Rosters> rosters, Dictionary<string, MFLPlayer> players)
        {
            List<string> tempPlayersList;
            List<string> tempPlayerRankingsList;
            Dictionary<string, POR> tempPORDict;
            POR tempPOREntry;
            string temp = "";
            string fullName;
            int playerNameLength;
            int commaIndex;
            int indexAfterCommaSpace;
            string lastNameTemp;
            string firstNameTemp;

           MFLPlayer tempPlayer = new MFLPlayer();

            foreach (Rosters ros in rosters)
            {
                tempPlayersList = new List<string>();
                tempPlayerRankingsList = new List<string>();
                tempPORDict = new Dictionary<string, POR>();

                foreach (string p in ros.Bench)
                {
                    if (players.ContainsKey(p))
                    {
                        tempPOREntry = new POR();

                        temp = players[p].Name;

                        players[p].OnRoster = true;

                        fullName = "";
                        playerNameLength = temp.Length;
                        commaIndex = temp.IndexOf(',');
                        indexAfterCommaSpace = commaIndex + 2;

                        lastNameTemp = temp.Substring(0, commaIndex);
                        firstNameTemp =temp.Substring(indexAfterCommaSpace);

                        fullName = '"' + firstNameTemp + " " + lastNameTemp + '"';
                        fullName = fullName.Remove(0, 1);
                        fullName = fullName.Remove(fullName.Length - 1, 1);


                        tempPlayer = GetPlayerData(p, players);
                        tempPlayerRankingsList.Add(tempPlayer.KeepTradeCutValue);

                        tempPlayersList.Add(fullName);

                        tempPOREntry.PORName = fullName;
                        tempPOREntry.PORPosition = tempPlayer.Position;
                        tempPOREntry.PORValue = Convert.ToInt32(tempPlayer.KeepTradeCutValue);

                        tempPORDict.Add(p, tempPOREntry);
                        //ros.PlayersOnRoster.Add(p, tempPOR);
                    }
                }
                ros.PlayerNames = tempPlayersList;
                ros.PlayerTradeValues = tempPlayerRankingsList;
                ros.PlayersOnRoster = tempPORDict;
            }
        }

        public List<Rosters> AddDraftPicksToRoster(List<Rosters> rosters, List<MFLDraftPicks> draftPicks)
        {
            List<string> tempPicks = new List<string>();
            foreach (var ros in rosters)
            {
                foreach (var pick in draftPicks)
                {
                    if (ros.FranchiseID == pick.PickOwnedBy)
                    {
                        tempPicks.Add(pick.FullPickText);
                    }
                }
                ros.DraftPicks = tempPicks;
                tempPicks = new List<string>();
            }

            return rosters;
        }

        public List<Rosters> GetTotalDraftCapital(List<Rosters> rosters, Dictionary<string, string> dpr)
        {
            int tempTotal = 0;
            foreach (var ros in rosters)
            {
                foreach (var pick in ros.DraftPicks)
                {
                    tempTotal += Int32.Parse(dpr[pick]);
                }
                ros.TeamRankingAverage = ros.TeamRankingAverage + tempTotal;
                ros.TotalDraftCapital = tempTotal;
                tempTotal = 0;
            }
            return rosters;
        }

        public MFLPlayer GetPlayerData(string playerID, Dictionary<string, MFLPlayer> playerList)
        {
            if (playerList.ContainsKey(playerID))
            {
                return playerList[playerID];
            }
            return null;
        }

        public List<Rosters> AverageTeamRanking(List<Rosters> rosters, Dictionary<string, MFLPlayer> players)
        {
            double totalTemp = 0.0;
            double qbTemp = 0.0;
            double rbTemp = 0.0;
            double wrTemp = 0.0;
            double teTemp = 0.0;
            int qbCount = 0, rbCount = 0, wrCount = 0, teCount = 0;
            double parseTemp = 0.0;

            foreach (Rosters ros in rosters)
            {
                foreach (string playerID in ros.Bench)
                {
                    if (players.ContainsKey(playerID))
                    {
                        MFLPlayer currentPlayer = new MFLPlayer();
                        currentPlayer = GetPlayerData(playerID, players);
                        if (currentPlayer.Position == null)
                        {
                            break;
                        }
                        if (currentPlayer.KeepTradeCutValue != "")
                        {
                            parseTemp = Convert.ToDouble(currentPlayer.KeepTradeCutValue);

                            totalTemp = totalTemp + Convert.ToDouble(currentPlayer.KeepTradeCutValue);

                            if (currentPlayer.Position.Contains("QB"))
                            {
                                qbTemp = qbTemp + Convert.ToDouble(currentPlayer.KeepTradeCutValue);
                                qbCount++;
                            }
                            if (currentPlayer.Position.Contains("RB"))
                            {
                                rbTemp = rbTemp + Convert.ToDouble(currentPlayer.KeepTradeCutValue);
                                rbCount++;
                            }
                            if (currentPlayer.Position.Contains("WR"))
                            {
                                wrTemp = wrTemp + Convert.ToDouble(currentPlayer.KeepTradeCutValue);
                                wrCount++;
                            }
                            if (currentPlayer.Position.Contains("TE"))
                            {
                                teTemp = teTemp + Convert.ToDouble(currentPlayer.KeepTradeCutValue);
                                teCount++;
                            }
                        }
                        else
                        {
                            currentPlayer.KeepTradeCutValue = "1.0";
                            //System.Diagnostics.Debug.WriteLine("TESTING PLAYER: " + currentPlayer.FirstName + " " + currentPlayer.LastName + ": " + currentPlayer.KeepTradeCutValue);
                            totalTemp = totalTemp + Convert.ToDouble(currentPlayer.KeepTradeCutValue);

                            if (currentPlayer.Position.Contains("QB"))
                            {
                                qbTemp = qbTemp + Convert.ToDouble(currentPlayer.KeepTradeCutValue);
                                qbCount++;
                            }
                            if (currentPlayer.Position.Contains("RB"))
                            {
                                rbTemp = rbTemp + Convert.ToDouble(currentPlayer.KeepTradeCutValue);
                                rbCount++;
                            }
                            if (currentPlayer.Position.Contains("WR"))
                            {
                                wrTemp = wrTemp + Convert.ToDouble(currentPlayer.KeepTradeCutValue);
                                wrCount++;
                            }
                            if (currentPlayer.Position.Contains("TE"))
                            {
                                teTemp = teTemp + Convert.ToDouble(currentPlayer.KeepTradeCutValue);
                                teCount++;
                            }
                        }

                    }
                }
                ros.TeamRankingAverage += totalTemp;
                ros.QBRankingAverage = qbTemp;
                ros.RBRankingAverage = rbTemp;
                ros.WRRankingAverage = wrTemp;
                ros.TERankingAverage = teTemp;

                totalTemp = 0;
                qbTemp = 0;
                rbTemp = 0;
                wrTemp = 0;
                teTemp = 0;
                qbCount = 0;
                rbCount = 0;
                wrCount = 0;
                teCount = 0;
            }
            return rosters;
        }

        public List<Rosters> RankPositionGroups(List<Rosters> rosters)
        {
            List<Rosters> rankedRosters = new List<Rosters>();

            rankedRosters = rosters.OrderByDescending(o => o.QBRankingAverage).ToList();
            int count = 1;
            foreach (var ros in rankedRosters)
            {
                ros.QBRank = count;
                count++;
            }

            rankedRosters = rosters.OrderByDescending(o => o.RBRankingAverage).ToList();
            count = 1;
            foreach (var ros in rankedRosters)
            {
                ros.RBRank = count;
                count++;
            }

            rankedRosters = rosters.OrderByDescending(o => o.WRRankingAverage).ToList();
            count = 1;
            foreach (var ros in rankedRosters)
            {
                ros.WRRank = count;
                count++;
            }

            rankedRosters = rosters.OrderByDescending(o => o.TERankingAverage).ToList();
            count = 1;
            foreach (var ros in rankedRosters)
            {
                ros.TERank = count;
                count++;
            }

            return rankedRosters;
        }

        public List<Rosters> SortRostersByRanking(List<Rosters> rosters)
        {
            List<Rosters> sortedRosters = rosters.OrderByDescending(o => o.TeamRankingAverage).ToList();
            return sortedRosters;
        }

        public void OrderStartingLineupRanking(List<Rosters> rosters)
        {
            List<Rosters> sortedRosters = rosters.OrderByDescending(o => o.TeamStartingTotal).ToList();
            int count = 0;
            foreach (var ros in sortedRosters)
            {
                count++;
                ros.StartingTeamRank = count;
            }

        }

        public List<Rosters> RankStartingLineups(List<Rosters> rosters, MFLLeagueInfo leagueInfo)
        {
            int remainingFlex = Convert.ToInt32(leagueInfo.MaxFLEXCount) + Convert.ToInt32(leagueInfo.MaxRECFLEXCount) + leagueInfo.SUPERFLEXCount;
            int recflexAdded = 0;
            int qbsAdded = 0;
            int rbsAdded = 0;
            int wrsAdded = 0;
            int tesAdded = 0;
            int totalAdded = 0;

            double startingQBTotal = 0.0;
            double startingRBTotal = 0.0;
            double startingWRTotal = 0.0;
            double startingTETotal = 0.0;
            double startingFLEXTotal = 0.0;

            List<string> skippedPlayerNames = new List<string>();
            List<string> startingPlayerNames = new List<string>();
            List<string> flexPlayerNames = new List<string>();
            List<string> superflexPlayerNames = new List<string>();

            foreach (var ros in rosters)
            {
                foreach (var player in ros.PlayersOnRoster.OrderByDescending(o => o.Value.PORValue))
                {
                    if (qbsAdded == Convert.ToInt32(leagueInfo.MinQBCount) && rbsAdded == Convert.ToInt32(leagueInfo.MinRBCount) && wrsAdded == Convert.ToInt32(leagueInfo.MinWRCount) && tesAdded == Convert.ToInt32(leagueInfo.MinTECount))
                        break;

                    if (player.Value.PORPosition == "QB" && qbsAdded < Convert.ToInt32(leagueInfo.MinQBCount))
                    {
                        skippedPlayerNames.Add(player.Value.PORName);
                        startingPlayerNames.Add(player.Value.PORName);
                        startingQBTotal += player.Value.PORValue;
                        qbsAdded++;
                        totalAdded++;
                        continue;
                    }
                    if (player.Value.PORPosition == "RB" && rbsAdded < Convert.ToInt32(leagueInfo.MinRBCount))
                    {
                        skippedPlayerNames.Add(player.Value.PORName);
                        startingPlayerNames.Add(player.Value.PORName);
                        startingRBTotal += player.Value.PORValue;
                        rbsAdded++;
                        totalAdded++;
                        continue;
                    }

                    if (player.Value.PORPosition == "WR" && wrsAdded < Convert.ToInt32(leagueInfo.MinWRCount))
                    {
                        skippedPlayerNames.Add(player.Value.PORName);
                        startingPlayerNames.Add(player.Value.PORName);
                        startingRBTotal += player.Value.PORValue;
                        wrsAdded++;
                        totalAdded++;
                        continue;
                    }

                    if (player.Value.PORPosition == "TE" && tesAdded < Convert.ToInt32(leagueInfo.MinTECount))
                    {
                        skippedPlayerNames.Add(player.Value.PORName);
                        startingPlayerNames.Add(player.Value.PORName);
                        startingTETotal += player.Value.PORValue;
                        tesAdded++;
                        totalAdded++;
                        continue;
                    }
                }

                foreach (var player in ros.PlayersOnRoster.OrderByDescending(o => o.Value.PORValue))
                {
                    if (skippedPlayerNames.Contains(player.Value.PORName))
                        continue;

                    if(player.Value.PORPosition == "QB" && Convert.ToInt32(leagueInfo.MaxQBCount) != qbsAdded && Convert.ToInt32(leagueInfo.StartersCount) > totalAdded)
                    {
                        skippedPlayerNames.Add(player.Value.PORName);
                        flexPlayerNames.Add(player.Value.PORName);
                        startingQBTotal += player.Value.PORValue;
                        qbsAdded++;
                        totalAdded++;
                        continue;
                    }

                    if (player.Value.PORPosition == "RB" && Convert.ToInt32(leagueInfo.MaxRBCount) != rbsAdded && Convert.ToInt32(leagueInfo.StartersCount) > totalAdded)
                    {
                        skippedPlayerNames.Add(player.Value.PORName);
                        flexPlayerNames.Add(player.Value.PORName);
                        startingRBTotal += player.Value.PORValue;
                        rbsAdded++;
                        totalAdded++;
                        continue;
                    }

                    if (player.Value.PORPosition == "WR" && Convert.ToInt32(leagueInfo.MaxWRCount) != wrsAdded && Convert.ToInt32(leagueInfo.StartersCount) > totalAdded)
                    {
                        skippedPlayerNames.Add(player.Value.PORName);
                        flexPlayerNames.Add(player.Value.PORName);
                        startingRBTotal += player.Value.PORValue;
                        wrsAdded++;
                        totalAdded++;
                        continue;
                    }

                    if (player.Value.PORPosition == "TE" && Convert.ToInt32(leagueInfo.MaxTECount) != tesAdded && Convert.ToInt32(leagueInfo.StartersCount) > totalAdded)
                    {
                        skippedPlayerNames.Add(player.Value.PORName);
                        flexPlayerNames.Add(player.Value.PORName);
                        startingTETotal += player.Value.PORValue;
                        tesAdded++;
                        totalAdded++;
                        continue;
                    }

                    if ((player.Value.PORPosition == "WR" || player.Value.PORPosition == "TE") && Convert.ToInt32(leagueInfo.MaxRECFLEXCount) != recflexAdded && Convert.ToInt32(leagueInfo.StartersCount) > totalAdded)
                    {
                        skippedPlayerNames.Add(player.Value.PORName);
                        flexPlayerNames.Add(player.Value.PORName);
                        startingFLEXTotal += player.Value.PORValue;
                        recflexAdded++;
                        totalAdded++;
                        continue;
                    }
                }

                ros.QBStartingTotal = startingQBTotal;
                ros.RBStartingTotal = startingRBTotal;
                ros.WRStartingTotal = startingWRTotal;
                ros.TEStartingTotal = startingTETotal;
                ros.FLEXStartingTotal = startingFLEXTotal;
                ros.TeamStartingTotal = startingQBTotal + startingRBTotal + startingWRTotal + startingTETotal + startingFLEXTotal;

                ros.StartingPlayerList = startingPlayerNames;
                ros.StartingFlexList = flexPlayerNames;
                
                totalAdded = 0;
                qbsAdded = 0;
                rbsAdded = 0;
                wrsAdded = 0;
                tesAdded = 0;
                recflexAdded = 0;
                totalAdded = 0;

                startingQBTotal = 0.0;
                startingRBTotal = 0.0;
                startingWRTotal = 0.0;
                startingTETotal = 0.0;
                startingFLEXTotal = 0.0;
                startingPlayerNames = new List<string>();
                flexPlayerNames = new List<string>();
                skippedPlayerNames = new List<string>();
            }
            return rosters;
        }

        public void ScrapeRankings(string previousScrapeDate)
        {
            var webRoot = _env.WebRootPath;
            string newScrapeDate = DateTime.Now.ToString("MM-dd-yyyy");

            if (previousScrapeDate != newScrapeDate)
            {
                string url = "https://keeptradecut.com/dynasty-rankings?page=0&filters=QB|WR|RB|TE|RDP&format=1";

                HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(url);

                var nameTable = doc.DocumentNode.SelectNodes("//div[@class='player-name']");
                var valueTable = doc.DocumentNode.SelectNodes("//div[@class='value']");
                var positionTeamTable = doc.DocumentNode.SelectNodes("//div[@class='position-team']");


                List<string> nameList = new List<string>();
                List<string> valueList = new List<string>();
                List<string> positionList = new List<string>();
                List<string> teamList = new List<string>();

                string temp, positionTemp, teamTemp = "";
                int tempSize = 0;

                foreach (var name in nameTable.Skip(1))
                {
                    temp = name.InnerText;
                    temp = temp.Trim();
                    temp = temp.Replace("//n", "");
                    tempSize = temp.Length;
                    if (temp.EndsWith("FA"))
                    {
                        teamTemp = temp.Substring(temp.Length - 2);
                        temp = temp.Substring(0, tempSize - 2);
                        tempSize = temp.Length;
                        teamList.Add(teamTemp);
                    }
                    else
                    {
                        teamTemp = temp.Substring(temp.Length - 3);
                        temp = temp.Substring(0, tempSize - 3);
                        tempSize = temp.Length;
                        teamList.Add(teamTemp);
                    }
                    if (temp.EndsWith("R"))
                    {
                        temp = temp.Substring(0, tempSize - 1);
                        temp = temp.Trim();
                        temp = temp.Replace("\\n", "");
                    }
                    if (temp.Contains("&#x27;"))
                    {
                        temp = temp.Replace("&#x27;", "'");
                    }
                    if (temp.Contains("Lamical"))
                        temp = "La'Mical Perine";
                    if (temp.Contains("JaMycal"))
                        temp = "Jamycal Hasty";
                    if (temp.Contains("Herndon"))
                        temp = "Christopher Herndon";
                    nameList.Add(temp);
                }
                foreach (var value in valueTable.Skip(1))
                {
                    temp = value.InnerText;
                    temp = temp.Trim();
                    temp = temp.Replace("//n", "");
                    valueList.Add(temp);
                }
                foreach (var positionteam in positionTeamTable.Skip(1))
                {
                    temp = positionteam.InnerText;
                    temp = temp.Trim();
                    temp = temp.Replace("//n", "");

                    positionTemp = temp.Substring(0, 2);
                    if (positionTemp == "RD")
                        positionTemp = "NA";

                    temp = temp.Remove(0, 3);

                    teamTemp = temp;

                    positionList.Add(positionTemp);
                }


                string tempName, tempValue, tempPosition, tempTeam;
                int count = 0;
                List<Player> ktcList = new List<Player>();

                Player newKtc = new Player();

                foreach (var p in nameList)
                {
                    newKtc = new Player();
                    tempName = p;
                    if (valueList.ElementAt(count) != null)
                    {
                        tempValue = valueList.ElementAt(count);
                    }
                    else
                    {
                        tempValue = "NA";
                    }
                    if (teamList.ElementAt(count) != null)
                    {
                        tempTeam = teamList.ElementAt(count);
                    }
                    else
                    {
                        tempTeam = "NA";
                    }
                    if (positionList.ElementAt(count) != null)
                    {
                        tempPosition = positionList.ElementAt(count);
                    }
                    else
                    {
                        tempPosition = "NA";
                    }
                    newKtc.Name = tempName;
                    newKtc.Value = tempValue;
                    newKtc.Position = tempPosition;
                    newKtc.Team = tempTeam;

                    ktcList.Add(newKtc);

                    count++;
                }

                string path = System.IO.Path.Combine(webRoot, "KTCScrape.csv");
                //string fileName = "C:\\Users\\timca\\source\\repos\\DynastyRanker\\DynastyRanker\\Data\\KTCScrape.csv";
                string newLine = "";

                System.IO.File.WriteAllText(path, String.Empty);
                foreach (var p in ktcList)
                {
                    newLine = "";
                    newLine = p.Name + "," + p.Position + "," + p.Team + "," + p.Value + Environment.NewLine;
                    System.IO.File.AppendAllText(path, newLine);
                }

                //string filename = "C:\\Users\\timca\\source\\repos\\DynastyRanker\\DynastyRanker\\Data\\LastScrapeDate.txt";
                string data = System.IO.Path.Combine(webRoot, "LastScrapeDate.txt");
                System.IO.File.WriteAllText(data, newScrapeDate);
            }
        }

        public void ScrapeSFRankings(string previousScrapeDate)
        {
            var webRoot = _env.WebRootPath;
            string newScrapeDate = DateTime.Now.ToString("MM-dd-yyyy");

            if (previousScrapeDate != newScrapeDate)
            {
                string url = "https://keeptradecut.com/dynasty-rankings?format=2";

                HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(url);

                var nameTable = doc.DocumentNode.SelectNodes("//div[@class='player-name']");
                var valueTable = doc.DocumentNode.SelectNodes("//div[@class='value']");
                var positionTeamTable = doc.DocumentNode.SelectNodes("//div[@class='position-team']");


                List<string> nameList = new List<string>();
                List<string> valueList = new List<string>();
                List<string> positionList = new List<string>();
                List<string> teamList = new List<string>();

                string temp, positionTemp, teamTemp = "";
                int tempSize = 0;

                foreach (var name in nameTable.Skip(1))
                {
                    temp = name.InnerText;
                    temp = temp.Trim();
                    temp = temp.Replace("//n", "");
                    tempSize = temp.Length;
                    if (temp.EndsWith("FA"))
                    {
                        teamTemp = temp.Substring(temp.Length - 2);
                        temp = temp.Substring(0, tempSize - 2);
                        tempSize = temp.Length;
                        teamList.Add(teamTemp);
                    }
                    else
                    {
                        teamTemp = temp.Substring(temp.Length - 3);
                        temp = temp.Substring(0, tempSize - 3);
                        tempSize = temp.Length;
                        teamList.Add(teamTemp);
                    }
                    if (temp.EndsWith("R"))
                    {
                        temp = temp.Substring(0, tempSize - 1);
                        temp = temp.Trim();
                        temp = temp.Replace("\\n", "");
                    }
                    if (temp.Contains("&#x27;"))
                        temp = temp.Replace("&#x27;", "'");
                    if (temp.Contains("Lamical"))
                        temp = "La'Mical Perine";
                    if (temp.Contains("JaMycal"))
                        temp = "Jamycal Hasty";
                    nameList.Add(temp);
                }
                foreach (var value in valueTable.Skip(1))
                {
                    temp = value.InnerText;
                    temp = temp.Trim();
                    temp = temp.Replace("//n", "");
                    valueList.Add(temp);
                }
                foreach (var positionteam in positionTeamTable.Skip(1))
                {
                    temp = positionteam.InnerText;
                    temp = temp.Trim();
                    temp = temp.Replace("//n", "");

                    positionTemp = temp.Substring(0, 2);
                    if (positionTemp == "RD")
                        positionTemp = "NA";

                    temp = temp.Remove(0, 3);

                    teamTemp = temp;

                    positionList.Add(positionTemp);
                }


                string tempName, tempValue, tempPosition, tempTeam;
                int count = 0;
                List<Player> ktcList = new List<Player>();

                Player newKtc = new Player();

                foreach (var p in nameList)
                {
                    newKtc = new Player();
                    tempName = p;
                    if (valueList.ElementAt(count) != null)
                    {
                        tempValue = valueList.ElementAt(count);
                    }
                    else
                    {
                        tempValue = "NA";
                    }
                    if (teamList.ElementAt(count) != null)
                    {
                        tempTeam = teamList.ElementAt(count);
                    }
                    else
                    {
                        tempTeam = "NA";
                    }
                    if (positionList.ElementAt(count) != null)
                    {
                        tempPosition = positionList.ElementAt(count);
                    }
                    else
                    {
                        tempPosition = "NA";
                    }
                    newKtc.Name = tempName;
                    newKtc.Value = tempValue;
                    newKtc.Position = tempPosition;
                    newKtc.Team = tempTeam;

                    ktcList.Add(newKtc);

                    count++;
                }

                //string fileName = "C:\\Users\\timca\\source\\repos\\DynastyRanker\\DynastyRanker\\Data\\KTCScrapeSF.csv";
                string path = System.IO.Path.Combine(webRoot, "KTCScrapeSF.csv");
                string newLine = "";

                System.IO.File.WriteAllText(path, String.Empty);
                foreach (var p in ktcList)
                {
                    newLine = "";
                    newLine = p.Name + "," + p.Position + "," + p.Team + "," + p.Value + Environment.NewLine;
                    System.IO.File.AppendAllText(path, newLine);
                }

                string data = System.IO.Path.Combine(webRoot, "LastScrapeDate.txt");
                //string filename = "C:\\Users\\timca\\source\\repos\\DynastyRanker\\DynastyRanker\\Data\\LastScrapeDate.txt";
                System.IO.File.WriteAllText(data, newScrapeDate);
            }
            //return newScrapeDate;
        }

        public List<POR> GetHighestValuesWaivers(Dictionary<string, MFLPlayer> players, Dictionary<string, string> dpr, List<Rosters> rosters)
        {
            POR unsignedPlayer = new POR();
            List<POR> unsignedPlayerList = new List<POR>();
            string temp;
            string fullName;
            int playerNameLength;
            int commaIndex;
            int indexAfterCommaSpace;
            string lastNameTemp = "";
            string firstNameTemp = "";
            foreach (var p in players)
            {
                if (!p.Value.OnRoster && (p.Value.Position == "QB" || p.Value.Position == "RB" || p.Value.Position == "WR" || p.Value.Position == "TE") && (p.Value.KeepTradeCutValue != null && Convert.ToInt32(p.Value.KeepTradeCutValue) != 0) && p.Value.Status != "R")
                {
                    if(p.Value.Position == "WR")
                    {

                    }
                    fullName = "";
                    temp = p.Value.Name;
                    playerNameLength = temp.Length;
                    commaIndex = temp.IndexOf(',');
                    indexAfterCommaSpace = commaIndex + 2;
                    lastNameTemp = temp.Substring(0, commaIndex);
                    firstNameTemp = temp.Substring(indexAfterCommaSpace);
                    fullName = '"' + firstNameTemp + " " + lastNameTemp + '"';
                    fullName = fullName.Remove(0, 1);
                    fullName = fullName.Remove(fullName.Length - 1, 1);
                    unsignedPlayer.PORName = fullName;

                    unsignedPlayer.PORPosition = p.Value.Position;
                    unsignedPlayer.PORValue = Convert.ToInt32(p.Value.KeepTradeCutValue);
                    unsignedPlayerList.Add(unsignedPlayer);
                    unsignedPlayer = new POR();
                }
            }
            return unsignedPlayerList;
        }
    }
}