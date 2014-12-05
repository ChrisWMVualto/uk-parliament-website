using System;
using System.Web.Mvc;
using Date.Extensions;
using RestSharp.Extensions;
using UKP.Website.Application;
using UKP.Website.Models.Guide;
using UKP.Website.Service;

namespace UKP.Website.Controllers
{
    public partial class GuideController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IVideoService _videoService;
        private readonly IConfiguration _configurationService;

        public GuideController(IEventService eventService, IVideoService videoService, IConfiguration configurationService)
        {
            _eventService = eventService;
            _videoService = videoService;
            _configurationService = configurationService;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            // TODO: Remove datetime
            var date = new DateTime(2014, 07, 07);
            var events = _eventService.GetEpgEvents(date);
            var model = new GuideViewModel(events, date);

            return View(model);
        }

        [HttpGet]
        public virtual PartialViewResult EpgInfo(Guid id)
        {
            var result = _videoService.GetVideo(id);
            return PartialView(MVC.Guide.Views._InfoPopup, result);
        }

        [HttpGet]
        public virtual PartialViewResult EpgDay(string date)
        {
            var dateob = date.HasValue() ? date.FromISO8601String() : DateTime.Today;

            var events = _eventService.GetEpgEvents(dateob);
            var model = new GuideViewModel(events, dateob.Value);
            return PartialView(MVC.Guide.Views._ChannelListing, model);
        }

        [HttpGet]
        public virtual PartialViewResult EpgDateBar(string date)
        {
            var dateob = date.HasValue() ? date.FromISO8601String() : DateTime.Today;

            return PartialView(MVC.Guide.Views._DateTabs, dateob.Value);
        }

        [HttpGet]
        public virtual PartialViewResult EpgDayTab(string date, bool previousDay)
        {
            return PartialView(MVC.Guide.Views._DateTab, previousDay ? date.FromISO8601String().Value.AddDays(-1) : date.FromISO8601String().Value.AddDays(1));
        }
    }
}