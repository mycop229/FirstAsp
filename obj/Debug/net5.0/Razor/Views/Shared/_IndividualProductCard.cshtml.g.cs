#pragma checksum "C:\Users\volko\source\repos\Tor\Views\Shared\_IndividualProductCard.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "61f4d5cfdd7fc234a36ccc3475bc6efd2db893b9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__IndividualProductCard), @"mvc.1.0.view", @"/Views/Shared/_IndividualProductCard.cshtml")]
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
#line 2 "C:\Users\volko\source\repos\Tor\Views\_ViewImports.cshtml"
using Tor.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\volko\source\repos\Tor\Views\Shared\_IndividualProductCard.cshtml"
using Tor;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"61f4d5cfdd7fc234a36ccc3475bc6efd2db893b9", @"/Views/Shared/_IndividualProductCard.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"defe9ca011afdb7d2429f4c44ccead177f7e5591", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__IndividualProductCard : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Tor.Models.Product>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("target", new global::Microsoft.AspNetCore.Html.HtmlString("_blank"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/jquery/dist/jquery.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<link rel=\"stylesheet\"  href=\"https://cdn.jsdelivr.net/npm/summernote@0.8.18/dist/summernote.min.css\">\r\n\r\n<link rel=\"stylesheet\" href=\"/css/productcard.css\" />\r\n\r\n<div");
            BeginWriteAttribute("class", " class =\"", 210, "\"", 304, 7);
            WriteAttributeValue("", 219, "filter", 219, 6, true);
#nullable restore
#line 9 "C:\Users\volko\source\repos\Tor\Views\Shared\_IndividualProductCard.cshtml"
WriteAttributeValue(" ", 225, Model.Category.Name.Replace(' ','_'), 226, 37, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 263, "col-lg-3", 264, 9, true);
            WriteAttributeValue(" ", 272, "col-md-6", 273, 9, true);
            WriteAttributeValue(" ", 281, "col-sm-6", 282, 9, true);
            WriteAttributeValue(" ", 290, "col-xs-6", 291, 9, true);
            WriteAttributeValue(" ", 299, "pb-4", 300, 5, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n\t<div class=\"bg-white \">\r\n\t\t\t\r\n\r\n\t\t\r\n\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "61f4d5cfdd7fc234a36ccc3475bc6efd2db893b95673", async() => {
                WriteLiteral("\r\n\t\t<img class=\"card-img-top img-fluid d-block mx-auto mb-2\"");
                BeginWriteAttribute("src", " src=\"", 494, "\"", 525, 2);
#nullable restore
#line 15 "C:\Users\volko\source\repos\Tor\Views\Shared\_IndividualProductCard.cshtml"
WriteAttributeValue("", 500, WC.ImagePath, 500, 13, false);

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\volko\source\repos\Tor\Views\Shared\_IndividualProductCard.cshtml"
WriteAttributeValue("", 513, Model.Image, 513, 12, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" alt=\"Card image cap\"/>\r\n\t\t\r\n\r\n\r\n\r\n\t\t<div class=\"card-body p-1 px-3 row\">\r\n\r\n\t\t\t<div class=\"col-12 brand\"><label><span class=\"h7\" style=\"color:black; margin-bottom: -15px;\">");
#nullable restore
#line 22 "C:\Users\volko\source\repos\Tor\Views\Shared\_IndividualProductCard.cshtml"
                                                                                                    Write(Model.Brand);

#line default
#line hidden
#nullable disable
                WriteLiteral("</span></label></div>\r\n\t\t\t<div class=\"col-12 desc\"><label class=\"align-items-center\">");
#nullable restore
#line 23 "C:\Users\volko\source\repos\Tor\Views\Shared\_IndividualProductCard.cshtml"
                                                                  Write(Model.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</label></div>\r\n\t\t\t<div class=\"col-12 desc\"><label><span class=\"h7\" style=\"color:gray;\">");
#nullable restore
#line 24 "C:\Users\volko\source\repos\Tor\Views\Shared\_IndividualProductCard.cshtml"
                                                                            Write(Model.Category.Translate);

#line default
#line hidden
#nullable disable
                WriteLiteral(" ");
#nullable restore
#line 24 "C:\Users\volko\source\repos\Tor\Views\Shared\_IndividualProductCard.cshtml"
                                                                                                      Write(Model.ApplicationType.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</span></label></div>\r\n\t\t\t<div class=\"col-12 price\"><label><span class=\"h7\" >");
#nullable restore
#line 25 "C:\Users\volko\source\repos\Tor\Views\Shared\_IndividualProductCard.cshtml"
                                                          Write(Model.Price.ToString("c0"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</span></label></div>\r\n\t\t</div>\r\n\t\t");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 14 "C:\Users\volko\source\repos\Tor\Views\Shared\_IndividualProductCard.cshtml"
                                                                        WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\t</div>\r\n</div>\r\n\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "61f4d5cfdd7fc234a36ccc3475bc6efd2db893b910842", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Tor.Models.Product> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
