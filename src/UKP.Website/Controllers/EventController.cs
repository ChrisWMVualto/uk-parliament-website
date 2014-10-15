using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Date.Extensions;
using UKP.Website.Extensions;
using UKP.Website.Extensions.SignalR;
using UKP.Website.Models.Event;
using UKP.Website.Service;
using UKP.Website.Service.Model;

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
        public virtual ActionResult Index(Guid id, string @in = null, string @out = null, bool? audioOnly = null, bool? autoStart = null)
        {
            // iso86601 strings used to be human url friendly
            var inPoint = @in.FromISO8601String();
            var outPoint = @out.FromISO8601String();

            var video = _videoService.GetVideo(id, inPoint, outPoint, audioOnly, autoStart.GetValueOrDefault(true));

            return View(new EventViewModel(video));
        }


        [HttpGet]
        public virtual JsonResult GetShareVideo(Guid id, TimeSpan? @in = null, TimeSpan? @out = null)
        {
            var video =_videoService.GetVideo(id, null, null, null, false);

            if (!@in.HasValue && !@out.HasValue) return this.JsonFormatted(video, JsonRequestBehavior.AllowGet);

            if (video == null) return null;

            DateTime? inPointDate = null;
            if(@in.HasValue && video.Event.PublishedStartTime.HasValue)
                inPointDate = video.Event.PublishedStartTime.Value.Add(@in.Value);

            DateTime? outPointDate = null;
            if(@out.HasValue & video.Event.PublishedStartTime.HasValue)
                outPointDate = video.Event.PublishedStartTime.Value.Add(@out.Value);

            var videoModel = _videoService.GetVideo(id, inPointDate, outPointDate, null, false);
            return this.JsonFormatted(videoModel, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public virtual JsonResult GetMainVideo(Guid id, string @in = null, string @out = null, bool? audioOnly = null)
        {
            var inPoint = @in.FromISO8601String();
            var outPoint = @out.FromISO8601String();

            var video =_videoService.GetVideo(id, inPoint, outPoint, audioOnly, true);
            return this.JsonFormatted(video, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual PartialViewResult EventTitle(Guid id)
        {
            var video = _videoService.GetVideo(id);
            return PartialView(MVC.Event.Views._EventTitle, video);
        }

        [HttpGet]
        public virtual PartialViewResult Clipping(Guid id, string @in = null, string @out = null)
        {
            var inPoint = @in.FromISO8601String();
            var outPoint = @out.FromISO8601String();

            var video = _videoService.GetVideo(id, inPoint, outPoint);

            TimeSpan? inPointTime = null;
            if (video.RequestedInPoint.HasValue && video.Event.PublishedStartTime.HasValue)
                inPointTime = video.Event.PublishedStartTime - video.RequestedInPoint;

            TimeSpan? outPointTime = null;
            if(video.RequestedOutPoint.HasValue && video.Event.PublishedStartTime.HasValue)
                outPointTime = video.Event.PublishedStartTime - video.RequestedOutPoint;

            return PartialView(MVC.Event.Views._Clipping, new ClippingViewModel(video, inPointTime, outPointTime));
        }


        [HttpGet]
        public virtual ActionResult LegacyPageRoute(int meetingId, TimeSpan? st)
        {
            var legacyVideo = _videoService.GetLegacyVideo(meetingId);
            if(st.HasValue)
            {
                var timeOfDay = legacyVideo.Event.ScheduledStartTime.Date.ToLocalTime().Add(st.Value);
                var date = timeOfDay.ToISO8601String();
                return RedirectToActionPermanent(MVC.Event.Index(legacyVideo.Event.Id, date));
            }
            return RedirectToActionPermanent(MVC.Event.Index(legacyVideo.Event.Id));
        }

        [HttpPost]
        public virtual HttpStatusCodeResult State(StateChangeModel stateChangeModel)
        {
            EventStateHub.EventStateChanged(stateChangeModel.EventId, new EventStates(stateChangeModel.PlanningState, stateChangeModel.RecordingState, stateChangeModel.RecordedState, stateChangeModel.PlayerState), stateChangeModel.StateChanged);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}