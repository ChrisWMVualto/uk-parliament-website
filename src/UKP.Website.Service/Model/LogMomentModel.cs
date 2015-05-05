﻿using System;

namespace UKP.Website.Service.Model
{
    public class LogMomentModel
    {
        public LogMomentModel(Guid id, string description, string thumbnailUrl, DateTime? inPoint, string iasDisplayAs)
        {
            Id = id;
            Description = description;
            ThumbnailUrl = thumbnailUrl;
            InPoint = inPoint;
            IasDisplayAs = iasDisplayAs;
        }

        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public string ThumbnailUrl { get; private set; }
        public DateTime? InPoint { get; private set; }
        public string IasDisplayAs { get; private set; }
    }
}
