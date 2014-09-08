using System.Collections.Generic;
using RestSharp;
using RestSharp.Extensions;
using UKP.Website.Application;
using UKP.Website.Models;
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
            request.AddParameter("keywords", search.Keywords);
            request.AddParameter("archiveOnly", true);
            request.AddParameter("format", "json");

            var response = client.Execute(request);
            var transforms = SearchTransforms.TransformArray(response.Content);
            return transforms;
        }
    }
}
