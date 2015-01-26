using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using Date.Extensions;
using RestSharp;
using RestSharp.Extensions;
using UKP.Website.Application;
using UKP.Website.Service.Model;
using UKP.Website.Service.Transforms;

namespace UKP.Website.Service
{
    public class EventService : IEventService
    {
        private readonly IRestClientWrapper _restClientWrapper;
        private readonly IConfiguration _configuration;

        public EventService(IRestClientWrapper restClientWrapper, IConfiguration configuration)
        {
            _restClientWrapper = restClientWrapper;
            _configuration = configuration;
        }

        public IEnumerable<EpgChannelModel> GetFullGuide(DateTime? date)
        {
            var dateob = date.HasValue ? date.Value : DateTime.Now;

            var events = GetFullGuideEPG(dateob).ToList();
            var epgModel = new List<EpgChannelModel>();

            for (var i = 1; i <= 20; i++)
            {
                var channelEvents = events.Where(x => x.ChannelName.Equals(i.ToString())).Select(x => new EpgEventModel(x, dateob));
                var channel = new EpgChannelModel(i, channelEvents.ToList());
                epgModel.Add(channel);
            }

            return epgModel;
        }

        public NowAndNextModel GetNowEvents(EventFilter eventFilter = EventFilter.COMMONS, int target = 6)
        {
            var events = GetMiniGuideEPG(eventFilter).ToList();
            var nowEvents = events.Where(x => x.HomeFilters.Live).OrderBy(x => x.DisplayStartDate).Take(target);
            var nextEvents = events.Where(x => x.HomeFilters.Next).OrderBy(x => x.DisplayStartDate);

            if (nowEvents.Count() == target)
                return new NowAndNextModel(nowEvents, true, true);

            var live = nowEvents.Count() != 0;
            var eventsDifference = target - nowEvents.Count();

            nowEvents = nextEvents.Take(eventsDifference).Any() ? nowEvents.Concat(nextEvents.Take(eventsDifference)) : nowEvents;
            return new NowAndNextModel(nowEvents.Take(target), false, live);
        }

        public IEnumerable<EventModel> GetMiniGuide(EventFilter eventFilter = EventFilter.COMMONS, int target = 12)
        {
            var events = GetMiniGuideEPG(eventFilter).ToList();
            var nowEvents = events.Where(x => x.HomeFilters.LiveAndArchive).OrderBy(x => x.DisplayStartDate).Take(target);
            var nextEvents = events.Where(x => x.HomeFilters.Next).OrderBy(x => x.DisplayStartDate);

            var eventsDifference = target - nowEvents.Count();
            return nextEvents.Take(eventsDifference).Any() ? nowEvents.Concat(nextEvents.Take(eventsDifference)).OrderBy(x => x.DisplayStartDate).Take(target) : nowEvents.Take(target);
        }

        public VideoCollectionModel GetRecentlyArchived(EventFilter eventFilter = EventFilter.COMMONS, int numEvents = 10)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/event/archived/{numEvents}/filter/{eventFilter}", Method.GET, _configuration.IasAuthKey);
            request.AddUrlSegment("numEvents", numEvents.ToString());
            request.AddUrlSegment("eventFilter", eventFilter.ToString());

            var response = client.Execute(request);

            if(response.StatusCode.Equals(HttpStatusCode.NotFound)) return null;
            if(!response.StatusCode.Equals(HttpStatusCode.OK)) throw new RestSharpException(response);

            return VideoTransforms.TransformArray(response.Content);
        }

        public LogMomentResultModel GetLogsBetween(Guid id, DateTime startTime, DateTime endTime)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/event/logs/{id}", Method.GET, _configuration.IasAuthKey);
            request.AddUrlSegment("id", id.ToString());
            request.AddParameter("startTime", startTime.ToISO8601String());
            request.AddParameter("endTime", endTime.ToISO8601String());
            request.AddParameter("format", "json");
            var response = client.Execute(request);

            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if(response.StatusCode != HttpStatusCode.OK)
            {
                throw new RestSharpException(response);
            }
            return LogMomentTransforms.TransformObject(response.Content);

        }

        private IEnumerable<EventModel> GetFullGuideEPG(DateTime? date)
        {
            var start = date.HasValue ? date.Value : DateTime.Now;
            var end = start.AddDays(1);
            return GetEPG(start, end, null);
        }

        private IEnumerable<EventModel> GetMiniGuideEPG(EventFilter? eventFilter)
        {
            var start = DateTime.Now.Date;
            var end = start.AddMonths(1);
            return GetEPG(start, end, eventFilter);
        }


        private IEnumerable<EventModel> GetEPG(DateTime start, DateTime end, EventFilter? eventFilter)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            //client.Proxy = new WebProxy("127.0.0.1", 8888); // <- Fiddler
            var request = _restClientWrapper.AuthRestRequest("api/epg/", Method.GET, _configuration.IasAuthKey);

            request.AddParameter("date", start.ToISO8601String());
            request.AddParameter("endDate", end.ToISO8601String());
            if(eventFilter != null) request.AddParameter("eventFilter", eventFilter);

            request.AddParameter("format", "json");

            var response = client.Execute(request);

            if(response.StatusCode == HttpStatusCode.NotFound) return null;
            if(response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);

            return EventTransforms.TransformEPG(response.Content); ;
        }

    }
}
