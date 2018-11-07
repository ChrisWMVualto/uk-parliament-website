namespace UKP.Website.Service.Model
{
    public class SearchMembersNameModel
    {
        public SearchMembersNameModel(int membersNameId, string member)
        {
            MembersNameId = membersNameId;
            Member = member;
        }

        public int MembersNameId { get; private set; }
        public string Member { get; private set; }
    }
}