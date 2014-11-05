namespace UKP.Website.Service.Model
{
    public class SearchMembersNameModel
    {
        public SearchMembersNameModel(int membersNameId, string fullTitle)
        {
            MembersNameId = membersNameId;
            FullTitle = fullTitle;
        }

        public int MembersNameId { get; private set; }
        public string FullTitle { get; private set; }
    }
}