using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Service.Transforms
{
    public static class TagTransforms
    {
        public static IEnumerable<TagModel> TransformArray(string jsonArray)
        {
            dynamic jArray = JArray.Parse(jsonArray);
            if(jArray == null) return Enumerable.Empty<TagModel>();

            var list = new List<TagModel>();
            foreach(var json in jArray)
            {
                list.Add(Transform(json));
            }

            return list;
        }

        public static TagModel Transform(dynamic json)
        {
            dynamic jObject = JObject.Parse(json.ToString());
            if(jObject == null) return null;

            var category = jObject.category.Value;
            var tag = jObject.tag.Value;
            var displayTag = jObject.displayTag.Value;

            return new TagModel(category, tag, displayTag);
        }
    }
}