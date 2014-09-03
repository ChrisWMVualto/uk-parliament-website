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

        public HomeController(IEpgService epgService)
        {
            _epgService = epgService;
        }

        [HttpGet]
        public virtual ActionResult All()
        {
            var model = new HomeViewsModel(_epgService.GetNowEvents(), _epgService.GetGuide(), _epgService.GetRecentlyArchived());
            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Commons()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public virtual ActionResult Lords()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public virtual ActionResult Committees()
        {
            throw new NotImplementedException();
        }
    }
}