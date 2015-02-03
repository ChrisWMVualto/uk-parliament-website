namespace UKP.Website.Service.Model
{
    public class ChannelModel
    {
        public ChannelModel(string name, string externalId)
        {
            Name = name;
            ExternalId = externalId;
        }

        public string Name { get; private set; }
        public string ExternalId { get; private set; }
    }
}
