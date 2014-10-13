using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UKP.Website.Application;
using UKP.Website.Service;

namespace UKP.Website.Controllers
{
    public partial class CookieController : Controller
    {
        [HttpGet]
        public virtual ActionResult Index(bool accepted)
        {
            if (accepted)
            {
                var cookie = new HttpCookie(ApplicationConstants.AcceptCookieName, true.ToString());
                cookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(cookie);
            }
            else
            {
                foreach (var cookie in Response.Cookies.AllKeys)
                    Response.Cookies.Get(cookie).Expires = DateTime.Now.AddDays(-1);

                Session.Add(ApplicationConstants.AcceptCookieName, false);
            }
            
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}