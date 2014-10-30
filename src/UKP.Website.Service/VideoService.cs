using System;
using System.Globalization;
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
    public class VideoService : IVideoService
    {
        private readonly IRestClientWrapper _restClientWrapper;
        private readonly IConfiguration _configuration;

        public VideoService(IRestClientWrapper restClientWrapper, IConfiguration configuration)
        {
            _restClientWrapper = restClientWrapper;
            _configuration = configuration;
        }

        public VideoModel GetVideo(Guid id, DateTime? inPoint = null, DateTime? outPoint = null, bool? audioOnly = null, bool? autoStart = null)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/video/{id}", Method.GET, _configuration.IasAuthKey);
            request.AddUrlSegment("id", id.ToString());
            if(inPoint.HasValue) request.AddParameter("in", inPoint.ToISO8601String());
            if(outPoint.HasValue) request.AddParameter("out", outPoint.ToISO8601String());
            if(audioOnly.HasValue) request.AddParameter("audioOnly", audioOnly.Value);
            if(autoStart.HasValue) request.AddParameter("autoStart", autoStart.Value);

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

            return VideoTransforms.Transform(response.Content);
        }

        public VideoModel GetVideoWithLogs(Guid id, DateTime? inPoint = null, DateTime? outPoint = null,
            bool? audioOnly = null, bool? autoStart = null)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/video/{id}", Method.GET, _configuration.IasAuthKey);
            request.AddUrlSegment("id", id.ToString());
            if (inPoint.HasValue) request.AddParameter("in", inPoint.ToISO8601String());
            if (outPoint.HasValue) request.AddParameter("out", outPoint.ToISO8601String());
            if (audioOnly.HasValue) request.AddParameter("audioOnly", audioOnly.Value);
            if (autoStart.HasValue) request.AddParameter("autoStart", autoStart.Value);
            request.AddParameter("processLogs", true);
            request.AddParameter("format", "json");
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new RestSharpException(response);
            }

            return VideoTransforms.Transform(response.Content);
        }


        public VideoModel GetLegacyVideo(int id)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/video/legacy/{id}", Method.GET, _configuration.IasAuthKey);
            request.AddUrlSegment("id", id.ToString(CultureInfo.InvariantCulture));
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

            return VideoTransforms.Transform(response.Content);
        }

        
    }
}
