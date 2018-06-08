using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
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
            MvcHandler.DisableMvcResponseHeader = true;
        }

        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            if (arg.ToLower() == "*")
            {
                return BrowserFilterAttribute.IsSupported().ToString() + Request.QueryString + Request.Form;
            }
            return base.GetVaryByCustomString(context, arg);
        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            if (HttpContext.Current != null) HttpContext.Current.Response.Headers.Remove("Server");
        }

    }
}
