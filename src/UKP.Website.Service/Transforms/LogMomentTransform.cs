using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Service.Transforms
{
    public static class LogMomentTransforms
    {
        public static IEnumerable<LogMomentModel> TransformArray(string jsonArray)
        {
            dynamic jArray = JArray.Parse(jsonArray);
            if (jArray == null) return Enumerable.Empty<LogMomentModel>();

            var list = new List<LogMomentModel>();
            foreach (var json in jArray)
            {
                list.Add(Transform(json));
            }

            return list;
        }

        public static LogMomentModel Transform(dynamic json)
        {
            dynamic jObject = JObject.Parse(json.ToString());
            if (jObject == null) return null;

            var id = jObject.log.id.Value;
            var title = jObject.log.title.Value;
            var thumbnailUrl = jObject.thumbnailImageUrl.Value;
            var inPoint = (DateTime)jObject.inPoint.Value;

            return new LogMomentModel(id, title, thumbnailUrl, inPoint);
        }
    }
}
