using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UKP.Website.Application;

namespace UKP.Website.Controllers
{
    public partial class CookieController : Controller
    {
        [HttpGet]
        public virtual ActionResult Index(bool accepted)
        {
            if (!accepted)
            {
                foreach (var toDelete in Response.Cookies.AllKeys)
                    Response.Cookies.Get(toDelete).Expires = DateTime.Now.AddDays(-1);
            }

            var cookie = Response.Cookies.Get(ApplicationConstants.AcceptCookieName) ?? new HttpCookie(ApplicationConstants.AcceptCookieName);

            cookie.Value = accepted.ToString();
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }

}