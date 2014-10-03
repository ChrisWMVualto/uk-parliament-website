using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UKP.Website.Service;

namespace UKP.Website.Controllers
{
    public partial class EpgController : Controller
    {
        private readonly IEventService _eventService;

        public EpgController(IEventService eventService)
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