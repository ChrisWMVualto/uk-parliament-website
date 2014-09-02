using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Extensions;
using UKP.Website.Application;
using UKP.Website.Service.Model;
using UKP.Website.Service.Transforms;

namespace UKP.Website.Service
{
    public class VideoService : IVideoService
    {
        private readonly IRestClientWrapper _restClientWrapper;
        private readonly IConfiguration _configuration;

        public VideoService(IRestClientWrapper restClientWrapper, IConfiguration configuration)
        {
            _restClientWrapper = restClientWrapper;
            _configuration = configuration;
        }

        public VideoModel GetVideo(Guid id)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/video/{id}", Method.GET, _configuration.IasAuthKey);
            request.AddUrlSegment("id", id.ToString());
            request.AddParameter("format", "json");

            var response = client.Execute(request);

            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if(response.StatusCode != HttpStatusCode.OK)
            {
                throw new RestSharpException(response);
            }

            return VideoTransforms.TransformVideo(response.Content);
        }


        public VideoModel GetLegacyVideo(int id)
        {
            var client = _restClientWrapper.GetClient(_configuration.IasBaseUrl);
            var request = _restClientWrapper.AuthRestRequest("api/video/legacy/{id}", Method.GET, _configuration.IasAuthKey);
            request.AddUrlSegment("id", id.ToString(CultureInfo.InvariantCulture));
            request.AddParameter("format", "json");

            var response = client.Execute(request);

            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if(response.StatusCode != HttpStatusCode.OK)
            {
                throw new RestSharpException(response);
            }

            return VideoTransforms.TransformVideo(response.Content);
        }
    }
}
