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

            var @event = jObject.@event;
            var logMoments = jObject.logMomentVideoItems;
            var stacks = jObject.stackVideoItems;
            var embedCode = (string)jObject.embedCode;
            var scriptableEmbedCode = (string)jObject.scriptableEmbedCode;
            var legacyEmbedCode = (string)jObject.legacyEmbedCode;
            var pageUrl = (string)jObject.webPageUrl;
            var shortWebPageUrl = (string)jObject.shortWebPageUrl;
            var requestedInPoint = (DateTime?)jObject.requestedInPoint;
            var requestedOutPoint = (DateTime?)jObject.requestedOutPoint;
            var thumbnailUrl = (string)jObject.thumbnailUrl;

            var eventModel = EventTransforms.Transform(@event.ToString());
            var momentModel = LogMomentTransforms.TransformObject(logMoments.ToString());
            var stacksModels = StackTransforms.TransformArray(stacks.ToString());

            return new VideoModel(eventModel, embedCode, legacyEmbedCode, momentModel, pageUrl, shortWebPageUrl, requestedInPoint, requestedOutPoint, stacksModels, thumbnailUrl, scriptableEmbedCode);
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
