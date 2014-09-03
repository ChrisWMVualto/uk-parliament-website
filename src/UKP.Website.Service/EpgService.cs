using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
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
        private readonly IVideoService _videoService;

        public EpgService(IRestClientWrapper restClientWrapper, IConfiguration configuration, IVideoService videoService)
        {
            _restClientWrapper = restClientWrapper;
            _configuration = configuration;
            _videoService = videoService;
        }

        private IEnumerable<EventModel> GetEvents()
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/epg/", Method.GET, _configuration.IasAuthKey);
            request.AddParameter("date", "2014-05-12T00:00:00+00:00");
            request.AddParameter("format", "json");

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);

            return VideoTransforms.TransformEPG(response.Content);;
        }

        public NowAndNextModel GetNowEvents(EventFilter eventFilter = EventFilter.ALL, int target = 6)
        {
            var events = GetEvents();

            if (eventFilter != null)
            {
                if (eventFilter == EventFilter.COMMONS)
                {
                    events = events.Where(x => x.House.Equals("Commons"));
                }

                if (eventFilter == EventFilter.LORDS)
                {
                    events = events.Where(x => x.House.Equals("Lords"));
                }
            }

            var nowEvents = events.Where(x => x.States.RecordingState.Equals(RecordingEventState.RECORDING));
            var nextEvents = events.Where(x => !x.States.PlanningState.Equals(PlanningEventState.VOID));

            if (nowEvents.Count() >= target)
            {
                var more = nowEvents.Count() > target;
                return new NowAndNextModel(nowEvents, more);
            }

            nowEvents = nowEvents.Concat(nextEvents.Take(target - nowEvents.Count()));
            return new NowAndNextModel(nowEvents, false);
        }

        public IEnumerable<EventModel> GetGuide(EventFilter eventFilter = EventFilter.ALL)
        {
            var events = GetEvents();
            return events;
        }

        public IEnumerable<EventModel> GetRecentlyArchived(EventFilter eventFilter = EventFilter.ALL)
        {
            // TODO: Find a way to get ten most recently archived events, and return here
            return null;
        }
    }
}
