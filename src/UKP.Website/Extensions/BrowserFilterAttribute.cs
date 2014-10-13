using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp.Extensions;
using UAParser;

namespace UKP.Website.Extensions
{
    public class BrowserFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ua = Parser.GetDefault().Parse(HttpContext.Current.Request.UserAgent);
            var browser = ua.UserAgent;

            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof (SkipBrowserFilterAttribute), false).Any())
            {
                return;
            }

            if (ua.OS.Family == "Linux")
            {
                if (BrowserSupported("Opera", 10))
                    return;
            }
            else
            {
                if (BrowserSupported("Opera", 23))
                    return;
            }

            if (BrowserSupported("Firefox", 31) || BrowserSupported("Safari", 6) || BrowserSupported("IE", 9))
            {
                return;
            }

            filterContext.Result = new RedirectToRouteResult(MVC.NotSupported.Index().GetRouteValueDictionary());
        }

        private static bool BrowserSupported(string browserName, int minVersion, string operatingSystem = null)
        {
            var ua = Parser.GetDefault().Parse(HttpContext.Current.Request.UserAgent);
            var browser = ua.UserAgent;

            return browser.Family == browserName && Int32.Parse(browser.Major) >= minVersion;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }

    public class SkipBrowserFilterAttribute : Attribute
    {
    }
}