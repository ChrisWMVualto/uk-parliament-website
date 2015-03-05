using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elmah;
using Ninject.Activation;
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
                if(filterContext.ActionDescriptor.GetCustomAttributes(typeof(SkipBrowserFilterAttribute), false).Any()) return;
                if(filterContext.RequestContext.HttpContext.Request.UrlReferrer != null)
                {
                    if (filterContext.RequestContext.HttpContext.Request.UrlReferrer.ToString().ToLower().Contains("parliament.uk"))
                    {
                        HttpContext.Current.Response.Redirect("/", true);
                        //filterContext.Result = new RedirectToRouteResult(MVC.Home.Commons().GetRouteValueDictionary());
                        return;
                    }
                }

                if (!IsSupported())
                {
                    filterContext.Result = NotSuppotedRouteResult();
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        public static bool IsSupported()
        {
            var supported = true;

            try
            {
                var ua = Parser.GetDefault().Parse(HttpContext.Current.Request.UserAgent);

                if(ua.OS.Family == "Linux")
                {
                    if (BrowserNotSupported("Opera", 10, ua.UserAgent))
                        supported = false;
                }
                else
                {
                    if(BrowserNotSupported("Opera", 23, ua.UserAgent))
                        supported = false;
                }

                if(BrowserNotSupported("Firefox", 30, ua.UserAgent) || BrowserNotSupported("Safari", 5, ua.UserAgent) || BrowserNotSupported("IE", 9, ua.UserAgent) || BrowserNotSupported("Chrome", 0, ua.UserAgent) || BrowserNotSupported("Chrome Mobile iOS", 0, ua.UserAgent) || BrowserNotSupported("Android", null, ua.UserAgent))
                {
                    ErrorSignal.FromCurrentContext().Raise(new Exception("BrowserNotSupported: " + HttpContext.Current.Request.UserAgent));
                    supported = false;
                }
            }
            catch(Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return supported;
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