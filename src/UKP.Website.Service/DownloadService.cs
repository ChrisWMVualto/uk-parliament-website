using System;
using System.Collections.Generic;
using System.Net;
using Date.Extensions;
using RestSharp;
using RestSharp.Extensions;
using UKP.Website.Application;
using UKP.Website.Service.Model;
using UKP.Website.Service.Transforms;

namespace UKP.Website.Service
{
    public class DownloadService : IDownloadService
    {
        private readonly IRestClientWrapper _restClientWrapper;
        private readonly IConfiguration _configuration;

        public DownloadService(IRestClientWrapper restClientWrapper, IConfiguration configuration)
        {
            _restClientWrapper = restClientWrapper;
            _configuration = configuration;
        }

        public DownloadResponseModel CreateDownload(Guid eventId, DateTime startTime, DateTime endTime, string emailAddress, bool audioOnly, string streamUrl = null)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/download", Method.POST, _configuration.IasAuthKey);
            request.AddParameter("EventId", eventId);
            request.AddParameter("StartTime", startTime.ToISO8601String());
            request.AddParameter("EndTime", endTime.ToISO8601String());
            request.AddParameter("Email", emailAddress);
            request.AddParameter("AudioOnly", audioOnly);
            if (streamUrl != null) request.AddParameter("StreamUrl", streamUrl);


            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);
            return DownloadTransforms.Transform(response.Content);
        }

        public DownloadUrlModel GetDownloadUrl(Guid id)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/download/downloadurl/{id}", Method.GET, _configuration.IasAuthKey);
            request.AddUrlSegment("id", id.ToString());

            var response = client.Execute(request);

            if (response.StatusCode.Equals(HttpStatusCode.OK)) return DownloadTransforms.TransformDownloadUrl(response.Content);
            if (response.StatusCode.Equals(HttpStatusCode.NoContent)) return null;
            throw new RestSharpException(response);
        }
    }
}
