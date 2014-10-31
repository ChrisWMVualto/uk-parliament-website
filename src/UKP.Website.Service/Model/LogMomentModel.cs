using System;

namespace UKP.Website.Service.Model
{
    public class LogMomentModel
    {
        public LogMomentModel(string id, string title, string thumbnailUrl, DateTime inPoint)
        {
            Id = id;
            Title = title;
            ThumbnailUrl = thumbnailUrl;
            InPoint = inPoint;
        }

        public string Id { get; private set; }
        public string Title { get; private set; }
        public string ThumbnailUrl { get; private set; }
        public DateTime InPoint { get; private set; }
    }
}
