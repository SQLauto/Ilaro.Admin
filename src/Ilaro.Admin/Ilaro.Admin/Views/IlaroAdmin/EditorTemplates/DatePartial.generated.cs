﻿using System;
using System.CodeDom.Compiler;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;
using Ilaro.Admin.Extensions;
using Ilaro.Admin.ViewModels;

#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ilaro.Admin.Views.IlaroAdmin.EditorTemplates
{
    [GeneratedCode("RazorGenerator", "2.0.0.0")]
    [PageVirtualPath("~/Views/IlaroAdmin/EditorTemplates/DatePartial.cshtml")]
    public partial class _DatePartial : WebViewPage<Property>
    {
        public _DatePartial()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\IlaroAdmin\EditorTemplates\DatePartial.cshtml"
  Html.ClearPrefix();
            
            #line default
            #line hidden
WriteLiteral("\r\n<label");

WriteAttribute("for", Tuple.Create(" for=\"", 72), Tuple.Create("\"", 98)
            
            #line 4 "..\..\Views\IlaroAdmin\EditorTemplates\DatePartial.cshtml"
, Tuple.Create(Tuple.Create("", 78), Tuple.Create<Object, Int32>(Html.Id(Model.Name)
            
            #line default
            #line hidden
, 78), false)
);

WriteLiteral(" class=\"control-label col-md-2\"");

WriteLiteral(">");

            
            #line 4 "..\..\Views\IlaroAdmin\EditorTemplates\DatePartial.cshtml"
                                                            Write(Model.DisplayName);

            
            #line default
            #line hidden
WriteLiteral(" ");

            
            #line 4 "..\..\Views\IlaroAdmin\EditorTemplates\DatePartial.cshtml"
                                                                               Write(Html.Condition(Model.IsRequired, () => "<span class=\"text-danger\">*</span>"));

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n<div");

WriteLiteral(" class=\"controls col-md-3\"");

WriteLiteral(">\r\n\t<div");

WriteLiteral(" class=\"input-group date date-picker\"");

WriteLiteral(" data-date-format=\"YYYY-MM-DD\"");

WriteLiteral(">\r\n");

WriteLiteral("\t\t");

            
            #line 7 "..\..\Views\IlaroAdmin\EditorTemplates\DatePartial.cshtml"
Write(Html.TextBox(Model.Name, Model.Value, Model, new { @class = "form-control" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n\t\t<span");

WriteLiteral(" class=\"input-group-addon\"");

WriteLiteral(">\r\n\t\t\t<span");

WriteLiteral(" class=\"glyphicon glyphicon-calendar\"");

WriteLiteral("></span>\r\n\t\t</span>\r\n\t</div>\r\n");

WriteLiteral("\t");

            
            #line 12 "..\..\Views\IlaroAdmin\EditorTemplates\DatePartial.cshtml"
Write(Html.Condition(!string.IsNullOrEmpty(Model.Description), () => "<span class=\"help-block\">" + Model.Description + "</span>"));

            
            #line default
            #line hidden
WriteLiteral("\r\n</div>\r\n");

            
            #line 14 "..\..\Views\IlaroAdmin\EditorTemplates\DatePartial.cshtml"
Write(Html.ValidationMessage(Model.Name));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
