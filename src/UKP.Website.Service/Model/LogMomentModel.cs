using System;

namespace UKP.Website.Service.Model
{
    public class LogMomentModel
    {
        public LogMomentModel(Guid id, string title, string thumbnailUrl, DateTime? inPoint, string member, string memberParty, string memberConstituency, string memberRole)
        {
            Id = id;
            Title = title;
            ThumbnailUrl = thumbnailUrl;
            InPoint = inPoint;
            Member = member;
            MemberParty = memberParty;
            MemberConstituency = memberConstituency;
            MemberRole = memberRole;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string ThumbnailUrl { get; private set; }
        public DateTime? InPoint { get; private set; }
        public string Member { get; private set; }
        public string MemberParty { get; private set; }
        public string MemberConstituency { get; private set; }
        public string MemberRole { get; private set; }
    }
}
