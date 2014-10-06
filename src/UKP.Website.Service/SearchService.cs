using System.Collections.Generic;
using System.Net;
using Date.Extensions;
using Newtonsoft.Json.Linq;
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

        public SearchModel Search(SearchFormModel search)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            client.Proxy = new WebProxy("127.0.0.1", 8888); // <- Fiddler

            var request = _restClientWrapper.AuthRestRequest("api/search/", Method.GET, _configuration.IasAuthKey);

            if (search.Keywords.HasValue())
                request.AddParameter("keywords", search.Keywords);
            
            if (search.House.HasValue())
                request.AddParameter("house", search.House);

            if (search.Business.HasValue())
                request.AddParameter("business", search.Business);

            if (search.MemberId.HasValue)
                request.AddParameter("memberId", search.MemberId.Value);

            request.AddParameter("fromDate", search.Period.ToISO8601String());
            request.AddParameter("archiveOnly", true);
            request.AddParameter("format", "json");

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);

            var transforms = SearchTransforms.Transform(response.Content);
            return transforms;
        }
    }
}
