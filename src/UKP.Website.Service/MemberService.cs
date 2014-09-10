using System.Net;
using RestSharp;
using RestSharp.Extensions;
using UKP.Website.Application;

namespace UKP.Website.Service
{
    public class MemberService : IMemberService
    {
        private readonly IRestClientWrapper _restClientWrapper;
        private readonly IConfiguration _configuration;

        public MemberService(IRestClientWrapper restClientWrapper, IConfiguration configuration)
        {
            _restClientWrapper = restClientWrapper;
            _configuration = configuration;
        }

        public string Search(string query)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest(string.Format("api/member/search/{0}", query), Method.GET, _configuration.IasAuthKey);

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (response.StatusCode != HttpStatusCode.OK) throw new RestSharpException(response);

            return response.Content;
        }
    }
}
