using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Web.Optimization;
using UKP.Website.Extensions;

namespace UKP.Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new SetBundlerOptimisation());
            GlobalFilters.Filters.Add(new RequireHttpsAttribute());
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            if (!Request.IsLocal && !Request.IsSecureConnection)
            {
                string path = string.Format("https{0}", Request.Url.AbsoluteUri.Substring(4));

                Response.Redirect(path);
            }
        }

        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            if(arg.ToLower() == "*")
            {
                return BrowserFilterAttribute.IsSupported().ToString() + Request.QueryString + Request.Form;
            }
            return base.GetVaryByCustomString(context, arg);
        }
    }
}
