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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace DynastyRanker.Controllers
{
    public class SleeperController : Controller
    {
        private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

        public List<SleeperUsers> sleeperUsers = new List<SleeperUsers>();
        public static List<Rosters> sleeperRosters = new List<Rosters>();
        public List<League> leagueList = new List<League>();
        public Dictionary<string, PlayerData> playerList = new Dictionary<string, PlayerData>();
        public List<KeepTradeCut> keepTradeCutList = new List<KeepTradeCut>();
        public UserInfo leagueInformation = new UserInfo();
        public List<TradedPick> tradedPicks = new List<TradedPick>();
        public string lastScrapeDate;
        //public List<Draft> drafts = new List<Draft>();
        public Draft draft = new Draft();
        public Dictionary<string, string> draftPickRankings = new Dictionary<string, string>();
        public Username currentUser = new Username();
        public bool includeDraftCapital = true;


        public SleeperController(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            _env = env;
        }

        #region ActionResults
        public ActionResult Index()
        {
            return View();
        }

        //[Route("Sleeper/DisplayLeague/{leagueId}")]
        public async Task<ActionResult> DisplayLeague(League league)
        {
            if (league.LeagueID != null)
            {
                try
                {
                    leagueInformation = await GetLeagueInformation(league.LeagueID);
                    sleeperUsers = await GetUsers(league.LeagueID);
                    sleeperRosters = await GetRosters(league.LeagueID);
                    playerList = GetPlayers();
                    lastScrapeDate = GetPreviousScrapeDate(lastScrapeDate);

                    //LoadSleeperPlayersTextFile();

                    //TODO Put the if condition here so we don't even have to go into the scrape functions
                    ScrapeRankings(lastScrapeDate);
                    ScrapeSFRankings(lastScrapeDate);

                    playerList = LoadRankings(playerList, keepTradeCutList, leagueInformation);

                    LinkUsersAndRosters(sleeperUsers, sleeperRosters);

                    if(leagueInformation.PreviousLeagueID != "")
                    {
                        try
                        {
                            draft = await GetDraftOrder(league.LeagueID);
                            if (draft.DraftOrder != null)
                            {
                                tradedPicks = await GetTradedDraftPicks(leagueInformation);
                                AddDraftPositionToRoster(draft, sleeperRosters);
                                sleeperRosters = AssignDraftPositionToPicks(sleeperRosters);
                                sleeperRosters = TradedDraftPicks(sleeperRosters, tradedPicks);
                                sleeperRosters = GetTotalDraftCapital(sleeperRosters, draftPickRankings);
                            }
                            else
                                includeDraftCapital = false;
                        }
                        catch
                        {
                            includeDraftCapital = false;
                        }
                    }

                    try
                    {
                        sleeperRosters = AverageTeamRanking(sleeperRosters, playerList);
                    }
                    catch
                    {
                        return RedirectToAction("BadLeague");
                    }

                    AddPlayerNamesToRosters(sleeperRosters, playerList);

                    sleeperRosters = RankPositionGroups(sleeperRosters);

                    sleeperRosters = SortRostersByRanking(sleeperRosters);

                    sleeperRosters = RankStartingLineups(sleeperRosters, leagueInformation);

                    OrderStartingLineupRanking(sleeperRosters);
                }
                catch
                {
                    return RedirectToAction("InvalidLeagueID");
                }
            }
            else
            {
                return RedirectToAction("InvalidLeagueID");
            }

            var viewModel = new DisplayLeagueViewModel
            {
                Rosters = sleeperRosters,
                UserInfo = leagueInformation,
                LastScrapeDate = lastScrapeDate,
                DraftPickRankings = draftPickRankings,
                TradedPicks = tradedPicks,
                IncludeDraftCapital = includeDraftCapital
            };

            return View(viewModel);
        }

        //[Route("Sleeper/DisplayLeague/{leagueId}")]
        public async Task<ActionResult> SelectLeague(string userName)
        {

            if (userName != null)
            {
                try
                {
                    currentUser = await GetUserIDFromUsername(userName);
                    string currentUserID = currentUser.UserID;
                    leagueList = await GetAllLeaguesForUser(currentUserID);
                }
                catch
                {
                    return RedirectToAction("InvalidUsername");
                }
            }
            else
            {
                return RedirectToAction("InvalidUsername");
            }

            var viewModel = new SelectLeagueViewModel
            {
                LeagueList = leagueList
            };

            return View(viewModel);
        }

        public ActionResult InvalidLeagueID()
        {
            return View();
        }

        public ActionResult InvalidUsername()
        {
            return View();
        }

        public ActionResult BadLeague()
        {
            return View();
        }

        #endregion

        #region Get Rosters/Users/Players

        public static async Task<UserInfo> GetLeagueInformation(string leagueID)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.sleeper.app/v1/league/" + leagueID);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            UserInfo leagueInfo= JsonConvert.DeserializeObject<UserInfo>(responseBody);
            bool isSuper = false;

            foreach(string position in leagueInfo.LeagueRosterPositions)
            {
                if(position == "QB")
                {
                    leagueInfo.QBCount++;
                    continue;
                }
                if (position == "RB")
                {
                    leagueInfo.RBCount++;
                    continue;
                }
                if (position == "WR")
                {
                    leagueInfo.WRCount++;
                    continue;
                }
                if (position == "TE")
                {
                    leagueInfo.TECount++;
                    continue;
                }
                if (position == "FLEX")
                {
                    leagueInfo.FLEXCount++;
                    continue;
                }
                if (position == "REC_FLEX")
                {
                    leagueInfo.RECFLEXCount++;
                    continue;
                }
                if (position == "SUPER_FLEX")
                {
                    leagueInfo.SUPERFLEXCount++;
                    continue;
                }
            }

            if (leagueInfo.LeagueRosterPositions.Contains("SUPER_FLEX") || leagueInfo.QBCount > 1)
            {
                isSuper = true;
            }

            leagueInfo.SuperFlex = isSuper;
            leagueInfo.LeagueID = leagueID;

            return leagueInfo;
        }

        /// Get Users
        /// Take in user submitted league ID and put that into the Sleeper API to return the Users in the league
        public static async Task<List<SleeperUsers>> GetUsers(string leagueID)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.sleeper.app/v1/league/" + leagueID + "/users");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            List<SleeperUsers> userList = JsonConvert.DeserializeObject<List<SleeperUsers>>(responseBody);

            return userList;

        }

        public static async Task<Username> GetUserIDFromUsername(string username)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.sleeper.app/v1/user/" + username);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            Username user = JsonConvert.DeserializeObject<Username>(responseBody);

            await GetAllLeaguesForUser(user.UserID);

            return user;

        }

        public static async Task<List<League>> GetAllLeaguesForUser(string userID)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.sleeper.app/v1/user/" + userID + "/leagues/nfl/2021");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            List<League> leagues = JsonConvert.DeserializeObject<List<League>>(responseBody);

            return leagues;
        }

        public static async Task<List<Rosters>> GetRosters(string leagueID)
        {

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.sleeper.app/v1/league/" + leagueID + "/rosters");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            List<Rosters> rosters = JsonConvert.DeserializeObject<List<Rosters>>(responseBody);

            return rosters;
        }

        public static async Task<List<TradedPick>> GetTradedDraftPicks(UserInfo leagueInfo)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.sleeper.app/v1/draft/" + leagueInfo.DraftID + "/traded_picks");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            List<TradedPick> tradedPicks = JsonConvert.DeserializeObject<List<TradedPick>>(responseBody);

            return tradedPicks;
        }

        public static async Task<Draft> GetDraftOrder(string leagueID)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.sleeper.app/v1/league/" + leagueID + "/drafts");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Draft> drafts = JsonConvert.DeserializeObject<List<Draft>>(responseBody);
            Draft draft = drafts.First();
            return draft;
        }

        public async void LoadSleeperPlayersTextFile()
        {
            var webRoot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webRoot, "SleeperGetPlayers.txt");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await
            client.GetAsync("https://api.sleeper.app/v1/players/nfl");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            System.IO.File.WriteAllText(file, responseBody);
        }

        public Dictionary<string, PlayerData> GetPlayers()
        {
            var webRoot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webRoot, "SleeperGetPlayers.txt");
            string json = System.IO.File.ReadAllText(file);

            Dictionary<string, PlayerData> playerList = JsonConvert.DeserializeObject<Dictionary<string, PlayerData>>(json);

            return playerList;
        }
        #endregion

        #region Add Owner IDs and Player Values to Rosters
        public void LinkUsersAndRosters(List<SleeperUsers> users, List<Rosters> rosters)
        {
            foreach (Rosters ros in rosters)
            {
                foreach (SleeperUsers su in users)
                {
                    if (su.UserID == ros.OwnerID)
                    {
                        su.RosterID = ros.RosterID;
                        ros.DisplayName = su.DisplayName;
                    }
                }
            }
        }

        public void AddPlayerNamesToRosters(List<Rosters> rosters, Dictionary<string, PlayerData> players)
        {
            List<string> tempPlayersList;
            List<string> tempPlayerRankingsList;
            Dictionary<string, POR> tempPORDict;
            POR tempPOREntry;
            string temp = "";


            PlayerData tempPlayer = new PlayerData();

            foreach (Rosters ros in rosters)
            {
                tempPlayersList = new List<string>();
                tempPlayerRankingsList = new List<string>();
                tempPORDict = new Dictionary<string, POR>();
                foreach (string p in ros.Bench)
                {
                    tempPOREntry = new POR();
                    temp = players[p].FirstName + " " + players[p].LastName;

                    tempPlayer = GetPlayerData(p, players);
                    tempPlayerRankingsList.Add(tempPlayer.KeepTradeCutValue);

                    tempPlayersList.Add(temp);

                    tempPOREntry.PORName = temp;
                    tempPOREntry.PORPosition = tempPlayer.Position;
                    tempPOREntry.PORValue = Convert.ToInt32(tempPlayer.KeepTradeCutValue);

                    tempPORDict.Add(p, tempPOREntry);
                    //ros.PlayersOnRoster.Add(p, tempPOR);
                    
                    
                }
                ros.PlayerNames = tempPlayersList;
                ros.PlayerTradeValues = tempPlayerRankingsList;
                ros.PlayersOnRoster = tempPORDict;
            }
        }
        #endregion

        #region Ranking Functions

        public Dictionary<string, PlayerData> LoadRankings(Dictionary<string, PlayerData> players, List<KeepTradeCut> ktc, UserInfo leagueInfo)
        {
            string sr = "";
            var webRoot = _env.WebRootPath;

            if (leagueInfo.SuperFlex)
                sr = System.IO.Path.Combine(webRoot, "KTCScrapeSF.csv");
            else
                sr = System.IO.Path.Combine(webRoot, "KTCScrape.csv");

            using (var reader = new StreamReader(sr))
            {
                List<string> playerNameList = new List<string>();
                List<string> playerPositionList = new List<string>();
                List<string> playerTeamList = new List<string>();
                List<string> playerKeepTradeCutList = new List<string>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    playerNameList.Add(values[0]);
                    playerPositionList.Add(values[1]);
                    playerTeamList.Add(values[2]);
                    playerKeepTradeCutList.Add(values[3]);

                    if(values[1] == "PI")
                    {
                        draftPickRankings.Add(values[0], values[3]);
                    }

                }

                foreach (var p in players)
                {
                    string temp = "";
                    int tempIndex = 0;

                    string firstNameTemp = p.Value.FirstName.Replace(".", string.Empty);
                    string lastNameTemp = p.Value.LastName.Replace(".", string.Empty);

                    temp = '"' + firstNameTemp + " " + lastNameTemp + '"';
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    if (playerNameList.Contains(temp))
                    {
                        tempIndex = playerNameList.IndexOf(temp);
                        p.Value.KeepTradeCutValue = playerKeepTradeCutList[tempIndex];
                    }

                }

                return players;
            }
        }

        public List<Rosters> AverageTeamRanking(List<Rosters> rosters, Dictionary<string, PlayerData> players)
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
                        PlayerData currentPlayer = new PlayerData();
                        currentPlayer = GetPlayerData(playerID, players);
                        if(currentPlayer.Position == null)
                        {
                            break;
                        }
                        if (currentPlayer.KeepTradeCutValue != "")
                        {
                            parseTemp = Convert.ToDouble(currentPlayer.KeepTradeCutValue);
                            //System.Diagnostics.Debug.WriteLine("TESTING PLAYER: " + currentPlayer.FirstName + " " + currentPlayer.LastName + ": " + currentPlayer.KeepTradeCutValue);
                            //totalTemp = totalTemp + Convert.ToDouble(currentPlayer.KeepTradeCutValue);
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

        public PlayerData GetPlayerData(string playerID, Dictionary<string, PlayerData> playerList)
        {
            if (playerList.ContainsKey(playerID))
            {
                return playerList[playerID];
            }
            return null;
        }

        public string GetPreviousScrapeDate(string date)
        {
            var webRoot = _env.WebRootPath;

            string path = System.IO.Path.Combine(webRoot, "LastScrapeDate.txt");
            date = System.IO.File.ReadAllText(path);

            return date;
        }

        public void ScrapeRankings(string previousScrapeDate)
        {
            string newScrapeDate = DateTime.Now.ToString("MM-dd-yyyy");
            var webRoot = _env.WebRootPath;

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
                    if (temp.Contains("Jr."))
                    {
                        temp = temp.Replace("Jr.", "");
                        temp = temp.Trim();
                    }
                    if (temp.Contains("."))
                    {
                        temp = temp.Replace(".", String.Empty);
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
            //return newScrapeDate;
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
                    if (temp.Contains("Jr."))
                    {
                        temp = temp.Replace("Jr.", "");
                        temp = temp.Trim();
                    }
                    if (temp.Contains("."))
                    {
                        temp = temp.Replace(".", String.Empty);
                    }
                    if (temp.Contains("&#x27;"))
                    {
                        temp = temp.Replace("&#x27;", "'");
                    }
                    if (temp.Contains("Lamical"))
                    {
                        temp = "La'Mical Perine";
                    }
                    if (temp.Contains("JaMycal"))
                    {
                        temp = "Jamycal Hasty";
                    }
                    if (temp.Contains("Irv Smith"))
                    {
                        temp = "Irv Smith";
                    }
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

        public List<Rosters> RankStartingLineups(List<Rosters> rosters, UserInfo leagueInfo)
        {
            int positionCounter = 0;

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
                foreach (var player in ros.PlayersOnRoster.Where(o => o.Value.PORPosition == "QB").OrderByDescending(o => o.Value.PORValue))
                {
                    skippedPlayerNames.Add(player.Value.PORName);
                    startingPlayerNames.Add(player.Value.PORName);
                    positionCounter++;
                    if (positionCounter == leagueInfo.QBCount)
                    {
                        startingQBTotal += player.Value.PORValue;
                        positionCounter = 0;
                        break;
                    }
                    else
                    {
                        startingQBTotal += player.Value.PORValue;
                    }
                }
                foreach (var player in ros.PlayersOnRoster.Where(o => o.Value.PORPosition == "RB").OrderByDescending(o => o.Value.PORValue))
                {
                    skippedPlayerNames.Add(player.Value.PORName);
                    startingPlayerNames.Add(player.Value.PORName);
                    positionCounter++;
                    if (positionCounter == leagueInfo.RBCount)
                    {
                        startingRBTotal += player.Value.PORValue;
                        positionCounter = 0;
                        break;
                    }
                    else
                    {
                        startingRBTotal += player.Value.PORValue;
                    }
                }
                foreach (var player in ros.PlayersOnRoster.Where(o => o.Value.PORPosition == "WR").OrderByDescending(o => o.Value.PORValue))
                {
                    skippedPlayerNames.Add(player.Value.PORName);
                    startingPlayerNames.Add(player.Value.PORName);
                    positionCounter++;
                    if (positionCounter == leagueInfo.WRCount)
                    {
                        startingWRTotal += player.Value.PORValue;
                        positionCounter = 0;
                        break;
                    }
                    else
                    {
                        startingWRTotal += player.Value.PORValue;
                    }
                }
                foreach (var player in ros.PlayersOnRoster.Where(o => o.Value.PORPosition == "TE").OrderByDescending(o => o.Value.PORValue))
                {
                    skippedPlayerNames.Add(player.Value.PORName);
                    startingPlayerNames.Add(player.Value.PORName);
                    positionCounter++;
                    if (positionCounter == leagueInfo.TECount)
                    {
                        startingTETotal += player.Value.PORValue;
                        positionCounter = 0;
                        break;
                    }
                    else
                    {
                        startingTETotal += player.Value.PORValue;
                    }
                }
                foreach (var player in ros.PlayersOnRoster.OrderByDescending(o => o.Value.PORValue))
                {
                    if (leagueInfo.SUPERFLEXCount != 0)
                    {
                        if (skippedPlayerNames.Contains(player.Value.PORName))
                        {
                            continue;
                        }
                        skippedPlayerNames.Add(player.Value.PORName);
                        flexPlayerNames.Add(player.Value.PORName);

                        positionCounter++;
                        if (positionCounter == leagueInfo.FLEXCount)
                        {
                            skippedPlayerNames.Add(player.Value.PORName);
                            startingFLEXTotal += player.Value.PORValue;
                            positionCounter = 0;
                            break;
                        }
                        else
                        {
                            startingFLEXTotal += player.Value.PORValue;
                        }
                    }
                    else if(leagueInfo.RECFLEXCount != 0)
                    {
                        if (skippedPlayerNames.Contains(player.Value.PORName) || player.Value.PORPosition == "QB" || player.Value.PORPosition == "RB")
                            continue;
                        
                        skippedPlayerNames.Add(player.Value.PORName);
                        flexPlayerNames.Add(player.Value.PORName);

                        positionCounter++;
                        if (positionCounter == leagueInfo.RECFLEXCount)
                        {
                            skippedPlayerNames.Add(player.Value.PORName);
                            startingFLEXTotal += player.Value.PORValue;
                            positionCounter = 0;
                            break;
                        }
                        else
                        {
                            startingFLEXTotal += player.Value.PORValue;
                        }
                    }
                    else if (leagueInfo.FLEXCount != 0)
                    {
                        if (skippedPlayerNames.Contains(player.Value.PORName) || player.Value.PORPosition == "QB")
                            continue;

                        skippedPlayerNames.Add(player.Value.PORName);
                        flexPlayerNames.Add(player.Value.PORName);

                        positionCounter++;
                        if (positionCounter == leagueInfo.FLEXCount)
                        {
                            skippedPlayerNames.Add(player.Value.PORName);
                            startingFLEXTotal += player.Value.PORValue;
                            positionCounter = 0;
                            break;
                        }
                        else
                        {
                            startingFLEXTotal += player.Value.PORValue;
                        }
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

                positionCounter = 0;

                startingQBTotal = 0.0;
                startingRBTotal = 0.0;
                startingWRTotal = 0.0;
                startingTETotal = 0.0;
                startingFLEXTotal = 0.0;
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

        #endregion

        #region Draft Picks Functions
        public void AddDraftPositionToRoster(Draft draft, List<Rosters> rosters)
        {
            List<string> startingDraftPicks = new List<string>();

            foreach (Rosters ros in rosters)
            {
                startingDraftPicks.Add("2021 1st");
                startingDraftPicks.Add("2021 2nd");
                startingDraftPicks.Add("2021 3rd");
                startingDraftPicks.Add("2021 4th");
                //startingDraftPicks.Add("2022 1st");
                //startingDraftPicks.Add("2022 2nd");
                //startingDraftPicks.Add("2022 3rd");
                //startingDraftPicks.Add("2022 4th");

                if (draft.DraftOrder.ContainsKey(ros.OwnerID))
                    ros.DraftPosition = draft.DraftOrder[ros.OwnerID];

                ros.DraftPicks = startingDraftPicks;
                startingDraftPicks = new List<string>();
            }
        }

        public List<Rosters> AssignDraftPositionToPicks(List<Rosters> rosters)
        {
            double leagueSize = rosters.Count(); //12
            double eml = 0.00D;
            string tempPick = "";
            foreach (Rosters ros in rosters)
            {
                for (int i = 0; i < ros.DraftPicks.Count(); i++)
                {
                    eml = ros.DraftPosition / leagueSize;
                    tempPick = ros.DraftPicks[i];
                    if (eml <= 0.34)
                    {
                        tempPick = tempPick.Insert(4, " Early");
                    }
                    if (eml >= 0.34 && eml <= 0.67)
                    {
                        tempPick = tempPick.Insert(4, " Mid");
                    }
                    if (eml >= 0.67)
                    {
                        tempPick = tempPick.Insert(4, " Late");
                    }
                    ros.DraftPicks[i] = tempPick;
                    tempPick = "";
                }

            }

            return rosters;
        }

        public List<Rosters> TradedDraftPicks(List<Rosters> rosters, List<TradedPick> tp)
        {
            Rosters originalOwnerRoster = new Rosters();
            Rosters newOwnerRoster = new Rosters();
            Rosters previousOwnerRoster = new Rosters();
            Rosters rosterOfRemovedPick = new Rosters();
            List<KeyValuePair<string, string>> picksToBeRemoved = new List<KeyValuePair<string, string>>();
            int pickFound = 0;

            foreach (var trade in tp)
            {
                //if (trade.Season == "2021" || trade.Season == "2022")
                if (trade.Season == "2021")
                {
                    string tempPick = "";

                    originalOwnerRoster = rosters.FirstOrDefault(o => Int32.Parse(o.RosterID) == trade.RosterIDOriginalOwnerForDraftPosition);
                    newOwnerRoster = rosters.FirstOrDefault(o => Int32.Parse(o.RosterID) == trade.RosterIDOfCurrentOwner);
                    previousOwnerRoster = rosters.FirstOrDefault(o => Int32.Parse(o.RosterID) == trade.RosterIDOfPreviousOwner);

                    tempPick = trade.Season + " " + trade.Round;
                    if (trade.Round == 1)
                        tempPick = tempPick + "st";
                    else if(trade.Round == 2)
                        tempPick = tempPick + "nd";
                    else if (trade.Round == 3)
                        tempPick = tempPick + "rd";
                    else if (trade.Round == 4)
                        tempPick = tempPick + "th";

                    double leagueSize = rosters.Count(); //12
                    double eml = 0.00D;
                    eml = originalOwnerRoster.DraftPosition / leagueSize;
                    if (eml <= 0.34)
                    {
                        tempPick = tempPick.Insert(4, " Early");
                    }
                    if (eml >= 0.34 && eml <= 0.67)
                    {
                        tempPick = tempPick.Insert(4, " Mid");
                    }
                    if (eml >= 0.67)
                    {
                        tempPick = tempPick.Insert(4, " Late");
                    }

                    //foreach (var pick in previousOwnerRoster.DraftPicks)
                    //    System.Diagnostics.Debug.WriteLine(pick);
                    //foreach (var pick in originalOwnerRoster.DraftPicks)
                    //    System.Diagnostics.Debug.WriteLine(pick);
                    //foreach (var pick in newOwnerRoster.DraftPicks)
                    //    System.Diagnostics.Debug.WriteLine(pick);

                    foreach (var pick in originalOwnerRoster.DraftPicks)
                    {
                        if (pick == tempPick)
                        {
                            pickFound = 1;
                            break;
                        }
                    }

                    if(pickFound == 1)
                    {
                        picksToBeRemoved.Add(new KeyValuePair<string, string>(originalOwnerRoster.RosterID, tempPick));
                        //previousOwnerRoster.DraftPicks.Remove(tempPick);
                        newOwnerRoster.DraftPicks.Add(tempPick);
                        pickFound = 0;
                    }

                    newOwnerRoster = new Rosters();
                    previousOwnerRoster = new Rosters();
                    originalOwnerRoster = new Rosters();

                }
            }

            foreach (var pick in picksToBeRemoved)
            {
                rosterOfRemovedPick = new Rosters();
                rosterOfRemovedPick = rosters.FirstOrDefault(o => o.RosterID == pick.Key);

                if (rosterOfRemovedPick.DraftPicks.Contains(pick.Value))
                {
                    rosterOfRemovedPick.DraftPicks.Remove(pick.Value);
                }
            }

            return rosters;
        }

        public List<Rosters> GetTotalDraftCapital(List<Rosters> rosters, Dictionary<string, string> dpr)
        {
            int tempTotal = 0;
            foreach(var ros in rosters)
            {
                foreach(var pick in ros.DraftPicks)
                {
                    tempTotal += Int32.Parse(dpr[pick]);
                }
                ros.TeamRankingAverage = ros.TeamRankingAverage + tempTotal;
                ros.TotalDraftCapital = tempTotal;
                tempTotal = 0;
            }
            return rosters;
        }
        #endregion

        #region COMMENTED OUT FUNCTIONS

        //public ActionResult TeamBreakdown(string leagueID, string name)
        //{
        //    foreach (var ros in sleeperRosters)
        //    {
        //        if (ros.DisplayName == name)
        //        {
        //            ros.SelectedRoster = 1;
        //            continue;
        //        }
        //    }

        //    //sleeperRosters = FindTradeTargets(sleeperRosters);

        //    var viewModel = new TeamBreakdownViewModel
        //    {
        //        Rosters = sleeperRosters,
        //        SelectedRosterVM = sleeperRosters.Find(x => x.SelectedRoster == 1),
        //        LeagueID = leagueID
        //    };

        //    return View(viewModel);
        //}
        //public List<Rosters> FindTradeTargets(List<Rosters> rosters)
        //{
        //    var tempRoster = rosters.Find(x => x.SelectedRoster == 1);

        //    List<string> tempTradeCandidates = new List<string>();

        //    bool qbAdv;
        //    bool rbAdv;
        //    bool wrAdv;
        //    bool teAdv;

        //    foreach (var ros in rosters)
        //    {

        //        if (ros.RosterID != tempRoster.RosterID)
        //        {
        //            qbAdv = false;
        //            rbAdv = false;
        //            wrAdv = false;
        //            teAdv = false;

        //            //if(ros.QBRankingAverage > tempRoster.QBRankingAverage || ros.RBRankingAverage > tempRoster.RBRankingAverage || ros.WRRankingAverage > tempRoster.WRRankingAverage || ros.TERankingAverage > tempRoster.TERankingAverage)
        //            if (ros.QBRankingAverage > tempRoster.QBRankingAverage)
        //                qbAdv = true;
        //            if (ros.RBRankingAverage > tempRoster.RBRankingAverage)
        //                rbAdv = true;
        //            if (ros.WRRankingAverage > tempRoster.WRRankingAverage)
        //                wrAdv = true;
        //            if (ros.TERankingAverage > tempRoster.TERankingAverage)
        //                teAdv = true;

        //            if (qbAdv && rbAdv && wrAdv && teAdv || !qbAdv && !rbAdv && !wrAdv && !teAdv)
        //            {
        //                continue;
        //            }

        //            if (qbAdv || rbAdv || wrAdv || teAdv)
        //            {
        //                tempTradeCandidates.Add(ros.DisplayName);
        //            }
        //        }
        //    }
        //    tempRoster.TradeCandidates = tempTradeCandidates;

        //    return rosters;
        //}

        //public Dictionary<string, PlayerData> LoadRankings(Dictionary<string, PlayerData> players, List<KeepTradeCut> ktc)
        //{

        //    using (var reader = new StreamReader("C:\\Users\\timca\\source\\repos\\DynastyRanker\\DynastyRanker\\Data\\Value_Scores_Full_Data_data.csv"))
        //    {
        //        List<string> playerNameList = new List<string>();
        //        List<string> playerPositionList = new List<string>();
        //        List<string> playerKeepTradeCutList = new List<string>();

        //        while (!reader.EndOfStream)
        //        {
        //            var line = reader.ReadLine();
        //            var values = line.Split(',');

        //            playerNameList.Add(values[0]);
        //            playerPositionList.Add(values[1]);
        //        }

        //        foreach (var p in players)
        //        {
        //            string temp = "";
        //            string tempKTCValue = "";
        //            KeepTradeCut tempKTC = new KeepTradeCut();
        //            string firstNameTemp = p.Value.FirstName.Replace(".", string.Empty);
        //            string lastNameTemp = p.Value.LastName.Replace(".", string.Empty);
        //            temp = '"' + firstNameTemp + " " + lastNameTemp + '"';
        //            temp = temp.Remove(0, 1);
        //            temp = temp.Remove(temp.Length - 1, 1);

        //            if (playerNameList.Contains(temp))
        //            {
        //                if (ktc.Any(a => a.PlayerName == temp))
        //                {
        //                    tempKTC = ktc.Find(a => a.PlayerName == temp);
        //                    tempKTCValue = tempKTC.Value;
        //                    p.Value.KeepTradeCutValue = tempKTCValue;

        //                }
        //            }

        //        }

        //        return players;
        //    }
        //}

        //public List<KeepTradeCut> ScrapeRankings(Dictionary<string, PlayerData> players)
        //{
        //    string url = "https://keeptradecut.com/dynasty-rankings?page=0&filters=QB|WR|RB|TE|RDP&format=1";

        //    HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
        //    HtmlAgilityPack.HtmlDocument doc = web.Load(url);

        //    var nameTable = doc.DocumentNode.SelectNodes("//div[@class='player-name']");
        //    var valueTable = doc.DocumentNode.SelectNodes("//div[@class='value']");

        //    List<string> nameList = new List<string>();
        //    List<string> valueList = new List<string>();
        //    string temp = "";
        //    int tempSize = 0;
        //    foreach (var name in nameTable)
        //    {
        //        temp = name.InnerText;
        //        temp = temp.Trim();
        //        temp = temp.Replace("//n", "");
        //        tempSize = temp.Length;
        //        System.Diagnostics.Debug.WriteLine(temp);
        //        if (temp.EndsWith("R"))
        //        {
        //            temp = temp.Substring(0, tempSize - 1);
        //            temp = temp.Trim();
        //            temp = temp.Replace("\\n", "");
        //        }
        //        if (temp.Contains("."))
        //        {
        //            temp = temp.Replace(".", String.Empty);
        //        }
        //        if (temp.Contains("&#x27;"))
        //        {
        //            temp = temp.Replace("&#x27;", "'");
        //        }
        //        nameList.Add(temp);
        //    }
        //    foreach (var value in valueTable)
        //    {
        //        temp = value.InnerText;
        //        temp = temp.Trim();
        //        temp = temp.Replace("//n", "");
        //        valueList.Add(temp);
        //    }

        //    string tempName, tempValue;
        //    int count = 0;
        //    List<KeepTradeCut> ktcList = new List<KeepTradeCut>();
        //    KeepTradeCut newKtc = new KeepTradeCut();

        //    foreach (var p in nameList)
        //    {
        //        newKtc = new KeepTradeCut();
        //        tempName = p;
        //        tempValue = valueList.ElementAt(count);
        //        count++;

        //        newKtc.PlayerName = tempName;
        //        newKtc.Value = tempValue;

        //        ktcList.Add(newKtc);
        //    }

        //    return ktcList;
        //}


        //public List<Rosters> SortPlayersOnRosterByValue(List<Rosters> rosters)
        //{
        //    List<Rosters> sortedRosters = new List<Rosters>();

        //    foreach(var ros in sortedRosters)
        //    {
        //        ros.PlayersOnRoster = ros.PlayersOnRoster.OrderByDescending(o => o.Value.PORValue);
        //    }
        //    return sortedRosters;
        //}
        #endregion
    }
}

