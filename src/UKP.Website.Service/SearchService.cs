using System;
using System.Net;
using Date.Extensions;
using RestSharp;
using RestSharp.Extensions;
using UKP.Website.Application;
using UKP.Website.Service.Model;
using UKP.Website.Service.Transforms;

namespace UKP.Website.Service
{
    public class SearchService : ISearchService
    {
        private readonly IRestClientWrapper _restClientWrapper;
        private readonly IConfiguration _configuration;

        public SearchService(IRestClientWrapper restClientWrapper, IConfiguration configuration)
        {
            _restClientWrapper = restClientWrapper;
            _configuration = configuration;
        }

        public VideoCollectionModel Search(string keywords, int? memberId, string house, string business, DateTime period, int pageNum)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            //client.Proxy = new WebProxy("127.0.0.1", 8888); // <- Fiddler

            var request = _restClientWrapper.AuthRestRequest("api/search/", Method.GET, _configuration.IasAuthKey);

            if (keywords.HasValue())
                request.AddParameter("keywords", keywords);
            
            if (house.HasValue())
                request.AddParameter("house", house);

            if (business.HasValue())
                request.AddParameter("business", business);

            if (memberId.HasValue)
                request.AddParameter("memberId", memberId);

            request.AddParameter("fromDate", period.ToISO8601String());
            request.AddParameter("archiveOnly", true);
            request.AddParameter("format", "json");
            request.AddParameter("pageNumber", pageNum);

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);

            var results = VideoTransforms.TransformArray(response.Content);
            return results;
        }
    }
}
