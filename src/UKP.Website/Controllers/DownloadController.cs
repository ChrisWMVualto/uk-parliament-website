using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UKP.Website.Controllers
{
    public class DownloadController : Controller
    {
        [HttpGet]
        public ActionResult Index(Guid id)
        {


            return Redirect("https://www.google.com");
        }
    }
}