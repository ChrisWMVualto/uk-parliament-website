using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UKP.Website.Models;
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
        public virtual ActionResult Index()
        {
            return RedirectToAction(MVC.Home.Commons());
        }

        [HttpGet]   
        public virtual ActionResult Commons()
        {
            var model = new HomeViewsModel(_eventService.GetNowEvents(), _eventService.GetGuide(), _recessService.GetRecessMessage(), EventFilter.COMMONS);
            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Lords()
        {
            var model = new HomeViewsModel(_eventService.GetNowEvents(EventFilter.LORDS), _eventService.GetGuide(EventFilter.LORDS), _recessService.GetRecessMessage(EventFilter.LORDS), EventFilter.LORDS);
            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Committees()
        {
            var model = new HomeViewsModel(_eventService.GetNowEvents(EventFilter.COMMITTEES), _eventService.GetGuide(EventFilter.COMMITTEES), _recessService.GetRecessMessage(EventFilter.COMMITTEES), EventFilter.COMMITTEES);
            return View(model);
        }

        [ChildActionOnly]
        public virtual PartialViewResult RecentlyArchive(EventFilter eventFilter)
        {
            var model = _eventService.GetRecentlyArchived(eventFilter);
            return PartialView(MVC.Home.Views._RecentlyArchived, model);
        }
    }
}