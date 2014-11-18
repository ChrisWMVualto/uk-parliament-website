using System;
using System.Net;
using RestSharp;
using RestSharp.Extensions;
using UKP.Website.Application;

namespace UKP.Website.Service
{
    public class MembersService : IMembersService
    {
        private readonly IConfiguration _configurationService;
        private readonly IRestClientWrapper _restClientWrapper;

        public MembersService(IConfiguration configurationService, IRestClientWrapper _restClientWrapper)
        {
            _configurationService = configurationService;
            this._restClientWrapper = _restClientWrapper;
        }

        public string WildcardLookup(string name)
        {
            name = "name*" + name;
            var client = _restClientWrapper.GetClient(_configurationService.GetMemberAutocompleteApi);
            var request = _restClientWrapper.RestRequest(name, Method.GET);

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            if (response.StatusCode != HttpStatusCode.OK) throw new RestSharpException();

            return response.Content;
        }
    }
}
