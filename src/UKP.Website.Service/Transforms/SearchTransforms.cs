using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Service.Transforms
{
    public static class SearchTransforms
    {
        public static IEnumerable<SearchResultsModel> TransformArray(string jsonArray)
        {
            dynamic jArray = JArray.Parse(jsonArray);
            if (jArray == null) return Enumerable.Empty<SearchResultsModel>();

            var list = new List<SearchResultsModel>();
            foreach (var json in jArray)
            {
                list.Add(Transform(json));
            }

            return list;
        }

        public static SearchResultsModel Transform(dynamic json)
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
