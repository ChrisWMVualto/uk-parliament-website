namespace UKP.Website.Service.Model
{
    public class MomentModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }

        public MomentModel(string id, string title, string thumbnailUrl)
        {
            Id = id;
            Title = title;
            ThumbnailUrl = thumbnailUrl;
        }
    }
}
