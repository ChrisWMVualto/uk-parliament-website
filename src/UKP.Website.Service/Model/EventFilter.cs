using System.Collections.Generic;

namespace UKP.Website.Service.Model
{
    public static class EventString
    {
        public static string GetString(EventFilter filter)
        {
            var eventStrings = new Dictionary<EventFilter, string>
            {
                { EventFilter.COMMITTEES, "Committee" },
                { EventFilter.COMMONS, "Commons" },
                { EventFilter.LORDS, "Lords" }
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
