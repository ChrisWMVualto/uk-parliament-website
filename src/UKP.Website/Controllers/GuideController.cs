using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UKP.Website.Service;

namespace UKP.Website.Controllers
{
    public partial class GuideController : Controller
    {
        private readonly IEventService _eventService;

        public GuideController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            _eventService.GetEpg();

            return View();
        }
    }
}