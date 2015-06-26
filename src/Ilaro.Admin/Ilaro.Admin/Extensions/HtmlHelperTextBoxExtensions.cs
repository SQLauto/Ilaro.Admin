﻿using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Ilaro.Admin.ViewModels;

namespace Ilaro.Admin.Extensions
{
    /// <summary>
    /// Own TextBox extensions, thanks that we create own metadata and 
    /// based on we get unobtrusive validation attibute and pass 
    /// this attributes to mvc TextBox
    /// </summary>
    public static class HtmlHelperTextBoxExtensions
    {
        public static MvcHtmlString TextBox(
            this HtmlHelper htmlHelper,
            string name,
            object value,
            Property property)
        {
            return TextBox(
                htmlHelper,
                name,
                value,
                property,
                htmlAttributes: (IDictionary<string, object>)null);
        }

        public static MvcHtmlString TextBox(
            this HtmlHelper htmlHelper,
            string name,
            object value,
            Property property,
            object htmlAttributes)
        {
            return TextBox(
                htmlHelper,
                name,
                value,
                property,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString TextBox(
            this HtmlHelper htmlHelper,
            string name,
            object value,
            Property property,
            IDictionary<string, object> htmlAttributes)
        {
            // create own metadata based on PropertyViewModel
            var metadata = new ModelMetadata(
                ModelMetadataProviders.Current,
                property.Entity.Type,
                null,
                property.PropertyType,
                property.Name);
            var validationAttributes = htmlHelper
                .GetUnobtrusiveValidationAttributes(name, metadata);

            htmlAttributes = htmlAttributes.Merge(validationAttributes);

            return htmlHelper.TextBox(name, value, htmlAttributes);
        }
    }
}