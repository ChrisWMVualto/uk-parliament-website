using System;
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
            var end = start.AddDays(30);

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
            var events = GetEvents().Where(x => x.House.Equals(EventString.GetString(eventFilter)));

            var nowEvents = events.Where(x => x.States.RecordingState.Equals(RecordingEventState.RECORDING));
            var nextEvents = events.Where(x => x.States.PlanningState.Equals(PlanningEventState.NEW) || x.States.PlanningState.Equals(PlanningEventState.PROPOSED));

            if (nowEvents.Count() >= target)
            {
                var more = nowEvents.Count() > target;
                return new NowAndNextModel(nowEvents.Take(target), more);
            }

            nowEvents = nowEvents.Concat(nextEvents.Take(target - nowEvents.Count()));
            return new NowAndNextModel(nowEvents, false);
        }

        public IEnumerable<EventModel> GetGuide(EventFilter eventFilter = EventFilter.COMMONS, int target = 15)
        {
            return GetNowEvents(eventFilter, target).Events;
        }

        public IEnumerable<EventModel> GetRecentlyArchived(EventFilter eventFilter = EventFilter.COMMONS, int numEvents = 10)
        {
            var url = string.Format(String.Format("api/event/archived/{0}/filter/{1}", numEvents, EventString.GetString(eventFilter)).ToLower(), numEvents);

            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest(url, Method.GET, _configuration.IasAuthKey);

            var response = client.Execute(request);

            if (response.StatusCode.Equals(HttpStatusCode.NotFound)) return null;
            if (!response.StatusCode.Equals(HttpStatusCode.OK)) throw new RestSharpException(response);

            return VideoTransforms.TransformArray(response.Content);
        }
    }
}
