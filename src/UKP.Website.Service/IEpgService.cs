using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface IEpgService
    {
        NowAndNextModel GetNowEvents(EventFilter eventFilter = EventFilter.ALL, int target = 6);
        IEnumerable<EventModel> GetGuide(EventFilter eventFilter = EventFilter.ALL);
        IEnumerable<EventModel> GetRecentlyArchived(EventFilter eventFilter = EventFilter.ALL);
    }
}
