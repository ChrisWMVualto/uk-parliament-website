namespace UKP.Website.Service.Model
{
    public class VideoModel
    {
        public VideoModel(EventModel eventModel, string embedCode, string legacyEmbedCode)
        {
            EventModel = eventModel;
            EmbedCode = embedCode;
            LegacyEmbedCode = legacyEmbedCode;
        }

        public string EmbedCode { get; private set; }
        public string LegacyEmbedCode { get; set; }
        public EventModel EventModel { get; private set; }
    }
}