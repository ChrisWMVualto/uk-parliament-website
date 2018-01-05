using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;
using Date.Extensions;
using Elmah;
using UKP.Website.Application;
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
        private readonly IEventService _eventService;
        private readonly IDownloadService _downloadService;
        private readonly IConfiguration _configuration;

        public EventController(IVideoService videoService, IEventService eventService, IDownloadService downloadService, IConfiguration configuration)
        {
            _videoService = videoService;
            _eventService = eventService;
            _downloadService = downloadService;
            _configuration = configuration;
        }

        [HttpGet]
        [OutputCache(Duration=20, VaryByCustom= "*")]
        public virtual ActionResult Index(Guid id, string @in = null, string @out = null, bool? audioOnly = null, bool? autoStart = null, bool? agenda = null)
        {
            var inPoint = ConvertDateTimeFormatFromPattern(id, @in);
            var outPoint = ConvertDateTimeFormatFromPattern(id, @out);
            var video = _videoService.GetVideo(id, inPoint: inPoint, outPoint: outPoint, audioOnly: audioOnly, autoStart: autoStart.GetValueOrDefault(true), statsEnabled:Request.CookiesAllowed());
            if(video == null) return RedirectToAction(MVC.Home._404());

            Session["CaptchaCompleted"] = false;

            return View(new EventViewModel(video, agenda.GetValueOrDefault()));
        }

        [HttpGet]
        public virtual ActionResult LegacyPageRoute(int meetingId, TimeSpan? st)
        {
            var legacyVideo = _videoService.GetLegacyVideo(meetingId);
            if(legacyVideo == null) return RedirectToAction(MVC.Home._404());

            if(st.HasValue)
            {
                return RedirectToActionPermanent(MVC.Event.Index(legacyVideo.Event.Id, st.ToString()));
            }
            return RedirectToActionPermanent(MVC.Event.Index(legacyVideo.Event.Id));
        }

        [HttpGet]
        public virtual JsonResult GetShareVideo(Guid id, string @in = null, string @out = null)
        {
            var inPoint = @in.FromISO8601String();
            var outPoint = @out.FromISO8601String();
            var video =_videoService.GetVideo(id, inPoint: inPoint, outPoint: outPoint, audioOnly: null, autoStart: false, statsEnabled: Request.CookiesAllowed(), requestedUsersIPAddress: Request.IPAddress());

            return this.JsonFormatted(video, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(Duration=20, VaryByParam="*")]
        public virtual JsonResult ShowAudioOnly(Guid id, string @in = null, string @out = null)
        {
            var inPoint = @in.FromISO8601String();
            var outPoint = @out.FromISO8601String();
            var video =_videoService.GetVideo(id, inPoint, outPoint, null, false, statsEnabled: Request.CookiesAllowed());

            return this.JsonFormatted(EventViewModel.ShowAudioOnly(video), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetMainVideo(Guid id, string @in = null, string @out = null, bool? audioOnly = null, bool? autoStart = null)
        {
            var inPoint = @in.FromISO8601String();
            var outPoint = @out.FromISO8601String();

            var video =_videoService.GetVideo(id, inPoint, outPoint, audioOnly, autoStart, statsEnabled: Request.CookiesAllowed());
            return this.JsonFormatted(video, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(Duration=120, VaryByParam="*")]
        public virtual PartialViewResult EventTitle(Guid id, string @in = null, string @out = null)
        {
            var inPoint = @in.FromISO8601String();
            var outPoint = @out.FromISO8601String();
            var video = _videoService.GetVideo(id, inPoint, outPoint, statsEnabled: Request.CookiesAllowed());
            return PartialView(MVC.Event.Views._EventTitle, video);
        }

        [HttpGet]
        public virtual PartialViewResult DownloadForm(Guid id, string @in = null, string @out = null)
        {
            var inPoint = @in.FromISO8601String();
            var outPoint = @out.FromISO8601String();
            var video = _videoService.GetVideo(id, inPoint, outPoint, statsEnabled: Request.CookiesAllowed());
            return PartialView(MVC.Event.Views._DownloadForm, video);
        }

        [HttpGet]
        public virtual PartialViewResult DownloadTab(Guid id, string @in = null, string @out = null)
        {
            var inPoint = @in.FromISO8601String();
            var outPoint = @out.FromISO8601String();
            var video = _videoService.GetVideo(id, inPoint, outPoint, statsEnabled: Request.CookiesAllowed());

            return PartialView(MVC.Event.Views._Download, new DownloadViewModel(video));
        }

        [HttpGet]
        public virtual PartialViewResult Clipping(Guid id, string @in = null, string @out = null)
        {
            var inPoint = @in.FromISO8601String();
            var outPoint = @out.FromISO8601String();
            var video = _videoService.GetVideo(id, inPoint, outPoint, statsEnabled: Request.CookiesAllowed());

            TimeSpan? inPointTime = null;
            if(video.RequestedInPoint.HasValue) inPointTime = video.RequestedInPoint.Value.ToLocalTime().TimeOfDay;

            TimeSpan? outPointTime = null;
            if(video.RequestedOutPoint.HasValue) outPointTime = video.RequestedOutPoint.Value.ToLocalTime().TimeOfDay;

            var inPointDate = video.RequestedInPoint.HasValue ? video.RequestedInPoint.Value : video.Event.DisplayStartDate;
            var outPointDate = video.RequestedOutPoint.HasValue ? video.RequestedOutPoint.Value : video.Event.DisplayEndDate;

            return PartialView(MVC.Event.Views._Clipping, new ClippingViewModel(video, inPointTime, outPointTime, inPointDate, outPointDate));
        }


        [HttpGet]
        [OutputCache(Duration = 12, VaryByParam = "*")]
        public virtual PartialViewResult Logs(Guid id, string @in = null, string @out = null)
        {
            var inPoint = @in.FromISO8601String();
            var outPoint = @out.FromISO8601String();

            var video = _videoService.GetVideo(id, inPoint: inPoint, outPoint: outPoint, statsEnabled: Request.CookiesAllowed(), processLogs: true, noCache: true);
            var logRestuls = video.LogMoments.Results;

            return PartialView(MVC.Event.Views._LogMoment, logRestuls);
        }


        [HttpGet]
        [OutputCache(Duration = 20, VaryByParam = "*")]
        public virtual PartialViewResult EventLogsBetween(Guid id, string startTime = null, string @in = null, string @out = null)
        {
            var start = startTime.FromISO8601String() ?? DateTime.Now;
            var inPoint = @in.FromISO8601String();
            var outPoint = @out.FromISO8601String();
            var end = start.AddHours(3);
            var logs = _eventService.GetLogsBetween(id, start, end);
            var results = logs.Results;

            // Don't add new logs that don't fit in the clipped range.
            if(inPoint.HasValue && outPoint.HasValue)
                results = results.Where(x => x.InPoint >= inPoint.Value.ToUniversalTime() && x.InPoint < outPoint.Value.ToUniversalTime());
  
            return PartialView(MVC.Event.Views._LogMoment, results);
        }

        [HttpGet]
        [OutputCache(Duration = 120, VaryByParam = "*")]
        public virtual PartialViewResult Stack(Guid id, string @in = null, string @out = null)
        {
            var inPoint = @in.FromISO8601String();
            var outPoint = @out.FromISO8601String();

            var video = _videoService.GetVideo(id, inPoint: inPoint, outPoint: outPoint, statsEnabled: Request.CookiesAllowed());
            return PartialView(MVC.Event.Views._Stack, video);
        }


        [HttpPost]
        public virtual HttpStatusCodeResult State(StateChangeModel stateChangeModel)
        {
            try
            {
                HttpResponse.RemoveOutputCacheItem(string.Format("/Event/Index/{0}", stateChangeModel.EventId));
            }
            catch(Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }

            EventStateHub.EventStateChanged(stateChangeModel.EventId, new EventStates(stateChangeModel.PlanningState, stateChangeModel.RecordingState, stateChangeModel.RecordedState, stateChangeModel.PlayerState), stateChangeModel.StateChanged);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public virtual HttpStatusCodeResult LogUpdate(LogUpdateModel logUpdateModel)
        {
            EventStateHub.LogUpdate(logUpdateModel.LogUpdateType, logUpdateModel.EventId, logUpdateModel.LogMomentId, logUpdateModel.Description, logUpdateModel.Timecode, logUpdateModel.DisplayAs);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public virtual ContentResult CreateDownload(CreateDownloadModel model)
        {
            var response = new CreateDownloadResponseModel(model.EmailAddress,true);
            try
            {
                var startTime = DateTime.Parse(model.StartTime);
                response.InPointHasError = false;
                var endTime = DateTime.Parse(model.EndTime);
                response.OutPointHasError = false;

                var apiResponse =_downloadService.CreateDownload(model.EventId, startTime, endTime, model.EmailAddress, model.AudioOnly, model.StreamUrl);
                response.Success = apiResponse.Successful;
                response.Message = apiResponse.Message;
                if (apiResponse.Successful)
                {
                    response.Message += String.Format(" You have {0} downloads remaining. This will reset in {1} hours",apiResponse.DownloadsRemaining, apiResponse.ResetHours);
                }
                if (apiResponse.ResetMinutes > 0)
                {
                    response.Message += String.Format(" & {0} minutes", apiResponse.ResetMinutes);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error has occurred.";
            }

            var json = new JavaScriptSerializer().Serialize(response);

            return Content(json);
        }

        [HttpPost]
        public virtual bool ValidateCaptchaToken(CaptchaVerifyModel data)
        {
            bool valid = _downloadService.VerifyCaptcha(_configuration.RecaptchaSecret, data.Response);

            if (valid)
            {
                Session["CaptchaCompleted"] = true;
            }

            return valid;
        }

        public virtual void ResetCaptcha()
        {
            Session["CaptchaCompleted"] = false;
        }

        [HttpGet]
        public virtual JsonResult ValidateCaptcha()
        {
            var temp = this.Json(new { captchaCompleted = (bool)Session["CaptchaCompleted"] });
            return this.Json(new { captchaCompleted = (bool)Session["CaptchaCompleted"]}, JsonRequestBehavior.AllowGet);
        }

        private DateTime? ConvertDateTimeFormatFromPattern(Guid id, string value)
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