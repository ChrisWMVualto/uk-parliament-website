using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface IEpgService
    {
        NowAndNextModel GetNowEvents(EventFilter eventFilter = EventFilter.COMMONS, int target = 6);
        IEnumerable<EventModel> GetGuide(EventFilter eventFilter = EventFilter.COMMONS, int target = 15);
        IEnumerable<EventModel> GetRecentlyArchived(EventFilter eventFilter = EventFilter.COMMONS, int numEvents = 10);
    }
}
