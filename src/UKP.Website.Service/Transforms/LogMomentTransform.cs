﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Service.Transforms
{
    public static class LogMomentTransforms
    {
        public static IEnumerable<LogMomentModel> TransformArray(dynamic jsonArray)
        {
            if(jsonArray == null) return Enumerable.Empty<LogMomentModel>();

            var list = new List<LogMomentModel>();
            foreach(var json in jsonArray)
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
                logMoments = TransformArray(jObject.results);

            return new LogMomentResultModel(logMoments, total, containsLogMoments);
        }

        public static LogMomentModel Transform(dynamic json)
        {
            var id = (Guid)json.log.id;
            var title = (string)json.log.title;
            var thumbnailUrl = (string)json.thumbnailImageUrl;
            var inPoint = (DateTime?)json.inPoint;
            var member = (string)json.log.member;

            return new LogMomentModel(id, title, thumbnailUrl, inPoint, member);
        }
    }
}
