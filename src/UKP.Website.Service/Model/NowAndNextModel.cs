using System.Collections.Generic;

namespace UKP.Website.Service.Model
{
    public class NowAndNextModel
    {
        public NowAndNextModel(IEnumerable<EventModel> events, bool allLive, bool hasLive)
        {
            Events = events;
            AllLive = allLive;
            HasLive = hasLive;
        }

        public IEnumerable<EventModel> Events { get; set; }
        public bool AllLive{ get; set; }
        public bool HasLive { get; set; }
    }
}
