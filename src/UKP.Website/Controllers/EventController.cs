using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Date.Extensions;
using UKP.Website.Service;

namespace UKP.Website.Controllers
{
    public partial class EventController : Controller
    {
        private readonly IVideoService _videoService;

        public EventController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet]
        public virtual ActionResult Index(Guid id, string @in = null, string @out = null)
        {
            // iso86601 strings used to be human url friendly
            var inPoint = @in.FromISO8601String();
            var outPoint = @out.FromISO8601String();

            var video = _videoService.GetVideo(id);
            return null;
        }


        [HttpGet]
        public virtual ActionResult LegacyPageRoute(int meetingId, TimeSpan? st)
        {
            var legacyVideo = _videoService.GetLegacyVideo(meetingId);
            if (st.HasValue)
            {
                var timeOfDay = legacyVideo.EventModel.ScheduledStartTime.Date.ToLocalTime().Add(st.Value);
                var date = timeOfDay.ToISO8601String();
                return RedirectToActionPermanent(MVC.Event.Index(legacyVideo.EventModel.Id, date));
            }
            return RedirectToActionPermanent(MVC.Event.Index(legacyVideo.EventModel.Id));
        }
    }
}