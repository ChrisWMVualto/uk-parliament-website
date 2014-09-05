using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using Date.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog.Targets;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Extensions;
using UKP.Website.Application;
using UKP.Website.Service.Model;
using UKP.Website.Service.Transforms;

namespace UKP.Website.Service
{
    public class EpgService : IEpgService
    {
        private readonly IRestClientWrapper _restClientWrapper;
        private readonly IConfiguration _configuration;

        public EpgService(IRestClientWrapper restClientWrapper, IConfiguration configuration)
        {
            _restClientWrapper = restClientWrapper;
            _configuration = configuration;
        }

        private IEnumerable<EventModel> GetEvents()
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/epg/", Method.GET, _configuration.IasAuthKey);

            // TODO: Remove hardcoded date
            var start = new DateTime(2014, 05, 12);
            var end = start.AddMonths(1);

            request.AddParameter("date", start.ToISO8601String());
            request.AddParameter("endDate", end.ToISO8601String());
            request.AddParameter("format", "json");

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);

            return VideoTransforms.TransformEPG(response.Content);;
        }

        public NowAndNextModel GetNowEvents(EventFilter eventFilter = EventFilter.ALL, int target = 6)
        {
            var events = GetEvents();

            if (eventFilter == EventFilter.COMMONS)
            {
                events = events.Where(x => x.House.Equals(EventConstants.BUSINESS_COMMITTEE));
            }

            if (eventFilter == EventFilter.LORDS)
            {
                events = events.Where(x => x.House.Equals(EventConstants.HOUSE_LORDS));
            }

            if (eventFilter == EventFilter.COMMITTEES)
            {
                events = events.Where(x => x.Business.Equals(EventConstants.BUSINESS_COMMITTEE));
            }

            var nowEvents = events.Where(x => x.States.RecordingState.Equals(RecordingEventState.RECORDING));
            var nextEvents = events.Where(x => x.States.PlanningState.Equals(PlanningEventState.PROPOSED) || x.States.PlanningState.Equals(PlanningEventState.CONFIRMED));

            if (nowEvents.Count() >= target)
            {
                var more = nowEvents.Count() > target;
                return new NowAndNextModel(nowEvents.Take(target), more);
            }

            nowEvents = nowEvents.Concat(nextEvents.Take(target - nowEvents.Count()));
            return new NowAndNextModel(nowEvents, false);
        }

        public IEnumerable<EventModel> GetGuide(EventFilter eventFilter = EventFilter.ALL, int target = 15)
        {
            return GetNowEvents(eventFilter, target).Events;
        }

        public IEnumerable<EventModel> GetRecentlyArchived(EventFilter eventFilter = EventFilter.ALL, int numEvents = 10)
        {
            var url = string.Format("api/event/archived/{0}/", numEvents);

            if (eventFilter == EventFilter.COMMONS)
            {
                url += "filter/commons";
            }

            if (eventFilter == EventFilter.LORDS)
            {
                url += "filter/lords";
            }

            if (eventFilter == EventFilter.COMMITTEES)
            {
                url += "filter/committee";
            }

            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest(url, Method.GET, _configuration.IasAuthKey);

            var response = client.Execute(request);

            if (response.StatusCode.Equals(HttpStatusCode.NotFound)) return null;
            if (!response.StatusCode.Equals(HttpStatusCode.OK)) throw new RestSharpException(response);

            return VideoTransforms.TransformArray(response.Content);
        }
    }
}
