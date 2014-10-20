using System;
using System.Collections.Generic;

namespace UKP.Website.Service.Model
{
    public class VideoModel
    {
        public VideoModel(EventModel @event, string embedCode, string legacyEmbedCode, LogMomentResultModel moments, string pageUrl, string shortWebPageUrl, DateTime? requestedInPoint, DateTime? requestedOutPoint)
        {
            Event = @event;
            EmbedCode = embedCode;
            LegacyEmbedCode = legacyEmbedCode;
            Moments = moments;
            PageUrl = pageUrl;
            ShortWebPageUrl = shortWebPageUrl;
            RequestedInPoint = requestedInPoint;
            RequestedOutPoint = requestedOutPoint;
        }

        public string EmbedCode { get; private set; }
        public string LegacyEmbedCode { get; private set; }
        public LogMomentResultModel Moments { get; private set; }
        public string PageUrl { get; private set; }
        public string ShortWebPageUrl { get; private set; }
        public DateTime? RequestedInPoint { get; private set; }
        public DateTime? RequestedOutPoint { get; private set; }
        public EventModel Event { get; private set; }
    }
}