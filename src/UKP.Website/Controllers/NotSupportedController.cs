using System.Web.Mvc;
using UKP.Website.Extensions;

namespace UKP.Website.Controllers
{
    public partial class NotSupportedController : Controller
    {
        [HttpGet]
        [SkipBrowserFilter]
        [RequireHttps]
        public virtual ActionResult Index()
        {
            Response.StatusCode = 403;
            return View();
        }
    }
}