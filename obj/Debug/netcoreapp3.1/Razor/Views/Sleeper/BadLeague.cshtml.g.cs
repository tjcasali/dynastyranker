#pragma checksum "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\BadLeague.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bdd865facb73288c24082dc75e98efa739780989"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sleeper_BadLeague), @"mvc.1.0.view", @"/Views/Sleeper/BadLeague.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bdd865facb73288c24082dc75e98efa739780989", @"/Views/Sleeper/BadLeague.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5a9530e6195efcf2b269f4598e81ab155c1830ef", @"/Views/_ViewImports.cshtml")]
    public class Views_Sleeper_BadLeague : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DynastyRanker.Models.UserInfo>
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "bdd865facb73288c24082dc75e98efa7397809894312", async() => {
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bdd865facb73288c24082dc75e98efa7397809895426", async() => {
                WriteLiteral(@"

    <div class=""homePage"">

        <div class=""littleGap""></div>

        <div class=""infobox-container"">
            <div class=""infobox-child"">
                <h1>Unsupported League!</h1><br />
                <p>
                    This league either has settings that we can't support or contains invalid teams. Try again with a different league.
                </p>

            </div>
        </div>

        <div class=""littleGap""></div>

        <div class=""sleeperIDBiggerPadding"">
            <img alt=""Sleeper Logo"" class=""sleeperMFLLogo""");
                BeginWriteAttribute("src", " src=\"", 682, "\"", 741, 1);
#nullable restore
#line 24 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\BadLeague.cshtml"
WriteAttributeValue("", 688, Url.Content("~/Images/sleeperlogo_without_text.png"), 688, 53, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\n            <h1>Enter your Sleeper User Name.</h1><br />\n\n");
#nullable restore
#line 27 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\BadLeague.cshtml"
             using(Html.BeginForm("SelectLeague", "Sleeper"))
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\BadLeague.cshtml"
           Write(Html.TextBoxFor(m => m.UserName, new { @class = "sleeperIDField" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("                <button type=\"submit\" class=\"btn btn-info\">Submit</button>\n");
#nullable restore
#line 31 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\BadLeague.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("        </div>\n\n        <div class=\"tinyGap\"></div>\n        <div class=\"alignCenter\">- OR -</div>\n        <div class=\"tinyGap\"></div>\n\n        <div class=\"sleeperID\">\n            <img alt=\"Sleeper Logo\" class=\"sleeperMFLLogo\"");
                BeginWriteAttribute("src", " src=\"", 1277, "\"", 1336, 1);
#nullable restore
#line 39 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\BadLeague.cshtml"
WriteAttributeValue("", 1283, Url.Content("~/Images/sleeperlogo_without_text.png"), 1283, 53, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\n            <h1>Enter your Sleeper League ID.</h1><br />\n            <div class=\"container\">\n");
#nullable restore
#line 42 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\BadLeague.cshtml"
                 using (Html.BeginForm("DisplayLeague", "Sleeper"))
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 44 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\BadLeague.cshtml"
               Write(Html.TextBoxFor(m => m.LeagueID, new { @class = "sleeperIDField" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <button type=\"submit\" class=\"btn btn-info\">Submit</button>            \n");
#nullable restore
#line 46 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\BadLeague.cshtml"
                }

#line default
#line hidden
#nullable disable
                WriteLiteral("            </div>\n            <img alt=\"Sleeper ID\"");
                BeginWriteAttribute("src", " src=\"", 1768, "\"", 1818, 1);
#nullable restore
#line 48 "E:\projects\DynastyRanker\DynastyRanker\Views\Sleeper\BadLeague.cshtml"
WriteAttributeValue("", 1774, Url.Content("~/Images/SleeperLeagueID.png"), 1774, 44, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"hideIf600\">\n        </div>\n\n    </div>\n");
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
