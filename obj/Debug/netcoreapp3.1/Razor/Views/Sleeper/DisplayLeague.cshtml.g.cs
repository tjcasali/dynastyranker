#pragma checksum "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e61e2ac9449d0d5d5fa6a665bb1277a4a876f296"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sleeper_DisplayLeague), @"mvc.1.0.view", @"/Views/Sleeper/DisplayLeague.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\projects\DynastyRanker\DynastyRanker\Views\_ViewImports.cshtml"
using DynastyRanker;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\projects\DynastyRanker\DynastyRanker\Views\_ViewImports.cshtml"
using DynastyRanker.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e61e2ac9449d0d5d5fa6a665bb1277a4a876f296", @"/Views/Sleeper/DisplayLeague.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5a9530e6195efcf2b269f4598e81ab155c1830ef", @"/Views/_ViewImports.cshtml")]
    public class Views_Sleeper_DisplayLeague : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DynastyRanker.ViewModels.DisplayLeagueViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/site.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("leagueBody"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e61e2ac9449d0d5d5fa6a665bb1277a4a876f2964335", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n<meta charset=\"UTF-8\">\n\n");
#nullable restore
#line 6 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
   
    int count = 1;
    Dictionary<int, string> rankingsDict = new Dictionary<int, string>();

    if (rankingsDict.Count == 0)
    {
        rankingsDict.Add(1, "1st");
        rankingsDict.Add(2, "2nd");
        rankingsDict.Add(3, "3rd");
        rankingsDict.Add(4, "4th");
        rankingsDict.Add(5, "5th");
        rankingsDict.Add(6, "6th");
        rankingsDict.Add(7, "7th");
        rankingsDict.Add(8, "8th");
        rankingsDict.Add(9, "9th");
        rankingsDict.Add(10, "10th");
        rankingsDict.Add(11, "11th");
        rankingsDict.Add(12, "12th");
        rankingsDict.Add(13, "13th");
        rankingsDict.Add(14, "14th");
        rankingsDict.Add(15, "15th");
        rankingsDict.Add(16, "16th");
        rankingsDict.Add(17, "17th");
        rankingsDict.Add(18, "18th");
        rankingsDict.Add(19, "19th");
        rankingsDict.Add(20, "20th");
        rankingsDict.Add(21, "21st");
        rankingsDict.Add(22, "22nd");
        rankingsDict.Add(23, "23rd");
        rankingsDict.Add(24, "24th");
        rankingsDict.Add(25, "25th");
        rankingsDict.Add(26, "26th");
        rankingsDict.Add(27, "27th");
        rankingsDict.Add(28, "28th");
        rankingsDict.Add(29, "29th");
        rankingsDict.Add(30, "30th");
        rankingsDict.Add(31, "31st");
        rankingsDict.Add(32, "32nd");
        rankingsDict.Add(33, "33rd");
        rankingsDict.Add(34, "34th");
        rankingsDict.Add(35, "35th");
        rankingsDict.Add(36, "36th");
        rankingsDict.Add(37, "37th");
        rankingsDict.Add(38, "38th");
        rankingsDict.Add(39, "39th");
        rankingsDict.Add(40, "40th");
        rankingsDict.Add(41, "41st");
        rankingsDict.Add(42, "42nd");
        rankingsDict.Add(43, "43rd");
        rankingsDict.Add(44, "44th");
        rankingsDict.Add(45, "45th");
        rankingsDict.Add(46, "46th");
        rankingsDict.Add(47, "47th");
        rankingsDict.Add(48, "48th");
        rankingsDict.Add(49, "49th");
        rankingsDict.Add(50, "50th");
        rankingsDict.Add(51, "51st");
        rankingsDict.Add(52, "52nd");
        rankingsDict.Add(53, "53rd");
        rankingsDict.Add(54, "54th");
        rankingsDict.Add(55, "55th");
        rankingsDict.Add(56, "56th");
        rankingsDict.Add(57, "57th");
        rankingsDict.Add(58, "58th");
        rankingsDict.Add(59, "59th");
        rankingsDict.Add(60, "60th");
        rankingsDict.Add(61, "61st");
        rankingsDict.Add(62, "62nd");
        rankingsDict.Add(63, "63rd");
        rankingsDict.Add(64, "64th");
        rankingsDict.Add(65, "65th");
        rankingsDict.Add(66, "66th");
        rankingsDict.Add(67, "67th");
        rankingsDict.Add(68, "68th");
        rankingsDict.Add(69, "69th");
        rankingsDict.Add(70, "70th");
        rankingsDict.Add(71, "71st");
        rankingsDict.Add(72, "72nd");
        rankingsDict.Add(73, "73rd");
        rankingsDict.Add(74, "74th");
        rankingsDict.Add(75, "75th");
        rankingsDict.Add(76, "76th");
        rankingsDict.Add(77, "77th");
        rankingsDict.Add(78, "78th");
        rankingsDict.Add(79, "79th");
        rankingsDict.Add(80, "80th");
        rankingsDict.Add(81, "81st");
        rankingsDict.Add(82, "82nd");
        rankingsDict.Add(83, "83rd");
        rankingsDict.Add(84, "84th");
        rankingsDict.Add(85, "85th");
        rankingsDict.Add(86, "86th");
        rankingsDict.Add(87, "87th");
        rankingsDict.Add(88, "88th");
        rankingsDict.Add(89, "89th");
        rankingsDict.Add(90, "90th");
        rankingsDict.Add(91, "91st");
        rankingsDict.Add(92, "92nd");
        rankingsDict.Add(93, "93rd");
        rankingsDict.Add(94, "94th");
        rankingsDict.Add(95, "95th");
        rankingsDict.Add(96, "96th");
        rankingsDict.Add(97, "97th");
        rankingsDict.Add(98, "98th");
        rankingsDict.Add(99, "99th");
        rankingsDict.Add(100, "100th");
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e61e2ac9449d0d5d5fa6a665bb1277a4a876f2969694", async() => {
                WriteLiteral(@"
    <div class=""littleGap""></div>

    <div class=""infobox-container"">
        <div class=""infobox2-child"">
            <p>
                <div class=""alignCenter""><text class=""boldText"">Team Total</text> ranks the rosters by the owners that hold the most trade value.<br /></div>
                <div class=""alignCenter""><text class=""boldText"">Best Starting Lineup</text> ranks the teams based on the value of the best possible starting lineup.<br /></div>
                <div class=""alignCenter"">
                    All values are based on 12 team 0.5 PPR
");
#nullable restore
#line 125 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                     if (Model.UserInfo.SuperFlex == true)
                    {
                        

#line default
#line hidden
#nullable disable
                WriteLiteral(" Superflex ");
#nullable restore
#line 127 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                
                    }

#line default
#line hidden
#nullable disable
                WriteLiteral("leagues, determined at <a class=\"greenA\" href=\"https://keeptradecut.com/\">KeepTradeCut.com</a>\r\n                </div>\n                <div class=\"alignCenter\"><text class=\"boldText\">Rankings Last Updated: </text>");
#nullable restore
#line 130 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                                                         Write(Model.LastScrapeDate);

#line default
#line hidden
#nullable disable
                WriteLiteral("<br /></div>\n                <div class=\"legend\">&#11088; : Starters<br />&#128170; : Best Flex Options</div>         \n            </p>\n        </div>\n    </div>\n\n    <div class=\"tinyGap\"></div>\n    <div class=\"rostersParent\">\n");
#nullable restore
#line 138 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
          
            int topFive = 0;
        

#line default
#line hidden
#nullable disable
                WriteLiteral("        <div id=\"roster\">\n            <div class=\"position-rankings\">Top Available QBs:</div>\n");
#nullable restore
#line 143 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             if (!Model.TopWaiverPlayers.Where(o => o.PORPosition == "QB").Any())
            {
                topFive = 0;
                

#line default
#line hidden
#nullable disable
                WriteLiteral("No ranked players at this position.");
#nullable restore
#line 146 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                                
            }
            else
            {
                topFive = 0;
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 152 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             foreach (var player in Model.TopWaiverPlayers.Where(o => o.PORPosition == "QB").OrderByDescending(o => o.PORValue))
            {
                if (topFive < 5)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 156 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
               Write(player.PORName);

#line default
#line hidden
#nullable disable
                WriteLiteral(": ");
#nullable restore
#line 157 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
               Write(player.PORValue);

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <br />\n");
#nullable restore
#line 159 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                    topFive++;
                }
                else
                {
                    topFive = 0;
                    break;
                }
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </div>\n\n        <div id=\"roster\">\n            <div class=\"position-rankings\">Top Available RBs:</div>\n");
#nullable restore
#line 171 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             if (!Model.TopWaiverPlayers.Where(o => o.PORPosition == "RB").Any())
            {
                topFive = 0;
                

#line default
#line hidden
#nullable disable
                WriteLiteral("No ranked players at this position.");
#nullable restore
#line 174 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                                
            }
            else
            {
                topFive = 0;
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 180 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             foreach (var player in Model.TopWaiverPlayers.Where(o => o.PORPosition == "RB").OrderByDescending(o => o.PORValue))
            {
                if (topFive < 5)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 184 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
               Write(player.PORName);

#line default
#line hidden
#nullable disable
                WriteLiteral(": ");
#nullable restore
#line 185 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
               Write(player.PORValue);

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <br />\n");
#nullable restore
#line 187 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                    topFive++;
                }
                else
                {
                    topFive = 0;
                    break;
                }
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </div>\n\n        <div id=\"roster\">\n            <div class=\"position-rankings\">Top Available WRs:</div>\n");
#nullable restore
#line 199 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             if (!Model.TopWaiverPlayers.Where(o => o.PORPosition == "WR").Any())
            {
                topFive = 0;
                

#line default
#line hidden
#nullable disable
                WriteLiteral("No ranked players at this position.");
#nullable restore
#line 202 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                                
            }
            else
            {
                topFive = 0;
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 208 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             foreach (var player in Model.TopWaiverPlayers.Where(o => o.PORPosition == "WR").OrderByDescending(o => o.PORValue))
            {
                if (topFive < 5)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 212 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
               Write(player.PORName);

#line default
#line hidden
#nullable disable
                WriteLiteral(": ");
#nullable restore
#line 213 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
               Write(player.PORValue);

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <br />\n");
#nullable restore
#line 215 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                    topFive++;
                }
                else
                {
                    topFive = 0;
                    break;
                }
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </div>\n\n        <div id=\"roster\">\n            <div class=\"position-rankings\">Top Available TEs:</div>\n");
#nullable restore
#line 227 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             if (!Model.TopWaiverPlayers.Where(o => o.PORPosition == "TE").Any())
            {
                topFive = 0;
                

#line default
#line hidden
#nullable disable
                WriteLiteral("No ranked players at this position.");
#nullable restore
#line 230 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                                
            }
            else
            {
                topFive = 0;
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 236 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             foreach (var player in Model.TopWaiverPlayers.Where(o => o.PORPosition == "TE").OrderByDescending(o => o.PORValue))
            {
                if (topFive < 5)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 240 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
               Write(player.PORName);

#line default
#line hidden
#nullable disable
                WriteLiteral(": ");
#nullable restore
#line 241 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
               Write(player.PORValue);

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <br />\n");
#nullable restore
#line 243 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                    topFive++;
                }
                else
                {
                    topFive = 0;
                    break;
                }
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </div>\n    </div>\n    <div class=\"tinyGap\"></div>\n    <div class=\"rostersParent\">\n");
#nullable restore
#line 255 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
         foreach (var roster in Model.Rosters)
        {

#line default
#line hidden
#nullable disable
                WriteLiteral("        <div id=\"roster\">\n            <div id=rosterBoldCenter>");
#nullable restore
#line 258 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                Write(rankingsDict[count]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" : ");
#nullable restore
#line 258 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                       Write(roster.DisplayName);

#line default
#line hidden
#nullable disable
                WriteLiteral("<br /></div>\n\n            <div id=rosterBoldCenter>Team Total: ");
#nullable restore
#line 260 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                            Write(roster.TeamRankingAverage);

#line default
#line hidden
#nullable disable
                WriteLiteral("</div><br />\n            <div id=rosterBoldCenter>Best Starting Lineup:<br />");
#nullable restore
#line 261 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                           Write(rankingsDict[roster.StartingTeamRank]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <text>-</text> ");
#nullable restore
#line 261 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                                                                                 Write(roster.TeamStartingTotal);

#line default
#line hidden
#nullable disable
                WriteLiteral("</div><br />\n\n            <div class=\"position-rankings\">QB Ranking: ");
#nullable restore
#line 263 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                  Write(rankingsDict[roster.QBRank]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <text> - </text>");
#nullable restore
#line 263 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                                                               Write(roster.QBRankingAverage);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </div>\n");
#nullable restore
#line 264 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             foreach (var player in roster.PlayersOnRoster.Where(o => o.Value.PORPosition == "QB").OrderByDescending(o => o.Value.PORValue))
            {
                if (roster.StartingPlayerList.Contains(player.Value.PORName))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <span>&#11088;</span>\n");
#nullable restore
#line 269 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                }
                if (roster.StartingFlexList.Contains(player.Value.PORName))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <span>&#128170;</span>\n");
#nullable restore
#line 273 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                }
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 274 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
           Write(player.Value.PORName);

#line default
#line hidden
#nullable disable
                WriteLiteral(": ");
#nullable restore
#line 275 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
           Write(player.Value.PORValue);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <br />\n");
#nullable restore
#line 276 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("            <br />\n            <div class=\"position-rankings\">RB Ranking: ");
#nullable restore
#line 278 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                  Write(rankingsDict[roster.RBRank]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <text> - </text>");
#nullable restore
#line 278 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                                                               Write(roster.RBRankingAverage);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </div>\n");
#nullable restore
#line 279 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             foreach (var player in roster.PlayersOnRoster.Where(o => o.Value.PORPosition == "RB").OrderByDescending(o => o.Value.PORValue))
            {
                if (roster.StartingPlayerList.Contains(player.Value.PORName))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <span>&#11088;</span>\n");
#nullable restore
#line 284 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                }
                if (roster.StartingFlexList.Contains(player.Value.PORName))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <span>&#128170;</span>\n");
#nullable restore
#line 288 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                }
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 289 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
           Write(player.Value.PORName);

#line default
#line hidden
#nullable disable
                WriteLiteral(": ");
#nullable restore
#line 290 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
           Write(player.Value.PORValue);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <br />\n");
#nullable restore
#line 291 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("            <br />\n            <div class=\"position-rankings\">WR Ranking: ");
#nullable restore
#line 293 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                  Write(rankingsDict[roster.WRRank]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <text> - </text>");
#nullable restore
#line 293 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                                                               Write(roster.WRRankingAverage);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </div>\n");
#nullable restore
#line 294 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             foreach (var player in roster.PlayersOnRoster.Where(o => o.Value.PORPosition == "WR").OrderByDescending(o => o.Value.PORValue))
            {
                if (roster.StartingPlayerList.Contains(player.Value.PORName))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <span>&#11088;</span>\n");
#nullable restore
#line 299 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                }
                if (roster.StartingFlexList.Contains(player.Value.PORName))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <span>&#128170;</span>\n");
#nullable restore
#line 303 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                }
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 304 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
           Write(player.Value.PORName);

#line default
#line hidden
#nullable disable
                WriteLiteral(": ");
#nullable restore
#line 305 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
           Write(player.Value.PORValue);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <br />\n");
#nullable restore
#line 306 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("            <br />\n            <div class=\"position-rankings\">TE Ranking: ");
#nullable restore
#line 308 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                  Write(rankingsDict[roster.TERank]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <text> - </text>");
#nullable restore
#line 308 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                                                               Write(roster.TERankingAverage);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </div>\n");
#nullable restore
#line 309 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             foreach (var player in roster.PlayersOnRoster.Where(o => o.Value.PORPosition == "TE").OrderByDescending(o => o.Value.PORValue))
            {
                if (roster.StartingPlayerList.Contains(player.Value.PORName))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <span>&#11088;</span>\n");
#nullable restore
#line 314 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                }
                if (roster.StartingFlexList.Contains(player.Value.PORName))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <span>&#128170;</span>\n");
#nullable restore
#line 318 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                }
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 319 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
           Write(player.Value.PORName);

#line default
#line hidden
#nullable disable
                WriteLiteral(": ");
#nullable restore
#line 320 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
           Write(player.Value.PORValue);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <br />\n");
#nullable restore
#line 321 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("            <br />\n");
#nullable restore
#line 323 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             if (Model.IncludeDraftCapital == true)
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                <div class=\"position-rankings\">Draft Capital: ");
#nullable restore
#line 325 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                         Write(roster.TotalDraftCapital);

#line default
#line hidden
#nullable disable
                WriteLiteral("</div>\n");
#nullable restore
#line 326 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                foreach (var pick in roster.DraftPicks.OrderByDescending(o => Int32.Parse(Model.DraftPickRankings[o])))
                {
                    if (pick.Contains("2021"))
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 330 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                   Write(pick);

#line default
#line hidden
#nullable disable
                WriteLiteral(": ");
#nullable restore
#line 331 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                   Write(Model.DraftPickRankings[pick]);

#line default
#line hidden
#nullable disable
                WriteLiteral("<br />\n");
#nullable restore
#line 332 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                    }
                }
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 335 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
              
                count++;
            

#line default
#line hidden
#nullable disable
                WriteLiteral("        </div>\n");
#nullable restore
#line 339 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
        }

#line default
#line hidden
#nullable disable
                WriteLiteral("    </div>\n        \n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DynastyRanker.ViewModels.DisplayLeagueViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
