using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface IEpgService
    {
        NowAndNextModel GetNowEvents(string eventFilter = null, int target = 6);
        IEnumerable<EventModel> GetGuide(string eventFilter = null);
        IEnumerable<EventModel> GetRecentlyArchived(string eventFilter = null);
    }
}
