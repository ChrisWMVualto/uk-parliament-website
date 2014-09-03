using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface IEpgService
    {
        IEnumerable<EventModel> GetNowEvents();
        IEnumerable<EventModel> GetGuide();
        IEnumerable<EventModel> GetRecentlyArchived();
    }
}
