using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UKP.Website.Service;

namespace UKP.Website.Controllers
{
    public partial class CookieController : Controller
    {
        // GET: Cookie
        public virtual ActionResult Index(bool accepted)
        {
            if (accepted)
            {
                var cookie = new HttpCookie("Cookies", true.ToString());
                cookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(cookie);
            }
            else
            {
                foreach (var cookie in Response.Cookies.AllKeys)
                    Response.Cookies.Get(cookie).Expires = DateTime.Now.AddDays(-1);

                Session.Add("Cookies", false);
            }
            
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}