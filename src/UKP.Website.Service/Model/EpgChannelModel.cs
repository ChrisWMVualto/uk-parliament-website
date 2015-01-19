using System.Collections.Generic;

namespace UKP.Website.Service.Model
{
    public class EpgChannelModel
    {
        public int ChannelNum { get; private set; }
        public List<EpgEventModel> Events { get; private set; }

        public EpgChannelModel(int channelNum, List<EpgEventModel> events)
        {
            ChannelNum = channelNum;
            Events = events;
        }
    }
}
