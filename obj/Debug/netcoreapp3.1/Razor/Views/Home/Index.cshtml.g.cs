#pragma checksum "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "723b07e5a285e19a06bdba08593488877096d996"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"723b07e5a285e19a06bdba08593488877096d996", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5a9530e6195efcf2b269f4598e81ab155c1830ef", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DynastyRanker.Models.UserInfo>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/site.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("background-color:#18202F;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "723b07e5a285e19a06bdba08593488877096d9964277", async() => {
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
            WriteLiteral("\n\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "723b07e5a285e19a06bdba08593488877096d9965391", async() => {
                WriteLiteral(@"

    <div class=""homePage"">

        <div class=""littleGap""></div>

        <div class=""infobox-container"">
            <div class=""infobox-child"">

                <h4>2022/2023 Draft Capital Added!</h4><br />
                <p>
                    Sleeper leagues should now see 2022/2023 draft pick values in their league breakdown. If you haven't done your rookie draft yet you'll still see
                    your 2021 pick values as well. Working on getting this added to MFL leagues now.
                    <br /><br />If your league no longer works properly, please email me the league ID so I can test!
                </p>
            </div>
        </div>

        <div class=""littleGap""></div>

        <div class=""infobox-container"">
            <div class=""infobox-child"">
                <div class=""subtitle"">Use the trade value chart at <a class=""biggerA greenA"" href=""https://keeptradecut.com/"">KeepTradeCut.com</a> to rank the teams in your Dynasty league. </div><br />
                <p>
           ");
                WriteLiteral(@"         KeepTradeCut created an incredible system to determine player trade values based on crowdsourced data. Here at DynastyRanker you can rank
                    the teams in your league using the data at KeepTradeCut and browse through your leagues position rankings to find trades that can benefit
                    both owners.
                </p>
            </div>
        </div>

        <div class=""littleGap""></div>

        <div class=""sleeperIDBiggerPadding"">
            <img alt=""Sleeper Logo"" class=""sleeperMFLLogo""");
                BeginWriteAttribute("src", " src=\"", 1687, "\"", 1746, 1);
#nullable restore
#line 39 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
WriteAttributeValue("", 1693, Url.Content("~/Images/sleeperlogo_without_text.png"), 1693, 53, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\n            <h1>Enter your Sleeper User Name.</h1><br />\n\n");
#nullable restore
#line 42 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
             using (Html.BeginForm("SelectLeague", "Sleeper"))
            {
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 44 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
Write(Html.TextBoxFor(m => m.UserName, new { @class = "sleeperIDField" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <button type=\"submit\" class=\"btn btn-info\">Submit</button>            ");
#nullable restore
#line 45 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
                                                                                          }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </div>\n\n        <div class=\"tinyGap\"></div>\n        <div class=\"alignCenter\">- OR -</div>\n        <div class=\"tinyGap\"></div>\n\n        <div class=\"sleeperID\">\n            <img alt=\"Sleeper Logo\" class=\"sleeperMFLLogo\"");
                BeginWriteAttribute("src", " src=\"", 2274, "\"", 2333, 1);
#nullable restore
#line 53 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
WriteAttributeValue("", 2280, Url.Content("~/Images/sleeperlogo_without_text.png"), 2280, 53, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\n            <h1>Enter your Sleeper League ID.</h1><br />\n            <div class=\"container\">\n");
#nullable restore
#line 56 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
                 using (Html.BeginForm("DisplayLeague", "Sleeper"))
                {
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 58 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
Write(Html.TextBoxFor(m => m.LeagueID, new { @class = "sleeperIDField" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <button type=\"submit\" class=\"btn btn-info\">Submit</button>");
#nullable restore
#line 59 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
                                                                              }

#line default
#line hidden
#nullable disable
                WriteLiteral("            </div>\n            <img alt=\"Sleeper ID\"");
                BeginWriteAttribute("src", " src=\"", 2720, "\"", 2770, 1);
#nullable restore
#line 61 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
WriteAttributeValue("", 2726, Url.Content("~/Images/SleeperLeagueID.png"), 2726, 44, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"hideIf600\">\n        </div>\n\n        <div class=\"tinyGap\"></div>\n        <div class=\"alignCenter\">- OR -</div>\n        <div class=\"tinyGap\"></div>\n\n        <div class=\"sleeperID\">\n            <img alt=\"Sleeper Logo\" class=\"sleeperMFLLogo\"");
                BeginWriteAttribute("src", " src=\"", 3016, "\"", 3058, 1);
#nullable restore
#line 69 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
WriteAttributeValue("", 3022, Url.Content("~/Images/mfl-cap.png"), 3022, 36, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\n            <h1>Enter your MyFantasyLeague League ID.</h1><br />\n\n");
#nullable restore
#line 72 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
             using (Html.BeginForm("MFLDisplayLeague", "MFL"))
            {
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 74 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
Write(Html.TextBoxFor(m => m.LeagueID, new { @class = "sleeperIDField" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <button type=\"submit\" class=\"btn btn-info\">Submit</button>            ");
#nullable restore
#line 75 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
                                                                                          }

#line default
#line hidden
#nullable disable
                WriteLiteral("            <img alt=\"Sleeper ID\"");
                BeginWriteAttribute("src", " src=\"", 3402, "\"", 3448, 1);
#nullable restore
#line 76 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
WriteAttributeValue("", 3408, Url.Content("~/Images/MFLLeagueID.png"), 3408, 40, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"hideIf600\">\n        </div>\n\n        <div class=\"littleGap\"></div>\n\n        <img alt=\"Dynasty Ranker\"");
                BeginWriteAttribute("src", " src=\"", 3557, "\"", 3603, 1);
#nullable restore
#line 81 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
WriteAttributeValue("", 3563, Url.Content("~/Images/DRHomepage6.png"), 3563, 40, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"hideIf1200\">\n    </div>\n");
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
            WriteLiteral("\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DynastyRanker.Models.UserInfo> Html { get; private set; }
    }
}
#pragma warning restore 1591
