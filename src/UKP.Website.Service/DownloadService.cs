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

        public void CreateDownload(Guid eventId, int startTime, int endTime, string emailAddress, bool audioOnly)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/download", Method.POST, _configuration.IasAuthKey);
            request.AddParameter("EventId", eventId);
            request.AddParameter("StartTime", startTime);
            request.AddParameter("EndTime", endTime);
            request.AddParameter("Email", emailAddress);
            request.AddParameter("AudioOnly", audioOnly);


            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);
        }
    }
}
