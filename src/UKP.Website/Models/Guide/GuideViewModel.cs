using System.Collections.Generic;
using System.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Models.Guide
{
    public class GuideViewModel
    {
        public List<EpgChannelModel> Events { get; private set; }

        public GuideViewModel(List<EpgChannelModel> events)
        {
            Events = events;
        }
    }
}