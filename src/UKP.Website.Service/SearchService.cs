using System.Collections.Generic;
using System.Net;
using Date.Extensions;
using RestSharp;
using RestSharp.Contrib;
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

        public IEnumerable<SearchModel> Search(SearchQueryModel search)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/search/", Method.GET, _configuration.IasAuthKey);
            request.AddParameter("keywords", HttpUtility.UrlEncode(search.Keywords));
            request.AddParameter("tags", HttpUtility.UrlEncode(search.Tags));
            request.AddParameter("memberId", HttpUtility.UrlEncode(search.MemberId.ToString()));
            request.AddParameter("fromDate", HttpUtility.UrlEncode(search.Period.ToISO8601String()));
            request.AddParameter("archiveOnly", true);
            request.AddParameter("format", "json");

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);

            var transforms = SearchTransforms.TransformArray(response.Content);
            return transforms;
        }
    }
}
