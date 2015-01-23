namespace UKP.Website.Service.Model
{
    public class ChannelModel
    {
        public string Name { get; set; }
        public string ExternalId { get; set; }

        public ChannelModel(string name, string externalId)
        {
            Name = name;
            ExternalId = externalId;
        }
    }
}
