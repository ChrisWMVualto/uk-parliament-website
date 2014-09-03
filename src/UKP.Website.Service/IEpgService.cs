using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface IEpgService
    {
        NowAndNextModel GetNowEvents(int target = 6);
        IEnumerable<EventModel> GetGuide();
        IEnumerable<EventModel> GetRecentlyArchived();
    }
}
