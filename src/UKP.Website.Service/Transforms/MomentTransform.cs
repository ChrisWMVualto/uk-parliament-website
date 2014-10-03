using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Service.Transforms
{
    public static class MomentTransforms
    {
        public static IEnumerable<MomentModel> TransformArray(string jsonArray)
        {
            dynamic jArray = JArray.Parse(jsonArray);
            if (jArray == null) return Enumerable.Empty<MomentModel>();

            var list = new List<MomentModel>();
            foreach (var json in jArray)
            {
                list.Add(Transform(json));
            }

            return list;
        }

        public static MomentModel Transform(dynamic json)
        {
            dynamic jObject = JObject.Parse(json.ToString());
            if (jObject == null) return null;

            var id = jObject.log.id.Value;
            var title = jObject.log.title.Value;
            var thumbnailUrl = jObject.thumbnailImageUrl.Value;

            return new MomentModel(id, title, thumbnailUrl);
        }
    }
}
