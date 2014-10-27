using System.Collections.Generic;
using System.Linq;
using UKP.Website.Service.Model;

namespace UKP.Website.Models.Guide
{
    public class GuideViewModel
    {
        public IEnumerable<EpgEventModel> Events { get; private set; }

        public GuideViewModel(IEnumerable<EpgEventModel> events)
        {
            Events = events;
        }
    }
}