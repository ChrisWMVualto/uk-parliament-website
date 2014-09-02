namespace UKP.Website.Service.Model
{
    public class VideoModel
    {
        public VideoModel(EventModel eventModel, string embedCode)
        {
            EventModel = eventModel;
            EmbedCode = embedCode;
        }

        public string EmbedCode { get; private set; }
        public EventModel EventModel { get; private set; }
    }
}