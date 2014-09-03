using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UKP.Website.Service;

namespace UKP.Website.Controllers
{
    public partial class MeetingController : Controller
    {
        private readonly IVideoService _videoService;

        public MeetingController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet]
        public virtual ActionResult Index(Guid id, DateTime? inPoint = null, DateTime? outPoint = null)
        {
            var video = _videoService.GetVideo(id);
            return null;
        }


        [HttpGet]
        public virtual ActionResult LegacyPageRoute(int meetingId, TimeSpan? st)
        {
            var legacyVideo = _videoService.GetLegacyVideo(meetingId);
            if (st.HasValue)
            {
                var startTime = legacyVideo.EventModel.ScheduledStartTime.Date.Add(st.Value);
                return RedirectToActionPermanent(MVC.Meeting.Index(legacyVideo.EventModel.Id, startTime));
            }
            return RedirectToActionPermanent(MVC.Meeting.Index(legacyVideo.EventModel.Id));
        }
    }
}