using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elmah;
using RestSharp.Extensions;
using UAParser;
using System.Web.Optimization;

namespace UKP.Website.Extensions
{
    public class BrowserFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var ua = Parser.GetDefault().Parse(HttpContext.Current.Request.UserAgent);

                if(filterContext.ActionDescriptor.GetCustomAttributes(typeof(SkipBrowserFilterAttribute), false).Any()) return;

                if(ua.OS.Family == "Linux")
                {
                    if(BrowserNotSupported("Opera", 10, ua.UserAgent))
                        filterContext.Result = NotSuppotedRouteResult();
                }
                else
                {
                    if(BrowserNotSupported("Opera", 23, ua.UserAgent))
                        filterContext.Result = NotSuppotedRouteResult();
                }

                if (BrowserNotSupported("Firefox", 30, ua.UserAgent) || BrowserNotSupported("Safari", 5, ua.UserAgent) || BrowserNotSupported("IE", 9, ua.UserAgent) || BrowserNotSupported("Chrome", 0, ua.UserAgent) || BrowserNotSupported("Chrome Mobile iOS", 0, ua.UserAgent) || BrowserNotSupported("Android", null, ua.UserAgent))
                {
                    ErrorSignal.FromCurrentContext().Raise(new Exception("BrowserNotSupported: " + HttpContext.Current.Request.UserAgent));
                    filterContext.Result = NotSuppotedRouteResult();
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        private static bool BrowserNotSupported(string browserName, int? minVersion, UserAgent userAgent)
        {
            return userAgent.Family.ToLower() == browserName.ToLower() && (int.Parse(userAgent.Major) < minVersion || minVersion == null);
        }

        private static ActionResult NotSuppotedRouteResult()
        {
            return new RedirectToRouteResult(MVC.NotSupported.Index().GetRouteValueDictionary());
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }

    public class SkipBrowserFilterAttribute : Attribute
    {
    }
}