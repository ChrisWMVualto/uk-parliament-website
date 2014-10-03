using System;

namespace UKP.Website.Service.Model
{
    public class EventModel
    {
        public EventModel(Guid id, string title, string house, string business, EventStates states, DateTime? actualLiveStartTime, DateTime scheduledStartTime, DateTime scheduledEndTime, DateTime? publishedStartTime, DateTime? publishedEndTime, DateTime? actualStartTime, DateTime? actualEndTime)
        {
            Id = id;
            Title = title;
            House = house;
            Business = business;
            States = states;
            ActualLiveStartTime = actualLiveStartTime;
            ScheduledStartTime = scheduledStartTime;
            ScheduledEndTime = scheduledEndTime;
            PublishedStartTime = publishedStartTime;
            PublishedEndTime = publishedEndTime;
            ActualStartTime = actualStartTime;
            ActualEndTime = actualEndTime;

            HomeFilters = new HomeFilter(States);
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string House { get; private set; }
        public string Business { get; private set; }
        public EventStates States { get; private set; }
        public DateTime? ActualLiveStartTime { get; private set; }
        public DateTime ScheduledStartTime { get; private set; }
        public DateTime ScheduledEndTime { get; private set; }
        public DateTime? PublishedStartTime { get; private set; }
        public DateTime? PublishedEndTime { get; private set; }
        public DateTime? ActualStartTime { get; private set; }
        public DateTime? ActualEndTime { get; private set; }

        public HomeFilter HomeFilters { get; private set; }

        public DateTime DisplayTime
        {
            get { return PublishedStartTime.HasValue ? PublishedStartTime.Value : ScheduledStartTime; }
        }

        public DateTime EndDisplayTime
        {
            get { return PublishedEndTime.HasValue ? PublishedEndTime.Value : ScheduledEndTime; }
        }

    }
}
