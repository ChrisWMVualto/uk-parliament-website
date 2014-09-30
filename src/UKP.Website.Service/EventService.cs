﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        private IEnumerable<EventModel> GetEvents()
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/epg/", Method.GET, _configuration.IasAuthKey);

            // TODO: Remove hardcoded date
            var start = new DateTime(2014, 07, 04);
            var end = start.AddMonths(1);

            request.AddParameter("date", start.ToISO8601String());
            request.AddParameter("endDate", end.ToISO8601String());
            request.AddParameter("format", "json");

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);

            return VideoTransforms.TransformEPG(response.Content);;
        }

        public NowAndNextModel GetNowEvents(EventFilter eventFilter = EventFilter.COMMONS, int target = 6)
        {
            var events = GetEvents().Where(x => x.House.Equals(EventString.GetEventType(eventFilter)));
            events = RunEventFilter(events, eventFilter);

            var nowEvents = events.Where(x => x.Live);
            var nextEvents = events.Where(x => x.Next);

            if (nowEvents.Count() >= target)
            {
                var allLive = nowEvents.Count() == target;
                return new NowAndNextModel(nowEvents.Take(target), allLive, true);
            }

            var live = nowEvents.Count() != 0;

            nowEvents = nextEvents.Take(target - nowEvents.Count()).Count() > 1 ? nowEvents.Concat(nextEvents.Take(target - nowEvents.Count())) : nowEvents;
            return new NowAndNextModel(nowEvents, false, live);
        }

        public IEnumerable<EventModel> GetGuide(EventFilter eventFilter = EventFilter.COMMONS, int target = 12)
        {
            var events = GetEvents().Where(x => x.House.Equals(EventString.GetEventType(eventFilter)));
            events = RunEventFilter(events, eventFilter);

            var nowEvents = events.Where(x => x.Live);
            var nextEvents = events.Where(x => x.Next);

            if (nowEvents.Count() >= target)
                return nowEvents.OrderBy(x => x.DisplayTime).Take(target).Select(
                            x => new EventModel(x.Id, x.Title, x.House, x.Business, x.States, x.ActualLiveStartTime, x.ScheduledStartTime, x.PublishedStartTime, x.ActualStartTime, x.ActualEndTime));

            var additionalRequired = target - nowEvents.Count();
            nowEvents = nextEvents.Take(additionalRequired).Any() ? nowEvents.OrderBy(x => x.DisplayTime).Concat(nextEvents.Take(additionalRequired)) : nowEvents;
            return nowEvents.Take(target).Select(
                            x => new EventModel(x.Id, x.Title, x.House, x.Business, x.States, x.ActualLiveStartTime, x.ScheduledStartTime, x.PublishedStartTime, x.ActualStartTime, x.ActualEndTime));
        }

        public IEnumerable<EventModel> GetRecentlyArchived(EventFilter eventFilter = EventFilter.COMMONS, int numEvents = 10)
        {
            var url = string.Format(String.Format("api/event/archived/{0}/filter/{1}", numEvents, eventFilter.GetEventType()).ToLower(), numEvents);

            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest(url, Method.GET, _configuration.IasAuthKey);

            var response = client.Execute(request);

            if(response.StatusCode.Equals(HttpStatusCode.NotFound)) return null;
            if(!response.StatusCode.Equals(HttpStatusCode.OK)) throw new RestSharpException(response);

            return VideoTransforms.TransformArray(response.Content).OrderBy(x => x.ActualEndTime).Take(numEvents);
        }

        internal IEnumerable<EventModel> RunEventFilter(IEnumerable<EventModel> events, EventFilter filter)
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
