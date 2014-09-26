using System;

namespace UKP.Website.Service.Model
{
    public class EventModel
    {
        public EventModel(Guid id, string title, string house, string business, EventStates states, DateTime? actualLiveStartTime, DateTime scheduledStartTime, DateTime? publishedStartTime, DateTime? actualStartTime)
        {
            Id = id;
            Title = title;
            House = house;
            Business = business;
            States = states;
            ActualLiveStartTime = actualLiveStartTime;
            ScheduledStartTime = scheduledStartTime;
            PublishedStartTime = publishedStartTime;
            ActualStartTime = actualStartTime;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string House { get; private set; }
        public string Business { get; private set; }
        public EventStates States { get; private set; }
        public DateTime? ActualLiveStartTime { get; private set; }
        public DateTime ScheduledStartTime { get; private set; }
        public DateTime? PublishedStartTime { get; private set; }
        public DateTime? ActualStartTime { get; private set; }

        // TODO: Set states as par spec
        public bool Live
        {
            get
            {
                return States.PlanningState.Equals(PlanningEventState.CONFIRMED) &&
                       States.RecordingState.Equals(RecordingEventState.RECORDING) &&
                       States.RecordedState.Equals(RecordedEventState.HOLD);
            }
        }

        // TODO: Write in some logic to return tre if this is a 'next' event, or false otherwise
        public bool Next
        {
            get
            {
                return true;
            }
        }
    }
}
