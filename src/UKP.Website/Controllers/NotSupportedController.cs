using System.Web.Mvc;
using UKP.Website.Extensions;

namespace UKP.Website.Controllers
{
    public partial class NotSupportedController : Controller
    {
        // GET: NotSupported
        [SkipBrowserFilter]
        public virtual ActionResult Index()
        {
            Response.StatusCode = 403;
            return View();
        }
    }
}