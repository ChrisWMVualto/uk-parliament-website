using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            request.AddParameter("date", "2014-05-13T12:14:41+01:00");
            request.AddParameter("format", "json");

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);

            return VideoTransforms.TransformEPG(response.Content);;
        }

        public IEnumerable<EventModel> GetNowEvents()
        {
            // TODO: Filter based on event states (recording/upcoming)
            var events = GetEvents();
            return events;
        }

        public IEnumerable<EventModel> GetGuide()
        {
            var events = GetEvents();
            return events;
        }

        public IEnumerable<EventModel> GetRecentlyArchived()
        {
            // TODO: Find a way to get ten most recently archived events, and return here
            return null;
        }
    }
}
