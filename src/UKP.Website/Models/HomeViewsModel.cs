using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Models
{
    public class HomeViewsModel
    {
        public HomeViewsModel(NowAndNextModel nowNext, IEnumerable<EventModel> epg, IEnumerable<EventModel> recentlyArchived)
        {
            NowNext = nowNext;
            Epg = epg;
            RecentlyArchived = recentlyArchived;
        }

        public NowAndNextModel NowNext { get; private set; }
        public IEnumerable<EventModel> Epg { get; private set; }
        public IEnumerable<EventModel> RecentlyArchived { get; set; }
    }
}