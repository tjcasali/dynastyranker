#pragma checksum "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "36714fe94666f32089df55b9b6f1799dc43a3b93"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"36714fe94666f32089df55b9b6f1799dc43a3b93", @"/Views/Sleeper/DisplayLeague.cshtml")]
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "36714fe94666f32089df55b9b6f1799dc43a3b934335", async() => {
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
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36714fe94666f32089df55b9b6f1799dc43a3b936624", async() => {
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
#line 47 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                     if (Model.UserInfo.SuperFlex == true)
                    {
                        

#line default
#line hidden
#nullable disable
                WriteLiteral(" Superflex ");
#nullable restore
#line 49 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                
                    }

#line default
#line hidden
#nullable disable
                WriteLiteral("leagues, determined at <a href=\"https://keeptradecut.com/\">KeepTradeCut.com</a>\n                </div>\n                <div class=\"alignCenter\"><text class=\"boldText\">Rankings Last Updated: </text>");
#nullable restore
#line 52 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                                                         Write(Model.LastScrapeDate);

#line default
#line hidden
#nullable disable
                WriteLiteral("<br /></div>\n                <div class=\"legend\">&#11088; : Starters<br />&#128170; : Best Flex Options</div>         \n            </p>\n        </div>\n    </div>\n\n    <div class=\"littleGap\"></div>\n\n    <div class=\"rostersParent\">\n");
#nullable restore
#line 61 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
         foreach (var roster in Model.Rosters)
        {

#line default
#line hidden
#nullable disable
                WriteLiteral("        <div id=\"roster\">\n            <div id=rosterBoldCenter>");
#nullable restore
#line 64 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                Write(rankingsDict[count]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" : ");
#nullable restore
#line 64 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                       Write(roster.DisplayName);

#line default
#line hidden
#nullable disable
                WriteLiteral("<br /></div>\n\n            <div id=rosterBoldCenter>Team Total: ");
#nullable restore
#line 66 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                            Write(roster.TeamRankingAverage);

#line default
#line hidden
#nullable disable
                WriteLiteral("</div><br />\n            <div id=rosterBoldCenter>Best Starting Lineup:<br />");
#nullable restore
#line 67 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                           Write(rankingsDict[roster.StartingTeamRank]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <text>-</text> ");
#nullable restore
#line 67 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                                                                                 Write(roster.TeamStartingTotal);

#line default
#line hidden
#nullable disable
                WriteLiteral("</div><br />\n\n            <div class=\"position-rankings\">QB Ranking: ");
#nullable restore
#line 69 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                  Write(rankingsDict[roster.QBRank]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <text> - </text>");
#nullable restore
#line 69 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                                                               Write(roster.QBRankingAverage);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </div>\n");
#nullable restore
#line 70 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             foreach (var player in roster.PlayersOnRoster.Where(o => o.Value.PORPosition == "QB").OrderByDescending(o => o.Value.PORValue))
            {
                if (roster.StartingPlayerList.Contains(player.Value.PORName))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <span>&#11088;</span>\n");
#nullable restore
#line 75 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                }
                if (roster.StartingFlexList.Contains(player.Value.PORName))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <span>&#128170;</span>\n");
#nullable restore
#line 79 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                }
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 80 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
           Write(player.Value.PORName);

#line default
#line hidden
#nullable disable
                WriteLiteral(": ");
#nullable restore
#line 81 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
           Write(player.Value.PORValue);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <br />\n");
#nullable restore
#line 82 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("            <br />\n            <div class=\"position-rankings\">RB Ranking: ");
#nullable restore
#line 84 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                  Write(rankingsDict[roster.RBRank]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <text> - </text>");
#nullable restore
#line 84 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                                                               Write(roster.RBRankingAverage);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </div>\n");
#nullable restore
#line 85 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             foreach (var player in roster.PlayersOnRoster.Where(o => o.Value.PORPosition == "RB").OrderByDescending(o => o.Value.PORValue))
            {
                if (roster.StartingPlayerList.Contains(player.Value.PORName))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <span>&#11088;</span>\n");
#nullable restore
#line 90 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                }
                if (roster.StartingFlexList.Contains(player.Value.PORName))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <span>&#128170;</span>\n");
#nullable restore
#line 94 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                }
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 95 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
           Write(player.Value.PORName);

#line default
#line hidden
#nullable disable
                WriteLiteral(": ");
#nullable restore
#line 96 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
           Write(player.Value.PORValue);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <br />\n");
#nullable restore
#line 97 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("            <br />\n            <div class=\"position-rankings\">WR Ranking: ");
#nullable restore
#line 99 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                  Write(rankingsDict[roster.WRRank]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <text> - </text>");
#nullable restore
#line 99 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                                                               Write(roster.WRRankingAverage);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </div>\n");
#nullable restore
#line 100 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             foreach (var player in roster.PlayersOnRoster.Where(o => o.Value.PORPosition == "WR").OrderByDescending(o => o.Value.PORValue))
            {
                if (roster.StartingPlayerList.Contains(player.Value.PORName))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <span>&#11088;</span>\n");
#nullable restore
#line 105 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                }
                if (roster.StartingFlexList.Contains(player.Value.PORName))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <span>&#128170;</span>\n");
#nullable restore
#line 109 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                }
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 110 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
           Write(player.Value.PORName);

#line default
#line hidden
#nullable disable
                WriteLiteral(": ");
#nullable restore
#line 111 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
           Write(player.Value.PORValue);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <br />\n");
#nullable restore
#line 112 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("            <br />\n            <div class=\"position-rankings\">TE Ranking: ");
#nullable restore
#line 114 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                  Write(rankingsDict[roster.TERank]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <text> - </text>");
#nullable restore
#line 114 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                                                               Write(roster.TERankingAverage);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </div>\n");
#nullable restore
#line 115 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             foreach (var player in roster.PlayersOnRoster.Where(o => o.Value.PORPosition == "TE").OrderByDescending(o => o.Value.PORValue))
            {
                if (roster.StartingPlayerList.Contains(player.Value.PORName))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <span>&#11088;</span>\n");
#nullable restore
#line 120 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                }
                if (roster.StartingFlexList.Contains(player.Value.PORName))
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <span>&#128170;</span>\n");
#nullable restore
#line 124 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                }
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 125 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
           Write(player.Value.PORName);

#line default
#line hidden
#nullable disable
                WriteLiteral(": ");
#nullable restore
#line 126 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
           Write(player.Value.PORValue);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <br />\n");
#nullable restore
#line 127 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("            <br />\n");
#nullable restore
#line 129 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
             if (Model.IncludeDraftCapital == true)
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                <div class=\"position-rankings\">Draft Capital: ");
#nullable restore
#line 131 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                                                         Write(roster.TotalDraftCapital);

#line default
#line hidden
#nullable disable
                WriteLiteral("</div>\n");
#nullable restore
#line 132 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                foreach (var pick in roster.DraftPicks.OrderByDescending(o => Int32.Parse(Model.DraftPickRankings[o])))
                {
                    if (pick.Contains("2021"))
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 136 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                   Write(pick);

#line default
#line hidden
#nullable disable
                WriteLiteral(": ");
#nullable restore
#line 137 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                   Write(Model.DraftPickRankings[pick]);

#line default
#line hidden
#nullable disable
                WriteLiteral("<br />\n");
#nullable restore
#line 138 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
                    }
                }
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 141 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
              
                count++;
            

#line default
#line hidden
#nullable disable
                WriteLiteral("        </div>\n");
#nullable restore
#line 145 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\DisplayLeague.cshtml"
        }

#line default
#line hidden
#nullable disable
                WriteLiteral("\n    </div>\n");
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