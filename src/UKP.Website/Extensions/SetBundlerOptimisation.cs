﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elmah;
using RestSharp.Extensions;
using UAParser;

namespace UKP.Website.Extensions
{
    using System.Web.Optimization;

    public class SetBundlerOptimisation : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var ua = Parser.GetDefault().Parse(HttpContext.Current.Request.UserAgent);

                if (BrowserIs("IE", 9, ua.UserAgent))
                    BundleTable.EnableOptimizations = false;

                else
                    BundleTable.EnableOptimizations = true;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        private static bool BrowserIs(string browserName, int version, UserAgent userAgent)
        {
            return userAgent.Family.ToLower() == browserName.ToLower() && int.Parse(userAgent.Major) == version;
        }
    }
}