using System;
using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface IEventService
    {
        NowAndNextModel GetNowEvents(EventFilter eventFilter = EventFilter.COMMONS, int target = 6);
        IEnumerable<EventModel> GetMiniGuide(EventFilter eventFilter = EventFilter.COMMONS, int target = 12);
        VideoCollectionModel GetRecentlyArchived(EventFilter eventFilter = EventFilter.COMMONS, int numEvents = 10);
        IEnumerable<EpgChannelModel> GetFullGuide(DateTime? date);
        LogMomentResultModel GetLogsBetween(Guid id, DateTime startTime, DateTime endTime);
    }
}
