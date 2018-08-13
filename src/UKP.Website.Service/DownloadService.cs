using System;
using System.Collections.Generic;
using System.Net;
using Date.Extensions;
using log4net;
using log4net.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private ILog _logger;

        public DownloadService(IRestClientWrapper restClientWrapper, IConfiguration configuration)
        {
            _restClientWrapper = restClientWrapper;
            _configuration = configuration;
            _logger = LogManager.GetLogger(typeof(DownloadService));
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


            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);
            _logger.Info("UKP Web Create Download Successfully Sent To IAS");
            return DownloadTransforms.Transform(response.Content);
        }

        public bool VerifyCaptcha(string secret, string token)
        {
            var client = _restClientWrapper.GetClient(_configuration.GoogleRecaptchaVerifyUrl);
            var request = _restClientWrapper.RestRequest("", Method.POST);
            request.AddParameter("secret", secret);
            request.AddParameter("response", token);

            var response = client.Execute(request);

            dynamic content = JsonConvert.DeserializeObject(response.Content);

            return content.success;
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
