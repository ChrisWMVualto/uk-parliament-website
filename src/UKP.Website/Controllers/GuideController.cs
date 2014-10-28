using System;
using System.Web.Mvc;
using UKP.Website.Models.Guide;
using UKP.Website.Service;

namespace UKP.Website.Controllers
{
    public partial class GuideController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IVideoService _videoService;

        public GuideController(IEventService eventService, IVideoService videoService)
        {
            _eventService = eventService;
            _videoService = videoService;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            var events = _eventService.GetEpgEvents();
            var model = new GuideViewModel(events);

            return View(model);
        }

        [HttpGet]
        public virtual PartialViewResult EpgInfo(Guid id)
        {
            var result = _videoService.GetVideo(id);
            return PartialView(MVC.Guide.Views._InfoPopup, result);
        }
    }
}