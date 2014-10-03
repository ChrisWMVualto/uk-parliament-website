using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Models
{
    public class HomeViewsModel
    {
        public HomeViewsModel(NowAndNextModel nowNext, IEnumerable<EventModel> epg, RecessMessageModel recessMessage, EventFilter eventFilter)
        {
            NowNext = nowNext;
            Epg = epg;
            RecessMessage = recessMessage;
            EventFilter = eventFilter;
        }

        public NowAndNextModel NowNext { get; private set; }
        public IEnumerable<EventModel> Epg { get; private set; }
        public IEnumerable<EventModel> RecentlyArchived { get; private set; }
        public RecessMessageModel RecessMessage { get; private set; }
        public EventFilter EventFilter { get; private set; }
    }
}