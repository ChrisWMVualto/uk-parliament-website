using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UKP.Website
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "LegacyPageRoute",
                url: "main/player.aspx",
                defaults: new { controller = "Meeting", action = "LegacyPageRoute", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "LegacyPlayerRoute",
                url: "embed/js.ashx",
                defaults: new { controller = "Player", action = "LegacyPlayerRoute", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}