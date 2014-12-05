using System;

namespace UKP.Website.Service.Model
{
    public class LogMomentModel
    {
        public LogMomentModel(Guid id, string title, string thumbnailUrl, DateTime? inPoint, string member)
        {
            Id = id;
            Title = title;
            ThumbnailUrl = thumbnailUrl;
            InPoint = inPoint;
            Member = member;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string ThumbnailUrl { get; private set; }
        public DateTime? InPoint { get; private set; }
        public string Member { get; private set; }
    }
}
