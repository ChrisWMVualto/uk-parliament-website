using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKP.Website.Service.Model
{
    public class NowAndNextModel
    {
        public NowAndNextModel(IEnumerable<EventModel> events, bool moreEvents)
        {
            Events = events;
            MoreEvents = moreEvents;
        }

        public IEnumerable<EventModel> Events { get; set; }
        public bool MoreEvents { get; set; }
    }
}
