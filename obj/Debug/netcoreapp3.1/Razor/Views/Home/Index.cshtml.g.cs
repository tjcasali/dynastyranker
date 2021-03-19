#pragma checksum "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8bba4b4ad3acfbd3ee97c88e82018150f0c276fd"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8bba4b4ad3acfbd3ee97c88e82018150f0c276fd", @"/Views/Home/Index.cshtml")]
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
            WriteLiteral("<!--<link href=\"~/StyleSheet.css\" rel=\"stylesheet\" />-->\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "8bba4b4ad3acfbd3ee97c88e82018150f0c276fd4337", async() => {
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
            WriteLiteral("\n\n\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8bba4b4ad3acfbd3ee97c88e82018150f0c276fd5453", async() => {
                WriteLiteral("\n\n    <div class=\"homePage\">\n\n        <div class=\"littleGap\"></div>\n\n        <div class=\"infobox-container\">\n\n            <div class=\"infobox-child\">\n");
                WriteLiteral(@"                <div class=""subtitle"">Use the trade value chart at <a class=""biggerA"" href=""https://keeptradecut.com/"">KeepTradeCut.com</a> to rank the teams in your Dynasty league. </div><br />
                <p>
                    KeepTradeCut created an incredible system to determine player trade values based on crowdsourced data. Here at DynastyRanker you can rank
                    the teams in your league using the data at KeepTradeCut and browse through your leagues position rankings to find trades that can benefit
                    both owners.
                </p>
            </div>
        </div>

        <div class=""littleGap""></div>

        <div class=""sleeperIDBiggerPadding"">
            <h1>Enter your Sleeper User Name.</h1><br />

");
#nullable restore
#line 30 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
             using (Html.BeginForm("SelectLeague", "Sleeper"))
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 32 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
           Write(Html.TextBoxFor(m => m.UserName, new { @class = "sleeperIDField" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("                <button type=\"submit\" class=\"btn btn-info\">Submit</button>            \n");
#nullable restore
#line 34 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </div>\n\n        <div class=\"tinyGap\"></div>\n        <div class=\"alignCenter\">- OR -</div>\n        <div class=\"tinyGap\"></div>\n\n        <div class=\"sleeperID\">\n            <h1>Enter your Sleeper League ID.</h1><br />\n\n");
#nullable restore
#line 44 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
             using (Html.BeginForm("DisplayLeague", "Sleeper"))
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 46 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
           Write(Html.TextBoxFor(m => m.LeagueID, new { @class = "sleeperIDField" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("                <button type=\"submit\" class=\"btn btn-info\">Submit</button>            \n");
#nullable restore
#line 48 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("            <img alt=\"Sleeper ID\"");
                BeginWriteAttribute("src", " src=\"", 1940, "\"", 1983, 1);
#nullable restore
#line 49 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
WriteAttributeValue("", 1946, Url.Content("~/SleeperLeagueID.png"), 1946, 37, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"hideIfSmol\">\n        </div>\n\n        <div class=\"littleGap\"></div>\n\n        <img alt=\"Dynasty Ranker\"");
                BeginWriteAttribute("src", " src=\"", 2093, "\"", 2132, 1);
#nullable restore
#line 54 "E:\projects\DynastyRanker\DynastyRanker\Views\Home\Index.cshtml"
WriteAttributeValue("", 2099, Url.Content("~/DRHomepage6.png"), 2099, 33, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"hideIfSmol\">\n    </div>\n");
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