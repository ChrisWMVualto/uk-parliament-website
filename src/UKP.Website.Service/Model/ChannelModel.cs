namespace UKP.Website.Service.Model
{
    public class ChannelModel
    {
        public string Name { get; set; }
        public string ExternalId { get; set; }
        public int InternalId { get; set; }

        public ChannelModel(string name, string externalId, int internalId)
        {
            Name = name;
            ExternalId = externalId;
            InternalId = internalId;
        }
    }
}
