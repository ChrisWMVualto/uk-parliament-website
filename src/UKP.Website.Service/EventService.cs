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

        public IEnumerable<EventModel> GetEpg()
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            //client.Proxy = new WebProxy("127.0.0.1", 8888); // <- Fiddler
            var request = _restClientWrapper.AuthRestRequest("api/epg/", Method.GET, _configuration.IasAuthKey);

            // TODO: Remove hardcoded date
            var start = new DateTime(2014, 07, 04);
            //var start = DateTime.Now.Date;
            var end = start.AddDays(1);

            request.AddParameter("date", start.ToISO8601String());
            request.AddParameter("endDate", end.ToISO8601String());
            request.AddParameter("format", "json");

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);

            return EventTransforms.TransformEPG(response.Content);;
        }

        public List<EpgChannelModel> GetEpgEvents()
        {
            var events = GetEpg();
            var epgModel = new List<EpgChannelModel>();

            for (var i = 1; i <= 20; i++)
            {
                var channelEvents = events.Where(x => x.ChannelName.Equals(i.ToString())).Select(x => new EpgEventModel(x));
                var channel = new EpgChannelModel(i, channelEvents.ToList());
                epgModel.Add(channel);
            }

            return epgModel;
        }

        public NowAndNextModel GetNowEvents(EventFilter eventFilter = EventFilter.COMMONS, int target = 6)
        {
            var events = RunEventFilter(GetEvents(), eventFilter).ToList();
            var nowEvents = events.Where(x => x.HomeFilters.Live).OrderBy(x => x.DisplayStartDate).Take(target);
            var nextEvents = events.Where(x => x.HomeFilters.Next).OrderBy(x => x.DisplayStartDate);

            if (nowEvents.Count() == target)
                return new NowAndNextModel(nowEvents, true, true);

            var live = nowEvents.Count() != 0;
            var eventsDifference = target - nowEvents.Count();

            nowEvents = nextEvents.Take(eventsDifference).Count() > 1 ? nowEvents.Concat(nextEvents.Take(eventsDifference)) : nowEvents;
            return new NowAndNextModel(nowEvents, false, live);
        }

        public IEnumerable<EventModel> GetGuide(EventFilter eventFilter = EventFilter.COMMONS, int target = 12)
        {
            var events = RunEventFilter(GetEvents(), eventFilter).ToList();
            var nowEvents = events.Where(x => x.HomeFilters.Live).OrderBy(x => x.DisplayStartDate).Take(target);
            var nextEvents = events.Where(x => x.HomeFilters.Next).OrderBy(x => x.DisplayStartDate);

            var eventsDifference = target - nowEvents.Count();
            return nextEvents.Take(eventsDifference).Any() ? nowEvents.Concat(nextEvents.Take(eventsDifference)) : nowEvents;
        }

        public VideoCollectionModel GetRecentlyArchived(EventFilter eventFilter = EventFilter.COMMONS, int numEvents = 10)
        {
            var url = string.Format(String.Format("api/event/archived/{0}/filter/{1}", numEvents, eventFilter.GetEventType()).ToLower(), numEvents);

            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest(url, Method.GET, _configuration.IasAuthKey);

            var response = client.Execute(request);

            if(response.StatusCode.Equals(HttpStatusCode.NotFound)) return null;
            if(!response.StatusCode.Equals(HttpStatusCode.OK)) throw new RestSharpException(response);

            return VideoTransforms.TransformArray(response.Content);
        }

        private IEnumerable<EventModel> GetEvents()
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/epg/", Method.GET, _configuration.IasAuthKey);

            // TODO: Remove hardcoded date
            var start = new DateTime(2014, 07, 04);
            //var start = DateTime.Now.Date;
            var end = start.AddMonths(1);

            request.AddParameter("date", start.ToISO8601String());
            request.AddParameter("endDate", end.ToISO8601String());
            request.AddParameter("format", "json");

            var response = client.Execute(request);

            if(response.StatusCode == HttpStatusCode.NotFound) return null;
            if(response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);

            return EventTransforms.TransformEPG(response.Content); ;
        }

        private IEnumerable<EventModel> RunEventFilter(IEnumerable<EventModel> events, EventFilter filter)
        {
            if (filter == EventFilter.COMMONS)
                return events.Where(x => x.House.Equals(EventConstants.HOUSE_COMMONS) || x.House.Equals(EventConstants.HOUSE_JOINT))
                             .Where(x => x.Business.Equals(EventConstants.BUSINESS_CHAMBER) || x.Business.Equals(EventConstants.BUSINESS_COMMITTEE));

            if (filter == EventFilter.LORDS)
                return events.Where(x => x.House.Equals(EventConstants.HOUSE_LORDS) || x.House.Equals(EventConstants.HOUSE_JOINT))
                             .Where(x => x.Business.Equals(EventConstants.BUSINESS_CHAMBER) || x.Business.Equals(EventConstants.BUSINESS_COMMITTEE));

            return events.Where(x => x.Business.Equals(EventConstants.BUSINESS_COMMITTEE))
                         .Where(x => x.House.Equals(EventConstants.HOUSE_LORDS) || x.House.Equals(EventConstants.HOUSE_COMMONS) || x.House.Equals(EventConstants.HOUSE_JOINT));
        }
    }
}
