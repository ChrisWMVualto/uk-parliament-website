﻿using System;
using System.Collections.Generic;

namespace UKP.Website.Service.Model
{
    public class VideoModel
    {
        public VideoModel(EventModel @event, string embedCode, string legacyEmbedCode, LogMomentResultModel logMoments, string pageUrl, string shortWebPageUrl,
            DateTime? requestedInPoint, DateTime? requestedOutPoint, IEnumerable<StackModel> stacks, string thumbnailUrl, string scriptableEmbedCode, bool requestedAudioOnly, bool requestedAutoStart)
        {
            Event = @event;
            EmbedCode = embedCode;
            LegacyEmbedCode = legacyEmbedCode;
            LogMoments = logMoments;
            PageUrl = pageUrl;
            ShortWebPageUrl = shortWebPageUrl;
            RequestedInPoint = requestedInPoint;
            RequestedOutPoint = requestedOutPoint;
            Stacks = stacks;
            ThumbnailUrl = thumbnailUrl;
            ScriptableEmbedCode = scriptableEmbedCode;
            RequestedAudioOnly = requestedAudioOnly;
            RequestedAutoStart = requestedAutoStart;
        }

        public VideoModel(EventModel @event, LogMomentResultModel logMoments)
        {
            Event = @event;
            LogMoments = logMoments;
        }

        public string EmbedCode { get; private set; }
        public string LegacyEmbedCode { get; private set; }
        public LogMomentResultModel LogMoments { get; private set; }
        public string PageUrl { get; private set; }
        public string ShortWebPageUrl { get; private set; }
        public DateTime? RequestedInPoint { get; private set; }
        public DateTime? RequestedOutPoint { get; private set; }
        public IEnumerable<StackModel> Stacks { get; private set; }
        public string ThumbnailUrl { get; private set; }
        public string ScriptableEmbedCode { get; private set; }
        public bool RequestedAudioOnly { get; private set; }
        public bool RequestedAutoStart { get; private set; }
        public EventModel Event { get; private set; }
    }
}