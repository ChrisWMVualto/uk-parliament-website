﻿using System;
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
            var inPoint = CovertDateTimeFormatFromPattern(id, @in);
            var outPoint = CovertDateTimeFormatFromPattern(id, @out);

            var video = _videoService.GetVideo(id, inPoint, outPoint, audioOnly, autoStart.GetValueOrDefault(true));
            if(video == null || video.Event.States.RecordedState == RecordedEventState.REVOKE) return RedirectToAction(MVC.Home._404());

            return View(new EventViewModel(video));
        }

        [HttpGet]
        public virtual JsonResult GetShareVideo(Guid id, string @in = null, string @out = null)
        {
            var inPoint = @in.FromISO8601String();
            var outPoint = @out.FromISO8601String();

            var video =_videoService.GetVideo(id, inPoint, outPoint, null, false);

            return this.JsonFormatted(video, JsonRequestBehavior.AllowGet);
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
            if(video.RequestedInPoint.HasValue) inPointTime = video.RequestedInPoint.Value.ToLocalTime().TimeOfDay;

            TimeSpan? outPointTime = null;
            if(video.RequestedOutPoint.HasValue) outPointTime = video.RequestedOutPoint.Value.ToLocalTime().TimeOfDay;

            var inPointDate = video.RequestedInPoint.HasValue ? video.RequestedInPoint.Value : video.Event.DisplayStartDate;
            var outPointDate = video.RequestedOutPoint.HasValue ? video.RequestedOutPoint.Value : video.Event.DisplayEndDate;

            return PartialView(MVC.Event.Views._Clipping, new ClippingViewModel(video, inPointTime, outPointTime, inPointDate, outPointDate));
        }


        [HttpGet]
        public virtual PartialViewResult Stack(Guid id)
        {
            var video = _videoService.GetVideo(id);
            return PartialView(MVC.Event.Views._Stack, video);
        }

        [HttpGet]
        public virtual ActionResult LegacyPageRoute(int meetingId, TimeSpan? st)
        {
            var legacyVideo = _videoService.GetLegacyVideo(meetingId);
            if (legacyVideo == null) return RedirectToAction(MVC.Home._404());

            if(st.HasValue)
            {
                return RedirectToActionPermanent(MVC.Event.Index(legacyVideo.Event.Id, st.ToString()));
            }
            return RedirectToActionPermanent(MVC.Event.Index(legacyVideo.Event.Id));
        }

        [HttpPost]
        public virtual HttpStatusCodeResult State(StateChangeModel stateChangeModel)
        {
            EventStateHub.EventStateChanged(stateChangeModel.EventId, new EventStates(stateChangeModel.PlanningState, stateChangeModel.RecordingState, stateChangeModel.RecordedState, stateChangeModel.PlayerState), stateChangeModel.StateChanged);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private DateTime? CovertDateTimeFormatFromPattern(Guid id, string value)
        {
            DateTime? dateTimePoint;

            if(value != null && !value.ToLower().Contains("-"))
            {
                if(value.Length < 19)
                {
                    // HH:mm:ss
                    var tempVideo = _videoService.GetVideo(id);
                    var timeOfDay = TimeSpan.Parse(value);
                    dateTimePoint = tempVideo.Event.DisplayStartDate.ToLocalTime().Date.Add(timeOfDay);
                }
                else
                {
                    // HH:mm:ss:ffyyyymmdd
                    dateTimePoint = value.FromSMPTEString();
                }
            }
            else
            {
                // iso86601 strings used to be human url friendly, yyyy-MM-ddTHH:mm:ssK
                dateTimePoint = value.FromISO8601String();
            }
            return dateTimePoint;
        }
    }
}