using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Service.Transforms
{
    public static class SearchTransforms
    {
        public static SearchModel Transform(string jsonArray)
        {
            dynamic jObject = JObject.Parse(jsonArray);
            if (jObject == null) return new SearchModel(null, 0, 0);

            var jArray = JArray.Parse(jObject.results.ToString());
            var list = new List<SearchResultsModel>();
            foreach (var json in jArray)
            {
                list.Add(TransformResult(json));
            }

            var totalCount = (int)jObject.totalCount.Value;
            var pageSize = (int)jObject.pageSize.Value;

            return new SearchModel(list, totalCount, pageSize);
        }

        public static SearchResultsModel TransformResult(dynamic json)
        {
            dynamic jObject = JObject.Parse(json.ToString());
            if (jObject == null) return null;

            var pageUrl = jObject.webPageUrl.Value;
            var @event = jObject.@event;
            var moments = jObject.logMomentVideoItems;

            var eventModel = VideoTransforms.Transform(@event.ToString());
            var momentModel = MomentTransforms.TransformArray(moments.ToString());

            return new SearchResultsModel(eventModel, momentModel, pageUrl);
        }
    }
}
