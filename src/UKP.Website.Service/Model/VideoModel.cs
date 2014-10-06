﻿using System.Collections.Generic;

namespace UKP.Website.Service.Model
{
    public class VideoModel
    {
        public VideoModel(EventModel @event, string embedCode, string legacyEmbedCode, IEnumerable<LogMomentModel> moments, string pageUrl)
        {
            Event = @event;
            EmbedCode = embedCode;
            LegacyEmbedCode = legacyEmbedCode;
            Moments = moments;
            PageUrl = pageUrl;
        }

        public string EmbedCode { get; private set; }
        public string LegacyEmbedCode { get; private set; }
        public IEnumerable<LogMomentModel> Moments { get; private set; }
        public string PageUrl { get; private set; }
        public EventModel Event { get; private set; }
    }
}