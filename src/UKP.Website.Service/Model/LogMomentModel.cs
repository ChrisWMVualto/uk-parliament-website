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

        public string MemberDisplay { get { return GetMemberDisplay(); }}

        private string GetMemberDisplay()
        {
            var cons = string.IsNullOrEmpty(MemberConstituency) ? "" : string.Format("{0}, ", MemberConstituency);
            var consAndParty = string.IsNullOrEmpty(MemberConstituency) && string.IsNullOrEmpty(MemberParty)
                ? string.Empty
                : string.Format("({0}{1})", cons, MemberParty);

            return string.Format("{0} {1}", Member, consAndParty);
        }
    }
}
