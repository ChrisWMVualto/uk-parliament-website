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
            var cookieExpiry = accepted ? DateTime.Now.AddYears(1) : DateTime.Now.AddMonths(2);

            if (!accepted)
            {
                foreach (var toDelete in Response.Cookies.AllKeys)
                    Response.Cookies.Get(toDelete).Expires = DateTime.Now.AddDays(-1);
            }

            var cookie = Response.Cookies.Get(ApplicationConstants.AcceptCookieName) ?? new HttpCookie(ApplicationConstants.AcceptCookieName);

            cookie.Value = accepted.ToString();
            cookie.Expires = cookieExpiry;
            Response.Cookies.Add(cookie);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public virtual JsonResult CookieStateSet()
        {
            var cookie = Request.Cookies.Get(ApplicationConstants.AcceptCookieName);
            return Json(new { CookieSet = cookie != null });
        }

        [HttpPost]
        public virtual JsonResult CookiesAllowed()
        {
            var cookie = Request.Cookies.Get(ApplicationConstants.AcceptCookieName);
            var allowed = false;

            if(cookie != null)
                allowed = cookie.Value.ToLower() == "true";

            return Json(new { CookiesAllowed = allowed });
        }
    }

}