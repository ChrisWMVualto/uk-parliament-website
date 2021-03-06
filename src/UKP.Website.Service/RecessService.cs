﻿using System;
using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Extensions;
using UKP.Website.Application;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public class RecessService : IRecessService
    {
        private readonly IRestClientWrapper _restClientWrapper;
        private readonly IConfiguration _configuration;

        public RecessService(IRestClientWrapper restClientWrapper, IConfiguration configuration)
        {
            _restClientWrapper = restClientWrapper;
            _configuration = configuration;
        }

        public RecessMessageModel GetRecessMessage(RecessMessageType type)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/recess/{type}", Method.GET, _configuration.IasAuthKey);
            request.AddUrlSegment("type", type.ToString());

            var response = client.Execute(request);

            if(response.StatusCode == HttpStatusCode.NotFound) return null;
            if(response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);

            dynamic json = JObject.Parse(response.Content);
            var date = json.expireMessage.Value;
            var message = json.message.Value;
            return new RecessMessageModel(date, message);
        }
    }
}
