#pragma checksum "E:\Work\DesiClothing\DesiClothing4u\DesiClothing4u.UI\Views\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "467b7db20d7daf505b48b92bee5fc705902e0b7a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
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
#line 1 "E:\Work\DesiClothing\DesiClothing4u\DesiClothing4u.UI\Views\_ViewImports.cshtml"
using DesiClothing4u.UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Work\DesiClothing\DesiClothing4u\DesiClothing4u.UI\Views\_ViewImports.cshtml"
using DesiClothing4u.UI.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"467b7db20d7daf505b48b92bee5fc705902e0b7a", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e48719c01b05fdd779a20f756a2eb6aa1e279e1", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DesiClothing4u.Common.Models.Error>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<h2>\r\n\r\n\r\n");
#nullable restore
#line 5 "E:\Work\DesiClothing\DesiClothing4u\DesiClothing4u.UI\Views\Shared\Error.cshtml"
     if (Model != null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <i class=\"Welcome\">");
#nullable restore
#line 7 "E:\Work\DesiClothing\DesiClothing4u\DesiClothing4u.UI\Views\Shared\Error.cshtml"
                      Write(Model.ErrorMessage);

#line default
#line hidden
#nullable disable
            WriteLiteral("</i>\r\n");
#nullable restore
#line 8 "E:\Work\DesiClothing\DesiClothing4u\DesiClothing4u.UI\Views\Shared\Error.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</h2>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DesiClothing4u.Common.Models.Error> Html { get; private set; }
    }
}
#pragma warning restore 1591
