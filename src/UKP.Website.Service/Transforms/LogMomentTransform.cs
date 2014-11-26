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

        public static LogMomentResultModel TransformObject(string jsonObject)
        {
            dynamic jObject = JObject.Parse(jsonObject);

            var logMoments = Enumerable.Empty<LogMomentModel>();
            var total = (int) jObject.totalResults;
            var containsLogMoments = (bool)jObject.containsLogMoments;
            
            
            if (total > 0)
                logMoments = TransformArray(jObject.results.ToString());

            return new LogMomentResultModel(logMoments, total, containsLogMoments);
        }

        public static LogMomentModel Transform(dynamic json)
        {
            dynamic jObject = JObject.Parse(json.ToString());
            if (jObject == null) return null;

            var id = (Guid)jObject.log.id;
            var title = (string)jObject.log.title;
            var thumbnailUrl = (string)jObject.thumbnailImageUrl;
            var inPoint = (DateTime)jObject.inPoint;
            var member = (string)jObject.log.member;

            return new LogMomentModel(id, title, thumbnailUrl, inPoint, member);
        }
    }
}
