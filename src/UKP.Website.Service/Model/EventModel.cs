using System;

namespace UKP.Website.Service.Model
{
    public class EventModel
    {
        public EventModel(Guid id, string title, EventStates states, DateTime? actualLiveStartTime, DateTime scheduledStartTime, DateTime? publishedStartTime, DateTime? actualStartTime)
        {
            Id = id;
            Title = title;
            States = states;
            ActualLiveStartTime = actualLiveStartTime;
            ScheduledStartTime = scheduledStartTime;
            PublishedStartTime = publishedStartTime;
            ActualStartTime = actualStartTime;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public EventStates States { get; private set; }
        public DateTime? ActualLiveStartTime { get; private set; }
        public DateTime ScheduledStartTime { get; private set; }
        public DateTime? PublishedStartTime { get; private set; }
        public DateTime? ActualStartTime { get; private set; }
    }
}
