using System.Web.Mvc;
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

        public HomeController(IEventService eventService, IRecessService recessService)
        {
            _eventService = eventService;
            _recessService = recessService;
        }

        [HttpGet]
        [OutputCache(Duration=30, VaryByParam="none")]
        public virtual ActionResult Index()
        {
            return RedirectToAction(MVC.Home.Commons());
        }

        [HttpGet]
        [OutputCache(Duration=30, VaryByParam="none")]
        public virtual ActionResult Commons()
        {
            var model = new HomeViewsModel(_eventService.GetNowEvents(), _eventService.GetMiniGuide(), _recessService.GetRecessMessage(RecessMessageType.HOUSE_OF_COMMONS), EventFilter.COMMONS);
            return View(model);
        }

        [HttpGet]
        [OutputCache(Duration=30, VaryByParam="none")]
        public virtual ActionResult Lords()
        {
            var model = new HomeViewsModel(_eventService.GetNowEvents(EventFilter.LORDS), _eventService.GetMiniGuide(EventFilter.LORDS), _recessService.GetRecessMessage(RecessMessageType.HOUSE_OF_LORDS), EventFilter.LORDS);
            return View(model);
        }

        [HttpGet]
        [OutputCache(Duration=30, VaryByParam="none")]
        public virtual ActionResult Committees()
        {
            var model = new HomeViewsModel(_eventService.GetNowEvents(EventFilter.COMMITTEES), _eventService.GetMiniGuide(EventFilter.COMMITTEES), _recessService.GetRecessMessage(RecessMessageType.ALL_COMMITTEES), EventFilter.COMMITTEES);
            return View(model);
        }

        [ChildActionOnly]
        [OutputCache(Duration=600, VaryByParam="none")]
        public virtual PartialViewResult RecentlyArchive(EventFilter eventFilter)
        {
            var model = _eventService.GetRecentlyArchived(eventFilter);
            return PartialView(MVC.Home.Views._RecentlyArchived, model);
        }

        public virtual ActionResult _404()
        {
            return View();
        }
    }
}