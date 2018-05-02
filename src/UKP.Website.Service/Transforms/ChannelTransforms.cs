using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Service.Transforms
{
    public static class ChannelTransforms
    {
        public static IEnumerable<ChannelModel> TransformChannels(string jsonArray)
        {
            dynamic jArray = JArray.Parse(jsonArray);
            if (jArray == null) return Enumerable.Empty<ChannelModel>();

            var list = new List<ChannelModel>();
            foreach (var json in jArray)
            {
                list.Add(Transform(json));
            }

            return list;
        }

        public static ChannelModel Transform(dynamic json)
        {
            dynamic Object = JObject.Parse(json.ToString());

            var name = (string)Object.name;
            var externalId = (string)Object.externalChannelId;
            var internalId = (int)Object.internalChannelId;

            return new ChannelModel(name, externalId, internalId);
        }
    }
}
