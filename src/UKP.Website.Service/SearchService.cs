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
    public class SearchService : ISearchService
    {
        private readonly IRestClientWrapper _restClientWrapper;
        private readonly IConfiguration _configuration;

        public SearchService(IRestClientWrapper restClientWrapper, IConfiguration configuration)
        {
            _restClientWrapper = restClientWrapper;
            _configuration = configuration;
        }

        public VideoCollectionModel Search(string keywords, int? memberId, string house, string business, DateTime? from, DateTime? to, int pageNum, bool isMemberKeywordSearch)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/search/", Method.GET, _configuration.IasAuthKey);

            if(keywords.HasValue())
                request.AddParameter("keywords", keywords);

            if(house.HasValue())
                request.AddParameter("house", house);

            if(business.HasValue())
                request.AddParameter("business", business);

            if(memberId.HasValue)
                request.AddParameter("memberId", memberId);

            if(from.HasValue)
                request.AddParameter("from", from.ToISO8601String());

            if(to.HasValue)
                request.AddParameter("to", to.ToISO8601String());

            if(isMemberKeywordSearch)
                request.AddParameter("isMemberKeywordSearch", true);

            request.AddParameter("archiveOnly", true);
            request.AddParameter("format", "json");
            request.AddParameter("pageNumber", pageNum);

            var response = client.Execute(request);
            if(response.StatusCode == HttpStatusCode.NotFound) return null;
            if(response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);

            return VideoTransforms.TransformArray(response.Content);
        }

        public LogMomentResultModel SearchMoments(Guid eventId, string keywords, int? memberId, int pageSize, bool isMemberKeywordSearch)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/search/logs/{eventId}", Method.GET, _configuration.IasAuthKey);

            request.AddUrlSegment("eventId", eventId.ToString());

            if(keywords.HasValue())
                request.AddParameter("keywords", keywords);

            if(memberId.HasValue)
                request.AddParameter("memberId", memberId);

            request.AddParameter("pageSize", pageSize);

            if(isMemberKeywordSearch)
                request.AddParameter("isMemberKeywordSearch", true);

            var response = client.Execute(request);
            if(response.StatusCode == HttpStatusCode.NotFound) return null;
            if(response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);

            return LogMomentTransforms.TransformObject(response.Content);
        }

        public IEnumerable<TagModel> GetTags()
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/search/tags", Method.GET, _configuration.IasAuthKey);

            var response = client.Execute(request);
            if(response.StatusCode == HttpStatusCode.NotFound) return null;
            if(response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);

            return TagTransforms.TransformArray(response.Content);
        }

        public IEnumerable<SearchMembersNameModel> SearchMembers(string keywords)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/search/members", Method.GET, _configuration.IasAuthKey);

            if(keywords.HasValue())
                request.AddParameter("keywords", keywords + "*");

            var response = client.Execute(request);
            if(response.StatusCode == HttpStatusCode.NotFound) return null;
            if(response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);

            return SearchMembersTransforms.TransformArray(response.Content);
        }
    }
}
