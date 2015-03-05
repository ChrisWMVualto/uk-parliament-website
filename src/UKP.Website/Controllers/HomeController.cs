using System.Web.Mvc;
using System.Web.UI;
using UKP.Website.Application;
using UKP.Website.Extensions;
using UKP.Website.Models;
using UKP.Website.Models.Home;
using UKP.Website.Service;
using UKP.Website.Service.Model;

namespace UKP.Website.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IRecessService _recessService;
        private readonly IConfiguration _configuration;

        public HomeController(IEventService eventService, IRecessService recessService, IConfiguration configuration)
        {
            _eventService = eventService;
            _recessService = recessService;
            _configuration = configuration;
        }

        [HttpGet]
        [OutputCache(Duration=45, VaryByCustom="*", Location = OutputCacheLocation.Server)]
        public virtual ActionResult Index()
        {
            return RedirectToAction(MVC.Home.Commons());
        }

        [HttpGet]
        [OutputCache(Duration=45, VaryByCustom="*", Location = OutputCacheLocation.Server)]
        public virtual ActionResult Commons()
        {
            var model = new HomeViewsModel(_eventService.GetNowEvents(), _eventService.GetMiniGuide(), _recessService.GetRecessMessage(RecessMessageType.HOUSE_OF_COMMONS), EventFilter.COMMONS);
            return View(model);
        }

        [HttpGet]
        [OutputCache(Duration=45, VaryByCustom="*", Location = OutputCacheLocation.ServerAndClient)]
        public virtual ActionResult Lords()
        {
            var model = new HomeViewsModel(_eventService.GetNowEvents(EventFilter.LORDS), _eventService.GetMiniGuide(EventFilter.LORDS), _recessService.GetRecessMessage(RecessMessageType.HOUSE_OF_LORDS), EventFilter.LORDS);
            return View(model);
        }

        [HttpGet]
        [OutputCache(Duration=45, VaryByCustom="*", Location = OutputCacheLocation.ServerAndClient)]
        public virtual ActionResult Committees()
        {
            var model = new HomeViewsModel(_eventService.GetNowEvents(EventFilter.COMMITTEES), _eventService.GetMiniGuide(EventFilter.COMMITTEES), _recessService.GetRecessMessage(RecessMessageType.ALL_COMMITTEES), EventFilter.COMMITTEES);
            return View(model);
        }

        [ChildActionOnly]
        [OutputCache(Duration=600, VaryByParam="*")]
        public virtual PartialViewResult RecentlyArchive(EventFilter eventFilter)
        {
            var model = _eventService.GetRecentlyArchived(eventFilter);
            return PartialView(MVC.Home.Views._RecentlyArchived, model);
        }

        public virtual ActionResult _404()
        {
            return View();
        }


        [OutputCache(Duration=45, VaryByParam="*", Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult Robots()
        {
            var robotsFile = "~/robots.txt.disallow";
            if (_configuration.RobotsAllow)
            {
                robotsFile = "~/robots.txt.allow";
            }
            return File(robotsFile, "text/plain");
        }
    }
}