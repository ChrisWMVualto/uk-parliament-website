using System.Collections.Generic;
using UKP.Website.Application;

namespace UKP.Website.Service.Model
{
    public static class EventString
    {
        public static RecessMessageType GetRecessMessageType(this EventFilter filter)
        {
            var eventStrings = new Dictionary<EventFilter, RecessMessageType>
            {
                { EventFilter.COMMITTEES, RecessMessageType.ALL_COMMITTEES },
                { EventFilter.COMMONS, RecessMessageType.HOUSE_OF_COMMONS },
                { EventFilter.LORDS, RecessMessageType.HOUSE_OF_LORDS }
            };

            return eventStrings[filter];
        }
    }

    public enum EventFilter
    {
        COMMITTEES = 1,
        COMMONS = 2,
        LORDS = 3,
    }
}
