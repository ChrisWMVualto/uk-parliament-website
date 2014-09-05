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
        private readonly IEpgService _epgService;
        private readonly IRecessService _recessService;

        public HomeController(IEpgService epgService, IRecessService recessService)
        {
            _epgService = epgService;
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
            var model = new HomeViewsModel(_epgService.GetNowEvents(), _epgService.GetGuide(), _epgService.GetRecentlyArchived(), _recessService.GetRecessMessage());
            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Lords()
        {
            var model = new HomeViewsModel(_epgService.GetNowEvents(EventFilter.LORDS), _epgService.GetGuide(EventFilter.LORDS), _epgService.GetRecentlyArchived(EventFilter.LORDS), _recessService.GetRecessMessage(EventFilter.LORDS));
            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Committees()
        {
            var model = new HomeViewsModel(_epgService.GetNowEvents(EventFilter.COMMITTEES), _epgService.GetGuide(EventFilter.COMMITTEES), _epgService.GetRecentlyArchived(EventFilter.COMMITTEES), _recessService.GetRecessMessage(EventFilter.COMMITTEES));
            return View(model);
        }
    }
}