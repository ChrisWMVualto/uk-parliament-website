using System;

namespace UKP.Website.Service.Model
{
    public class StackModel
    {
        public StackModel(Guid id, Guid eventId, string title, string member, string memberRole, string memberContext, string memberConstituency, string memberParty, string noneMember,
            string noneMemberContext, int? sortOrder)
        {
            Id = id;
            Title = title;
            Member = member;
            MemberRole = memberRole;
            MemberContext = memberContext;
            MemberConstituency = memberConstituency;
            MemberParty = memberParty;
            NoneMember = noneMember;
            NoneMemberContext = noneMemberContext;
            SortOrder = sortOrder;
            EventId = eventId;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Member { get; private set; }
        public string MemberRole { get; private set; }
        public string MemberContext { get; private set; }
        public string MemberConstituency { get; private set; }
        public string MemberParty { get; private set; }
        public string NoneMember { get; private set; }
        public string NoneMemberContext { get; private set; }
        public int? SortOrder { get; private set; }
        public Guid EventId { get; private set; }
    }
}