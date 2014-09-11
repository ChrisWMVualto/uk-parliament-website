namespace UKP.Website.Service.Model
{
    public class MomentModel
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public MomentModel(string id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
