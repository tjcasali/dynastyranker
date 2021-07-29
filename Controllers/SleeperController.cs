﻿using System;
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
        public List<POR> topWaiverPlayers = new List<POR>();
        public List<SleeperMatchups> matchups = new List<SleeperMatchups>();
        public RankingLists rankingLists = new RankingLists();

        public SleeperController(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            _env = env;
        }

        #region ActionResults
        public ActionResult Index()
        {
            return View();
        }

        /// DisplayLeague(League league)
        /// The psuedo main function of the Sleeper functionality. Called by the submit button click on the homepage
        /// and calls all of our functions and returns the DisplayLeague View.
        public async Task<ActionResult> DisplayLeague(string leagueID)
        {
            //Requires the user to fill in the text field. Otherwise it returns InvalidLeagueID
            if (leagueID != null)
            {
                try
                {
                    leagueInformation = await GetLeagueInformation(leagueID);
                    sleeperUsers = await GetUsers(leagueID);
                    sleeperRosters = await GetRosters(leagueID);
                    playerList = GetPlayers();
                    lastScrapeDate = GetPreviousScrapeDate(lastScrapeDate);
                    //matchups = await GetMatchups(leagueInformatio6n);
                    //LoadSleeperPlayersTextFile();

                    //TODO Put the if condition here so we don't even have to go into the scrape functions
                    //ScrapeRankings(lastScrapeDate);
                    //ScrapeSFRankings(lastScrapeDate);
                    playerList = LoadRankings(playerList, keepTradeCutList, leagueInformation);
                    playerList = LoadFantasyProsProjections(playerList);
                    LinkUsersAndRosters(sleeperUsers, sleeperRosters);

                    // Check if the inputted league and see if it has a previous league ID, meaning that there is a draft order for the rookie draft.
                    if(leagueInformation.PreviousLeagueID != "")
                    {
                        try
                        {
                            draft = await GetDraftOrder(leagueID);
                            if (draft.DraftOrder != null)
                            {
                                tradedPicks = await GetTradedDraftPicks(leagueInformation);
                                AddDraftPositionToRoster(draft, sleeperRosters);
                                sleeperRosters = AssignDraftPositionToPicks(sleeperRosters);
                                sleeperRosters = TradedDraftPicks(sleeperRosters, tradedPicks, draft);
                                sleeperRosters = GetTotalDraftCapital(sleeperRosters, draftPickRankings);
                            }
                            else
                                includeDraftCapital = false;
                        }
                        catch
                        {
                            //If any of these functions API calls return bad data we will just ignore the Draft Capital portion.
                            includeDraftCapital = false;
                        }
                    }

                    //This is in a Try Catch because this function is prone to break if the league has unique settings that I can't account for.
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
                    sleeperRosters = RankStartingLineups(sleeperRosters, leagueInformation);
                    //sleeperRosters = GetStrengthOfSchedule(sleeperRosters, matchups);
                    //sleeperRosters = RankStrengthOfSchedule(sleeperRosters);
                    sleeperRosters = SortRostersByRanking(sleeperRosters);
                    OrderStartingLineupRanking(sleeperRosters);
                    OrderTotalTeamRanking(sleeperRosters);
                    topWaiverPlayers = GetHighestValuesWaivers(playerList, draftPickRankings, sleeperRosters, draft);
                    rankingLists = SetRankings(sleeperRosters);
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
                IncludeDraftCapital = includeDraftCapital,
                TopWaiverPlayers = topWaiverPlayers,
                DraftInfo = draft,
                LeagueID = leagueInformation.LeagueID,
                RankingLists = rankingLists
            };

            return View(viewModel);
        }

        public async Task<ActionResult> DisplayLeagueByStarting(string leagueID)
        {
            //Requires the user to fill in the text field. Otherwise it returns InvalidLeagueID
            if (leagueID != null)
            {
                try
                {
                    leagueInformation = await GetLeagueInformation(leagueID);
                    sleeperUsers = await GetUsers(leagueID);
                    sleeperRosters = await GetRosters(leagueID);
                    playerList = GetPlayers();
                    lastScrapeDate = GetPreviousScrapeDate(lastScrapeDate);

                    playerList = LoadRankings(playerList, keepTradeCutList, leagueInformation);
                    playerList = LoadFantasyProsProjections(playerList);
                    LinkUsersAndRosters(sleeperUsers, sleeperRosters);

                    // Check if the inputted league and see if it has a previous league ID, meaning that there is a draft order for the rookie draft.
                    if (leagueInformation.PreviousLeagueID != "")
                    {
                        try
                        {
                            draft = await GetDraftOrder(leagueID);
                            if (draft.DraftOrder != null)
                            {
                                tradedPicks = await GetTradedDraftPicks(leagueInformation);
                                AddDraftPositionToRoster(draft, sleeperRosters);
                                sleeperRosters = AssignDraftPositionToPicks(sleeperRosters);
                                sleeperRosters = TradedDraftPicks(sleeperRosters, tradedPicks, draft);
                                sleeperRosters = GetTotalDraftCapital(sleeperRosters, draftPickRankings);
                            }
                            else
                                includeDraftCapital = false;
                        }
                        catch
                        {
                            //If any of these functions API calls return bad data we will just ignore the Draft Capital portion.
                            includeDraftCapital = false;
                        }
                    }

                    //This is in a Try Catch because this function is prone to break if the league has unique settings that I can't account for.
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
                    sleeperRosters = RankStartingLineups(sleeperRosters, leagueInformation);
                    //sleeperRosters = GetStrengthOfSchedule(sleeperRosters, matchups);
                    //sleeperRosters = RankStrengthOfSchedule(sleeperRosters);
                    sleeperRosters = SortRostersByStarting(sleeperRosters);
                    OrderStartingLineupRanking(sleeperRosters);
                    OrderTotalTeamRanking(sleeperRosters);
                    topWaiverPlayers = GetHighestValuesWaivers(playerList, draftPickRankings, sleeperRosters, draft);
                    rankingLists = SetRankings(sleeperRosters);

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
                IncludeDraftCapital = includeDraftCapital,
                TopWaiverPlayers = topWaiverPlayers,
                DraftInfo = draft,
                LeagueID = leagueInformation.LeagueID,
                RankingLists = rankingLists
            };

            return View(viewModel);
        }

        /// SelectLeague(string userName)
        /// Calls the SelectLeague View where the user picks the league they're going to view.
        /// Uses the SelectLeagueViewModel which contains the list of leagues returned by the API.
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

        /// GetLeagueInformation(string leagueID)
        /// Call the Sleeper API to return the league information
        /// This is necessary as it returns the starting lineup position counts and helps us determine if it's a superflex league
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
                if (position == "BN")
                {
                    break;
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

        /// GetUsers(string leagueID)
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

        /// GetUserIDFromUsername(string username)
        /// Call the sleeper API to get the UserID from the Username, we'll need the ID to call other functions
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

        /// GetAllLeaguesForUser(string userID)
        /// Now that we have the UserID we can get the list of leagues that the user is in
        public static async Task<List<League>> GetAllLeaguesForUser(string userID)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.sleeper.app/v1/user/" + userID + "/leagues/nfl/2021");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            List<League> leagues = JsonConvert.DeserializeObject<List<League>>(responseBody);

            return leagues;
        }

        /// GetRosters(string leagueID)
        /// Call the Sleeper API to return all of the rosters in a given league ID.
        public static async Task<List<Rosters>> GetRosters(string leagueID)
        {

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.sleeper.app/v1/league/" + leagueID + "/rosters");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            List<Rosters> rosters = JsonConvert.DeserializeObject<List<Rosters>>(responseBody);

            return rosters;
        }

        /// GetTradedDraftPicks(UserInfo leagueInfo)
        /// This function helps us properly display all of the draft capital because without it everybody would just have their picks 1-4
        public static async Task<List<TradedPick>> GetTradedDraftPicks(UserInfo leagueInfo)
        {
            HttpClient client = new HttpClient();
            //HttpResponseMessage response = await client.GetAsync("https://api.sleeper.app/v1/draft/" + leagueInfo.DraftID + "/traded_picks");
            HttpResponseMessage response = await client.GetAsync("https://api.sleeper.app/v1/league/" + leagueInfo.LeagueID + "/traded_picks");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            List<TradedPick> tradedPicks = JsonConvert.DeserializeObject<List<TradedPick>>(responseBody);

            return tradedPicks;
        }

        /// GetDraftOrder(string leagueID)
        /// Getting the draft order allows us to differentiate between Early/Mid/Late values on each individual pick.
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

        public static async Task<List<SleeperMatchups>> GetMatchups(UserInfo leagueInfo)
        {
            HttpClient client = new HttpClient();
            int playoffWeekStart = Convert.ToInt32(leagueInfo.PlayoffWeekStart);
            List<SleeperMatchups> matchups = new List<SleeperMatchups>();

            for (int i = 1; i < 14; i++)
            {
                HttpResponseMessage response = await client.GetAsync("https://api.sleeper.app/v1/league/" + leagueInfo.LeagueID + "/matchups/" + i);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<SleeperMatchups> thisWeeksMatchups = JsonConvert.DeserializeObject<List<SleeperMatchups>>(responseBody);
                SleeperMatchups matchup = new SleeperMatchups();
                foreach(var m in thisWeeksMatchups)
                {
                    matchup.Week = i.ToString();
                    matchup.RosterId = m.RosterId;
                    matchup.MatchupId = m.MatchupId;
                    matchups.Add(matchup);
                    matchup = new SleeperMatchups();
                }
            }
            return matchups;
        }

        /// LoadSleeperPlayersTextFile()
        /// This call is massive so instead of running this everytime somebody enters a league ID I pull it down into a text file as it doesn't change often.
        /// The call for this is currently commented out as there is no need for this to run right now.
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

        /// GetPlayers()
        /// Return the list of players that we put in the local text file. 
        public Dictionary<string, PlayerData> GetPlayers()
        {
            var webRoot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webRoot, "SleeperGetPlayers.txt");
            string json = System.IO.File.ReadAllText(file);

            Dictionary<string, PlayerData> playerList = JsonConvert.DeserializeObject<Dictionary<string, PlayerData>>(json);

            playerList = GetPlayersConcatenateName(playerList);

            return playerList;
        }

        public Dictionary<string,PlayerData> GetPlayersConcatenateName(Dictionary<string, PlayerData> players)
        {
            string tempName = "";

            foreach(var player in players)
            {
                tempName = player.Value.FirstName + " " + player.Value.LastName;
                player.Value.FullName = tempName;
                tempName = "";
            }
            return players;
        }
        #endregion

        #region Add Owner IDs and Player Values to Rosters

        /// LinkUsersAndRosters(List<SleeperUsers> users, List<Rosters> rosters)
        /// The Sleeper API call for rosters doesn't include the roster names in the call so this links that parameter in both Models.
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

        /// AddPlayerNamesToRosters(List<Rosters> rosters, Dictionary<string, PlayerData> players)
        /// The Sleeper API call for rosters doesn't include the roster names in the call so this links that parameter in both Models.
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

                //Ros.Bench contains all of the players on each roster
                if(ros.Bench != null)
                {
                    foreach (string p in ros.Bench)
                    {
                        if (players.ContainsKey(p))
                        {
                            tempPOREntry = new POR();
                            temp = players[p].FirstName + " " + players[p].LastName;

                            //OnRoster was added in 2.0 in order to determine which players are free agents as there's no call for that on the Sleeper API.
                            //We'll need this for GetHighestValueWaivers
                            players[p].OnRoster = true;

                            //Send the player ID to GetPlayerData to return the POR data
                            tempPlayer = GetPlayerData(p, players);
                            tempPlayerRankingsList.Add(tempPlayer.KeepTradeCutValue);

                            tempPlayersList.Add(temp);

                            tempPOREntry.PORName = temp;
                            tempPOREntry.PORPosition = tempPlayer.Position;
                            tempPOREntry.PORValue = Convert.ToInt32(tempPlayer.KeepTradeCutValue);
                            tempPOREntry.PORProjection = Convert.ToDouble(tempPlayer.FantasyProsProjection);

                            tempPORDict.Add(p, tempPOREntry);
                        }
                    }
                }
                ros.PlayerNames = tempPlayersList;
                ros.PlayerTradeValues = tempPlayerRankingsList;
                ros.PlayersOnRoster = tempPORDict;
            }
        }
        #endregion

        #region Ranking Functions

        /// LoadRankings(Dictionary<string, PlayerData> players, List<KeepTradeCut> ktc, UserInfo leagueInfo)
        /// Take the data from our scraped CSVs and put that into a KTC list so we can access the keeptradecut values
        public Dictionary<string, PlayerData> LoadRankings(Dictionary<string, PlayerData> players, List<KeepTradeCut> ktc, UserInfo leagueInfo)
        {
            string sr = "";
            var webRoot = _env.WebRootPath;

            //Check if the league has 1 or more QBs, then pull the respective .csv
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

                string tempName = "";

                while (!reader.EndOfStream)
                {
                    //Christian McCaffrey,RB,CAR,9999
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if(values[0] == "" || values[1] == "" || values[2] == "" || values[3] == "")
                    {
                        break;
                    }

                    tempName = values[0].Replace(".", String.Empty);
                    playerNameList.Add(tempName);
                    playerPositionList.Add(values[1]);
                    playerTeamList.Add(values[2]);
                    playerKeepTradeCutList.Add(values[3]);
                    
                    //Draft pick rankings have the position "PI"
                    if(values[1] == "PI")
                    {
                        draftPickRankings.Add(values[0], values[3]);
                    }
                }

                foreach (var p in players)
                {
                    string temp = "";
                    int tempIndex = 0;

                    //KTCScrape.csv doesn't have periods in names like DJ Moore and AJ Brown
                    string firstNameTemp = p.Value.FirstName.Replace(".", string.Empty);
                    string lastNameTemp = p.Value.LastName.Replace(".", string.Empty);

                    //temp looks like "/ McCaffrey, Christian /", so we need to change it to firstname lastname
                    temp = '"' + firstNameTemp + " " + lastNameTemp + '"';
                    temp = temp.Remove(0, 1);
                    temp = temp.Remove(temp.Length - 1, 1);

                    if(temp.Contains("Irv Smith") && !playerNameList.Contains(temp))
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

        public Dictionary<string, PlayerData> LoadFantasyProsProjections(Dictionary<string, PlayerData> players)
        {
            string sr = "";
            var webRoot = _env.WebRootPath;

            string tempName = "";
            string tempValue = "";
            PlayerData tempPlayer = new PlayerData();
            string tempPlayerID = "";
            double tempDouble;
            List<string> seenPlayers = new List<string>();

            sr = System.IO.Path.Combine(webRoot, "projections.csv");
            
            using (var reader = new StreamReader(sr))
            {
                while (!reader.EndOfStream)
                {
                    //ex. Josh Allen,378.7
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    //ex. Josh Allen
                    tempName = values[0];
                    //ex. 378.7
                    tempDouble = Math.Round(Convert.ToDouble(values[1]));
                    tempValue = tempDouble.ToString();

                    if(tempName == "AJ Dillon")
                        tempName = "A.J. Dillon";
                    if (tempName == "KJ Hamler")
                        tempName = "K.J. Hamler";
                    if (tempName == "Scotty Miller")
                        tempName = "Scott Miller";
                    if (tempName == "Will Fuller V")
                        tempName = "Will Fuller";
                    if (tempName == "Jeff Wilson Jr.")
                        tempName = "Jeffery Wilson";

                    if (tempName.EndsWith("I"))
                    {
                        while (tempName.EndsWith("I"))
                        {
                            tempName = tempName.Remove(tempName.Length - 1);
                        }
                        tempName = tempName.Trim();
                    }
                    

                    //Get the player ID where the name from the projections csv matches in the players list
                    tempPlayerID = players.FirstOrDefault(x => x.Value.FullName == tempName).Key;

                    //Return PlayerData using that player ID
                    if (tempPlayerID != "" && tempPlayerID != null)
                    {
                        tempPlayer = GetPlayerData(tempPlayerID, players);
                        //Assign the new FantasyPros Projection
                        tempPlayer.FantasyProsProjection = tempValue;
                        seenPlayers.Add(tempName);
                        continue;
                    }
                    else if(tempName.Contains("Jr.") || tempName.Contains("Sr."))
                    {
                        tempName = tempName.Replace("Jr.", String.Empty);
                        tempName = tempName.Trim();
                        tempName = tempName.Replace("Sr.", String.Empty);
                        tempName = tempName.Trim();

                        tempPlayerID = players.FirstOrDefault(x => x.Value.FullName == tempName).Key;

                        if (tempPlayerID != "" && tempPlayerID != null)
                        {
                            tempPlayer = GetPlayerData(tempPlayerID, players);
                            //Assign the new FantasyPros Projection
                            tempPlayer.FantasyProsProjection = tempValue;
                            seenPlayers.Add(tempName);
                            continue;
                        }
                    }
                    else if(tempName.Contains("."))
                    {
                        tempName = tempName.Replace(".", String.Empty);

                        tempPlayerID = players.FirstOrDefault(x => x.Value.FullName == tempName).Key;

                        if (tempPlayerID != "" && tempPlayerID != null)
                        {
                            tempPlayer = GetPlayerData(tempPlayerID, players);
                            //Assign the new FantasyPros Projection
                            tempPlayer.FantasyProsProjection = tempValue;
                            seenPlayers.Add(tempName);
                            continue;
                        }
                    }
                }
            }
            return players;
        }


        /// AverageTeamRanking(List<Rosters> rosters, Dictionary<string, PlayerData> players)
        /// Loop through each player on each roster and sum up the positional totals and the team total
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
                if (ros.Bench != null)
                {
                    foreach (string playerID in ros.Bench)
                    {
                        if (players.ContainsKey(playerID))
                        {
                            PlayerData currentPlayer = new PlayerData();
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

        /// GetPlayerData(string playerID, Dictionary<string, PlayerData> playerList)
        /// This gets called in a few functions to turn the player ID into the players actual data, mainly used to get their keeptradecut value.
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
                List<string> rookieList = new List<string>();

                string temp, positionTemp, teamTemp = "";
                int tempSize = 0;

                int avg2022First = 0;
                int avg2022Second = 0;
                int avg2022Third = 0;
                int avg2022Fourth = 0;
                int avg2023First = 0;
                int avg2023Second = 0;
                int avg2023Third = 0;
                int avg2023Fourth = 0;

                foreach (var name in nameTable.Skip(1))
                {
                    temp = name.InnerText;
                    temp = temp.Replace(".", string.Empty);
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
                        rookieList.Add("true");
                    }
                    else
                        rookieList.Add("false");

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


                string tempName, tempValue, tempPosition, tempTeam, tempIsRookie;
                int count = 0;
                List<Player> ktcList = new List<Player>();

                Player newKtc = new Player();

                foreach (var p in nameList)
                {
                    newKtc = new Player();
                    tempName = p;

                    if (valueList.ElementAt(count) != null)
                        tempValue = valueList.ElementAt(count);
                    else
                        tempValue = "NA";

                    if (teamList.ElementAt(count) != null)
                        tempTeam = teamList.ElementAt(count);
                    else
                        tempTeam = "NA";

                    if (positionList.ElementAt(count) != null)
                        tempPosition = positionList.ElementAt(count);
                    else
                        tempPosition = "NA";

                    if (rookieList.ElementAt(count) != null)
                        tempIsRookie = rookieList.ElementAt(count);
                    else
                        tempIsRookie = "NA";

                    if (tempName.Contains("2022") && tempName.Contains("1st"))
                        avg2022First += Convert.ToInt32(tempValue);
                    if (tempName.Contains("2022") && tempName.Contains("2nd"))
                        avg2022Second += Convert.ToInt32(tempValue);
                    if (tempName.Contains("2022") && tempName.Contains("3rd"))
                        avg2022Third += Convert.ToInt32(tempValue);
                    if (tempName.Contains("2022") && tempName.Contains("4th"))
                        avg2022Fourth += Convert.ToInt32(tempValue);
                    if (tempName.Contains("2023") && tempName.Contains("1st"))
                        avg2023First += Convert.ToInt32(tempValue);
                    if (tempName.Contains("2023") && tempName.Contains("2nd"))
                        avg2023Second += Convert.ToInt32(tempValue);
                    if (tempName.Contains("2023") && tempName.Contains("3rd"))
                        avg2023Third += Convert.ToInt32(tempValue);
                    if (tempName.Contains("2023") && tempName.Contains("4th"))
                        avg2023Fourth += Convert.ToInt32(tempValue);  

                    newKtc.Name = tempName;
                    newKtc.Value = tempValue;
                    newKtc.Position = tempPosition;
                    newKtc.Team = tempTeam;
                    newKtc.IsRookie = tempIsRookie;

                    ktcList.Add(newKtc);

                    count++;
                }

                newKtc = new Player();
                avg2022First = avg2022First / 3;
                //avg2022First = Math.Truncate(avg2022First);
                newKtc.Name = "2022 1st";
                newKtc.Value = Convert.ToString(avg2022First);
                newKtc.Position = "PI";
                newKtc.Team = "NA";
                newKtc.IsRookie = "NA";
                ktcList.Add(newKtc);

                newKtc = new Player();
                avg2022Second = avg2022Second / 3;
                //avg2022Second = Math.Truncate(avg2022Second);
                newKtc.Name = "2022 2nd";
                newKtc.Value = Convert.ToString(avg2022Second);
                newKtc.Position = "PI";
                newKtc.Team = "NA";
                newKtc.IsRookie = "NA";
                ktcList.Add(newKtc);

                newKtc = new Player();
                avg2022Third = avg2022Third / 3;
                //avg2022Third = Math.Truncate(avg2022Third);
                newKtc.Name = "2022 3rd";
                newKtc.Value = Convert.ToString(avg2022Third);
                newKtc.Position = "PI";
                newKtc.Team = "NA";
                newKtc.IsRookie = "NA";
                ktcList.Add(newKtc);

                newKtc = new Player();
                avg2022Fourth = avg2022Fourth / 3;
                //avg2022Fourth = Math.Truncate(avg2022Fourth);
                newKtc.Name = "2022 4th";
                newKtc.Value = Convert.ToString(avg2022Fourth);
                newKtc.Position = "PI";
                newKtc.Team = "NA";
                newKtc.IsRookie = "NA";
                ktcList.Add(newKtc);

                newKtc = new Player();
                avg2023First = avg2023First / 3;
                //avg2023First = Math.Truncate(avg2023First);
                newKtc.Name = "2023 1st";
                newKtc.Value = Convert.ToString(avg2023First);
                newKtc.Position = "PI";
                newKtc.Team = "NA";
                newKtc.IsRookie = "NA";
                ktcList.Add(newKtc);

                newKtc = new Player();
                avg2023Second = avg2023Second / 3;
                //avg2023Second = Math.Truncate(avg2023Second);
                newKtc.Name = "2023 2nd";
                newKtc.Value = Convert.ToString(avg2023Second);
                newKtc.Position = "PI";
                newKtc.Team = "NA";
                newKtc.IsRookie = "NA";
                ktcList.Add(newKtc);

                newKtc = new Player();
                avg2023Third= avg2023Third / 3;
                //avg2023Third = Math.Truncate(avg2023Third);
                newKtc.Name = "2023 3rd";
                newKtc.Value = Convert.ToString(avg2023Third);
                newKtc.Position = "PI";
                newKtc.Team = "NA";
                newKtc.IsRookie = "NA";
                ktcList.Add(newKtc);

                newKtc = new Player();
                avg2023Fourth = avg2023Fourth / 3;
                //avg2023Fourth = Math.Truncate(avg2023Fourth);
                newKtc.Name = "2023 4th";
                newKtc.Value = Convert.ToString(avg2023Fourth);
                newKtc.Position = "PI";
                newKtc.Team = "NA";
                newKtc.IsRookie = "NA";
                ktcList.Add(newKtc);



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

                int avg2022First = 0;
                int avg2022Second = 0;
                int avg2022Third = 0;
                int avg2022Fourth = 0;
                int avg2023First = 0;
                int avg2023Second = 0;
                int avg2023Third = 0;
                int avg2023Fourth = 0;

                string temp, positionTemp, teamTemp = "";
                int tempSize = 0;

                foreach (var name in nameTable.Skip(1))
                {
                    temp = name.InnerText;
                    temp = temp.Replace(".", string.Empty);
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
                    if (tempName.Contains("2022") && tempName.Contains("1st"))
                        avg2022First += Convert.ToInt32(tempValue);
                    if (tempName.Contains("2022") && tempName.Contains("2nd"))
                        avg2022Second += Convert.ToInt32(tempValue);
                    if (tempName.Contains("2022") && tempName.Contains("3rd"))
                        avg2022Third += Convert.ToInt32(tempValue);
                    if (tempName.Contains("2022") && tempName.Contains("4th"))
                        avg2022Fourth += Convert.ToInt32(tempValue);
                    if (tempName.Contains("2023") && tempName.Contains("1st"))
                        avg2023First += Convert.ToInt32(tempValue);
                    if (tempName.Contains("2023") && tempName.Contains("2nd"))
                        avg2023Second += Convert.ToInt32(tempValue);
                    if (tempName.Contains("2023") && tempName.Contains("3rd"))
                        avg2023Third += Convert.ToInt32(tempValue);
                    if (tempName.Contains("2023") && tempName.Contains("4th"))
                        avg2023Fourth += Convert.ToInt32(tempValue);

                    newKtc.Name = tempName;
                    newKtc.Value = tempValue;
                    newKtc.Position = tempPosition;
                    newKtc.Team = tempTeam;

                    ktcList.Add(newKtc);
           
                    count++;
                }

                newKtc = new Player();
                avg2022First = avg2022First / 3;
                //avg2022First = Math.Truncate(avg2022First);
                newKtc.Name = "2022 1st";
                newKtc.Value = Convert.ToString(avg2022First);
                newKtc.Position = "PI";
                newKtc.Team = "NA";
                newKtc.IsRookie = "NA";
                ktcList.Add(newKtc);

                newKtc = new Player();
                avg2022Second = avg2022Second / 3;
                //avg2022Second = Math.Truncate(avg2022Second);
                newKtc.Name = "2022 2nd";
                newKtc.Value = Convert.ToString(avg2022Second);
                newKtc.Position = "PI";
                newKtc.Team = "NA";
                newKtc.IsRookie = "NA";
                ktcList.Add(newKtc);

                newKtc = new Player();
                avg2022Third = avg2022Third / 3;
                //avg2022Third = Math.Truncate(avg2022Third);
                newKtc.Name = "2022 3rd";
                newKtc.Value = Convert.ToString(avg2022Third);
                newKtc.Position = "PI";
                newKtc.Team = "NA";
                newKtc.IsRookie = "NA";
                ktcList.Add(newKtc);

                newKtc = new Player();
                avg2022Fourth = avg2022Fourth / 3;
                //avg2022Fourth = Math.Truncate(avg2022Fourth);
                newKtc.Name = "2022 4th";
                newKtc.Value = Convert.ToString(avg2022Fourth);
                newKtc.Position = "PI";
                newKtc.Team = "NA";
                newKtc.IsRookie = "NA";
                ktcList.Add(newKtc);

                newKtc = new Player();
                avg2023First = avg2023First / 3;
                //avg2023First = Math.Truncate(avg2023First);
                newKtc.Name = "2023 1st";
                newKtc.Value = Convert.ToString(avg2023First);
                newKtc.Position = "PI";
                newKtc.Team = "NA";
                newKtc.IsRookie = "NA";
                ktcList.Add(newKtc);

                newKtc = new Player();
                avg2023Second = avg2023Second / 3;
                //avg2023Second = Math.Truncate(avg2023Second);
                newKtc.Name = "2023 2nd";
                newKtc.Value = Convert.ToString(avg2023Second);
                newKtc.Position = "PI";
                newKtc.Team = "NA";
                newKtc.IsRookie = "NA";
                ktcList.Add(newKtc);

                newKtc = new Player();
                avg2023Third = avg2023Third / 3;
                //avg2023Third = Math.Truncate(avg2023Third);
                newKtc.Name = "2023 3rd";
                newKtc.Value = Convert.ToString(avg2023Third);
                newKtc.Position = "PI";
                newKtc.Team = "NA";
                newKtc.IsRookie = "NA";
                ktcList.Add(newKtc);

                newKtc = new Player();
                avg2023Fourth = avg2023Fourth / 3;
                //avg2023Fourth = Math.Truncate(avg2023Fourth);
                newKtc.Name = "2023 4th";
                newKtc.Value = Convert.ToString(avg2023Fourth);
                newKtc.Position = "PI";
                newKtc.Team = "NA";
                newKtc.IsRookie = "NA";
                ktcList.Add(newKtc);


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
                System.IO.File.WriteAllText(data, newScrapeDate);
            }
            //return newScrapeDate;
        }

        public List<Rosters> SortRostersByRanking(List<Rosters> rosters)
        {
            List<Rosters> sortedRosters = rosters.OrderByDescending(o => o.TeamRankingAverage).ToList();
            return sortedRosters;
        }
        public List<Rosters> SortRostersByStarting(List<Rosters> rosters)
        {
            List<Rosters> sortedRosters = rosters.OrderByDescending(o => o.TeamStartingTotal).ToList();
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

        public void OrderTotalTeamRanking(List<Rosters> rosters)
        {
            List<Rosters> sortedRosters = rosters.OrderByDescending(o => o.TeamRankingAverage).ToList();
            int count = 0;
            foreach (var ros in sortedRosters)
            {
                count++;
                ros.TeamTotalRank = count;
            }

        }

        public List<Rosters> RankStartingLineups(List<Rosters> rosters, UserInfo leagueInfo)
        {
            int positionCounter = 0;
            int remainingFlex = leagueInfo.FLEXCount + leagueInfo.RECFLEXCount + leagueInfo.SUPERFLEXCount;
            int superflexAdded = 0;
            int recflexAdded = 0;

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
                if (leagueInfo.QBCount != 0)
                {
                    foreach (var player in ros.PlayersOnRoster.Where(o => o.Value.PORPosition == "QB").OrderByDescending(o => o.Value.PORProjection))
                    {
                        skippedPlayerNames.Add(player.Value.PORName);
                        startingPlayerNames.Add(player.Value.PORName);
                        positionCounter++;
                        if (positionCounter == leagueInfo.QBCount)
                        {
                            startingQBTotal += player.Value.PORProjection;
                            positionCounter = 0;
                            break;
                        }
                        else
                        {
                            startingQBTotal += player.Value.PORProjection;
                        }
                    }
                }
                if (leagueInfo.RBCount != 0)
                {
                    foreach (var player in ros.PlayersOnRoster.Where(o => o.Value.PORPosition == "RB").OrderByDescending(o => o.Value.PORProjection))
                    {
                        skippedPlayerNames.Add(player.Value.PORName);
                        startingPlayerNames.Add(player.Value.PORName);
                        positionCounter++;
                        if (positionCounter == leagueInfo.RBCount)
                        {
                            startingRBTotal += player.Value.PORProjection;
                            positionCounter = 0;
                            break;
                        }
                        else
                        {
                            startingRBTotal += player.Value.PORProjection;
                        }
                    }
                }
                if (leagueInfo.WRCount != 0)
                {
                    foreach (var player in ros.PlayersOnRoster.Where(o => o.Value.PORPosition == "WR").OrderByDescending(o => o.Value.PORProjection))
                    {
                        skippedPlayerNames.Add(player.Value.PORName);
                        startingPlayerNames.Add(player.Value.PORName);
                        positionCounter++;
                        if (positionCounter == leagueInfo.WRCount)
                        {
                            startingWRTotal += player.Value.PORProjection;
                            positionCounter = 0;
                            break;
                        }
                        else
                        {
                            startingWRTotal += player.Value.PORProjection;
                        }
                    }
                }
                if (leagueInfo.TECount != 0)
                {
                    foreach (var player in ros.PlayersOnRoster.Where(o => o.Value.PORPosition == "TE").OrderByDescending(o => o.Value.PORProjection))
                    {
                        skippedPlayerNames.Add(player.Value.PORName);
                        startingPlayerNames.Add(player.Value.PORName);
                        positionCounter++;
                        if (positionCounter == leagueInfo.TECount)
                        {
                            startingTETotal += player.Value.PORProjection;
                            positionCounter = 0;
                            break;
                        }
                        else
                        {
                            startingTETotal += player.Value.PORProjection;
                        }
                    }
                }


                foreach (var player in ros.PlayersOnRoster.OrderByDescending(o => o.Value.PORProjection))
                {
                    if (leagueInfo.SUPERFLEXCount != 0 && leagueInfo.SUPERFLEXCount != superflexAdded)
                    {
                        if (skippedPlayerNames.Contains(player.Value.PORName))
                        {
                            continue;
                        }
                        skippedPlayerNames.Add(player.Value.PORName);
                        flexPlayerNames.Add(player.Value.PORName);
                        if (player.Value.PORPosition == "QB")
                            superflexAdded++;
                        positionCounter++;
                        if (positionCounter == leagueInfo.FLEXCount + leagueInfo.RECFLEXCount + leagueInfo.SUPERFLEXCount)
                        {
                            skippedPlayerNames.Add(player.Value.PORName);
                            startingFLEXTotal += player.Value.PORProjection;
                            positionCounter = 0;
                            recflexAdded = 0;
                            superflexAdded = 0;
                            break;
                        }
                        else
                        {
                            startingFLEXTotal += player.Value.PORProjection;
                        }
                    }
                    else if (leagueInfo.RECFLEXCount != 0)
                    {
                        if (skippedPlayerNames.Contains(player.Value.PORName) || player.Value.PORPosition == "QB" || player.Value.PORPosition == "RB")
                            continue;

                        skippedPlayerNames.Add(player.Value.PORName);
                        flexPlayerNames.Add(player.Value.PORName);
                        if (player.Value.PORPosition == "WR" || player.Value.PORPosition == "TE")
                            recflexAdded++;
                        positionCounter++;
                        if (positionCounter == leagueInfo.FLEXCount + leagueInfo.RECFLEXCount + leagueInfo.SUPERFLEXCount)
                        {
                            skippedPlayerNames.Add(player.Value.PORName);
                            startingFLEXTotal += player.Value.PORProjection;
                            positionCounter = 0;
                            recflexAdded = 0;
                            superflexAdded = 0;
                            break;
                        }
                        else
                        {
                            startingFLEXTotal += player.Value.PORProjection;
                        }
                    }
                    else if (leagueInfo.FLEXCount != 0)
                    {
                        if (skippedPlayerNames.Contains(player.Value.PORName) || player.Value.PORPosition == "QB")
                            continue;

                        skippedPlayerNames.Add(player.Value.PORName);
                        flexPlayerNames.Add(player.Value.PORName);

                        positionCounter++;
                        if (positionCounter == leagueInfo.FLEXCount + leagueInfo.RECFLEXCount + leagueInfo.SUPERFLEXCount)
                        {
                            skippedPlayerNames.Add(player.Value.PORName);
                            startingFLEXTotal += player.Value.PORProjection;
                            positionCounter = 0;
                            recflexAdded = 0;
                            superflexAdded = 0;
                            break;
                        }
                        else
                        {
                            startingFLEXTotal += player.Value.PORProjection;
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

        public List<POR> GetHighestValuesWaivers(Dictionary<string, PlayerData> players, Dictionary<string, string> dpr, List<Rosters> rosters, Draft draft)
        {
            POR unsignedPlayer = new POR();
            List<POR> unsignedPlayerList = new List<POR>();
            if (draft.Status != "complete")
            {
                foreach (var p in players)
                {

                    if (!p.Value.OnRoster && (p.Value.Position == "QB" || p.Value.Position == "RB" || p.Value.Position == "WR" || p.Value.Position == "TE") && (p.Value.KeepTradeCutValue != null || p.Value.KeepTradeCutValue != "0") && p.Value.YearsExperience != "0")
                    {
                        unsignedPlayer.PORName = p.Value.FirstName + " " + p.Value.LastName;
                        unsignedPlayer.PORPosition = p.Value.Position;
                        unsignedPlayer.PORValue = Convert.ToInt32(p.Value.KeepTradeCutValue);
                        unsignedPlayerList.Add(unsignedPlayer);
                        unsignedPlayer = new POR();
                    }
                }
            }

            else 
            {
                foreach (var p in players)
                {
                    if (!p.Value.OnRoster && (p.Value.Position == "QB" || p.Value.Position == "RB" || p.Value.Position == "WR" || p.Value.Position == "TE") && (p.Value.KeepTradeCutValue != null || p.Value.KeepTradeCutValue != "0"))
                    {
                        unsignedPlayer.PORName = p.Value.FirstName + " " + p.Value.LastName;
                        unsignedPlayer.PORPosition = p.Value.Position;
                        unsignedPlayer.PORValue = Convert.ToInt32(p.Value.KeepTradeCutValue);
                        unsignedPlayerList.Add(unsignedPlayer);
                        unsignedPlayer = new POR();
                    }
                }
            }
            return unsignedPlayerList;
        }

        #endregion

        #region Draft Picks Functions
        public void AddDraftPositionToRoster(Draft draft, List<Rosters> rosters)
        {
            List<string> startingDraftPicks = new List<string>();

            foreach (Rosters ros in rosters)
            {
                if(draft.Rounds >= 4)
                {
                    if(draft.Status != "complete")
                    {
                        startingDraftPicks.Add("2021 1st");
                        startingDraftPicks.Add("2021 2nd");
                        startingDraftPicks.Add("2021 3rd");
                        startingDraftPicks.Add("2021 4th");
                    }
                    startingDraftPicks.Add("2022 1st");
                    startingDraftPicks.Add("2022 2nd");
                    startingDraftPicks.Add("2022 3rd");
                    startingDraftPicks.Add("2022 4th");
                    startingDraftPicks.Add("2023 1st");
                    startingDraftPicks.Add("2023 2nd");
                    startingDraftPicks.Add("2023 3rd");
                    startingDraftPicks.Add("2023 4th");
                }
                else if (draft.Rounds == 3)
                {
                    if (draft.Status != "complete")
                    {
                        startingDraftPicks.Add("2021 1st");
                        startingDraftPicks.Add("2021 2nd");
                        startingDraftPicks.Add("2021 3rd");
                    }
                    startingDraftPicks.Add("2022 1st");
                    startingDraftPicks.Add("2022 2nd");
                    startingDraftPicks.Add("2022 3rd");
                    startingDraftPicks.Add("2023 1st");
                    startingDraftPicks.Add("2023 2nd");
                    startingDraftPicks.Add("2023 3rd");
                }
                else if (draft.Rounds == 2)
                {
                    if (draft.Status != "complete")
                    {
                        startingDraftPicks.Add("2021 1st");
                        startingDraftPicks.Add("2021 2nd");
                    }
                    startingDraftPicks.Add("2021 1st");
                    startingDraftPicks.Add("2021 2nd");
                    startingDraftPicks.Add("2022 1st");
                    startingDraftPicks.Add("2022 2nd");
                    startingDraftPicks.Add("2023 1st");
                    startingDraftPicks.Add("2023 2nd");
                }
                else if (draft.Rounds == 1)
                {
                    if (draft.Status != "complete")
                    {
                        startingDraftPicks.Add("2021 1st");
                    }
                    startingDraftPicks.Add("2022 1st");
                    startingDraftPicks.Add("2023 1st");
                }
                else
                {
                    if (draft.Status != "complete")
                    {
                        startingDraftPicks.Add("2021 1st");
                        startingDraftPicks.Add("2021 2nd");
                        startingDraftPicks.Add("2021 3rd");
                        startingDraftPicks.Add("2021 4th");
                    }
                    startingDraftPicks.Add("2022 1st");
                    startingDraftPicks.Add("2022 2nd");
                    startingDraftPicks.Add("2022 3rd");
                    startingDraftPicks.Add("2022 4th");
                    startingDraftPicks.Add("2023 1st");
                    startingDraftPicks.Add("2023 2nd");
                    startingDraftPicks.Add("2023 3rd");
                    startingDraftPicks.Add("2023 4th");
                }

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
                    if(tempPick.Contains("2021"))
                    {
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
                    }

                    ros.DraftPicks[i] = tempPick;
                    tempPick = "";
                }

            }

            return rosters;
        }

        public List<Rosters> TradedDraftPicks(List<Rosters> rosters, List<TradedPick> tp, Draft draft)
        {
            Rosters originalOwnerRoster = new Rosters();
            Rosters newOwnerRoster = new Rosters();
            Rosters previousOwnerRoster = new Rosters();
            Rosters rosterOfRemovedPick = new Rosters();
            List<KeyValuePair<string, string>> picksToBeRemoved = new List<KeyValuePair<string, string>>();
            int pickFound = 0;

            foreach (var trade in tp)
            {
                System.Diagnostics.Debug.WriteLine(trade.Season + " " + trade.Round + " " + trade.Position);

                if (trade.Season == "2021" || trade.Season == "2022" || trade.Season =="2023")
                {
                    if (draft.Status == "complete" && trade.Season == "2021")
                    {
                        continue;
                    }

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

                    if (trade.Season == "2021")
                    {
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
                    }

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

            foreach (var ros in rosters)
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

        public List<Rosters> GetStrengthOfSchedule(List<Rosters> rosters, List<SleeperMatchups> matchups)
        {
            Rosters matchupRoster = new Rosters();
            foreach(var r in rosters)
            {
                foreach(var m in matchups)
                {
                    if(m.RosterId == r.RosterID)
                    {
                        matchupRoster = rosters.Where(o => o.RosterID == m.MatchupId).First();
                        r.StrengthOfScheduleTotal += matchupRoster.TeamStartingTotal;
                        continue;
                    }
                }
            }
            return rosters;
        }
        public List<Rosters> RankStrengthOfSchedule(List<Rosters> rosters)
        {
            List<Rosters> rankedRosters = new List<Rosters>();
            rankedRosters = rosters.OrderByDescending(o => o.StrengthOfScheduleTotal).ToList();
            int count = 1;
            foreach (var ros in rankedRosters)
            {
                ros.StrengthOfScheduleRank = count;
                count++;
            }
            return rankedRosters;
        }

        public RankingLists SetRankings(List<Rosters> rosters)
        {
            RankingLists rl = new RankingLists();
            List<Rosters> rankedRosters = new List<Rosters>();
            List<string> teamNames = new List<string>();

            //OrderBy for parameters that I've already ranked 1-12, OrderByDescending for summed values
            //QB Ranking
            foreach(var ros in rosters.OrderBy(o=> o.QBRank))
            {
                teamNames.Add(ros.DisplayName);
            }
            rl.QBRankingList = teamNames;
            teamNames = new List<string>();

            //RB Ranking
            foreach (var ros in rosters.OrderBy(o => o.RBRank))
            {
                teamNames.Add(ros.DisplayName);
            }
            rl.RBRankingList = teamNames;
            teamNames = new List<string>();

            //WR Ranking
            foreach (var ros in rosters.OrderBy(o => o.WRRank))
            {
                teamNames.Add(ros.DisplayName);
            }
            rl.WRRankingList = teamNames;
            teamNames = new List<string>();

            //TE Ranking
            foreach (var ros in rosters.OrderBy(o => o.TERank))
            {
                teamNames.Add(ros.DisplayName);
            }
            rl.TERankingList = teamNames;
            teamNames = new List<string>();

            //Total Draft Capital
            rankedRosters = rosters.OrderByDescending(o => o.TotalDraftCapital).ToList();
            foreach (var ros in rankedRosters)
            {
                teamNames.Add(ros.DisplayName);
            }
            rl.DraftCapitalRankingList = teamNames;
            teamNames = new List<string>();

            //Total Team Ranking
            rankedRosters = rosters.OrderBy(o => o.TeamTotalRank).ToList();
            foreach (var ros in rankedRosters)
            {
                teamNames.Add(ros.DisplayName);
            }
            rl.TeamTotalRankingList = teamNames;
            teamNames = new List<string>();

            //Starting Lineup Ranking
            rankedRosters = rosters.OrderBy(o => o.StartingTeamRank).ToList();
            foreach (var ros in rankedRosters)
            {
                teamNames.Add(ros.DisplayName);
            }
            rl.StaringLineupRankingList = teamNames;
            teamNames = new List<string>();

            return rl;
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

