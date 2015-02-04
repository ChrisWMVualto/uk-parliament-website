namespace UKP.Website.Service.Model
{
    public class ChannelModel
    {
        public ChannelModel(string name, string externalId, int internalId)
        {
            Name = name;
            ExternalId = externalId;
            InternalId = internalId;
        }

        public string Name { get; private set; }
        public string ExternalId { get; private set; }
        public int InternalId { get; set; }
    }
}
