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

        public void CreateDownload(string emailAddress)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/download/", Method.POST, _configuration.IasAuthKey);
            request.AddParameter("emailAddress", emailAddress);

            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);
        }
    }
}
