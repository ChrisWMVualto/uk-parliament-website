using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Service.Transforms
{
    public static class VideoTransforms
    {
        public static VideoModel Transform(dynamic json)
        {
            dynamic jObject = JObject.Parse(json.ToString());
            if(jObject == null) return null;

            var pageUrl = jObject.webPageUrl.Value;
            var @event = jObject.@event;
            var moments = jObject.logMomentVideoItems;
            var embedCode = (string)jObject.embedCode;
            var legacyEmbedCode = (string)jObject.legacyEmbedCode;

            var eventModel = EventTransforms.Transform(@event.ToString());
            var momentModel = LogMomentTransforms.TransformArray(moments.ToString());

            return new VideoModel(eventModel, embedCode, legacyEmbedCode, momentModel, pageUrl);
        }

        public static VideoCollectionModel TransformArray(string jsonArray)
        {
            dynamic jObject = JObject.Parse(jsonArray);
            if(jObject == null) return new VideoCollectionModel(Enumerable.Empty<VideoModel>(), 0, 0);

            var jArray = JArray.Parse(jObject.results.ToString());
            var list = new List<VideoModel>();
            foreach(var json in jArray)
            {
                list.Add(VideoTransforms.Transform(json));
            }

            var totalCount = (int)jObject.totalCount.Value;
            var pageSize = (int)jObject.pageSize.Value;

            return new VideoCollectionModel(list, totalCount, pageSize);
        }
    }
}
