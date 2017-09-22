using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Service.Transforms
{
    public static class DownloadTransforms
    {

        public static DownloadResponseModel Transform(dynamic json)
        {
            dynamic Object = JObject.Parse(json.ToString());

            var success = (bool)Object.successful;
            var message = (string)Object.message;

            return new DownloadResponseModel(success, message);
        }
    }
}
