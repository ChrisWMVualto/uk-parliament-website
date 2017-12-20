using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog.Internal;
using UKP.Website.Application;
using System.Configuration;
using Microsoft.Ajax.Utilities;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace UKP.Website.Extensions
{
    public static class ConfigurationHelper
    {

        public static MvcHtmlString GetValue(this HtmlHelper helper, string value, string @default = "",
            bool exceptionOnEmpty = false)
        {
            var configValue = String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[value]) ? @default : ConfigurationManager.AppSettings[value];

            if (exceptionOnEmpty && String.IsNullOrWhiteSpace(configValue))
            {
                throw new Exception("Invalid config value");
            }

            return new MvcHtmlString(configValue);
        }
    }
}