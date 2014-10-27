using System.Web.Mvc;
using UKP.Website.Models.Guide;
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
            var events = _eventService.GetEpgEvents();
            var model = new GuideViewModel(events);

            return View(model);
        }
    }
}