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
            var resetHours = (int)Object.resetHours;
            var resetMinutes = (int)Object.resetMinutes;
            var email = (string)Object.email;
            var downloadsRemaining = (int)Object.downloadsRemaining;

            return new DownloadResponseModel(success, message, resetHours, resetMinutes, email, downloadsRemaining);
        }
    }
}
