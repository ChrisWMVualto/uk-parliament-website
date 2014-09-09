using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Service.Transforms
{
    public static class SearchTransforms
    {
        public static IEnumerable<SearchModel> TransformArray(string jsonArray)
        {
            dynamic jArray = JArray.Parse(jsonArray);
            if (jArray == null) return Enumerable.Empty<SearchModel>();

            var list = new List<SearchModel>();
            foreach (var json in jArray)
            {
                list.Add(Transform(json));
            }

            return list;
        }

        public static SearchModel Transform(dynamic json)
        {
            dynamic jObject = JObject.Parse(json.ToString());
            if (jObject == null) return null;

            var pageUrl = jObject.webPageUrl.Value;
            var @event = jObject.@event;
            var moments = jObject.logMomentVideoItems;

            var eventModel = VideoTransforms.Transform(@event.ToString());
            var momentModel = MomentTransforms.TransformArray(moments.ToString());

            return new SearchModel(eventModel, momentModel, pageUrl);
        }
    }
}
