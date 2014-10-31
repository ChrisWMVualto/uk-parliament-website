namespace UKP.Website.Service.Model
{
    public class TagModel
    {
        public TagModel(string category, string tag, string displayTag)
        {
            Category = category;
            Tag = tag;
            DisplayTag = displayTag;
        }

        public string Category { get; private set; }
        public string Tag { get; private set; }
        public string DisplayTag { get; private set; }
    }
}